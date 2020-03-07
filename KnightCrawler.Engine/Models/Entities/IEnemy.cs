// <copyright file="IEnemy.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using KnightCrawler.Engine.Models.Behaviours;

    /// <summary>
    /// An enemy that is trying to hurt the player.
    /// </summary>
    public interface IEnemy : IEntity
    {
        /// <summary>
        /// Gets or sets the movement behaviour of the enemy.
        /// </summary>
        IEnemyMovementBehaviour MovementBehaviour { get; set; }

        /// <summary>
        /// Gets or sets the movement behaviour of the enemy.
        /// </summary>
        IEnemyBehaviour AttackBehaviour { get; set; }
    }
}
