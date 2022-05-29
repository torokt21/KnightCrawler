// <copyright file="RoomNeigbours.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.World
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using KnightCrawler.Data;

    /// <summary>
    /// The neigbours of a room.
    /// </summary>
    public class RoomNeigbours
    {
        /// <summary>
        /// Gets or sets the room left of this room.
        /// </summary>
        public Room Left { get; set; }

        /// <summary>
        /// Gets or sets the room right of this room.
        /// </summary>
        public Room Right { get; set; }

        /// <summary>
        /// Gets or sets the room below this room.
        /// </summary>
        public Room Bottom { get; set; }

        /// <summary>
        /// Gets or sets the room above this room.
        /// </summary>
        public Room Top { get; set; }

        /// <summary>
        /// Gets the room to the given direction.
        /// </summary>
        /// <param name="direction">The direction to look.</param>
        /// <returns>Room instance.</returns>
        public Room this[Direction direction]
        {
            get => this.GetRoomToThe(direction);
            set => this.SetRoomToThe(direction, value);
        }

        /// <summary>
        /// Gets the room to the given direction from this room.
        /// </summary>
        /// <param name="direction">The neighbour's direction.</param>
        /// <returns>Returns the neighbouring room.</returns>
        public Room GetRoomToThe(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    return this.Left;

                case Direction.Right:
                    return this.Right;

                case Direction.Up:
                    return this.Top;

                case Direction.Down:
                    return this.Bottom;
            }

            throw new InvalidEnumArgumentException(nameof(direction), (int)direction, typeof(Direction));
        }

        /// <summary>
        /// Sets the room to the given direction from this room.
        /// </summary>
        /// <param name="direction">The new neighbour's direction.</param>
        /// <param name="room">The new neighbour to add.</param>
        public void SetRoomToThe(Direction direction, Room room)
        {
            switch (direction)
            {
                case Direction.Left:
                    this.Left = room;
                    break;

                case Direction.Right:
                    this.Right = room;
                    break;

                case Direction.Up:
                    this.Top = room;
                    break;

                case Direction.Down:
                    this.Bottom = room;
                    break;
            }
        }

        /// <summary>
        /// Gets all neigbouring rooms.
        /// </summary>
        /// <returns>Returns the room of all neighbouring Rooms.</returns>
        public List<Room> GetAll()
        {
            // You could loop through the enum values and add them that way... but... this works...
            List<Room> list = new List<Room>();

            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                Room neighbour = this.GetRoomToThe(dir);
                if (neighbour != null)
                {
                    list.Add(neighbour);
                }
            }

            return list;
        }
    }
}