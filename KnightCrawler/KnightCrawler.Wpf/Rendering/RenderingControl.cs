// <copyright file="RenderingControl.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Wpf.Rendering
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;
    using KnightCrawler.Data;
    using KnightCrawler.Engine;
    using KnightCrawler.Engine.Models;
    using KnightCrawler.Engine.Models.Entities;
    using KnightCrawler.Engine.Models.Weapons;
    using KnightCrawler.Engine.Models.World;
    using KnightCrawler.Engine.Sounds;

    /// <summary>
    /// The control that renders the gameplay.
    /// </summary>
    internal partial class RenderingControl : FrameworkElement
    {
        private FontFamily font = new FontFamily(new Uri("pack://application:,,,/KnightCrawler.Wpf;component/"), "./#Yoster Island");

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderingControl"/> class.
        /// </summary>
        public RenderingControl()
        {
            this.Loaded += this.RoomRenderer_Loaded;
        }

        private DispatcherTimer Timer { get; set; } = new DispatcherTimer();

        private GameModel Model { get; set; }

        private GameLogic Logic { get; set; }

        private int TileSize { get; set; }

        private int LeftOffset { get; set; }

        private Random Random { get; set; } = new Random();

        /// <summary>
        /// Gets or sets the translatre transform, that shifts everything into the actual room.
        /// </summary>
        private Transform TranslateToRoom { get; set; }

        /// <summary>
        /// Initializes the FrameworkElement.
        /// </summary>
        /// <param name="vm">The game view model.</param>
        public void Initialize(GameViewModel vm)
        {
            this.Model = vm.GameModel;
            this.Logic = vm.GameLogic;

            this.SizeChanged += this.RoomRenderer_SizeChanged;

            this.Model.Player.Damaged += this.Player_HealthChanged;
            this.Model.Player.Healed += this.Player_HealthChanged;
            this.Model.Player.Stats.UpgradedStat += this.Stats_StatUpgraded;
            this.Model.Player.Stats.AddedScore += this.Stats_AddedScore;

            SoundPlayer.PlaySound("game-theme.mp3", SoundPlayer.SoundType.Music, true);

            this.UpdateRoom();

            this.Timer = new DispatcherTimer(TimeSpan.FromMilliseconds(15), DispatcherPriority.Render, this.Timer_Tick, Dispatcher.CurrentDispatcher);
            this.Timer.Start();
        }

        /// <inheritdoc/>
        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawDrawing(this.DrawRoom());
            drawingContext.DrawDrawing(this.DrawUI());

            if (DebugMode && Debugger.IsAttached)
            {
                drawingContext.DrawDrawing(this.DrawHitboxes());
                drawingContext.DrawDrawing(this.DrawHealthbars());
                drawingContext.DrawDrawing(this.DrawEnemyPaths());
                drawingContext.DrawDrawing(this.DrawLayoutName());

                Drawing lastSwing = this.DrawLastSwing();
                if (lastSwing != null)
                {
                    drawingContext.DrawDrawing(lastSwing);
                }
            }
        }

        private static GeometryDrawing CreateGeoDrawing(Rect rectangle, Brush brush, Pen pen)
        {
            Geometry geometry = new RectangleGeometry(rectangle);
            return new GeometryDrawing(
                brush,
                pen,
                geometry);
        }

        private static GeometryDrawing CreateGeoDrawing(Rect rectangle, Brush brush) => CreateGeoDrawing(rectangle, brush, null);

        private static GeometryDrawing CreateGeoDrawing(Rect rectangle, string imageName) => CreateGeoDrawing(rectangle, BrushLookup.GetBrush(imageName), null);

        private static GeometryDrawing CreateGeoDrawing(Rect rectangle, Pen pen) => CreateGeoDrawing(rectangle, null, pen);

        /// <summary>
        /// Updates the room's stored drawings.
        /// </summary>
        private void UpdateRoom()
        {
            // Updating the transform.
            this.TranslateToRoom = new TranslateTransform(this.LeftOffset + this.TileSize, 2 * this.TileSize);

            // Creating a new random from the current room's hash, so every update results in the same randomized textures.
            this.Random = new Random(this.Model.CurrentFloor.CurrentRoom.GetHashCode());

            // Updating textures that don't need to be recalculated every frame.
            this.UpdateRoomFrame();
            this.UpdateEmptyRoomFloor();
            this.UpdateOverlayingWalls();
            this.UpdateHealth();
            this.UpdateMinimap();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.InvalidateVisual();
            this.Logic.GameTick();
        }

        private void Player_RoomLeft(object sender, Engine.Models.EventArgs.LeavingRoomEventArgs e)
        {
            this.UpdateRoom();
            this.UpdateMinimap();
        }

        private void Player_HealthChanged(object sender, EventArgs e)
        {
            this.UpdateHealth();
        }

        private void Stats_AddedScore(object sender, EventArgs e)
        {
            this.UpdateHealth();
        }

        private void Stats_StatUpgraded(object sender, Engine.Models.EventArgs.StatUpgadeEventArgs e)
        {
            // Updating the health bar if the player's max health has been upgraded.
            if (e.Stat == PlayerStats.UpgradablePlayerStat.MaxHealth)
            {
                this.UpdateHealth();
            }

            // Updating the room textures, becuase the chest needs to be drawn open.
            this.UpdateRoom();
        }

        private void RoomRenderer_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            if (window != null)
            {
                window.PreviewKeyDown += this.Window_KeyDown;
                window.PreviewKeyUp += this.Window_KeyUp;
            }

            this.Model.Player.RoomLeft += this.Player_RoomLeft;
            this.Model.Player.FloorLeft += this.Player_FloorLeft;

            this.UpdateRoom();
        }

        private void Player_FloorLeft(object sender, EventArgs e)
        {
            this.UpdateRoom();
            this.UpdateMinimap();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.Logic.PressedKeys.Contains(e.Key))
            {
                this.Logic.PressedKeys.Remove(e.Key);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!this.Logic.PressedKeys.Contains(e.Key))
            {
                this.Logic.PressedKeys.Add(e.Key);
            }
        }

        private void RoomRenderer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Calculating the dimensions of one cell.
            this.TileSize = (int)Math.Ceiling(this.ActualHeight / (Layout.RoomHeight + 3));

            // The left offset of all drawings, to keep the room centered.
            this.LeftOffset = Convert.ToInt32((this.ActualWidth - ((Layout.RoomWidth + 2) * this.TileSize)) / 2);

            // Updating the minimap rectangle
            Size minimapSize = new Size(this.TileSize * 3, this.TileSize * 2);
            Point minimapPosition = new Point(this.ActualWidth - this.GUIOffset - minimapSize.Width, this.GUIOffset);

            this.MinimapRect = new Rect(minimapPosition, minimapSize);

            // The room size has changed, so the saved drawings need to be redrawn.
            this.UpdateRoom();
        }

        /// <summary>
        /// Simplifies the way a GeometryDrawing is initialized.
        /// </summary>
        /// <param name="x">The x coordinate in cells.</param>
        /// <param name="y">The y coordinate in cells.</param>
        /// <param name="brush">The brush to use.</param>
        /// <returns>Returns a drawing that snaps to the layour grid.</returns>
        private GeometryDrawing CreateGridDrawing(int x, int y, Brush brush)
        {
            return CreateGeoDrawing(new Rect(this.LeftOffset + (x * this.TileSize), y * this.TileSize, this.TileSize, this.TileSize), brush);
        }

        /// <summary>
        /// Simplifies the way a GeometryDrawing is initialized.
        /// </summary>
        /// <param name="x">The x coordinate in cells.</param>
        /// <param name="y">The y coordinate in cells.</param>
        /// <param name="brushName">The name of the brush to use.</param>
        /// <returns>Returns a drawing that snaps to the layour grid.</returns>
        private GeometryDrawing CreateGridDrawing(int x, int y, string brushName) => this.CreateGridDrawing(x, y, BrushLookup.GetBrush(brushName));

        /// <summary>
        /// Returns a drawing containing everything in the room.
        /// </summary>
        /// <returns>Returns the complete drawing of the current room.</returns>
        private Drawing DrawRoom()
        {
            DrawingGroup group = new DrawingGroup();
            group.Children.Add(this.RoomFrame);
            group.Children.Add(this.RoomFloor);

            group.Children.Add(this.GetNonEmptyTileDrawings());

            if (this.Model.Player.Weapon is Sword)
            {
                group.Children.Add(this.DrawPlayerSword());
            }

            group.Children.Add(this.GetEntityDrawings());
            group.Children.Add(this.OverlayingWalls);

            group.Opacity = Floor.CurrentRoomOpacityAnimationProgress;
            return group;
        }
    }
}