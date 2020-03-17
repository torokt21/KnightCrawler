// <copyright file="Entity.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Entities
{
    using System.Windows;
    using KnightCrawler.Engine.Models.World;

    /// <summary>
    /// An entity that can be drawn on the map.
    /// </summary>
    public abstract class Entity : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        /// <param name="startingPos">The starting position of the entity.</param>
        public Entity(Point startingPos)
        {
            this.Location = startingPos;
        }

        /// <inheritdoc/>
        public Point Location { get; set; }

        /// <inheritdoc/>
        public Size Size { get; set; }

        /// <summary>
        /// Gets or sets the room the entity is in.
        /// </summary>
        internal Room Room { get; set; }

        /// <inheritdoc/>
        public void Spawn(Room room)
        {
            this.Room = room;
        }
    }
}
