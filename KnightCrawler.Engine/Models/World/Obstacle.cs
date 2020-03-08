// <copyright file="Obstacle.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.World
{
    /// <summary>
    /// An obstacle found in rooms restricting movement.
    /// </summary>
    public class Obstacle : FloorTile
    {
        /// <inheritdoc/>
        public override bool BlocksEntities => true;
    }
}
