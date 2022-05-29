// <copyright file="FloorGenerator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/* ===============================
 * Idea:
 * Floor difficulty translates to the number of rooms between the starting room and the exit room. Higher difficulty = more rooms.
 * This number is split into two numbers (positive and negative). Horizontal (h) and vertical (v) offset. These numbers mean which
 * direction the player needs to go in order get to the exit the fastest.
 * ----------
 * Example:
 * v = 5
 * h = -2
 * would mean that the exit room is 5 rooms down, and 2 rooms to the left from the starting room.
 * ----------
 * After we know where he two rooms are - compared to each other, we need to connect them. This is done by randomizing the order of
 * the directions the rooms follow eachother.
 * ----------
 * Based on the previous example we could go in this order
 * up up up up up left left
 * But by randomizing the orders of the necessary directions, we can end up with a room layout like
 * up up left up left up up
 * We still get to the same room, but in a way that would feel much more organic.
 * ----------
 * Now that we have the order of directions we need to find rooms that can have doors through the corresponding walls. The starting
 * room is free on all 4 sides, we would need a room above the first room that is not blocked in either the up or down direction.
 * Then we need a room above this room that is not blocked on its left or bottom, etc...
 * ----------
 * After the "spine" of the floor has been built up, we add random rooms/branches to the existing rooms, expanding the map.
 */

