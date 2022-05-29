// <copyright file="IFloorGeneratorLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.World
{
    using System.Collections.Generic;
    using KnightCrawler.Data;

    /// <summary>
    /// The logic required to generate a floor.
    /// </summary>
    public interface IFloorGeneratorLogic
    {
        /// <summary>
        /// Gets or sets the current floor difficulty.
        /// </summary>
        int CurrentDifficulty { get; set; }

        /// <summary>
        /// Return the collection of layouts that are open in the specified directions.
        /// </summary>
        /// <param name="dir1">Direction 1.</param>
        /// <param name="dir2">Direction 2.</param>
        /// <returns>Return a collection of layouts.</returns>
        List<Layout> GetLayoutsByOpenDirection(Direction dir1, Direction dir2);

        /// <summary>
        /// Return the collection of layouts that are open in the specified directions.
        /// </summary>
        /// <param name="dir">Direction 1.</param>
        /// <returns>Return a collection of layouts.</returns>
        List<Layout> GetLayoutsByOpenDirection(Direction dir);

        /// <summary>
        /// Return the collection of layouts that are open in the specified directions.
        /// </summary>
        /// <param name="directions">Array of directions.</param>
        /// <returns>Return a collection of layouts.</returns>
        List<Layout> GetLayoutsByOpenDirection(Direction[] directions);
    }
}