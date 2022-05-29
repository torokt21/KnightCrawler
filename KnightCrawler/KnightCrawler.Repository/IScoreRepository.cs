// <copyright file="IScoreRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Repository
{
    using System.Collections.Generic;
    using KnightCrawler.Data;

    /// <summary>
    /// The repository responsible for creating and loading player scores.
    /// </summary>
    public interface IScoreRepository
    {
        /// <summary>
        /// Gets all player scores.
        /// </summary>
        /// <returns>Returns the list of all past scores.</returns>
        List<PlayerScore> GetAll();

        /// <summary>
        /// Iserts a new player score.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="score">The score of the player.</param>
        void InsertNew(string name, int score);
    }
}