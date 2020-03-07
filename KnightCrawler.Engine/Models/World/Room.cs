// <copyright file="Room.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.World
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using KnightCrawler.Engine.Factories;
    using KnightCrawler.Engine.Models.Entities;

    /// <summary>
    /// The game room object.
    /// </summary>
    public class Room
    {
        private const int RoomHeigth = 10;
        private const int RoomWidth = 15;

        /// <summary>
        /// Initializes a new instance of the <see cref="Room"/> class.
        /// </summary>
        public Room(string fileName)
        {

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
        /// Loads the layout of the room from a specified file.
        /// </summary>
        /// <param name="filename">The file to load the layout from.</param>
        public void LoadLayout(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
            {
                throw new ArgumentException("Invalid filename", nameof(filename));
            }

            using (StreamReader reader = new StreamReader(filename))
            {
                // Reading all lines of the file
                string[] lines = reader.ReadToEnd()
                    .Trim()
                    .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                for (int y = 0; y < lines.Length; y++)
                {
                    for (int x = 0; x < lines[y].Length; x++)
                    {
                        // Getting tile from character
                        FloorTile tile = TileFactory.CreateInstanceOrNull(lines[y][x]);

                        // If the character found is not a valid tile, it must be a different entity
                        if (tile == null)
                        {
                            tile = new EmptyTile();

                            Entity entity = EntityFactory.GetInstanceOrNull(lines[y][x]);

                            if (entity != null)
                            {
                                this.Entities.Add(entity);
                            }
                            else
                            {
                                throw new Exception("Unexpected character in map file: " + lines[y][x]);
                            }
                        }

                        this.FloorTiles[y, x] = tile;
                    }
                }
            }
        }
    }
}
