// <copyright file="ScoreBoardVM.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Wpf.VM
{
    using System.Collections.Generic;
    using GalaSoft.MvvmLight;
    using KnightCrawler.Data;
    using KnightCrawler.Engine.Models;

    /// <summary>
    /// view model for upgrades page.
    /// </summary>
    public class ScoreBoardVM : ViewModelBase
    {/// <summary>
     /// Initializes a new instance of the <see cref="ScoreBoardVM"/> class.
     /// </summary>
        public ScoreBoardVM()
        {
            this.Score = GameViewModel.GetInstance().GameLogic.LoadScore();
        }

        /// <summary>
        /// Gets or sets collection of the player scores.
        /// </summary>
        public List<PlayerScore> Score { get; set; }
    }
}