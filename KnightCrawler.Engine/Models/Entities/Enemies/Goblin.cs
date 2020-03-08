// <copyright file="Goblin.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Entities.Enemies
{
    using KnightCrawler.Engine.Models.Behaviours;

    /// <summary>
    /// A fairly fast enemy that chases the player.
    /// </summary>
    public class Goblin : Enemy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Goblin"/> class.
        /// </summary>
        public Goblin()
            : base(new AStarPathFinding(), null)
        {
        }
    }
}
