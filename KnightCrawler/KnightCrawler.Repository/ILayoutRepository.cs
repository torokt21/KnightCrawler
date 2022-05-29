// <copyright file="ILayoutRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Repository
{
    using System.Collections.Generic;
    using KnightCrawler.Data;

    /// <summary>
    /// The repository dealing with loading the room layouts.
    /// </summary>
    public interface ILayoutRepository
    {
        /// <summary>
        /// Gets the list of all the layouts.
        /// </summary>
        /// <returns>The list of the room layouts.</returns>
        List<Layout> GetAllLayouts();

        /// <summary>
        /// Creates a new layout with the given name.
        /// </summary>
        /// <param name="name">Name of the map. ASCII non-whitespace names only.</param>
        /// <returns>Returns a new layout instance.</returns>
        Layout Create(string name);

        /// <summary>
        /// Saves the layout.
        /// </summary>
        /// <param name="layout">The layout to save.</param>
        void Save(Layout layout);

        /// <summary>
        /// Loads the layout with the specified name.
        /// </summary>
        /// <param name="name">The name (id) of the layout.</param>
        /// <returns>Returns the loaded layout.</returns>
        Layout Load(string name);
    }
}