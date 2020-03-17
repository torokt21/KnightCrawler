// <copyright file="Types.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Specifies the different entity types in the game.
    /// </summary>
    public enum EntityType
    {
        /// <summary>
        /// Small, quick enemy.
        /// </summary>
        Goblin,

        /// <summary>
        /// An avarage enemy.
        /// </summary>
        Skeleton,

        /// <summary>
        /// A spectral, ghost type enemy.
        /// </summary>
        Spectre,

        /// <summary>
        /// A bigger, slower enemy.
        /// </summary>
        Ogre,
    }
}
