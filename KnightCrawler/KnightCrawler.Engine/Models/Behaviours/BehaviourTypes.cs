// <copyright file="BehaviourTypes.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Behaviours
{
    /// <summary>
    /// The entity movement types.
    /// </summary>
    public enum MovementType
    {
        /// <summary>
        /// The enemy walks to the target while avoiding obstacles.
        /// </summary>
        AStarPathfinding,

        /// <summary>
        /// The enemy flies through obstacles towards the target.
        /// </summary>
        Spectral,

        /// <summary>
        /// The enemy chases the player and does not pathfind.
        /// </summary>
        Chasing,

        /// <summary>
        /// The enemy does not move.
        /// </summary>
        Idle,
    }
}