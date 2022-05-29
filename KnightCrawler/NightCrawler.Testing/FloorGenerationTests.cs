// <copyright file="FloorGenerationTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NightCrawler.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using KnightCrawler.Data;
    using KnightCrawler.Engine.Models.World;
    using KnightCrawler.Repository;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Testing of the floor generation.
    /// </summary>
    [TestFixture]
    internal class FloorGenerationTests
    {
        private Layout EmptyRoomLayout
        {
            get
            {
                Layout layout = new Layout("empty", 1);

                for (int i = 0; i < layout.Tiles.GetLength(0); i++)
                {
                    for (int j = 0; j < layout.Tiles.GetLength(1); j++)
                    {
                        layout.Tiles[i, j] = FloorTileType.Empty;
                    }
                }

                return layout;
            }
        }

        /// <summary>
        /// Tests the function that splits delta positions into directions.
        /// </summary>
        /// <param name="dx">The difference in the Y coordinates.</param>
        /// <param name="dy">The difference in the X coordinates.</param>
        /// <param name="expected1">The first direction we expect.</param>
        /// <param name="expected1count">The number of occurrances of the first direction.</param>
        /// <param name="expected2">The second direction we expect.</param>
        /// <param name="expected2count">The number of occurrances of the second direction.</param>
        [TestCase(4, -2, Direction.Right, 4, Direction.Up, 2)]
        [TestCase(0, 20, Direction.Left, 0, Direction.Down, 20)]
        [TestCase(0, 0, Direction.Left, 0, Direction.Down, 0)]
        public void TestDeltaPositionToRandomDirectionsFunction(int dx, int dy, Direction expected1, int expected1count, Direction expected2, int expected2count)
        {
            FloorGenerator gen = new FloorGenerator(null);

            List<Direction> result = gen.DeltaPositionToDirections(dx, dy);

            Assert.That(result.Count(d => d == expected1), Is.EqualTo(expected1count));
            Assert.That(result.Count(d => d == expected2), Is.EqualTo(expected2count));
        }

        /// <summary>
        /// Tests if both the start and exitrooms are present on any generated floors.
        /// </summary>
        [TestCase]
        public void TestStartAndExitRoomBeingGenerated()
        {
            Mock<ILayoutRepository> mockedRepo = new Mock<ILayoutRepository>();

            List<Layout> layouts = new List<Layout>()
            {
                new Layout("Exit room", 0) { Type = RoomType.Exit },
                new Layout("Starting room", 0) { Type = RoomType.Starting },
                this.EmptyRoomLayout,
            };

            mockedRepo.Setup(r => r.GetAllLayouts()).Returns(layouts);

            FloorGenerator generator = new FloorGenerator(mockedRepo.Object);
            for (int i = 0; i < 10; i++)
            {
                Floor floor = generator.GenerateNextFloor();

                List<Room> rooms = floor.GetRooms();

                Assert.That(floor.GetRooms().Count(r => r.Type == RoomType.Exit), Is.EqualTo(1));
                Assert.That(floor.GetRooms().Count(r => r.Type == RoomType.Starting), Is.EqualTo(1));
            }
        }

        /// <summary>
        /// Tests if rooms that have neighbours are not blocked by an obstacle.
        /// </summary>
        [TestCase]
        public void TestConnectedRoomsAreNotBlockedByTheirLayouts()
        {
            Mock<ILayoutRepository> mockedRepo = new Mock<ILayoutRepository>();

            // Creating blocked rooms
            Layout leftBlockedLayout = this.EmptyRoomLayout;
            Layout rightBlockedLayout = this.EmptyRoomLayout;

            for (int i = 0; i < leftBlockedLayout.Tiles.GetLength(1); i++)
            {
                leftBlockedLayout.Tiles[0, i] = FloorTileType.Obstacle;
                rightBlockedLayout.Tiles[Layout.RoomWidth - 1, i] = FloorTileType.Obstacle;
            }

            Layout topBlockedLayout = this.EmptyRoomLayout;
            Layout bottomBlockedLayout = this.EmptyRoomLayout;

            for (int i = 0; i < leftBlockedLayout.Tiles.GetLength(0); i++)
            {
                topBlockedLayout.Tiles[i, 0] = FloorTileType.Obstacle;
                bottomBlockedLayout.Tiles[i, Layout.RoomHeight - 1] = FloorTileType.Obstacle;
            }

            List<Layout> layouts = new List<Layout>()
            {
                new Layout("Exit room", 0) { Type = RoomType.Exit },
                new Layout("Starting room", 0) { Type = RoomType.Starting },
                this.EmptyRoomLayout,
                leftBlockedLayout,
                rightBlockedLayout,
                bottomBlockedLayout,
                topBlockedLayout,
            };

            mockedRepo.Setup(r => r.GetAllLayouts()).Returns(layouts);

            FloorGenerator generator = new FloorGenerator(mockedRepo.Object);
            for (int i = 0; i < 10; i++)
            {
                Floor floor = generator.GenerateNextFloor();

                foreach (Room room in floor.GetRooms())
                {
                    foreach (Direction dir in Enum.GetValues(typeof(Direction)))
                    {
                        Room neighbour = room.Neighbours.GetRoomToThe(dir);
                        if (neighbour != null)
                        {
                            Assert.That(room.Layout.IsOpenToThe(dir));
                        }
                    }
                }
            }
        }
    }
}