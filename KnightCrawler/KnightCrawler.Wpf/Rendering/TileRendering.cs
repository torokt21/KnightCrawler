// <copyright file="TileRendering.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Wpf.Rendering
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using KnightCrawler.Data;
    using KnightCrawler.Engine.Models.World.Tiles;

    /// <summary>
    /// Responsible for drawing FloorTiles.
    /// </summary>
    internal partial class RenderingControl : FrameworkElement
    {
        private Drawing RoomFloor { get; set; }

        /// <summary>
        /// Creates a drawing of empty tiles and stores them in <see cref="RoomFloor"/>.
        /// </summary>
        private void UpdateEmptyRoomFloor()
        {
            DrawingGroup group = new DrawingGroup();

            for (int y = 0; y < Layout.RoomHeight; y++)
            {
                for (int x = 0; x < Layout.RoomWidth; x++)
                {
                    group.Children.Add(this.CreateGridDrawing(x + 1, y + 2, EmptyTile.GetRandomTextureVariant()));
                }
            }

            group.Children.Add(this.DrawClutter());
            group.Children.Add(this.DrawShadows());

            this.RoomFloor = group;
        }

        private Drawing DrawShadows()
        {
            DrawingGroup group = new DrawingGroup();

            // Top shadow
            for (int x = 0; x < Layout.RoomWidth; x++)
            {
                if (x != (int)(Layout.RoomWidth / 2) || (x == (int)(Layout.RoomWidth / 2) && this.Model.CurrentFloor.CurrentRoom.Neighbours.Top == null))
                {
                    group.Children.Add(this.CreateGridDrawing(x + 1, 2, BrushLookup.GetBrush(this.GetRandomShadowVariant(Direction.Up))));
                }
            }

            // Side shadows
            for (int y = 0; y < Layout.RoomHeight; y++)
            {
                bool isCenter = y == (int)(Layout.RoomHeight / 2);
                if (!isCenter || (isCenter && this.Model.CurrentFloor.CurrentRoom.Neighbours.Right == null))
                {
                    group.Children.Add(this.CreateGridDrawing(Layout.RoomWidth, y + 2, BrushLookup.GetBrush(this.GetRandomShadowVariant(Direction.Right))));
                }

                if (!isCenter || (isCenter && this.Model.CurrentFloor.CurrentRoom.Neighbours.Left == null))
                {
                    group.Children.Add(this.CreateGridDrawing(1, y + 2, BrushLookup.GetBrush(this.GetRandomShadowVariant(Direction.Left))));
                }
            }

            return group;
        }

        /// <summary>
        /// Draws random clutter on the floor.
        /// </summary>
        /// <returns>Returns the drawing of random cluttering items in the room.</returns>
        private Drawing DrawClutter()
        {
            DrawingGroup group = new DrawingGroup();

            string[] clutterTextures = new string[]
            {
                "clutter_axe",
                "clutter_bones",
                "clutter_bones_2",
                "clutter_bones_3",
                "clutter_crack",
                "clutter_crack_2",
                "clutter_dirt",
                "clutter_dirt_2",
                "clutter_dirt_3",
                "clutter_dirt_4",
                "clutter_money",
                "clutter_money_2",
                "clutter_skull",
            };

            // Generating the random number of clutter textures on the ground.
            int pieces = this.Random.Next(3, 15);

            // Keeps track of where clutter has been placed (to avoid overlaying textures)
            List<Point> placedLocations = new List<Point>();
            for (int i = 0; i < pieces; i++)
            {
                Point point = default;
                string texture = clutterTextures[this.Random.Next(clutterTextures.Length)];
                do
                {
                    int randX = this.Random.Next(Layout.RoomWidth);
                    int randY = this.Random.Next(Layout.RoomHeight);

                    point = new Point(randX, randY);
                }
                while (placedLocations.Any(p => p.Equals(point)));
                placedLocations.Add(point);
                group.Children.Add(this.CreateGridDrawing((int)point.X + 1, (int)(point.Y + 2), texture));
            }

            return group;
        }

        private string GetRandomShadowVariant(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    string[] leftVariants = new string[] { "shadow_left", "shadow_left_2", };
                    return leftVariants[this.Random.Next(leftVariants.Length)];

                case Direction.Right:
                    string[] rightVariants = new string[] { "shadow_right", "shadow_right_2", };
                    return rightVariants[this.Random.Next(rightVariants.Length)];

                case Direction.Up:
                    string[] topVariants = new string[] { "shadow_top", "shadow_top_2", };
                    return topVariants[this.Random.Next(topVariants.Length)];
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the DrawingGroup of all non-empty floor tiles.
        /// </summary>
        /// <returns>Returns a DrawingGroup.</returns>
        private DrawingGroup GetNonEmptyTileDrawings()
        {
            DrawingGroup group = new DrawingGroup();

            for (int y = 0; y < Layout.RoomHeight; y++)
            {
                for (int x = 0; x < Layout.RoomWidth; x++)
                {
                    if (!(this.Model.CurrentFloor.CurrentRoom[x, y] is EmptyTile))
                    {
                        group.Children.Add(this.CreateGridDrawing(x + 1, y + 2, this.Model.CurrentFloor.CurrentRoom[x, y].Texture));
                    }
                }
            }

            return group;
        }
    }
}