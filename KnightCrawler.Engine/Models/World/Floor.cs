// <copyright file="Floor.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.World
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
            this.Generate();
        }

        /// <summary>
        /// Gets the current room.
        /// </summary>
        public Room ActiveRoom { get; private set; }

        /// <summary>
        /// Gets the difficulty of the current floor.
        /// </summary>
        public int Difficulty { get; }

        private void Generate()
        {
            // TODO - generate floor respecting the floor difficulty
            Room r = new Room("rooms/0000.txt");
        }
    }
}
