// <copyright file="IEnemyMovementBehaviour.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Behaviours
{
    /// <summary>
    /// Defines the movement behaviour of an enemy.
    /// </summary>
    public interface IEnemyMovementBehaviour : IEnemyBehaviour
    {
        /// <summary>
        /// Gets or sets the movement speed of the enemy.
        /// </summary>
        double Speed { get; set; }
    }
}
