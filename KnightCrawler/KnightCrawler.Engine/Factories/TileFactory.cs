// <copyright file="TileFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Factories
{
    using System;
    using KnightCrawler.Data;
    using KnightCrawler.Engine.Models.World.Tiles;

    /// <summary>
    /// The factory class that creates floor tiles.
    /// </summary>
    internal static class TileFactory
    {
        /// <summary>
        /// Returns a new instance of a floor tile corresponding to the given enum.
        /// </summary>
        /// <param name="type">The floor tile type.</param>
        /// <param name="x">The tiles x location.</param>
        /// <param name="y">The tiles y location.</param>
        /// <returns>Return a new floor tile instance.</returns>
        internal static FloorTile CreateInstance(FloorTileType type, int x, int y)
        {
            switch (type)
            {
                case FloorTileType.Obstacle:
                    return new Obstacle(x, y);

                case FloorTileType.Empty:
                    return new EmptyTile(x, y);

                case FloorTileType.Chest:
                    return new Chest(x, y);

                case FloorTileType.Spike:
                    return new Spikes(x, y);

                case FloorTileType.Exit:
                    return new Exit(x, y);
            }

            throw new ArgumentException("Unexpected floor tile type.");
        }
    }
}