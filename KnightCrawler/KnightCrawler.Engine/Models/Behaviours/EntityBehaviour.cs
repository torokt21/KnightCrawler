// <copyright file="EntityBehaviour.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Behaviours
{
    using KnightCrawler.Engine.Models.Entities;

    /// <summary>
    /// Defines a behaviour of an entity.
    /// </summary>
    public abstract class EntityBehaviour : IEntityBehaviour
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityBehaviour"/> class.
        /// </summary>
        /// <param name="room">The room the enemy is in.</param>
        /// <param name="entity">The enemy.</param>
        public EntityBehaviour(Entity entity)
        {
            this.Entity = entity;
        }

        /// <summary>
        /// Gets the entity the movemente belongs to.
        /// </summary>
        protected Entity Entity { get; }

        /// <inheritdoc/>
        public abstract void OnTick(double deltaTime);
    }
}