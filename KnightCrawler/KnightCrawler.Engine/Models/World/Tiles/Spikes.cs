// <copyright file="Spikes.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.World.Tiles
{
    using KnightCrawler.Engine.Models.Behaviours;
    using KnightCrawler.Engine.Models.Entities;

    /// <summary>
    /// A tile with spikes. Stepping on it will hurt killable entities.
    /// </summary>
    public class Spikes : FloorTile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Spikes"/> class.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public Spikes(int x, int y)
            : base(x, y)
        {
        }

        /// <inheritdoc/>
        public override bool BlocksEntities => false;

        /// <inheritdoc/>
        public override bool BlocksPathFinding => true;

        /// <inheritdoc/>
        public override string Texture => "floor_spikes_anim_f3";

        /// <inheritdoc/>
        public override void SteppedOn(Entity entity, double deltaTime)
        {
            if (entity is Player)
            {
                (entity as KillableEntity).Damage(1, this);
            }
            else if (entity is Enemy)
            {
                // Spectral enemies can't get hurt on spikes
                if (!((entity as Enemy).MovementBehaviour is SpectralMovement))
                {
                    (entity as KillableEntity).Damage(deltaTime, this);
                }
            }
        }
    }
}