// <copyright file="FloorTile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.World.Tiles
{
    using System.Windows;
    using KnightCrawler.Engine.Models.Entities;

    /// <summary>
    /// A tile that makes up a rooms.
    /// </summary>
    public abstract class FloorTile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FloorTile"/> class.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public FloorTile(int x, int y)
        {
            this.Rectangle = new Rect(x, y, 1, 1);
        }

        /// <summary>
        /// Gets the texture of the tile.
        /// </summary>
        public abstract string Texture { get; }

        /// <summary>
        /// Gets a value indicating whether the tile can be walked through by regular enitites.
        /// </summary>
        public abstract bool BlocksEntities { get; }

        /// <summary>
        /// Gets a value indicating whether the tile blocks pathfinding alghorithms.
        /// </summary>
        public virtual bool BlocksPathFinding => this.BlocksEntities;

        /// <summary>
        /// Gets the rectangle of the tile.
        /// </summary>
        public Rect Rectangle { get; }

        /// <summary>
        /// Called, when an entity steps on the tile.
        /// </summary>
        /// <param name="entity">The entity that stepped on the tile.</param>
        /// <param name="deltaTime">The time elapsed since the last game tick.</param>
        public virtual void SteppedOn(Entity entity, double deltaTime)
        {
        }
    }
}