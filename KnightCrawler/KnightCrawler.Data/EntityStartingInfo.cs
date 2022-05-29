// <copyright file="EntityStartingInfo.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Data
{
    using System.Drawing;

    /// <summary>
    /// Contains the information about an entity required for it to spawn.
    /// </summary>
    public class EntityStartingInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityStartingInfo"/> class.
        /// </summary>
        /// <param name="position">The starting position of the entity.</param>
        /// <param name="type">THe entity type.</param>
        public EntityStartingInfo(Point position, EntityType type)
        {
            this.StartingPosition = position;
            this.Type = type;
        }

        /// <summary>
        /// Gets or sets the starting position of the entity.
        /// </summary>
        public Point StartingPosition { get; set; }

        /// <summary>
        /// Gets the type of the entity.
        /// </summary>
        public EntityType Type { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Type.ToString();
        }
    }
}