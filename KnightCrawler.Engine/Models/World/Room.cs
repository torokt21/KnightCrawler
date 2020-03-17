// <copyright file="Room.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.World
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using KnightCrawler.Engine.Factories;
    using KnightCrawler.Engine.Models.Entities;

    /// <summary>
    /// The game room object.
    /// </summary>
    public class Room
    {
        /// <summary>
        /// The number of tiles in a room vertically.
        /// </summary>
        public const int RoomHeigth = 10;

        /// <summary>
        /// The number of tiles in a room horizontally.
        /// </summary>
        public const int RoomWidth = 15;

        /// <summary>
        /// Initializes a new instance of the <see cref="Room"/> class.
        /// </summary>
        /// <param name="fileName">The name of the file to load.</param>
        public Room(string fileName)
        {
            // TODO - all layouts should be loaded when the application starts. Then each room instance would get a layout based on which directions are clear
            //this.LoadLayout(fileName);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the room has been cleared.
        /// </summary>
        public bool IsCleared { get; set; } = false;

        /// <summary>
        /// Gets the list of entities in the room.
        /// </summary>
        public List<Entity> Entities { get; } = new List<Entity>();

        /// <summary>
        /// Gets or sets the 2D array containing the layout of the floor obstacles.
        /// </summary>
        public FloorTile[,] FloorTiles { get; set; }

        /// <summary>
        /// Gets or sets the tile in the specified index of the room.
        /// </summary>
        /// <param name="y">The y location.</param>
        /// <param name="x">The x location.</param>
        /// <returns>The floor tile.</returns>
        public FloorTile this[int y, int x]
        {
            get => this.FloorTiles[y, x];
            set => this.FloorTiles[y, x] = value;
        }

        public void Spawn(Entity entity)
        {
            Entities.Add(entity);
            entity.Spawn(this);
        }
    }
}
