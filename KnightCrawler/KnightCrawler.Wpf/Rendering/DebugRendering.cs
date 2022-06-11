// <copyright file="DebugRendering.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Wpf.Rendering
{
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using KnightCrawler.Engine.Models.Behaviours;
    using KnightCrawler.Engine.Models.Entities;
    using KnightCrawler.Engine.Models.Weapons;

    /// <summary>
    /// Responsible for drawing debug info.
    /// </summary>
    internal partial class RenderingControl
    {
        private const bool DebugMode = true;

        private Drawing DrawEnemyPaths()
        {
            DrawingGroup group = new DrawingGroup();
            Pen pen = new Pen(new SolidColorBrush(Color.FromRgb(40, 40, 255)), 2);

            foreach (Enemy entity in this.Model.CurrentFloor.CurrentRoom.Entities.OfType<Enemy>().Where(e => e.MovementBehaviour is AStarPathFinding))
            {
                AStarPathFinding pathf = entity.MovementBehaviour as AStarPathFinding;
                if (pathf.Path == null || pathf.Path.Count <= 1)
                {
                    continue;
                }

                for (int i = 0; i < pathf.Path.Count - 1; i++)
                {
                    group.Children.Add(new GeometryDrawing(null, pen, this.ConnectTileCenters(pathf.Path[i].Rectangle, pathf.Path[i + 1].Rectangle)));
                }

                group.Children.Add(new GeometryDrawing(null, pen, this.ConnectTileCenters(pathf.Path[pathf.Path.Count - 2].Rectangle, pathf.Path[pathf.Path.Count - 1].Rectangle)));
            }

            return group;
        }

        private LineGeometry ConnectTileCenters(Rect rect1, Rect rect2)
        {
            Point rect1Center = new Point(this.LeftOffset + (this.TileSize * (rect1.Left + 1 + (rect1.Width / 2))), this.TileSize * (rect1.Top + 2 + (rect1.Height / 2)));
            Point rect2Center = new Point(this.LeftOffset + (this.TileSize * (rect2.Left + 1 + (rect2.Width / 2))), this.TileSize * (rect2.Top + 2 + (rect2.Height / 2)));
            return new LineGeometry(rect1Center, rect2Center);
        }

        private Drawing DrawHitboxes()
        {
            DrawingGroup group = new DrawingGroup();
            Pen pen = new Pen(new SolidColorBrush(Color.FromRgb(0, 255, 0)), 1);

            foreach (Entity entity in this.Model.CurrentFloor.CurrentRoom.Entities)
            {
                Rect hitbox = entity.Hitbox.Rectangle;
                group.Children.Add(CreateGeoDrawing(
                    new Rect(
                        (this.TileSize * (hitbox.Left + 1)) + this.LeftOffset,
                        this.TileSize * (hitbox.Top + 2),
                        this.TileSize * entity.Hitbox.Rectangle.Width,
                        this.TileSize * entity.Hitbox.Rectangle.Height), pen));
            }

            return group;
        }

        private Drawing DrawLastSwing()
        {
            if (this.Model.Player.Weapon is Sword)
            {
                Pen swingPen = new Pen(new SolidColorBrush(Color.FromRgb(255, 59, 140)), 1);

                Rect rectangle = (this.Model.Player.Weapon as Sword).LastSwingRectangle;

                rectangle = new Rect(
                    this.LeftOffset + ((rectangle.X + 1) * this.TileSize),
                    (rectangle.Y + 2) * this.TileSize,
                    rectangle.Width * this.TileSize,
                    rectangle.Height * this.TileSize);

                return new GeometryDrawing(null, swingPen, new RectangleGeometry(rectangle));
            }

            return null;
        }

        private DrawingGroup DrawHealthbars()
        {
            DrawingGroup group = new DrawingGroup();
            this.Model.CurrentFloor.CurrentRoom.Entities
                .OfType<KillableEntity>()
                .Select(e => this.CreateHealthBar(e.Health, e.MaxHealth, e.Hitbox.Rectangle.Left, e.Hitbox.Rectangle.Bottom + 3, e.Hitbox.Rectangle.Width))
                .ToList()
                .ForEach(d => group.Children.Add(d));

            return group;
        }

        private Drawing DrawLayoutName()
        {
            FormattedText formattedText = new FormattedText(
                "Layout: " + this.Model.CurrentFloor.CurrentRoom.Layout.Name,
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                this.font.GetTypefaces().First(),
                this.FontSize,
                Brushes.Black);

            Geometry textGeo = formattedText.BuildGeometry(new Point(this.GUIOffset, this.ActualHeight - formattedText.Height - this.GUIOffset));
            return new GeometryDrawing(new SolidColorBrush(Color.FromRgb(255, 255, 255)), null, textGeo);
        }

        private Drawing CreateHealthBar(double hp, double maxHp, double x, double y, double width)
        {
            int height = 5;

            DrawingGroup group = new DrawingGroup();
            SolidColorBrush greenBrush = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            SolidColorBrush redBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));

            // Drawing red background
            group.Children.Add(
                CreateGeoDrawing(
                    new Rect(this.LeftOffset + ((x + 1) * this.TileSize), ((y - 1) * this.TileSize) + 5, width * this.TileSize, height),
                    redBrush));

            // Drawing green foreground
            group.Children.Add(
                CreateGeoDrawing(
                new Rect(this.LeftOffset + ((x + 1) * this.TileSize), ((y - 1) * this.TileSize) + 5, (width * (hp / maxHp)) * this.TileSize, height),
                greenBrush));

            return group;
        }
    }
}