// <copyright file="GameViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using KnightCrawler.Engine.Models.World;

    public class GameViewModel
    {
        public GameViewModel()
        {
            this.StartNewGame();
        }

        Floor CurrentFloor { get; set; }

        public void StartNewGame()
        {
            CurrentFloor = new Floor();
        }
    }
}
