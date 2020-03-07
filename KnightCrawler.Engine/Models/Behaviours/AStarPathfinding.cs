// <copyright file="AStarPathFinding.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Behaviours
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// The movement behaciour for implementing the A* pathfinding algorithm.
    /// </summary>
    internal class AStarPathFinding : IEnemyMovementBehaviour
    {
        /// <summary>
        /// Determines how often the enemy recalculates the route to the player.
        /// </summary>
        private const int RecalculateEveryXTick = 5;

        private int recalculateSlower = 0;

        /// <inheritdoc/>
        public double Speed { get; set; }

        /// <inheritdoc/>
        public void OnTick()
        {
            if (this.recalculateSlower++ >= RecalculateEveryXTick)
            {
                this.FindPath();
            }
        }

        public void FindPath()
        {
            // TODO - Implement A*
        }
    }
}
