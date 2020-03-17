// <copyright file="EntityStartingInfo.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// Contains the information about an entity required for it to spawn.
    /// </summary>
    public struct EntityStartingInfo
    {
        /// <summary>
        /// Gets the starting position of the entity.
        /// </summary>
        public Point StartingPosition { get; }

        /// <summary>
        /// Gets the type of the entity.
        /// </summary>
        public EntityType Type { get; }
    }
}
