// <copyright file="Sword.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Weapons
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Windows;
    using KnightCrawler.Data;
    using KnightCrawler.Engine.Models.Entities;

    /// <summary>
    /// The most common melee weapon.
    /// </summary>
    public class Sword : Weapon
    {
        private const float SwingRectangleYOffset = 0.3f;

        private double swingDelayer = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sword"/> class.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="baseDamage">The weapon's base damage.</param>
        /// <param name="textureName">The name of the texture.</param>
        public Sword(Player player, double baseDamage, string textureName)
            : base(player, baseDamage, textureName)
        {
            player.Stats.UpgradedStat += this.Stats_StatUpgraded;
            this.UpdateSwingRadius();
        }

        /// <summary>
        /// Gets the progress of the swing animation in percentages (number between 0.0-1.0).
        /// </summary>
        public double AnimationProgress => this.swingDelayer == 0 ? 0 : (this.SwingDelay - this.swingDelayer) / this.SwingDelay;

        /// <summary>
        /// Gets the delay in second before the player can swing the sword again.
        /// </summary>
        public double SwingDelay => this.Player.Stats.SwingDelay;

        /// <summary>
        /// Gets the rectangle of the last swing.
        /// </summary>
        public Rect LastSwingRectangle { get; private set; }

        /// <summary>
        /// Gets which direction the sword should be displayed from the player.
        /// </summary>
        public Direction DisplayDirection => (this.AnimationProgress == 0) ? this.Player.LastHorizontalMovement : this.LastSwingDirection;

        /// <summary>
        /// Gets the full damage of one swing.
        /// </summary>
        public double FullDamage { get => this.Player.Stats.SwingDamage * this.BaseDamage; }

        /// <summary>
        /// Gets or sets the size of one swing's hitbox.
        /// </summary>
        private SizeF SwingRadius { get; set; }

        /// <summary>
        /// Gets or sets the last swing direction.
        /// </summary>
        private Direction LastSwingDirection { get; set; }

        private Random Random { get; } = new Random();

        /// <inheritdoc/>
        public override void OnTick(double deltaTime)
        {
            this.swingDelayer = Math.Max(this.swingDelayer - deltaTime, 0);
        }

        /// <inheritdoc/>
        public override void Attack(Direction direction)
        {
            // Stopping the player from swinging too fast
            if (this.swingDelayer > 0)
            {
                return;
            }

            // Saving the direction of the hit.
            this.LastSwingDirection = direction;

            // The top-left point of the swing rectangle hitbox.
            PointF swingLocation = default;

            // We need to rotate the swing radius rectangle if the player is attacking up/down.
            SizeF dimensions = default;

            switch (direction)
            {
                case Direction.Left:
                    swingLocation = new PointF(
                        this.Player.Location.X - this.SwingRadius.Width,
                        this.Player.Location.Y + SwingRectangleYOffset + ((this.Player.Size.Height - this.SwingRadius.Height) / 2));
                    dimensions = this.SwingRadius;
                    break;

                case Direction.Right:
                    swingLocation = new PointF(
                        this.Player.Location.X + this.Player.Size.Width,
                        this.Player.Location.Y + SwingRectangleYOffset + ((this.Player.Size.Height - this.SwingRadius.Height) / 2));
                    dimensions = this.SwingRadius;
                    break;

                case Direction.Up:
                    // Rotating the rectangle
                    dimensions = new SizeF(this.SwingRadius.Height, this.SwingRadius.Width);
                    swingLocation = new PointF(
                        (float)(this.Player.Hitbox.Rectangle.Left + ((this.Player.Hitbox.Rectangle.Width - dimensions.Width) / 2)),
                        (float)(this.Player.Hitbox.Rectangle.Top - dimensions.Height));
                    break;

                case Direction.Down:
                    // Rotating the rectangle
                    dimensions = new SizeF(this.SwingRadius.Height, this.SwingRadius.Width);
                    swingLocation = new PointF(
                        (float)(this.Player.Hitbox.Rectangle.Left + ((this.Player.Hitbox.Rectangle.Width - dimensions.Width) / 2)),
                        (float)this.Player.Hitbox.Rectangle.Bottom);
                    break;
            }

            this.LastSwingRectangle = new Rect(swingLocation.X, swingLocation.Y, dimensions.Width, dimensions.Height);

            var enemiesHit = this.Player.Room.EntitiesNear(this.LastSwingRectangle)
                .Where(e => !(e is Player) && e.Hitbox.Rectangle.IntersectsWith(this.LastSwingRectangle))
                .OfType<KillableEntity>()
                .ToList();

            enemiesHit.ForEach(e => e.Damage(this.FullDamage, this.Player));

            this.swingDelayer = this.SwingDelay;

            // Playing swish sound
            this.PlaySwishSound();

            if (enemiesHit.Count > 0)
            {
                this.PlayHitSound();
            }
        }

        private void PlaySwishSound()
        {
            int luck = this.Random.Next(1, 4);

            Sounds.SoundPlayer.PlaySound($"swish-{luck}.wav", Sounds.SoundPlayer.SoundType.SoundEffect);
        }

        private void PlayHitSound()
        {
            Sounds.SoundPlayer.PlaySound("enemy-hit.mp3", Sounds.SoundPlayer.SoundType.SoundEffect);
        }

        /// <summary>
        /// Called, when the player upgrades their stats.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private void Stats_StatUpgraded(object sender, Models.EventArgs.StatUpgadeEventArgs e)
        {
            // If the player upgraded their swing range
            if (e.Stat == PlayerStats.UpgradablePlayerStat.SwingRange)
            {
                this.UpdateSwingRadius();
            }
        }

        /// <summary>
        /// Updates the swing radisu if the player upgrades it.
        /// </summary>
        private void UpdateSwingRadius()
        {
            this.SwingRadius = new SizeF(this.Player.Stats.SwingRange, this.Player.Size.Height);
        }
    }
}