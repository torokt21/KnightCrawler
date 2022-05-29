// <copyright file="MindlessChasing.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Behaviours
{
    using KnightCrawler.Engine.Models.Entities;

    /// <summary>
    /// The movement behavoiur where the entity follows the player mindlessly.
    /// </summary>
    internal class MindlessChasing : ChasingBehaviourBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MindlessChasing"/> class.
        /// </summary>
        /// <param name="speed">The speed of the entity.</param>
        /// <param name="entity">The entity the movement belongs to.</param>
        public MindlessChasing(double speed, Entity entity)
            : base(speed, entity)
        {
        }

        /// <inheritdoc/>
        protected override bool CheckCollision => true;
    }
}