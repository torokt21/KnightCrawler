// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Wpf
{
    using System.Windows;
    using KnightCrawler.Engine.ViewModels;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();

            this.ViewModel = new GameViewModel();
            this.DataContext = this.ViewModel;

            this.jatekter.Initialize();
            this.jatekter.InvalidateVisual();
        }

        /// <summary>
        /// Gets or sets the view model of the game window.
        /// </summary>
        private GameViewModel ViewModel { get; set; }
    }
}
