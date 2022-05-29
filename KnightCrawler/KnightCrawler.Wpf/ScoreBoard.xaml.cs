// <copyright file="ScoreBoard.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Wpf
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for ScoreBoard.xaml.
    /// </summary>
    public partial class ScoreBoard : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreBoard"/> class.
        /// </summary>
        public ScoreBoard()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Navigator.Navigate("MainMenu.xaml");
        }
    }
}