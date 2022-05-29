// <copyright file="EmptyTile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.World.Tiles
{
    using System;

    /// <summary>
    /// An empty tile that all entities can be on.
    /// </summary>
    public class EmptyTile : FloorTile
    {
        private static Random random = new Random();

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyTile"/> class.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public EmptyTile(int x, int y)
            : base(x, y)
        {
            this.Texture = GetRandomTextureVariant();
        }

        /// <inheritdoc/>
        public override bool BlocksEntities => false;

        /// <inheritdoc/>
        public override string Texture { get; }

        /// <summary>
        /// Gets a variant of the floor texture.
        /// </summary>
        /// <returns>Returns the name of the texture.</returns>
        public static string GetRandomTextureVariant()
        {
            int luck = random.Next(0, 101);
            if (luck <= 94)
            {
                return "floor_1";
            }
            else if (luck <= 96)
            {
                return "floor_3";
            }
            else if (luck <= 98)
            {
                return "floor_4";
            }
            else
            {
                return "floor_2";
            }
        }
    }
}