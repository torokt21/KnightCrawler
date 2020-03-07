// <copyright file="EmptyTile.cs" company="PlaceholderCompany">
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
    /// An empty tile that all entities can be on.
    /// </summary>
    public class EmptyTile : FloorTile
    {
        /// <inheritdoc/>
        public override bool BlocksEntities => false;
    }
}
