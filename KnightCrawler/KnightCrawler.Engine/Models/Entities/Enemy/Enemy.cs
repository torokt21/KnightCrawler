// <copyright file="Enemy.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Entities
{
    using System;
    using System.Drawing;
    using System.Linq;
    using KnightCrawler.Data;
    using KnightCrawler.Engine.Factories;
    using KnightCrawler.Engine.Models.Behaviours;
    using KnightCrawler.Engine.Models.World;

    /// <summary>
    /// An enemy trying to kill the player.
    /// </summary>
    public class Enemy : KillableEntity, IEnemy
    {
        /// <summary>
        /// Keeps track of how long the enemy has yet to stay immobilized.
        /// </summary>
        private double immobilizedCounter = 0.5;

        /// <summary>
        /// Keeps track of how long the enemy won't hurt the target.
        /// </summary>
        private double harmlessCounter = 0.5;

        /// <summary>
        /// Initializes a new instance of the <see cref="Enemy"/> class.
        /// </summary>
        /// <param name="type">The type of the entity.</param>
        /// <param name="startingPos">The starting position of the enemy.</param>
        /// <param name="movementBehaviourType">The enemy's movement behaviour type.</param>
        /// <param name="attackBehaviour">The enemy's attack behaviour.</param>
        /// <param name="frames">The frames of the entity's texture.</param>
        /// <param name="room">The room the player spawns in..</param>
        /// <param name="speed">The speed of the enemy.</param>
        /// <param name="size">The size of the enemy hitbox.</param>
        /// <param name="maxHealth">The hitpoints the entity starts with.</param>
        public Enemy(
            EntityType type,
            Point startingPos,
            Room room,
            SizeF size,
            double maxHealth,
            MovementType movementBehaviourType,
            IEntityBehaviour attackBehaviour,
            string[] frames,
            double speed)
            : base(type, startingPos, room, size, maxHealth, frames)
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
        /// Gets a value indicating whether the enemy is currenty able to move.
        /// </summary>
        public bool IsImmobilized => this.immobilizedCounter > 0;

        /// <summary>
        /// Gets a value indicating whether the enemy is currenty able to move.
        /// </summary>
        public bool IsHarmless => this.harmlessCounter > 0;

        /// <summary>
        /// Handles every action of the enemy on every tick.
        /// </summary>
        /// <param name="deltaTime">The time elapsed since the last tick in miliseconds.</param>
        public override void OnTick(double deltaTime)
        {
            this.immobilizedCounter = Math.Max(0, this.immobilizedCounter - deltaTime);
            this.harmlessCounter = Math.Max(0, this.harmlessCounter - deltaTime);

            this.AttackBehaviour?.OnTick(deltaTime);

            if (!this.IsImmobilized)
            {
                this.MovementBehaviour?.OnTick(deltaTime);
            }

            // Getting nearby enemies, to prevent same enemy types to overlap completely.
            if (!this.IsImmobilized)
            {
                this.Room.EntitiesNear(this)
                    .OfType<Enemy>()
                    .Where(e => e != this)
                    .Where(e => e.EntityType == this.EntityType)
                    .Where(e => !e.IsImmobilized)
                    .Where(e => System.Windows.Point.Subtract(e.Hitbox.Rectangle.TopLeft, this.Hitbox.Rectangle.TopLeft).Length < 0.2)
                    .ToList()
                    .ForEach(e => e.immobilizedCounter = 0.5);
            }

            base.OnTick(deltaTime);
        }

        /// <inheritdoc/>
        public override void CollideWith(Entity entity)
        {
            base.CollideWith(entity);
            if (entity is Player && !this.IsHarmless)
            {
                (entity as Player).Damage(1, this);
            }
        }

        /// <inheritdoc/>
        public override void Damage(double damageValue, object dealer = null)
        {
            // Immobilizing the enemy for 1 sec
            if (dealer is Player)
            {
                this.immobilizedCounter = 1;
                this.harmlessCounter = 1;
            }

            base.Damage(damageValue, dealer);
        }
    }
}