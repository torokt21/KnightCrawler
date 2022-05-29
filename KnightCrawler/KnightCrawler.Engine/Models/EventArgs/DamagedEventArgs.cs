// <copyright file="DamagedEventArgs.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.EventArgs
{
    using KnightCrawler.Engine.Models.Entities;

    /// <summary>
    /// Contains the additional data of damaging an entity.
    /// </summary>
    public class DamagedEventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DamagedEventArgs"/> class.
        /// </summary>
        /// <param name="entity">The entity that was killed.</param>
        /// <param name="damagedBy">What the entity was killed by.</param>
        public DamagedEventArgs(KillableEntity entity, object damagedBy)
        {
            this.DamagedEntity = entity;
            this.DamagedBy = damagedBy;
        }

        /// <summary>
        /// Gets the entity that was damaged.
        /// </summary>
        public KillableEntity DamagedEntity { get; }

        /// <summary>
        /// Gets the object the entity was damaged by.
        /// </summary>
        public object DamagedBy { get; }
    }
}