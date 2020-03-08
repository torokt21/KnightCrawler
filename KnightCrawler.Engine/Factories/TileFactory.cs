// <copyright file="TileFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Factories
{
    using KnightCrawler.Engine.Models.World;

    /// <summary>
    /// The factory class that creates floor tiles.
    /// </summary>
    internal static class TileFactory
    {
        /// <summary>
        /// Returns a new instance of a floor tile corresponding to the given letter.
        /// </summary>
        /// <param name="c">The character representing the floor tile type.</param>
        /// <returns>New floor tile instance.</returns>
        internal static FloorTile CreateInstanceOrNull(char c)
        {
            c = c.ToString().ToLower()[0];
            switch (c)
            {
                case 'o':
                    return new EmptyTile();
                case 'w':
                    return new Obstacle();
                default:
                    return null;
            }
        }
    }
}
