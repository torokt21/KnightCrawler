// <copyright file="TileFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using KnightCrawler.Engine.Models.World;

    internal static class TileFactory
    {
        internal static FloorTile CreateInstanceOrNull(char c)
        {
            c = c.ToString().ToLower()[0];
            switch (c)
            {
                case 'o':
                    return new EmptyTile();
            }

            return null;
        }
    }
}
