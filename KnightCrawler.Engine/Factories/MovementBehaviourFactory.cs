// <copyright file="MovementBehaviourFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using KnightCrawler.Engine.Models.Behaviours;
    using KnightCrawler.Engine.Models.Entities;

    /// <summary>
    /// A factory class producing entity behaviour instances.
    /// </summary>
    public static class MovementBehaviourFactory
    {
        /// <summary>
        /// Produces a new enemy behaviour instance.
        /// </summary>
        /// <param name="movementType">The type of the movement behaviour.</param>
        /// <param name="parentEntity">The entity that will use the movement.</param>
        /// <param name="speed">The speed of the movement.</param>
        /// <returns>Returns a new movement behaviour instance.</returns>
        public static EntityMovementBehaviour GetBehaviourInstance(MovementType movementType, Entity parentEntity, double speed)
        {
            switch (movementType)
            {
                case MovementType.AStarPathfinding:
                    return new AStarPathFinding(speed, parentEntity);
                case MovementType.Spectral:
                    throw new NotImplementedException();
                case MovementType.Idle:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentException("The given entity type does has not been implemented yet.");
            }
        }
    }
}
