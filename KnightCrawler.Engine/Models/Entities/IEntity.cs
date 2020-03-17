// <copyright file="IEntity.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Entities
{
    using System.Windows;
    using KnightCrawler.Engine.Models.World;

    /// <summary>
    /// An entity which has a location on the map.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets the location of the entity.
        /// </summary>
        Point Location { get; set; }

        /// <summary>
        /// Gets or sets the size of the entity.
        /// </summary>
        Size Size { get; set; }

        /// <summary>
        /// Spawns the enemy in a room.
        /// </summary>
        /// <param name="room">The room to spawn in.</param>
        void Spawn(Room room);
    }
}
