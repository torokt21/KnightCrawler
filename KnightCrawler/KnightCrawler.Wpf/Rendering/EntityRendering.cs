// <copyright file="EntityRendering.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Wpf.Rendering
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using KnightCrawler.Data;
    using KnightCrawler.Engine.Models.Entities;
    using KnightCrawler.Engine.Models.Weapons;

    /// <summary>
    /// Responsible for rendering entities.
    /// </summary>
    internal partial class RenderingControl : FrameworkElement
    {
        private Brush damageOverlayBrush = new SolidColorBrush(Color.FromArgb(200, 210, 20, 20));

        /// <summary>
        /// Brush for entity shadows.
        /// </summary>
        private Brush shadowBrush = new SolidColorBrush(Color.FromArgb(70, 0, 0, 0));

        /// <summary>
        /// Eases the swing of the sword animation.
        /// </summary>
        /// <param name="p">The double between 0.0 and 1.0.</param>
        /// <returns>Returns the eased double.</returns>
        private static double ExponentialEaseOut(double p)
        {
            return (p == 1.0f) ? p : 1 - Math.Pow(5, -10 * p);
        }

        /// <summary>
        /// Translates a double between 0.0-1.0 to a double representing the animation of rotating the sword (0.0-1.0).
        /// </summary>
        /// <param name="p">The progress of the animation.</param>
        /// <returns>Returns how far the sword needs to be rotated.</returns>
        private static double TranslateDoubleToSwordRotation(double p)
        {
            // The ratio of time spent swinging to time spent pulling the sword back.
            double swingRatio = 0.9;
            if (p <= swingRatio)
            {
                return ExponentialEaseOut(p * (1 / swingRatio));
            }
            else
            {
                return 1 - ((p - swingRatio) * (1 / (1 - swingRatio)));
            }
        }

        /// <summary>
        /// Gets the DrawingGroup of all entities.
        /// </summary>
        /// <returns>Returns the DrawingGroup of all entities.</returns>
        private DrawingGroup GetEntityDrawings()
        {
            Brush redBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));

            DrawingGroup group = new DrawingGroup();
            var entities = this.Model.CurrentFloor.CurrentRoom.Entities.OrderBy(e => e.Hitbox.Rectangle.Bottom);

            foreach (Entity entity in entities)
            {
                group.Children.Add(this.DrawEntityShadow(entity));

                // The entity imagebrush.
                Brush entityBrush = BrushLookup.GetBrush(
                            entity.CurrentFrame,
                            entity.LastHorizontalMovement == Direction.Left);

                // The entity drawing.
                GeometryDrawing drawing = CreateGeoDrawing(
                        new Rect(
                            entity.Location.X * this.TileSize,
                            entity.Location.Y * this.TileSize,
                            entity.Size.Width * this.TileSize,
                            entity.Size.Height * this.TileSize),
                        entityBrush);

                group.Children.Add(drawing);

                // Drawing damaged overlay.
                if (entity is KillableEntity && (entity as KillableEntity).DamageAnimationProgress < 1)
                {
                    DrawingGroup overlay = new DrawingGroup();
                    overlay.OpacityMask = entityBrush;
                    overlay.Opacity = 1 - (entity as KillableEntity).DamageAnimationProgress;
                    overlay.Children.Add(new GeometryDrawing(this.damageOverlayBrush, null, drawing.Geometry));
                    group.Children.Add(overlay);
                }
            }

            group.Transform = this.TranslateToRoom;

            return group;
        }

        private Drawing DrawEntityShadow(Entity entity)
        {
            DrawingGroup group = new DrawingGroup();

            Size outerSize = new Size(entity.Hitbox.Rectangle.Width * 0.8, entity.Hitbox.Rectangle.Height / 2);
            Rect outerRect = new Rect(entity.Hitbox.Rectangle.X + ((entity.Hitbox.Rectangle.Width - outerSize.Width) / 2), entity.Hitbox.Rectangle.Y + (entity.Hitbox.Rectangle.Height / 2), outerSize.Width, outerSize.Height);

            Size innerSize = new Size(outerRect.Width / 1.4, outerRect.Height * 0.9);

            Rect innerRect = new Rect(
                outerRect.X + ((outerRect.Width - innerSize.Width) / 2),
                outerRect.Y + ((outerRect.Height - innerSize.Height) / 2),
                innerSize.Width,
                innerSize.Height);

            outerRect.Scale(this.TileSize, this.TileSize);
            innerRect.Scale(this.TileSize, this.TileSize);

            Geometry geo = new RectangleGeometry(outerRect);
            Geometry geo2 = new RectangleGeometry(innerRect);
            group.Children.Add(new GeometryDrawing(this.shadowBrush, null, geo));
            group.Children.Add(new GeometryDrawing(this.shadowBrush, null, geo2));

            return group;
        }

        private Drawing DrawPlayerSword()
        {
            Sword sword = this.Model.Player.Weapon as Sword;
            ImageBrush brush = BrushLookup.GetBrush(sword.TextureName);

            double swordRectSideRatio = brush.ImageSource.Width / brush.ImageSource.Height;
            double swordRectHeight = this.Model.Player.Size.Height / 1.6;

            Size size = new Size(swordRectHeight * swordRectSideRatio, swordRectHeight);
            Rect rect = default;

            TransformGroup transforms = new TransformGroup();

            if (sword.DisplayDirection == Direction.Left)
            {
                rect = new Rect(this.Model.Player.Hitbox.Rectangle.X - (size.Width / 2), this.Model.Player.Location.Y + (this.Model.Player.Size.Height / 5), size.Width, size.Height);
            }
            else if (sword.DisplayDirection == Direction.Right)
            {
                rect = new Rect(this.Model.Player.Hitbox.Rectangle.Right - (size.Width / 2), this.Model.Player.Location.Y + (this.Model.Player.Size.Height / 5), size.Width, size.Height);
            }
            else if (sword.DisplayDirection == Direction.Up)
            {
                rect = new Rect(this.Model.Player.Hitbox.Rectangle.Left + (size.Width / 2), this.Model.Player.Hitbox.Rectangle.Top - size.Height, size.Width, size.Height);
            }
            else if (sword.DisplayDirection == Direction.Down)
            {
                rect = new Rect(this.Model.Player.Hitbox.Rectangle.Left + (size.Width / 2), this.Model.Player.Hitbox.Rectangle.Bottom - size.Height, size.Width, size.Height);
            }

            rect.Scale(this.TileSize, this.TileSize);

            Geometry geo = new RectangleGeometry(rect);

            Drawing swordDrawing = new GeometryDrawing(brush, DebugMode ? new Pen(new SolidColorBrush(Color.FromRgb(60, 240, 14)), 1) : null, geo);

            DrawingGroup drawingGroup = new DrawingGroup();

            // Transforming
            drawingGroup.Children.Add(swordDrawing);
            transforms.Children.Add(this.RotateSword(sword, drawingGroup));
            transforms.Children.Add(this.TranslateToRoom);
            drawingGroup.Transform = transforms;

            return drawingGroup;
        }

        /// <summary>
        /// Returns a transform for the player's sword's correct rotation.
        /// </summary>
        /// <param name="sword">The sword to rotate.</param>
        /// <param name="drawing">The drawing of the sword.</param>
        /// <returns>Returns a rotate transform.</returns>
        private RotateTransform RotateSword(Sword sword, Drawing drawing)
        {
            double rotationCenterY = drawing.Bounds.Bottom - (drawing.Bounds.Height / 10);
            switch (sword.DisplayDirection)
            {
                case Direction.Left:
                    return new RotateTransform(-TranslateDoubleToSwordRotation(sword.AnimationProgress) * 180, drawing.Bounds.Left + (drawing.Bounds.Width / 2), rotationCenterY);

                case Direction.Right:
                    return new RotateTransform(TranslateDoubleToSwordRotation(sword.AnimationProgress) * 180, drawing.Bounds.Left + (drawing.Bounds.Width / 2), rotationCenterY);

                case Direction.Up:
                    return new RotateTransform((TranslateDoubleToSwordRotation(sword.AnimationProgress) * 180) - 90, drawing.Bounds.Left + (drawing.Bounds.Width / 2), rotationCenterY);

                case Direction.Down:
                    return new RotateTransform((TranslateDoubleToSwordRotation(sword.AnimationProgress) * 180) + 90, drawing.Bounds.Left + (drawing.Bounds.Width / 2), rotationCenterY);
            }

            return new RotateTransform(-TranslateDoubleToSwordRotation(sword.AnimationProgress) * 180, drawing.Bounds.Left + (drawing.Bounds.Width / 2), drawing.Bounds.Bottom);
        }
    }
}