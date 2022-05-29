// <copyright file="EntityTypes.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Data
{
    /// <summary>
    /// Specifies the different entity types in the game.
    /// </summary>
    public enum EntityType
    {
        /// <summary>
        /// Small, quick enemy.
        /// </summary>
        Goblin = 0,

        /// <summary>
        /// An avarage enemy.
        /// </summary>
        Skeleton = 1,

        /// <summary>
        /// A spectral, ghost type enemy.
        /// </summary>
        Spectre = 2,

        /// <summary>
        /// A bigger, slower enemy.
        /// </summary>
        Ogre = 3,

        /// <summary>
        /// A bit stronger orc than a warrior.
        /// </summary>
        MaskedOrc = 4,

        /// <summary>
        /// A stronger enemy.
        /// </summary>
        OrcWarrior = 5,

        /// <summary>
        /// A snail like creature.
        /// </summary>
        Swampy = 6,

        /// <summary>
        /// A zombie.
        /// </summary>
        Zombie = 7,

        /// <summary>
        /// A small, weak but quick zombie.
        /// </summary>
        TinyZombie = 8,

        /// <summary>
        /// The healing consumable.
        /// </summary>
        HpBottle = 99,

        /// <summary>
        /// The player.
        /// </summary>
        Player = 100,
    }
}