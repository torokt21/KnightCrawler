// <copyright file="UpgradesVM.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Wpf.VM
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.CommandWpf;
    using KnightCrawler.Engine.Models;

    /// <summary>
    /// view model for upgrades page.
    /// </summary>
    public class UpgradesVM : ViewModelBase
    {
        private GameViewModel Gvm;

        ICommand SpeedCmd { get; set; }

        ICommand HealthCmd { get; set; }

        ICommand SwingDmgCmd { get; set; }

        ICommand SwingSpeedCmd { get; set; }

        ICommand SwingRateCmd { get; set; }

        public UpgradesVM()
        {
            Gvm = GameViewModel.GetInstance();
        }
    }
}
