// <copyright file="GameViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models
{
    /// <summary>
    /// The game object.
    /// </summary>
    public class GameViewModel
    {
        private static GameViewModel instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameViewModel"/> class.
        /// </summary>
        private GameViewModel()
        {
            this.GameModel = GameModel.GetInstance();
            this.GameLogic = new GameLogic(GameModel.GetInstance());
        }

        /// <summary>
        /// Gets or sets the game model.
        /// </summary>
        public GameModel GameModel { get; set; }

        /// <summary>
        /// Gets or sets the game logic.
        /// </summary>
        public GameLogic GameLogic { get; set; }

        /// <summary>
        /// Gets the only instance of the game view model.
        /// </summary>
        /// <returns>The instance.</returns>
        public static GameViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new GameViewModel();
            }

            return instance;
        }
    }
}