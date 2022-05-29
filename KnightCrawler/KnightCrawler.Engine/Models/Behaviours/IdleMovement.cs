// <copyright file="IdleMovement.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Behaviours
{
    using KnightCrawler.Engine.Models.Entities;

    /// <summary>
    /// The movement behaviour of no movement. Yes. It's empty.
    /// </summary>
    public class IdleMovement : EntityMovementBehaviour
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdleMovement"/> class.
        /// </summary>
        /// <param name="entity">The netity the movement belongs to.</param>
        public IdleMovement(Entity entity)
            : base(0, entity)
        {
        }

        /// <inheritdoc/>
        public override void OnTick(double deltaTime)
        {
        }
    }
}