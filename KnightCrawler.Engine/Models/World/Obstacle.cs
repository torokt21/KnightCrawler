// <copyright file="Obstacle.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.World.Obstacles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using GalaSoft.MvvmLight;

    /// <summary>
    /// An obstacle found in rooms restricting movement.
    /// </summary>
    public class Obstacle : FloorTile
    {
        /// <inheritdoc/>
        public override bool BlocksEntities => true;
    }
}
