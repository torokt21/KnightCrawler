// <copyright file="Exit.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.World.Tiles
{
    using KnightCrawler.Engine.Models.Entities;

    /// <summary>
    /// The exit tile.
    /// </summary>
    public class Exit : FloorTile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Exit"/> class.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public Exit(int x, int y)
            : base(x, y)
        {
        }

        /// <inheritdoc/>
        public override bool BlocksEntities => false;

        /// <inheritdoc/>
        public override string Texture => "floor_ladder";

        /// <inheritdoc/>
        public override void SteppedOn(Entity entity, double deltaTime)
        {
            if (entity is Player)
            {
                (entity as Player).OnFloorLeft();
            }

            base.SteppedOn(entity, deltaTime);
        }
    }
}