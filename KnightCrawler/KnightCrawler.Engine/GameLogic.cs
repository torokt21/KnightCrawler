// <copyright file="GameLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Input;
    using CommonServiceLocator;
    using KnightCrawler.Data;
    using KnightCrawler.Engine.Models;
    using KnightCrawler.Engine.Models.Entities;
    using KnightCrawler.Engine.Models.EventArgs;
    using KnightCrawler.Engine.Models.World;
    using KnightCrawler.Repository;

    /// <summary>
    /// The main logic controlling the game.
    /// </summary>
    public class GameLogic
    {
        /// <summary>
        /// The timespan an entity's single frame is shown in miliseconds.
        /// </summary>
        private const double EntityFrameDelay = 0.1;

        /// <summary>
        /// The time of the last tick.
        /// </summary>
        private DateTime lastTick = DateTime.Now;

        /// <summary>
        /// Slows down the animations.
        /// </summary>
        private double milisecSinceLastFrameStep = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameLogic"/> class.
        /// </summary>
        /// <param name="model">The game model.</param>
        public GameLogic(GameModel model)
        {
            this.GameModel = model;

            this.ScoreRepository = ServiceLocator.Current.GetInstance<IScoreRepository>();
        }

        /// <summary>
        /// Called, when the player levels up, and can upggrade a stat.
        /// </summary>
        public event EventHandler<EventArgs> PromptStatUpgrade;

        /// <summary>
        /// Called, when the player's max health reaches 0. You know... dies.
        /// </summary>
        public event EventHandler<DamagedEventArgs> PlayerDied;

        /// <summary>
        /// Gets or sets the game model.
        /// </summary>
        public GameModel GameModel { get; set; }

        /// <summary>
        /// Gets the list of the player scores.
        /// </summary>
        public IScoreRepository ScoreRepository { get; }

        /// <summary>
        /// Gets the list of keys that are currently held down on the keyboard.
        /// </summary>
        public List<Key> PressedKeys { get; } = new List<Key>();

        /// <summary>
        /// Gets or sets a value indicating whether the game should be paused.
        /// </summary>
        public bool PauseGameplay { get; set; }

        private FloorGenerator FloorGenerator { get; set; }

        /// <summary>
        /// Upgrades the player's given stat.
        /// </summary>
        /// <param name="stat">The stat to level up.</param>
        public void UpgradePlayerStat(PlayerStats.UpgradablePlayerStat stat)
        {
            this.GameModel.Player.Stats.UpgradeStat(stat);
            this.PauseGameplay = false;
        }

        /// <summary>
        /// Resets the game and starts it.
        /// </summary>
        public void StartNewGame()
        {
            // Creating the player.
            this.GameModel.Player = new Player();
            this.GameModel.Player.RoomLeft += this.Player_RoomLeft;
            this.GameModel.Player.Killed += this.Player_Killed;
            this.GameModel.Player.FloorLeft += this.Player_FloorLeft;
            this.GameModel.Player.Stats.UpgradingStat += this.Stats_UpgradingStat;

            // Creating first floor
            this.FloorGenerator = new FloorGenerator();
            this.NextFloor();
            this.PauseGameplay = false;
        }

        /// <summary>
        /// Saves the score.
        /// </summary>
        /// <param name="name">The players name.</param>
        /// <param name="score">The players score.</param>
        public void SaveScore(string name, int score)
        {
            this.ScoreRepository.InsertNew(name, score);
        }

        /// <summary>
        /// Load the score.
        /// </summary>
        /// <returns>Returns a scorelist.</returns>
        public List<PlayerScore> LoadScore()
        {
            return this.ScoreRepository.GetAll().OrderByDescending(s => s.Score).ToList();
        }

        /// <summary>
        /// Moves the game to the next floor.
        /// </summary>
        public void NextFloor()
        {
            this.GameModel.CurrentFloor = this.FloorGenerator.GenerateNextFloor();

            // Calculating where the player spawns.
            PointF startingPoint = new PointF(
                    (Layout.RoomWidth - (float)this.GameModel.Player.Hitbox.RelativeHitbox.Width) / 2,
                    (float)(((Layout.RoomHeight - this.GameModel.Player.Hitbox.RelativeHitbox.Height) / 2) - this.GameModel.Player.Hitbox.RelativeHitbox.Top));

            Sounds.SoundPlayer.PlaySound("next-floor.mp3", Sounds.SoundPlayer.SoundType.SoundEffect);
            this.GameModel.CurrentFloor.MoveToRoom(this.GameModel.CurrentFloor.StartingRoom, startingPoint);
        }

        /// <summary>
        /// Calls the frame-by-frame logic of everything in the current room.
        /// </summary>
        public void GameTick()
        {
            // Getting the elapsed time since the last frame.
            double deltaTime = (DateTime.Now - this.lastTick).TotalSeconds;

            if (!this.PauseGameplay)
            {
                this.HandlePlayerMovement(deltaTime);
                this.HandlePlayerAttack(deltaTime);
                this.TickEnemies(deltaTime);
                this.GameModel.CurrentFloor.Tick(deltaTime);
            }

            // Resetting the elapsed time since the last frame.
            this.lastTick = DateTime.Now;
        }

        private void Stats_UpgradingStat(object sender, EventArgs e)
        {
            this.PauseGameplay = true;
            this.PromptStatUpgrade?.Invoke(sender, e);
        }

        private void Player_FloorLeft(object sender, EventArgs e)
        {
            this.NextFloor();
        }

        /// <summary>
        /// Ticks all enemies.
        /// </summary>
        /// <param name="deltaTime">The delta time.</param>
        private void TickEnemies(double deltaTime)
        {
            this.GameModel.CurrentFloor.CurrentRoom.Entities
                .ToList()
                .ForEach(e => e.OnTick(deltaTime));

            // Slowing down the animations
            this.milisecSinceLastFrameStep += deltaTime;

            if (this.milisecSinceLastFrameStep > EntityFrameDelay)
            {
                this.milisecSinceLastFrameStep = 0;
                this.GameModel.CurrentFloor.CurrentRoom.Entities
                    .ToList()
                    .ForEach(e => e.NextFrame());
            }
        }

        /// <summary>
        /// Takes care of the movement of the player.
        /// </summary>
        /// <param name="deltaTime">The delta time.</param>
        private void HandlePlayerMovement(double deltaTime)
        {
            // Player movement
            int dx = 0, dy = 0;
            if (this.PressedKeys.Contains(Key.D))
            {
                dx = 1;
            }
            else if (this.PressedKeys.Contains(Key.A))
            {
                dx = -1;
            }

            if (this.PressedKeys.Contains(Key.W))
            {
                dy = -1;
            }
            else if (this.PressedKeys.Contains(Key.S))
            {
                dy = 1;
            }

            if (dx != 0 || dy != 0)
            {
                this.GameModel.Player.MoveDirectional(dx, dy, deltaTime);
            }
        }

        /// <summary>
        /// Takes care of the attacks.
        /// </summary>
        /// <param name="deltaTime">The delta time.</param>
        private void HandlePlayerAttack(double deltaTime)
        {
            this.GameModel.Player.Weapon.OnTick(deltaTime);
            if (this.PressedKeys.Contains(Key.Left))
            {
                this.GameModel.Player.Weapon.Attack(Direction.Left);
            }
            else if (this.PressedKeys.Contains(Key.Up))
            {
                this.GameModel.Player.Weapon.Attack(Direction.Up);
            }
            else if (this.PressedKeys.Contains(Key.Right))
            {
                this.GameModel.Player.Weapon.Attack(Direction.Right);
            }

            if (this.PressedKeys.Contains(Key.Down))
            {
                this.GameModel.Player.Weapon.Attack(Direction.Down);
            }
        }

        private void Player_RoomLeft(object sender, Models.EventArgs.LeavingRoomEventArgs e)
        {
            this.GameModel.CurrentFloor.MoveToRoom(
                this.GameModel.CurrentFloor.CurrentRoom.Neighbours.GetRoomToThe(e.Direction),
                Utilities.ReverseDirection(e.Direction));
        }

        private void Player_Killed(object sender, Models.EventArgs.DamagedEventArgs e)
        {
            this.PauseGameplay = true;
            this.PlayerDied?.Invoke(sender, e);
        }
    }
}