﻿// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Wpf
{
    using System;
    using System.Threading;
    using System.Timers;
    using System.Windows;
    using System.Windows.Threading;
    using KnightCrawler.Engine.ViewModels;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();

            this.ViewModel = new GameViewModel();
            this.DataContext = this.ViewModel;

            this.jatekter.Initialize();

            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromMilliseconds(20);
            this.timer.Tick += this.Timer_Tick;
            this.timer.Start();
        }

        /// <summary>
        /// Gets or sets the view model of the game window.
        /// </summary>
        private GameViewModel ViewModel { get; set; }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.jatekter.InvalidateVisual();
        }
    }
}
