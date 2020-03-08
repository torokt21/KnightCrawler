// <copyright file="IEnemyBehaviour.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Behaviours
{
    /// <summary>
    /// An interface requiring the base functionality for an enemy's behaviour.
    /// </summary>
    public interface IEnemyBehaviour
    {
        /// <summary>
        /// Defines what the enemy does each gametick.
        /// </summary>
        void OnTick();
    }
}
