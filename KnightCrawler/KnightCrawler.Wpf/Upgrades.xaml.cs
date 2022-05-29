// <copyright file="Upgrades.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Wpf
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using KnightCrawler.Engine.Models;
    using static KnightCrawler.Engine.Models.Entities.PlayerStats;

    /// <summary>
    /// Interaction logic for Upgrades.xaml.
    /// </summary>
    public partial class Upgrades : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Upgrades"/> class.
        /// </summary>
        public Upgrades()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpgradablePlayerStat stat = (UpgradablePlayerStat)(sender as Control).Tag;
            GameViewModel.GetInstance().GameLogic.UpgradePlayerStat(stat);
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.GoBack();
        }
    }
}