// <copyright file="PlayerStats.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Entities
{
    using System;
    using KnightCrawler.Engine.Models.EventArgs;

    /// <summary>
    /// The stats of the player.
    /// </summary>
    public class PlayerStats
    {
        /// <summary>
        /// The max health the player spawns with.
        /// </summary>
        public const int InitialMaxHealth = 6;

        private Player player;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerStats"/> class.
        /// </summary>
        /// <param name="player">The player.</param>
        public PlayerStats(Player player)
        {
            this.player = player;
        }

        /// <summary>
        /// called, when the player has upgraded a stat.
        /// </summary>
        public event EventHandler<StatUpgadeEventArgs> UpgradedStat;

        /// <summary>
        /// called, when the player levels up, and can ugpgrade a stat.
        /// </summary>
        public event EventHandler<EventArgs> UpgradingStat;

        /// <summary>
        /// called, when score changes.
        /// </summary>
        public event EventHandler<EventArgs> AddedScore;

        /// <summary>
        /// The types of stats that can be upgraded when the player levels up.
        /// </summary>
        public enum UpgradablePlayerStat
        {
            /// <summary>
            /// <see cref="PlayerStats.MaxHealth"/>
            /// </summary>
            MaxHealth,

            /// <summary>
            /// <see cref="PlayerStats.Speed"/>
            /// </summary>
            Speed,

            /// <summary>
            /// <see cref="PlayerStats.SwingDelay"/>
            /// </summary>
            SwingRate,

            /// <summary>
            /// <see cref="PlayerStats.SwingDamage"/>
            /// </summary>
            SwingDamage,

            /// <summary>
            /// <see cref="PlayerStats.SwingRange"/>
            /// </summary>
            SwingRange,
        }

        /// <summary>
        /// Gets the maximum speed of the player.
        /// </summary>
        public float Speed { get; private set; } = 3;

        /// <summary>
        /// Gets the maximum speed of the player.
        /// </summary>
        public int MaxHealth { get; private set; } = InitialMaxHealth;

        /// <summary>
        /// Gets the rate the player can swing their melee weapon.
        /// </summary>
        public float SwingDelay { get; private set; } = 1.5f;

        /// <summary>
        /// Gets the damage the player deals when swinging a melee damage.
        /// </summary>
        public float SwingDamage { get; private set; } = 1;

        /// <summary>
        /// Gets the range of the meleeweapons held by the player.
        /// </summary>
        public float SwingRange { get; private set; } = 1;

        /// <summary>
        /// Gets the level of the player.
        /// </summary>
        public int Score { get; private set; } = 0;

        /// <summary>
        /// Upgrades the given stat.
        /// </summary>
        /// <param name="stat">The stat to upgrade.</param>
        public void UpgradeStat(UpgradablePlayerStat stat)
        {
            this.Score += 200;
            switch (stat)
            {
                case UpgradablePlayerStat.MaxHealth:
                    this.MaxHealth += 2;
                    this.player.Heal(2);
                    break;

                case UpgradablePlayerStat.Speed:
                    this.Speed += 0.3f;
                    break;

                case UpgradablePlayerStat.SwingRate:
                    this.SwingDelay *= 0.8f;
                    break;

                case UpgradablePlayerStat.SwingDamage:
                    this.SwingDamage += 0.3f;
                    break;

                case UpgradablePlayerStat.SwingRange:
                    this.SwingRange += 0.2f;
                    break;
            }

            Sounds.SoundPlayer.PlaySound("upgrade-select.mp3", Sounds.SoundPlayer.SoundType.SoundEffect);

            this.UpgradedStat?.Invoke(this, new StatUpgadeEventArgs(stat));
        }

        /// <summary>
        /// Upgrades the given stat.
        /// </summary>
        /// <param name="amount">The score amount given.</param>
        public void AddScore(int amount)
        {
            this.Score += amount;
            this.AddedScore?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Called when the player is about to upgrade their stats.
        /// </summary>
        /// <param name="sender">The reason the upgreade is happening.</param>
        public void OnUpgradingStats(object sender)
        {
            this.UpgradingStat?.Invoke(sender, new EventArgs());
        }
    }
}