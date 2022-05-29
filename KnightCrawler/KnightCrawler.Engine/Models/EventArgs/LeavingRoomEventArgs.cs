// <copyright file="LeavingRoomEventArgs.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.EventArgs
{
    using KnightCrawler.Data;
    using KnightCrawler.Engine.Models.World;

    /// <summary>
    /// EventArgs of the player leaving a room.
    /// </summary>
    public class LeavingRoomEventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LeavingRoomEventArgs"/> class.
        /// </summary>
        /// <param name="leftRoom">The room the player left.</param>
        /// <param name="direction">The direction the player left the room.</param>
        public LeavingRoomEventArgs(Room leftRoom, Direction direction)
        {
            this.Direction = direction;
            this.LeftRoom = leftRoom;
        }

        /// <summary>
        /// Gets the direction the player left the room.
        /// </summary>
        public Direction Direction { get; }

        /// <summary>
        /// Gets the room the player left.
        /// </summary>
        public Room LeftRoom { get; }
    }
}