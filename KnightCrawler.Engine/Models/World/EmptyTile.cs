// <copyright file="EmptyTile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.World
{
    /// <summary>
    /// An empty tile that all entities can be on.
    /// </summary>
    public class EmptyTile : FloorTile
    {
        /// <inheritdoc/>
        public override bool BlocksEntities => false;
    }
}
