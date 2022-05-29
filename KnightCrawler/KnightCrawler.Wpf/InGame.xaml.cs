// <copyright file="InGame.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Wpf
{
    using System;
    using System.Windows.Controls;
    using KnightCrawler.Engine.Models;
    using KnightCrawler.Engine.Sounds;

    /// <summary>
    /// Interaction logic for InGame.xaml.
    /// </summary>
    public partial class InGame : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InGame"/> class.
        /// </summary>
        public InGame()
        {
            this.InitializeComponent();

            GameViewModel vm = GameViewModel.GetInstance();

            vm.GameLogic.StartNewGame();

            // Cannot use datacontext, since Page does not inherit the window's datacontext.
            this.GameRendererPanel.Initialize(vm);

            vm.GameLogic.PromptStatUpgrade += this.GameLogic_PromptStatUpgrade;
            vm.GameLogic.PlayerDied += this.GameLogic_PlayerDied;
        }

        private void GameLogic_PlayerDied(object sender, Engine.Models.EventArgs.DamagedEventArgs e)
        {
            SoundPlayer.StopAll();
            Navigator.Navigate("NamePage.xaml");

            // this.NavigationService.Navigate(new Uri("MainMenu.xaml", UriKind.RelativeOrAbsolute));
        }

        private void GameLogic_PromptStatUpgrade(object sender, EventArgs e)
        {
            Navigator.Navigate("Upgrades.xaml");
        }
    }
}