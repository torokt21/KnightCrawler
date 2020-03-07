// <copyright file="EnemyBehaviour.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Behaviours
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using KnightCrawler.Engine.Models.Entities;
    using KnightCrawler.Engine.Models.World;

    /// <summary>
    /// Defines a behaviour of an enemy.
    /// </summary>
    internal abstract class EnemyBehaviour : IEnemyBehaviour
    {
        private readonly Room room;
        private readonly Enemy enemy;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyBehaviour"/> class.
        /// </summary>
        /// <param name="room">The room the enemy is in.</param>
        /// <param name="enemy">The enemy.</param>
        public EnemyBehaviour(Room room, Enemy enemy)
        {
            this.room = room;
            this.enemy = enemy;
        }

        /// <inheritdoc/>
        public abstract void OnTick();
    }
}
