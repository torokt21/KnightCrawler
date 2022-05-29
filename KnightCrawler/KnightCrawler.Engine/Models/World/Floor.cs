// <copyright file="Floor.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.World
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows;
    using KnightCrawler.Data;
    using KnightCrawler.Engine.Models.Entities;

    /// <summary>
    /// The floor class.
    /// </summary>
    public class Floor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Floor"/> class.
        /// </summary>
        /// <param name="difficulty">The difficulty of the floor.</param>
        public Floor(int difficulty)
        {
            this.Difficulty = difficulty;
        }

        /// <summary>
        /// Gets or sets the current room's opacity animation progress (0.0-1.0).
        /// </summary>
        public static double CurrentRoomOpacityAnimationProgress { get; set; }

        /// <summary>
        /// Gets or sets the current room.
        /// </summary>
        public Room CurrentRoom { get; set; }

        /// <summary>
        /// Gets or sets the starting room of the floor.
        /// </summary>
        public Room StartingRoom { get; set; }

        /// <summary>
        /// Gets the difficulty of the current floor.
        /// </summary>
        public int Difficulty { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return "Floor #" + this.Difficulty;
        }

        /// <summary>
        /// Creates and returns a list containing all the rooms on the floor.
        /// </summary>
        /// <returns>Returns the list of the rooms.</returns>
        public List<Room> GetRooms()
        {
            List<Room> rooms = new List<Room>();
            this.AddBranchToList(this.StartingRoom, ref rooms);
            return rooms;
        }

        /// <summary>
        /// Moves the player to the given room.
        /// </summary>
        /// <param name="room">Room to move to.</param>
        /// <param name="startingPoint">Starting position.</param>
        public void MoveToRoom(Room room, PointF startingPoint)
        {
            // Removing player from the old room.
            this.CurrentRoom?.Entities.Remove(GameModel.GetInstance().Player);

            // Adding player to the new room.
            room.Spawn(GameModel.GetInstance().Player);
            GameModel.GetInstance().Player.MoveTo(startingPoint);

            this.CurrentRoom = room;

            CurrentRoomOpacityAnimationProgress = 0;

            // If this is not the sarting room, we play a little whoosh shound.
            if (this.CurrentRoom.Type != RoomType.Starting)
            {
                Sounds.SoundPlayer.PlaySound("room-enter.wav", Sounds.SoundPlayer.SoundType.SoundEffect);
            }
        }

        /// <summary>
        /// Gets called each gametick.
        /// </summary>
        /// <param name="deltaTime">The time elapsed since the last tick.</param>
        public void Tick(double deltaTime)
        {
            CurrentRoomOpacityAnimationProgress += Math.Min(deltaTime * 2, 1);
        }

        /// <summary>
        /// Moves the player to the given room.
        /// </summary>
        /// <param name="room">Room to move to.</param>
        /// <param name="startingDirection">The side of the room the player will spawn.</param>
        public void MoveToRoom(Room room, Direction startingDirection)
        {
            PointF startingPoint = default;

            Player player = GameModel.GetInstance().Player;
            Rect playerSize = GameModel.GetInstance().Player.Hitbox.RelativeHitbox;

            switch (startingDirection)
            {
                case Direction.Left:
                    startingPoint = new PointF((float)-playerSize.Left + 0.01f, player.Location.Y);
                    break;

                case Direction.Right:
                    startingPoint = new PointF((float)(Layout.RoomWidth - playerSize.Width), player.Location.Y);
                    break;

                case Direction.Up:
                    startingPoint = new PointF(player.Location.X, (float)-playerSize.Top + 0.01f);
                    break;

                case Direction.Down:
                    startingPoint = new PointF(player.Location.X, (float)(Layout.RoomHeight - playerSize.Height - playerSize.Top - 0.01f));
                    break;
            }

            this.MoveToRoom(room, startingPoint);
        }

        /// <summary>
        /// Adds the room, and all its surrounding rooms to the given list recursively.
        /// </summary>
        /// <param name="room">The room to add.</param>
        /// <param name="rooms">The list of rooms found so far.</param>
        private void AddBranchToList(Room room, ref List<Room> rooms)
        {
            if (room != null && !rooms.Contains(room))
            {
                rooms.Add(room);
                foreach (Room neighbour in room.Neighbours.GetAll())
                {
                    this.AddBranchToList(neighbour, ref rooms);
                }
            }
        }
    }
}