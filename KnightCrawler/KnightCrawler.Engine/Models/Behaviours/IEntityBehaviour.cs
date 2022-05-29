// <copyright file="IEntityBehaviour.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Behaviours
{
    /// <summary>
    /// An interface requiring the base functionality for an enemy's behaviour.
    /// </summary>
    public interface IEntityBehaviour
    {
        /// <summary>
        /// Defines what the enemy does each gametick.
        /// </summary>
        /// <param name="deltaTime">The time elapsed since the last gametick.</param>
        void OnTick(double deltaTime);
    }
}