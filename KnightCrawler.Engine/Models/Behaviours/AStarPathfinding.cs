// <copyright file="AStarPathFinding.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Behaviours
{
    using KnightCrawler.Engine.Models.Entities;
    using KnightCrawler.Engine.Models.World;

    /// <summary>
    /// The movement behaciour for implementing the A* pathfinding algorithm.
    /// </summary>
    internal class AStarPathFinding : EntityMovementBehaviour
    {
        /// <summary>
        /// Determines after how many ticks the enemy recalculates the route to the player.
        /// </summary>
        private const int RecalculateEveryXTick = 5;

        private int recalculateCounter = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="AStarPathFinding"/> class.
        /// </summary>
        /// <param name="speed">The speed of the enemy.</param>
        /// <param name="entity">The entity.</param>
        public AStarPathFinding(double speed, Entity entity)
            : base(speed, entity)
        {
        }

        /// <inheritdoc/>
        public override void OnTick()
        {
            if (this.recalculateCounter++ >= RecalculateEveryXTick)
            {
                this.recalculateCounter = 0;
                this.FindPath();
            }
        }

        /// <summary>
        /// Find path.
        /// </summary>
        public void FindPath()
        {
            // TODO - Implement A*
        }
    }
}
