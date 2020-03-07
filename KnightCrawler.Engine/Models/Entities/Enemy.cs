// <copyright file="Enemy.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using KnightCrawler.Engine.Models.Behaviours;

    /// <summary>
    /// An enemy trying to kill the player.
    /// </summary>
    public abstract class Enemy : Entity, IEnemy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Enemy"/> class.
        /// </summary>
        /// <param name="movementBehaviour">The enemy's movement behaviour.</param>
        /// <param name="attackBehaviour">The enemy's arrack behaviour.</param>
        public Enemy(IEnemyMovementBehaviour movementBehaviour, IEnemyBehaviour attackBehaviour)
        {
            this.AttackBehaviour = attackBehaviour;
            this.MovementBehaviour = movementBehaviour;
        }

        /// <inheritdoc/>
        public IEnemyBehaviour AttackBehaviour { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <inheritdoc/>
        public IEnemyMovementBehaviour MovementBehaviour { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Handles every action of the enemy on every tick.
        /// </summary>
        /// <param name="deltaTime">The time elapsed since the last tick in miliseconds.</param>
        public virtual void OnTick(double deltaTime)
        {
            this.AttackBehaviour.OnTick();
            this.MovementBehaviour.OnTick();
        }
    }
}
