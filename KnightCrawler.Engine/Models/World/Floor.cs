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
        /// Gets the current room.
        /// </summary>
        public Room ActiveRoom { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Floor"/> class.
        /// </summary>
        public Floor()
        {
            this.Generate();
        }

        private void Generate()
        {
            // TODO
            Room r = new Room("rooms/0000.txt");
        }
    }
}
