// <copyright file="FloorTile.cs" company="PlaceholderCompany">
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
    /// A tile that makes up a rooms.
    /// </summary>
    public abstract class FloorTile
    {
        /// <summary>
        /// Gets a value indicating whether the tile can be walked through by regular enitites.
        /// </summary>
        public abstract bool BlocksEntities { get; }
    }
}
