// <copyright file="GameModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models
{
    using KnightCrawler.Engine.Models.Entities;
    using KnightCrawler.Engine.Models.World;

    /// <summary>
    /// The main model containing the information about the current game.
    /// </summary>
    public class GameModel
    {
        /// <summary>
        /// Gets or sets the current floor.
        /// </summary>
        public Floor CurrentFloor { get; set; }

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// Gets or sets the instance of the game model.
        /// </summary>
        private static GameModel Instance { get; set; }

        /// <summary>
        /// Creates a new instance of the game model.
        /// </summary>
        /// <param name="profile">The current player's profile.</param>
        /// <returns>Returns the only GameModel instance.</returns>
        public static GameModel GetInstance()
        {
            if (Instance == null)
            {
                Instance = new GameModel();
            }

            return Instance;
        }
    }
}