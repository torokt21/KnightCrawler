// <copyright file="IEnemy.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Entities
{
    using KnightCrawler.Engine.Models.Behaviours;

    /// <summary>
    /// An enemy that is trying to hurt the player.
    /// </summary>
    public interface IEnemy : IEntity
    {
        /// <summary>
        /// Gets or sets the movement behaviour of the enemy.
        /// </summary>
        EntityMovementBehaviour MovementBehaviour { get; set; }

        /// <summary>
        /// Gets or sets the movement behaviour of the enemy.
        /// </summary>
        IEntityBehaviour AttackBehaviour { get; set; }
    }
}
