// <copyright file="InGame.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Wpf
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using KnightCrawler.Engine;
    using KnightCrawler.Engine.Models;

    /// <summary>
    /// Interaction logic for InGame.xaml.
    /// </summary>
    public partial class InGame : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InGame"/> class.
        /// </summary>
        public InGame(GameViewModel game)
        {
            this.InitializeComponent();
            NavigationService navigationHostNS = NavigationService.GetNavigationService(this);
            this.GameRendererPanel.Initialize((this.DataContext as GameViewModel).GameModel, (this.DataContext as GameViewModel).GameLogic);
        }
    }
}
