// <copyright file="Layout.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.World
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using KnightCrawler.Engine.Models.Entities;

    /// <summary>
    /// Stores all information of the state of an uncleared room.
    /// </summary>
    public class Layout
    {
        /// <summary>
        /// Gets the difficulty of the room.
        /// </summary>
        public int Difficulty { get; }

        /// <summary>
        /// Gets the layout of the room's tiles (obstacles/clear tiles).
        /// </summary>
        public FloorTile[] Tiles { get; }

        /// <summary>
        /// Gets the list of entities and their info in the room.
        /// </summary>
        public List<EntityStartingInfo> EntityStartInfos { get; }
    }
}
