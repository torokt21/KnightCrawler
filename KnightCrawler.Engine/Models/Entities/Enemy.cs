// <copyright file="Enemy.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Entities
{
    using System.Windows;
    using KnightCrawler.Engine.Factories;
    using KnightCrawler.Engine.Models.Behaviours;

    /// <summary>
    /// An enemy trying to kill the player.
    /// </summary>
    public class Enemy : Entity, IEnemy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Enemy"/> class.
        /// </summary>
        /// <param name="startingInfo">The starting info of the enemy.</param>
        /// <param name="movementBehaviourType">The enemy's movement behaviour type.</param>
        /// <param name="attackBehaviour">The enemy's attack behaviour.</param>
        /// <param name="speed">The speed of the enemy.</param>
        /// <param name="startingPos">The starting position of the enemy.</param>
        public Enemy(
            Point startingPos,
            MovementType movementBehaviourType,
            IEntityBehaviour attackBehaviour,
            double speed)
            : base(startingPos)
        {
            this.AttackBehaviour = attackBehaviour;

            // Creating an instance of the movement behaviour based on the movement type.
            this.MovementBehaviour = MovementBehaviourFactory.GetBehaviourInstance(movementBehaviourType, this, speed);
        }

        /// <inheritdoc/>
        public IEntityBehaviour AttackBehaviour { get; set; }

        /// <inheritdoc/>
        public EntityMovementBehaviour MovementBehaviour { get; set; }

        /// <summary>
        /// Handles every action of the enemy on every tick.
        /// </summary>
        /// <param name="deltaTime">The time elapsed since the last tick in miliseconds.</param>
        public virtual void OnTick(double deltaTime)
        {
            this.AttackBehaviour?.OnTick();
            this.MovementBehaviour?.OnTick();
        }
    }
}
