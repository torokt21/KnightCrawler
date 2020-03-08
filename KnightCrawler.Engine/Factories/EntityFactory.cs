// <copyright file="EntityFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using KnightCrawler.Engine.Models.Behaviours;
    using KnightCrawler.Engine.Models.Entities;
    using KnightCrawler.Engine.Models.Entities.Enemies;

    /// <summary>
    /// The factory class producing enemy entities.
    /// </summary>
    internal static class EntityFactory
    {
        /// <summary>
        /// Creates an instance of an entity base on the letter representing it.
        /// </summary>
        /// <param name="c">The character representing an entity.</param>
        /// <returns>New instance.</returns>
        public static Entity GetInstanceOrNull(char c)
        {
            switch (c.ToString().ToLower()[0])
            {
                case 'g':
                    return new Goblin();
            }

            return null;
        }
    }
}