namespace KnightCrawler.Engine.Models.World
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using CommonServiceLocator;
    using KnightCrawler.Data;
    using KnightCrawler.Repository;

    /// <summary>
    /// The logic behind generating a floor.
    /// </summary>
    public class FloorGenerator
    {
        private readonly Random random = new Random();
        private readonly ILayoutRepository repository;

        private int difficulty = 0;

        private Floor floor;

        /// <summary>
        /// Initializes a new instance of the <see cref="FloorGenerator"/> class.
        /// </summary>
        public FloorGenerator()
        {
            this.repository = ServiceLocator.Current.GetInstance<ILayoutRepository>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FloorGenerator"/> class.
        /// </summary>
        /// <param name="repo">The repository that loads the possible room layouts.</param>
        public FloorGenerator(ILayoutRepository repo)
        {
            this.repository = repo;
        }

        /// <summary>
        /// Generates a new floor.
        /// </summary>
        /// <param name="difficulty">The difficulty of the floor to be generated.</param>
        /// <returns>Returns a new floor instance.</returns>
        public Floor GenerateNextFloor()
        {
            this.floor = new Floor(++this.difficulty);

            this.CreateStartExitConnection();
            this.CreateBranches();

            return this.floor;
        }

        /// <summary>
        /// Converts the given location difference into random directions.
        /// </summary>
        /// <param name="dX">The horizontal difference between the goal and the starting location. (+/-).</param>
        /// <param name="dY">The vertical difference between the goal and the starting location. (+/-).</param>
        /// <returns>Returns a list of directions that lead to the destination.</returns>
        public List<Direction> DeltaPositionToDirections(int dX, int dY)
        {
            List<Direction> directions = new List<Direction>();

            while (Math.Abs(dX) + Math.Abs(dY) > 0)
            {
                // Randomly choosing a direction (with proportional probability)
                if (this.random.Next(0, Math.Abs(dX) + Math.Abs(dY)) >= Math.Abs(dY))
                {
                    // Chose horizontal (X) movement
                    if (dX > 0)
                    {
                        dX--;
                        directions.Add(Direction.Right);
                    }
                    else
                    {
                        dX++;
                        directions.Add(Direction.Left);
                    }
                }
                else
                {
                    // Chose vertical (Y) movement
                    if (dY > 0)
                    {
                        dY--;
                        directions.Add(Direction.Down);
                    }
                    else
                    {
                        dY++;
                        directions.Add(Direction.Up);
                    }
                }
            }

            return directions;
        }

        private void CreateBranches()
        {
            // Number of branches.
            int branches = this.random.Next(3, 3 + (this.difficulty * 2));

            for (int i = 0; i < branches; i++)
            {
                int branchLength = this.random.Next(1, (int)(3 + (this.difficulty * 1.2)));

                // The room we branch out from.
                Room existingRoom;

                // The new branche's offset from the existing room.
                Point offset;
                Point totalOffset;

                // The directions the rooms are connected.
                Queue<Direction> directions;

                do
                {
                    offset = this.SplitDistanceIntoDirections(branchLength);

                    // Getting a random room the branch connects to.
                    existingRoom = this.GetRandomExistingRoom();

                    totalOffset = new Point(existingRoom.Offset.X + offset.X, existingRoom.Offset.Y + offset.Y);

                    // Creating a direction sequencre that contains which direction the rooms are connected to eachother.
                    directions = new Queue<Direction>(this.DeltaPositionToDirections(offset.X, offset.Y));
                }

                // If the selected existing room is not open to the first direction, or already has a neighbour to that direction, we try again
                while (this.floor.GetRooms().Any(r => r.Offset == totalOffset) || !existingRoom.Layout.IsOpenToThe(directions.Peek()) || existingRoom.Neighbours.GetRoomToThe(directions.Peek()) != null);

                // Getting a layout that can be at the end of the branch.
                Layout roomLayout = this.GetLayoutByFreeDirection(Utilities.ReverseDirection(directions.Last()));

                // Creating the instance of the room that will be at the end of the branch.
                Room endingRoom = new Room(roomLayout, totalOffset);
                this.ConnectToNeighbours(endingRoom);

                // Connecting the existing and the new room.
                this.ConnectRooms(existingRoom, endingRoom, directions);
            }
        }

        /// <summary>
        /// Connects the room to any rooms that are neighbours but not yet connected.
        /// </summary>
        /// <param name="newRoom">The room to connect to other rooms.</param>
        private void ConnectToNeighbours(Room newRoom)
        {
            // Loop through all neighbours.
            foreach (Room room in this.floor.GetRooms())
            {
                bool isNeighbour = true;
                Direction dir = default;
                if (room.Offset.Y == newRoom.Offset.Y)
                {
                    if (room.Offset.X == newRoom.Offset.X + 1)
                    {
                        dir = Direction.Right;
                    }
                    else if (room.Offset.X == newRoom.Offset.X - 1)
                    {
                        dir = Direction.Left;
                    }
                    else
                    {
                        isNeighbour = false;
                    }
                }
                else if (room.Offset.X == newRoom.Offset.X)
                {
                    if (room.Offset.Y == newRoom.Offset.Y + 1)
                    {
                        dir = Direction.Down;
                    }
                    else if (room.Offset.Y == newRoom.Offset.Y - 1)
                    {
                        dir = Direction.Up;
                    }
                    else
                    {
                        isNeighbour = false;
                    }
                }
                else
                {
                    isNeighbour = false;
                }

                // If they are in fact naighbours.
                if (isNeighbour)
                {
                    // Checking if the layouts are clear
                    if (newRoom.Layout.IsOpenToThe(dir) && room.Layout.IsOpenToThe(Utilities.ReverseDirection(dir)))
                    {
                        newRoom.Neighbours.SetRoomToThe(dir, room);
                        room.Neighbours.SetRoomToThe(Utilities.ReverseDirection(dir), newRoom);
                    }
                }
            }
        }

        private Room GetRandomExistingRoom()
        {
            List<Room> rooms = this.floor.GetRooms();
            return rooms[this.random.Next(rooms.Count)];
        }

        /// <summary>
        /// Creates the "spine" of the floor. Takes the starting and exit rooms and connects them using random rooms.
        /// </summary>
        private void CreateStartExitConnection()
        {
            // Placing the starting room.
            this.floor.StartingRoom = this.GetStartingRoomInstance();
            this.floor.CurrentRoom = this.floor.StartingRoom;

            // Randomizing the distance of the exit room from the starting room (respecting difficulty)
            int distance = 4 + Convert.ToInt32((this.difficulty + this.random.Next(0, 2)) * 1.2);

            // Splitting the distance (= offset) between X and Y coordinates.
            Point offsets;

            do
            {
                offsets = this.SplitDistanceIntoDirections(distance);
            }
            while (offsets.X == 0 || offsets.Y == 0); // Making sure the walk is not a straight line

            Room exitRoom = this.GetExitRoomInstance(offsets);

            // Connecting the starting and ending rooms
            this.ConnectRooms(this.floor.StartingRoom, exitRoom);
        }

        private Point SplitDistanceIntoDirections(int distance)
        {
            int xOffset = this.random.Next(-distance + 1, distance);
            int yOffset = ((this.random.Next(0, 2) * 2) - 1) * (distance - Math.Abs(xOffset));
            return new Point(xOffset, yOffset);
        }

        /// <summary>
        /// Creates a connection between two rooms by creating new rooms that connect them.
        /// </summary>
        /// <param name="room1">The starting room.</param>
        /// <param name="room2">The goal room.</param>
        private void ConnectRooms(Room room1, Room room2)
        {
            // Measuring how far away the rooms are.
            int deltaX = room1.Offset.X + room2.Offset.X;
            int deltaY = room1.Offset.Y + room2.Offset.Y;

            Queue<Direction> directions = new Queue<Direction>(this.DeltaPositionToDirections(deltaX, deltaY));
            this.ConnectRooms(room1, room2, directions);
        }

        /// <summary>
        /// Connects two rooms by adding rooms recursively by following the directions given.
        /// </summary>
        /// <param name="currentRoom">The room to connect to the target room.</param>
        /// <param name="targetRoom">The room to reach.</param>
        /// <param name="directions">The directions to follow.</param>
        private void ConnectRooms(Room currentRoom, Room targetRoom, Queue<Direction> directions)
        {
            // Getting which direction we need to leave the current room.
            Direction nextDirection = directions.Dequeue();

            // If this is the last recursion, we should be at the last room.
            if (directions.Count == 0)
            {
                currentRoom.Neighbours.SetRoomToThe(nextDirection, targetRoom);
                targetRoom.Neighbours.SetRoomToThe(Utilities.ReverseDirection(nextDirection), currentRoom);
                return;
            }

            Point offset = new Point(
                        currentRoom.Offset.X + Utilities.DirectionToXOffset(nextDirection),
                        currentRoom.Offset.Y + Utilities.DirectionToYOffset(nextDirection));

            // If there is already a room at that location, we don't want to overwrite it.
            Room roomAtThatOffset = this.floor.GetRooms().FirstOrDefault(r => r.Offset == offset);

            if (roomAtThatOffset == default)
            {
                // No room at that location, we can create a new one.
                roomAtThatOffset = new Room(
                    this.GetLayoutByFreeDirections(
                        Utilities.ReverseDirection(nextDirection),
                        directions.Peek()),
                    offset);
            }

            // Setting the current and new room to be neighbours.
            if (roomAtThatOffset.Layout.IsOpenToThe(Utilities.ReverseDirection(nextDirection)))
            {
                currentRoom.Neighbours.SetRoomToThe(nextDirection, roomAtThatOffset);
                this.ConnectToNeighbours(roomAtThatOffset);
            }

            // If we ran into an existing room, we need to check if that room has a layout that allows the branch to continue.
            if (roomAtThatOffset.Layout.IsOpenToThe(directions.Peek()))
            {
                this.ConnectRooms(roomAtThatOffset, targetRoom, directions);
            }
        }

        private Room GetStartingRoomInstance()
        {
            // Getting layout from the repository
            Layout startingLayout = this.repository
                .GetAllLayouts()
                .First(l => l.Type == RoomType.Starting);

            if (startingLayout == null)
            {
                throw new Exception("The starting room layout was not found!");
            }

            return new Room(startingLayout, new Point(0, 0));
        }

        private Room GetExitRoomInstance(Point offset)
        {
            // Getting layout from the repository
            Layout startingLayout = this.repository.GetAllLayouts()
                .First(l => l.Type == RoomType.Exit);

            if (startingLayout == null)
            {
                throw new Exception("The exit room layout was not found!");
            }

            return new Room(startingLayout, offset);
        }

        /// <summary>
        /// Returns a loayout that is free to enter/exit in both direction.
        /// </summary>
        /// <param name="freeDirection1">Free direction one.</param>
        /// <param name="freeDirection2">Free direction two.</param>
        /// <returns>A layout.</returns>
        private Layout GetLayoutByFreeDirections(Direction freeDirection1, Direction freeDirection2)
        {
            List<Layout> all = this.repository.GetAllLayouts()
                .Where(
                r => r.Difficulty <= this.difficulty &&
                r.Type == RoomType.Normal &&
                r.IsOpenToThe(freeDirection1) &&
                (freeDirection2 == default || r.IsOpenToThe(freeDirection2)))
                .ToList();

            return all[this.random.Next(all.Count)];
        }

        private Layout GetLayoutByFreeDirection(Direction freeDirection) => this.GetLayoutByFreeDirections(freeDirection, default);
    }
}