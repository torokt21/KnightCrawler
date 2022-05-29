// <copyright file="Obstacle.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.World.Tiles
{
    using System;

    /// <summary>
    /// An obstacle found in rooms restricting movement.
    /// </summary>
    public class Obstacle : FloorTile
    {
        private static Random random = new Random();

        private static string[] textureVariants = new string[]
        {
            "barrel",
            "crate",
            "crate_2",
            "crate_3",
            "crate_4",
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="Obstacle"/> class.
        /// </summary>
        /// <param name="x">The x coordinate of the tile.</param>
        /// <param name="y">The y coordinate of the tile.</param>
        public Obstacle(int x, int y)
            : base(x, y)
        {
            this.Texture = GetRandomTextureVariant();
        }

        /// <inheritdoc/>
        public override bool BlocksEntities => true;

        /// <inheritdoc/>
        public override string Texture { get; }

        private static string GetRandomTextureVariant()
        {
            return textureVariants[random.Next(textureVariants.Length)];
        }
    }
}