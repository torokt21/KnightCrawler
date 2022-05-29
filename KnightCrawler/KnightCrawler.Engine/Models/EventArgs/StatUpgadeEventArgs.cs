// <copyright file="StatUpgadeEventArgs.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.EventArgs
{
    using KnightCrawler.Engine.Models.Entities;

    /// <summary>
    /// The event arguments of the event where the player has leveled up, and upgraded a stat.
    /// </summary>
    public class StatUpgadeEventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatUpgadeEventArgs"/> class.
        /// </summary>
        /// <param name="stat">The upgraded stats.</param>
        public StatUpgadeEventArgs(PlayerStats.UpgradablePlayerStat stat)
        {
            this.Stat = stat;
        }

        /// <summary>
        /// Gets or sets the stat that has been upgraded.
        /// </summary>
        public Entities.PlayerStats.UpgradablePlayerStat Stat { get; set; }
    }
}