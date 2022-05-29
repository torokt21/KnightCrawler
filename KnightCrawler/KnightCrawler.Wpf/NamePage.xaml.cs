// <copyright file="NamePage.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Wpf
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for NamePage.xaml.
    /// </summary>
    public partial class NamePage : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamePage"/> class.
        /// </summary>
        public NamePage()
        {
            this.InitializeComponent();
            this.tt.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Navigator.Navigate("MainMenu.xaml");
        }
    }
}