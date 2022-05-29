// <copyright file="SpectralMovement.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Behaviours
{
    using KnightCrawler.Engine.Models.Entities;

    /// <summary>
    /// The movement which allows the entity to track the player through obstacles.
    /// </summary>
    internal class SpectralMovement : ChasingBehaviourBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpectralMovement"/> class.
        /// </summary>
        /// <param name="speed">The speed of the entity.</param>
        /// <param name="entity">The entity the movement belongs to.</param>
        public SpectralMovement(double speed, Entity entity)
            : base(speed, entity)
        {
        }

        /// <inheritdoc/>
        protected override bool CheckCollision => false;
    }
}