// <copyright file="KilledEventArgs.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.EventArgs
{
    /// <summary>
    /// Contains the additional data for the death of an entity.
    /// </summary>
    public class KilledEventArgs : System.EventArgs
    {
        /// <summary>
        /// Gets or sets the object the entity was killed by.
        /// </summary>
        public object KilledBy { get; set; }
    }
}
