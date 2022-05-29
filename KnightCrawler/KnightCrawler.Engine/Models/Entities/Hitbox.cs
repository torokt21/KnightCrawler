// <copyright file="Hitbox.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Entities
{
    using System.Drawing;
    using System.Windows;

    /// <summary>
    /// The hitbox of an entity.
    /// </summary>
    public class Hitbox
    {
        private const double HitboxRatio = 0.4;

        private Entity entity;

        /// <summary>
        /// Initializes a new instance of the <see cref="Hitbox"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public Hitbox(Entity entity)
        {
            this.entity = entity;

            // Measuring hitbox based on the ratio constant
            this.RelativeHitbox = new Rect(0, entity.Size.Height - (entity.Size.Height * HitboxRatio), entity.Size.Width, entity.Size.Height * HitboxRatio);
        }

        /// <summary>
        /// Gets or sets the rectangle of the hitbox.
        /// </summary>
        public Rect Rectangle { get; set; }

        /// <summary>
        /// Gets or sets the center point of the hitbox.
        /// </summary>
        public PointF Center { get; set; }

        /// <summary>
        /// Gets or sets the hitbox of the entity, measured from the entity's location.
        /// </summary>
        public Rect RelativeHitbox { get; set; }

        /// <summary>
        /// Updated the hitbox to follow the position.
        /// </summary>
        public void UpdateHitbox()
        {
            this.Rectangle = new Rect(
                this.entity.Location.X + this.RelativeHitbox.Left,
                this.entity.Location.Y + this.RelativeHitbox.Top,
                this.RelativeHitbox.Width,
                this.RelativeHitbox.Height);

            this.Center = new PointF((float)(this.Rectangle.Left + (this.Rectangle.Width / 2)), (float)(this.Rectangle.Top + (this.Rectangle.Height / 2)));
        }
    }
}