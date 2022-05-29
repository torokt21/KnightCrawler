// <copyright file="GUIRendering.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Wpf.Rendering
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using KnightCrawler.Data;
    using KnightCrawler.Engine.Models.World;

    /// <summary>
    /// Responsible for drawing the UI.
    /// </summary>
    internal partial class RenderingControl
    {
        private const double UIRoomScale = 0.03;

        private double FontSize => Math.Max(10, this.ActualHeight / 30);

        private double GUIOffset => this.TileSize / 6;

        private DrawingGroup Healthbar { get; set; }

        private DrawingGroup Minimap { get; set; }

        /// <summary>
        /// Gets or sets the rectangle of the minimap.
        /// </summary>
        private Rect MinimapRect { get; set; }

        // Minimap colors
        private SolidColorBrush MinimapClearedBrush { get; } = new SolidColorBrush(Color.FromRgb(160, 160, 160));

        private SolidColorBrush MinimapCurrentRoomBrush { get; } = new SolidColorBrush(Color.FromRgb(230, 230, 230));

        private SolidColorBrush MinimapNonClearedBrush { get; } = new SolidColorBrush(Color.FromRgb(100, 100, 100));

        private SolidColorBrush MinimapBackground { get; } = new SolidColorBrush(Color.FromArgb(20, 0, 0, 0));

        private Pen MinimapBorder { get; } = new Pen(new SolidColorBrush(Color.FromRgb(10, 10, 10)), 5);

        /// <summary>
        /// Gets the drawing ot the GUI.
        /// </summary>
        /// <returns>Drawing group.</returns>
        public DrawingGroup DrawUI()
        {
            DrawingGroup group = new DrawingGroup();

            group.Children.Add(this.Healthbar);
            group.Children.Add(this.Minimap);

            return group;
        }

        /// <summary>
        /// Updates the drawing of the player healthbar.
        /// </summary>
        private void UpdateHealth()
        {
            this.Healthbar = new DrawingGroup();

            double heartSize = this.TileSize / 1.4;

            // Calculating number of hearts
            int fullHearts = Convert.ToInt32(Math.Floor(this.Model.Player.Health / 2));
            int emptyHearts = Convert.ToInt32(Math.Floor((this.Model.Player.MaxHealth - this.Model.Player.Health) / 2));
            int halfHeart = this.Model.Player.Health % 2 == 1 ? 1 : 0;

            // Drawing full hearts
            for (int i = 0; i < fullHearts; i++)
            {
                this.Healthbar.Children.Add(
                    CreateGeoDrawing(
                        new Rect(this.GUIOffset + (i * heartSize), this.GUIOffset, heartSize, heartSize),
                        "ui_heart_full"));
            }

            // Drawing half heart
            if (halfHeart == 1)
            {
                this.Healthbar.Children.Add(
                    CreateGeoDrawing(
                        new Rect(this.GUIOffset + (fullHearts * heartSize), this.GUIOffset, heartSize, heartSize),
                        "ui_heart_half"));
            }

            // Drawing empty hearts
            for (int i = 0; i < emptyHearts; i++)
            {
                this.Healthbar.Children.Add(
                    CreateGeoDrawing(
                        new Rect(
                            this.GUIOffset + ((i + fullHearts + halfHeart) * heartSize), this.GUIOffset, heartSize, heartSize),
                        "ui_heart_empty"));
            }

            FormattedText formattedText = new FormattedText(
                "Score: " + this.Model.Player.Stats.Score,
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                this.font.GetTypefaces().First(),
                this.FontSize,
                Brushes.Black);

            Geometry textGeo = formattedText.BuildGeometry(new Point(this.GUIOffset, this.GUIOffset + (heartSize * 2)));
            this.Healthbar.Children.Add(new GeometryDrawing(new SolidColorBrush(Color.FromRgb(255, 255, 255)), null, textGeo));
        }

        private void UpdateMinimap()
        {
            this.Minimap = new DrawingGroup();

            this.Minimap.Children.Add(CreateGeoDrawing(this.MinimapRect, this.MinimapBackground));

            List<Room> rooms = this.Model.CurrentFloor.GetRooms()
                .Where(r => r.IsVisibleOnMinimap)
                .ToList();

            Size rectSize = new Size(
                this.TileSize * Layout.RoomWidth * UIRoomScale,
                this.TileSize * Layout.RoomHeight * UIRoomScale);

            Point currentRoomPos = new Point(
                this.MinimapRect.Left + ((this.MinimapRect.Width - rectSize.Width) / 2),
                this.MinimapRect.Top + ((this.MinimapRect.Height - rectSize.Height) / 2));

            // Adding rooms
            foreach (Room room in rooms)
            {
                Point pos = new Point(
                    currentRoomPos.X + ((room.Offset.X - this.Model.CurrentFloor.CurrentRoom.Offset.X) * (rectSize.Width + 2)),
                    currentRoomPos.Y + ((room.Offset.Y - this.Model.CurrentFloor.CurrentRoom.Offset.Y) * (rectSize.Height + 2)));
                Rect roomRect = new Rect(pos, rectSize);

                // Only draw on the minimap
                roomRect.Intersect(this.MinimapRect);

                double cornerRadius = roomRect.Height / 8;
                Geometry geo = new RectangleGeometry(roomRect, cornerRadius, cornerRadius);
                this.Minimap.Children.Add(new GeometryDrawing(this.GetMinimapRoomBrush(room), null, geo));
            }

            // Drawing border and background
            this.Minimap.Children.Add(CreateGeoDrawing(this.MinimapRect, this.MinimapBorder));

            // Writing floor number
            var tfc = this.font.GetTypefaces();

            FormattedText formattedText = new FormattedText(
                "Floor " + this.Model.CurrentFloor.Difficulty,
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                this.font.GetTypefaces().First(),
                this.FontSize,
                Brushes.Black);

            Geometry textGeometry = formattedText.BuildGeometry(
                new Point(this.MinimapRect.Left + ((this.MinimapRect.Width - formattedText.Width) / 2), this.MinimapRect.Bottom + this.GUIOffset));

            this.Minimap.Children.Add(new GeometryDrawing(new SolidColorBrush(Color.FromRgb(255, 255, 255)), null, textGeometry));
        }

        /// <summary>
        /// Returns the colored brush to use to draw the room on the minimap.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <returns>A SolidColorBursh.</returns>
        private Brush GetMinimapRoomBrush(Room room)
        {
            return room == this.Model.CurrentFloor.CurrentRoom ? this.MinimapCurrentRoomBrush : room.IsCleared ? this.MinimapClearedBrush : this.MinimapNonClearedBrush;
        }
    }
}