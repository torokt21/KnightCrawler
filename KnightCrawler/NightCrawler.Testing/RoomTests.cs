// <copyright file="RoomTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NightCrawler.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using KnightCrawler.Data;
    using KnightCrawler.Engine.Factories;
    using KnightCrawler.Engine.Models.Entities;
    using KnightCrawler.Engine.Models.World;
    using NUnit.Framework;

    /// <summary>
    /// Tests the room logic.
    /// </summary>
    [TestFixture]
    public class RoomTests
    {
        private Room TestRoom
        {
            get
            {
                Layout layout = new Layout("TestRoom", 1);
                return new Room(layout, new Point(1, 1));
            }
        }

        /// <summary>
        /// Tests the entity spawning logic.
        /// </summary>
        [TestCase]
        public void TestEntitySpawning()
        {
            Room room = this.TestRoom;
            Entity entity = EntityFactory.GetInstance(new EntityStartingInfo(new Point(0, 0), EntityType.Ogre), room);
            room.Spawn(entity);

            Assert.That(room.Entities.Contains(entity));
            Assert.That(room.Entities.Count, Is.EqualTo(1));
        }

        /// <summary>
        /// Tests if the room is marked Cleared even though it has enemies inside (it shouldn't be).
        /// </summary>
        [TestCase]
        public void TestEntitySpawningAndIfRoomIsCleared()
        {
            Room room = this.TestRoom;
            Entity entity = EntityFactory.GetInstance(new EntityStartingInfo(new Point(0, 0), EntityType.Ogre), room);
            room.Spawn(entity);
            Assert.That(!room.IsCleared, "Room was cleared with enemy still in it.");
        }

        /// <summary>
        /// Tests if a new room is marked undiscovered and not visible on the minimap by default.
        /// </summary>
        [TestCase]
        public void TestRoomIsNotDiscoveredByDefaultAndNotVisibleOnMinimap()
        {
            Room room = this.TestRoom;

            Assert.That(!room.IsDiscovered, "New room was discovered");
            Assert.That(!room.IsVisibleOnMinimap, "New room was visible on minimap");
        }

        /// <summary>
        /// Tests if a room is marked discovered as soon as the player spawns in it.
        /// </summary>
        [TestCase]
        public void TestPlayerRoomDiscovery()
        {
            Room room = this.TestRoom;

            Assert.That(!room.IsDiscovered);

            Player player = new Player();
            room.Spawn(player);

            Assert.That(room.IsDiscovered, "Room was not discovered even when the player spawned.");
        }

        /// <summary>
        /// Tests that a room gives an empty list when we query it's neigbours.
        /// </summary>
        [TestCase]
        public void TestRoomHasNoNeigboursByDefault()
        {
            Room room = this.TestRoom;
            Assert.That(room.Neighbours.GetAll(), Is.Empty);
        }
    }
}
