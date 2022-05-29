// <copyright file="WallRendering.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Wpf.Rendering
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using KnightCrawler.Data;

    /// <summary>
    /// Responsible for rendering the walls and doorways.
    /// </summary>
    internal partial class RenderingControl : FrameworkElement
    {
        private const int WallBannerVariantChance = 5;
        private const int WallHoleVariantChance = 10;

        private Drawing RoomFrame { get; set; }

        private DrawingGroup OverlayingWalls { get; set; }

        /// <summary>
        /// Builds a new randomized room frame.
        /// </summary>
        private void UpdateRoomFrame()
        {
            DrawingGroup group = new DrawingGroup();

            group.Children.Add(this.DrawWalls());
            group.Children.Add(this.DrawCorridors());

            this.RoomFrame = group;
        }

        private Drawing DrawWalls()
        {
            DrawingGroup group = new DrawingGroup();

            group.Children.Add(this.DrawRoomCorners());

            int xCenter = (int)Layout.RoomWidth / 2;
            int yCenter = (int)Layout.RoomHeight / 2;

            for (int i = 1; i <= Layout.RoomWidth; i++)
            {
                // Top walls
                if (i != xCenter + 1 || (i == xCenter + 1 && this.Model.CurrentFloor.CurrentRoom.Neighbours.Top == null))
                {
                    group.Children.Add(this.CreateGridDrawing(i, 0, BrushLookup.GetBrush("wall_top_mid")));
                    group.Children.Add(this.CreateGridDrawing(i, 1, BrushLookup.GetBrush(this.GetRandomWallTexture(true))));
                }
            }

            // Side walls
            for (int i = 2; i <= Layout.RoomHeight + 1; i++)
            {
                // True, if the wall is at a Y coordinate where a corridoor texture could be
                bool isCorridoorY = i <= yCenter + 2 && i >= yCenter;

                // Left wall
                if (!isCorridoorY || (isCorridoorY && this.Model.CurrentFloor.CurrentRoom.Neighbours.Left == null))
                {
                    group.Children.Add(this.CreateGridDrawing(0, i, "wall_side_mid_left"));
                }

                // Right wall
                if (!isCorridoorY || (isCorridoorY && this.Model.CurrentFloor.CurrentRoom.Neighbours.Right == null))
                {
                    group.Children.Add(this.CreateGridDrawing(Layout.RoomWidth + 1, i, "wall_side_mid_right"));
                }
            }

            return group;
        }

        private Drawing DrawRoomCorners()
        {
            DrawingGroup group = new DrawingGroup();

            // Top left corner
            group.Children.Add(this.CreateGridDrawing(0, 0, BrushLookup.GetBrush("wall_side_top_left")));
            group.Children.Add(this.CreateGridDrawing(0, 1, BrushLookup.GetBrush("wall_side_mid_left")));

            // Top right corner
            group.Children.Add(this.CreateGridDrawing(Layout.RoomWidth + 1, 0, BrushLookup.GetBrush("wall_side_top_right")));
            group.Children.Add(this.CreateGridDrawing(Layout.RoomWidth + 1, 1, BrushLookup.GetBrush("wall_side_mid_right")));

            // Bottom left corner
            group.Children.Add(this.CreateGridDrawing(0, Layout.RoomHeight + 2, BrushLookup.GetBrush("wall_side_front_left")));

            // Bottom right corner
            group.Children.Add(this.CreateGridDrawing(Layout.RoomWidth + 1, Layout.RoomHeight + 2, BrushLookup.GetBrush("wall_side_front_right")));

            return group;
        }

        private Drawing DrawCorridors()
        {
            DrawingGroup group = new DrawingGroup();
            int xCenter = (int)Layout.RoomWidth / 2;
            int yCenter = (int)Layout.RoomHeight / 2;

            // Drawing left corridor (if there is one)
            if (this.Model.CurrentFloor.CurrentRoom.Neighbours.Left != null)
            {
                group.Children.Add(this.CreateGridDrawing(0, yCenter, "wall_corner_bottom_right"));
                group.Children.Add(this.CreateGridDrawing(0, yCenter + 1, "wall_right"));
                group.Children.Add(this.CreateGridDrawing(0, yCenter + 2, Engine.Models.World.Tiles.EmptyTile.GetRandomTextureVariant()));
                group.Children.Add(this.CreateGridDrawing(0, yCenter + 2, "wall_corner_top_right"));
                group.Children.Add(this.CreateGridDrawing(0, yCenter + 3, "wall_corner_right"));
            }

            // Drawing right corridor (if there is one)
            if (this.Model.CurrentFloor.CurrentRoom.Neighbours.Right != null)
            {
                group.Children.Add(this.CreateGridDrawing(Layout.RoomWidth + 1, yCenter, "wall_corner_bottom_left"));
                group.Children.Add(this.CreateGridDrawing(Layout.RoomWidth + 1, yCenter + 1, "wall_left"));
                group.Children.Add(this.CreateGridDrawing(Layout.RoomWidth + 1, yCenter + 2, Engine.Models.World.Tiles.EmptyTile.GetRandomTextureVariant()));
                group.Children.Add(this.CreateGridDrawing(Layout.RoomWidth + 1, yCenter + 2, "wall_corner_top_left"));
                group.Children.Add(this.CreateGridDrawing(Layout.RoomWidth + 1, yCenter + 3, "wall_corner_left"));
            }

            // Drawing top corridor (if there is one)
            if (this.Model.CurrentFloor.CurrentRoom.Neighbours.Top != null)
            {
                group.Children.Add(this.CreateGridDrawing(xCenter, 1, BrushLookup.GetBrush("wall_right")));
                group.Children.Add(this.CreateGridDrawing(xCenter + 2, 1, BrushLookup.GetBrush("wall_left")));
                group.Children.Add(this.CreateGridDrawing(xCenter + 2, 0, BrushLookup.GetBrush("wall_inner_corner_l_top_left")));
                group.Children.Add(this.CreateGridDrawing(xCenter, 0, BrushLookup.GetBrush("wall_inner_corner_l_top_rigth")));
                group.Children.Add(this.CreateGridDrawing(xCenter + 1, 1, BrushLookup.GetBrush("floor_1")));
            }

            // Drawing bottom floortile if there is a corridoor there (rest of the corridoor must be drawn above entities)
            if (this.Model.CurrentFloor.CurrentRoom.Neighbours.Bottom != null)
            {
                group.Children.Add(this.CreateGridDrawing(xCenter + 1, Layout.RoomHeight + 2, BrushLookup.GetBrush("floor_1")));
            }

            return group;
        }

        private string GetRandomWallTexture(bool canBeBanner)
        {
            int luck = this.Random.Next(1, 101);

            string[] possibleNormalVariants = new string[]
            {
                "wall_hole_1",
                "wall_hole_2",
                "wall_sewer",
            };

            string[] possibleBannerVariants = new string[]
            {
                "wall_banner_red",
                "wall_banner_green",
                "wall_banner_blue",
                "wall_banner_yellow",
            };

            if (canBeBanner & luck <= WallBannerVariantChance)
            {
                return possibleBannerVariants[this.Random.Next(possibleBannerVariants.Length)];
            }
            else if (luck < WallHoleVariantChance)
            {
                return possibleNormalVariants[this.Random.Next(possibleNormalVariants.Length)];
            }

            return "wall_mid";
        }

        /// <summary>
        /// Gets the drawing of walls that must be drown on top.
        /// </summary>
        private void UpdateOverlayingWalls()
        {
            this.OverlayingWalls = new DrawingGroup();
            int xCenter = (int)Layout.RoomWidth / 2;

            for (int i = 1; i <= Layout.RoomWidth; i++)
            {
                bool isCoridoorBrim = Math.Abs(xCenter + 1 - i) <= 1;

                // Bottom wall brim
                if (!(isCoridoorBrim && this.Model.CurrentFloor.CurrentRoom.Neighbours.Bottom != null))
                {
                    this.OverlayingWalls.Children.Add(this.CreateGridDrawing(i, Layout.RoomHeight + 1, BrushLookup.GetBrush("wall_top_mid")));
                }

                // Bottom wall
                if (i != xCenter + 1 || (i == xCenter + 1 && this.Model.CurrentFloor.CurrentRoom.Neighbours.Bottom == null))
                {
                    this.OverlayingWalls.Children.Add(this.CreateGridDrawing(i, Layout.RoomHeight + 2, BrushLookup.GetBrush(this.GetRandomWallTexture(false))));
                }
            }

            // Drawing bottom corridor (if there is one)
            if (this.Model.CurrentFloor.CurrentRoom.Neighbours.Bottom != null)
            {
                this.OverlayingWalls.Children.Add(this.CreateGridDrawing(xCenter, Layout.RoomHeight + 1, BrushLookup.GetBrush("wall_top_right")));
                this.OverlayingWalls.Children.Add(this.CreateGridDrawing(xCenter + 2, Layout.RoomHeight + 1, BrushLookup.GetBrush("wall_top_left")));
                this.OverlayingWalls.Children.Add(this.CreateGridDrawing(xCenter, Layout.RoomHeight + 2, BrushLookup.GetBrush("wall_side_mid_left")));
                this.OverlayingWalls.Children.Add(this.CreateGridDrawing(xCenter + 2, Layout.RoomHeight + 2, BrushLookup.GetBrush("wall_side_mid_right")));
            }
        }
    }
}