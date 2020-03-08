// <copyright file="FloorTile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.World
{
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
