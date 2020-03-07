﻿// <copyright file="Entity.cs" company="PlaceholderCompany">
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
    /// An entity that can be drawn on the map.
    /// </summary>
    public abstract class Entity : IEntity
    {
        /// <inheritdoc/>
        public Point Location { get; set; }

        /// <inheritdoc/>
        public Size Size { get; set; }
    }
}