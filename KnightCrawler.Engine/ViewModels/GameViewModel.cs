// <copyright file="GameViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using KnightCrawler.Engine.Models.Entities;
    using KnightCrawler.Engine.Models.World;

    /// <summary>
    /// The main view model controlling the game.
    /// </summary>
    public class GameViewModel
    {
        /// <summary>
        /// The stopwatch measuring the elapsed time between ticks.
        /// </summary>
        private Stopwatch deltaTimeStopwatch = new Stopwatch();

        /// <summary>
        /// Initializes a new instance of the <see cref="GameViewModel"/> class.
        /// </summary>
        public GameViewModel()
        {
            this.StartNewGame();
        }

        /// <summary>
        /// Gets or sets the current floor.
        /// </summary>
        public Floor CurrentFloor { get; set; }

        /// <summary>
        /// Resets the game and starts it.
        /// </summary>
        public void StartNewGame()
        {
            this.CurrentFloor = new Floor(0);
        }

        /// <summary>
        /// Calls the frame-by-frame logic of everything in the current room.
        /// </summary>
        public void GameTick()
        {
            // Getting the elapsed time since the last frame.
            double deltaTime = this.deltaTimeStopwatch.ElapsedMilliseconds;

            this.CurrentFloor.ActiveRoom.Entities
                .OfType<Enemy>()
                .ToList()
                .ForEach(e => e.OnTick(deltaTime));

            // Resetting the elapsed time since the last frame.
            this.deltaTimeStopwatch.Restart();
        }
    }
}
