// <copyright file="Weapon.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Weapons
{
    using KnightCrawler.Data;
    using KnightCrawler.Engine.Models.Entities;

    /// <summary>
    /// A weapon wielded by the player.
    /// </summary>
    public abstract class Weapon : IWeapon
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Weapon"/> class.
        /// </summary>
        /// <param name="player">The player instance.</param>
        /// <param name="baseDamage"><see cref="BaseDamage"/>.</param>
        /// <param name="textureName"><see cref="TextureName"/>.</param>
        public Weapon(Player player, double baseDamage, string textureName)
        {
            this.BaseDamage = baseDamage;
            this.TextureName = textureName;
            this.Player = player;
        }

        /// <inheritdoc/>
        public string TextureName { get; }

        /// <inheritdoc/>
        public double BaseDamage { get; set; }

        /// <summary>
        /// Gets the player.
        /// </summary>
        protected Player Player { get; }

        /// <inheritdoc/>
        public abstract void Attack(Direction direction);

        /// <inheritdoc/>
        public abstract void OnTick(double deltaTime);
    }
}