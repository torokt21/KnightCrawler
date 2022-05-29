// <copyright file="IWeapon.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Weapons
{
    using KnightCrawler.Data;

    /// <summary>
    /// The interface describint the functionality of a weapon.
    /// </summary>
    public interface IWeapon
    {
        /// <summary>
        /// Gets the name of the weapon's texture.
        /// </summary>
        string TextureName { get; }

        /// <summary>
        /// Gets or sets the base damage of the weapon.
        /// </summary>
        double BaseDamage { get; set; }

        /// <summary>
        /// Called every gametick.
        /// </summary>
        /// <param name="deltaTime">The time elapsed since the last tick.</param>
        void OnTick(double deltaTime);

        /// <summary>
        /// Attacks in a given direction.
        /// </summary>
        /// <param name="direction">The direction to attack.</param>
        void Attack(Direction direction);
    }
}