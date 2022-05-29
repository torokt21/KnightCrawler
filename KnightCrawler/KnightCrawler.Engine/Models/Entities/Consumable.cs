// <copyright file="Consumable.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using KnightCrawler.Data;
    using KnightCrawler.Engine.Models.World;

    /// <summary>
    /// A consumable that heals the player.
    /// </summary>
    public abstract class Consumable : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Consumable"/> class.
        /// </summary>
        /// <param name="type">The entity type.</param>
        /// <param name="startingPos">The consumable's starting position.</param>
        /// <param name="room">The room the consumable is in.</param>
        /// <param name="size">The size of the texture.</param>
        /// <param name="frames">The frames of the consumable.</param>
        public Consumable(EntityType type, PointF startingPos, Room room, SizeF size, string[] frames)
            : base(type, startingPos, room, size, frames)
        {
        }

        /// <summary>
        /// called, when the player picks up.
        /// </summary>
        public event EventHandler OnPickedUp;

        /// <summary>
        /// Gets a value indicating whether the consumable can be picked up.
        /// </summary>
        protected abstract bool CanBePickedUp { get; }

        /// <inheritdoc/>
        public override void CollideWith(Entity entity)
        {
            base.CollideWith(entity);

            if (entity is Player && this.CanBePickedUp)
            {
                this.PickedUp(entity as Player);
                this.OnPickedUp?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// Called when the player picks up the consumable.
        /// </summary>
        /// <param name="player">The player.</param>
        protected abstract void PickedUp(Player player);
    }
}
