// <copyright file="EntityBehaviour.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Behaviours
{
    using KnightCrawler.Engine.Models.Entities;
    using KnightCrawler.Engine.Models.World;

    /// <summary>
    /// Defines a behaviour of an enemy.
    /// </summary>
    public abstract class EntityBehaviour : IEntityBehaviour
    {
        private readonly Entity entity;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityBehaviour"/> class.
        /// </summary>
        /// <param name="room">The room the enemy is in.</param>
        /// <param name="entity">The enemy.</param>
        public EntityBehaviour(Entity entity)
        {
            this.entity = entity;
        }

        /// <inheritdoc/>
        public abstract void OnTick();
    }
}
