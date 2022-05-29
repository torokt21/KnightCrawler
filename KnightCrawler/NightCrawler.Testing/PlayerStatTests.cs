// <copyright file="PlayerStatTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NightCrawler.Testing
{
    using KnightCrawler.Engine.Models.Entities;
    using NUnit.Framework;
    using static KnightCrawler.Engine.Models.Entities.PlayerStats;

    /// <summary>
    /// The testing of the player's stats.
    /// </summary>
    [TestFixture]
    public class PlayerStatTests
    {
        /// <summary>
        /// Tests if the player's max health changes in the right direction when upgrading.
        /// </summary>
        [TestCase]
        public void TestUpgradingMaxHealth()
        {
            Player player = new Player();
            double initialMaxHealth = player.MaxHealth;

            player.Stats.UpgradeStat(PlayerStats.UpgradablePlayerStat.MaxHealth);

            Assert.That(player.MaxHealth, Is.GreaterThan(initialMaxHealth));
        }

        /// <summary>
        /// Tests if the player's speed changes in the right direction when upgrading.
        /// </summary>
        [TestCase]
        public void TestUpgradingSpeed()
        {
            Player player = new Player();
            double initialSpeed = player.Stats.Speed;

            player.Stats.UpgradeStat(UpgradablePlayerStat.Speed);

            Assert.That(player.MaxHealth, Is.GreaterThan(initialSpeed));
        }

        /// <summary>
        /// Tests if the UpgradedStat event gets raised.
        /// </summary>
        [TestCase]
        public void TestUpgradingCallsEvent()
        {
            Player player = new Player();

            bool wasCalled = false;
            player.Stats.UpgradedStat += (o, e) => wasCalled = true;

            player.Stats.UpgradeStat(UpgradablePlayerStat.Speed);

            Assert.IsTrue(wasCalled);
        }
    }
}
