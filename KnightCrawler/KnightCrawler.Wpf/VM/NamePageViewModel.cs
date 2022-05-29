// <copyright file="NamePageViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Wpf.VM
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.CommandWpf;
    using KnightCrawler.Engine.Models;

    /// <summary>
    /// NamePage View model class.
    /// </summary>
    public class NamePageViewModel : ViewModelBase
    {
        private GameViewModel vm = GameViewModel.GetInstance();

        /// <summary>
        /// Initializes a new instance of the <see cref="NamePageViewModel"/> class.
        /// </summary>
        public NamePageViewModel()
        {
            this.SaveCmd = new RelayCommand(
            () =>
             this.vm.GameLogic.SaveScore(this.Name, this.vm.GameModel.Player.Stats.Score));
        }

        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets SaveCmd.
        /// </summary>
        public ICommand SaveCmd { get; set; }
    }
}