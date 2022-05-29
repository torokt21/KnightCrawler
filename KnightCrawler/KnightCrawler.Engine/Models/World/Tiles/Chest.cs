// <copyright file="Chest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.World.Tiles
{
    using KnightCrawler.Engine.Models.Entities;
    using KnightCrawler.Engine.Sounds;

    /// <summary>
    /// The floor tile representing the chests.
    /// </summary>
    public class Chest : FloorTile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Chest"/> class.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public Chest(int x, int y)
            : base(x, y)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether the chest has been collected.
        /// </summary>
        public bool Opened { get; set; }

        /// <inheritdoc/>
        public override bool BlocksEntities => false;

        /// <inheritdoc/>
        public override bool BlocksPathFinding => !this.Opened;

        /// <inheritdoc/>
        public override string Texture => this.Opened ? "chest_empty_open_anim_f2" : "chest_empty_open_anim_f0";

        /// <inheritdoc/>
        public override void SteppedOn(Entity entity, double deltaTime)
        {
            if (entity is Player && !this.Opened)
            {
                Player player = entity as Player;
                player.Stats.OnUpgradingStats(this);
                SoundPlayer.PlaySound("level-up.mp3", SoundPlayer.SoundType.SoundEffect);

                this.Opened = true;
            }

            base.SteppedOn(entity, deltaTime);
        }
    }
}