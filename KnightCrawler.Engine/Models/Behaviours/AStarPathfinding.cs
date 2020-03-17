// <copyright file="AStarPathFinding.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Behaviours
{
    /// <summary>
    /// The movement behaciour for implementing the A* pathfinding algorithm.
    /// </summary>
    internal class AStarPathFinding : IEnemyMovementBehaviour
    {
        /// <summary>
        /// Determines how often the enemy recalculates the route to the player.
        /// </summary>
        private const int RecalculateEveryXTick = 5;

        private int recalculateCounter = 0;

        /// <inheritdoc/>
        public double Speed { get; set; }

        /// <inheritdoc/>
        public void OnTick()
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
