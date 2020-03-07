// <copyright file="IEntity.cs" company="PlaceholderCompany">
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
    }
}
