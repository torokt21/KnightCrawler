// <copyright file="ChasingBehaviourBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Behaviours
{
    using System;
    using KnightCrawler.Engine.Models.Entities;

    /// <summary>
    /// The logic of the chasing behaviour.
    /// </summary>
    internal abstract class ChasingBehaviourBase : EntityMovementBehaviour
    {
        /// <summary>
        /// The difference in the coordinates from which the entity considers itself to be on the correct coordinate.
        /// </summary>
        private const float SameLineThreshold = 0.1f;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChasingBehaviourBase"/> class.
        /// </summary>
        /// <param name="speed">The speed ot the enemy.</param>
        /// <param name="entity">The entity.</param>
        public ChasingBehaviourBase(double speed, Entity entity)
            : base(speed, entity)
        {
            this.Target = GameModel.GetInstance().Player;
        }

        /// <summary>
        /// Gets a value indicating whether the entity colides with obstacles.
        /// </summary>
        protected abstract bool CheckCollision { get; }

        /// <summary>
        /// Gets or sets the entity the movement chases.
        /// </summary>
        private IEntity Target { get; set; }

        /// <inheritdoc/>
        public override void OnTick(double deltaTime)
        {
            double dx = this.Entity.Hitbox.Center.X - this.Target.Hitbox.Center.X;
            double dy = this.Entity.Hitbox.Center.Y - this.Target.Hitbox.Center.Y;

            int dirX = (Math.Abs(dx) < SameLineThreshold || this.Entity.Hitbox.Rectangle.IntersectsWith(this.Target.Hitbox.Rectangle)) ? 0 : (dx < 0) ? 1 : -1;
            int dirY = Math.Abs(dy) < SameLineThreshold || this.Entity.Hitbox.Rectangle.IntersectsWith(this.Target.Hitbox.Rectangle) ? 0 : (dy < 0) ? 1 : -1;

            this.Entity.MoveDirectional(dirX, dirY, (float)this.Speed, deltaTime, this.CheckCollision);
        }
    }
}