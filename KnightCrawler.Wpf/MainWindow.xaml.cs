// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Wpf
{
    using KnightCrawler.Engine.ViewModels;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        GameViewModel ViewModel { get; set; }

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
    }
}
