// <copyright file="KillableEntityTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NightCrawler.Testing
{
    using KnightCrawler.Engine.Models.Entities;
    using KnightCrawler.Engine.Models.EventArgs;
    using NUnit.Framework;

    /// <summary>
    /// Tests killable entities.
    /// </summary>
    [TestFixture]
    public class KillableEntityTests
    {
        private Player Player
        {
            get
            {
                return new Player();
            }
        }

        /// <summary>
        /// Tests if the killed event is raised when a killable entity dies.
        /// </summary>
        [TestCase]
        public void TestIfKilledEventIsCalledWhenEntityDies()
        {
            Player player = this.Player;

            bool wasCalled = false;
            DamagedEventArgs args = null;
            player.Killed += (o, e) =>
            {
                wasCalled = true;
                args = e;
            };

            // The damage dealer.
            object dealer = new object();

            player.Damage(double.MaxValue, dealer);
            Assert.That(player.Health, Is.Zero);
            Assert.That(args.DamagedBy, Is.EqualTo(dealer));
            Assert.IsTrue(wasCalled);
        }
    }
}
