// <copyright file="Layout.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Data
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Stores all information of the state of an uncleared room.
    /// </summary>
    public class Layout
    {
        /// <summary>
        /// The number of tiles in a room vertically.
        /// </summary>
        public const int RoomHeight = 11;

        /// <summary>
        /// The number of tiles in a room horizontally.
        /// </summary>
        public const int RoomWidth = 15;

        /// <summary>
        /// Initializes a new instance of the <see cref="Layout"/> class.
        /// </summary>
        /// <param name="name">Name (id) of the layout.</param>
        /// <param name="difficulty">The difficulty of the layout.</param>
        public Layout(string name, int difficulty)
        {
            this.Name = name;
            this.Difficulty = difficulty;
            this.EntityStartInfos = new List<EntityStartingInfo>();
            this.Tiles = new FloorTileType[RoomWidth, RoomHeight];
        }

        /// <summary>
        /// Gets or sets the type of the room.
        /// </summary>
        public RoomType Type { get; set; }

        /// <summary>
        /// Gets or sets the identifier name of the layout.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the difficulty of the room.
        /// </summary>
        public int Difficulty { get; set; }

        /// <summary>
        /// Gets or sets the layout of the room's tiles (obstacles/clear tiles).
        /// </summary>
        public FloorTileType[,] Tiles { get; set; }

        /// <summary>
        /// Gets or sets the list of entities and their info in the room.
        /// </summary>
        public List<EntityStartingInfo> EntityStartInfos { get; set; }

        /// <summary>
        /// Returns whether the layout allows the player to go in a room in a given direction.
        /// </summary>
        /// <param name="direction">The direction to check.</param>
        /// <returns>Returns true if the way is clear.</returns>
        public bool IsOpenToThe(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    return this.Tiles[0, Convert.ToInt32(Math.Floor((double)RoomHeight / 2))] == FloorTileType.Empty;

                case Direction.Right:
                    return this.Tiles[RoomWidth - 1, Convert.ToInt32(Math.Floor((double)RoomHeight / 2))] == FloorTileType.Empty;

                case Direction.Up:
                    return this.Tiles[Convert.ToInt32(Math.Floor((double)RoomWidth / 2)), 0] == FloorTileType.Empty;

                case Direction.Down:
                    return this.Tiles[Convert.ToInt32(Math.Floor((double)RoomWidth / 2)), RoomHeight - 1] == FloorTileType.Empty;
            }

            return false;
        }
    }
}