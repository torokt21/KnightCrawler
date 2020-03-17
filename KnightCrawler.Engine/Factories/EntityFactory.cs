// <copyright file="EntityFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Factories
{
    using KnightCrawler.Engine.Models.Behaviours;
    using KnightCrawler.Engine.Models.Entities;

    /// <summary>
    /// The factory class producing enemy entities.
    /// </summary>
    internal static class EntityFactory
    {
        /// <summary>
        /// Creates an instance of an entity base on the letter representing it.
        /// </summary>
        /// <param name="startingInfo">The information required to create the entity.</param>
        /// <returns>New instance.</returns>
        public static Entity GetInstanceOrNull(EntityStartingInfo startingInfo)
        {
            // TODO add attack behaviours
            switch (startingInfo.Type)
            {
                case EntityType.Goblin:
                    return new Enemy(
                        startingInfo.StartingPosition,
                        MovementType.AStarPathfinding,
                        null,
                        0.2);

                case EntityType.Skeleton:
                    break;
                case EntityType.Spectre:
                    break;
                case EntityType.Ogre:
                    break;
            }

            return null;
        }
    }
}
