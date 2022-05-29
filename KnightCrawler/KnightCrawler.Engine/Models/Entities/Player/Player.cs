// <copyright file="Player.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Entities
{
    using System;
    using System.Drawing;
    using KnightCrawler.Data;
    using KnightCrawler.Engine.Models.EventArgs;
    using KnightCrawler.Engine.Models.Weapons;
    using KnightCrawler.Engine.Models.World;

    /// <summary>
    /// The player class.
    /// </summary>
    public class Player : KillableEntity
    {
        private const int DamageCooldownInSeconds = 1;
        private double dmgCooldown = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        public Player()
            : base(
                  EntityType.Player,
                  new Point(0, 0),
                  null,
                  new SizeF(0.8f, 1.5f),
                  6,
                  new string[] { "knight_m_run_anim_f0", "knight_m_run_anim_f1", "knight_m_run_anim_f2", "knight_m_run_anim_f3" })
        {
            this.Stats = new PlayerStats(this);
            this.Weapon = new Sword(this, 1, "weapon_anime_sword");
        }

        /// <summary>
        /// Called, when the player leaves a room.
        /// </summary>
        public event EventHandler<LeavingRoomEventArgs> RoomLeft;

        /// <summary>
        /// Called, when the player leaves the floor.
        /// </summary>
        public event EventHandler<EventArgs> FloorLeft;

        /// <summary>
        /// Gets or sets the current score of the player.
        /// </summary>
        public int Score { get; set; }

        /// <inheritdoc/>
        public override double MaxHealth { get => this.Stats.MaxHealth; }

        /// <summary>
        /// Gets the stats of the player.
        /// </summary>
        public PlayerStats Stats { get; }

        /// <summary>
        /// Gets or sets the weapon in the player's hands.
        /// </summary>
        public Weapon Weapon { get; set; }

        /// <summary>
        /// Moves the player in the given direction.
        /// </summary>
        /// <param name="dx">The movement's direction on the x axis (-1; 0; +1).</param>
        /// <param name="dy">The movement's direction on the y axis (-1; 0; +1).</param>
        /// <param name="deltaTime">The time elapsed since the last tick.</param>
        public void MoveDirectional(int dx, int dy, double deltaTime)
        {
            this.MoveDirectional(dx, dy, this.Stats.Speed, deltaTime, true);
        }

        /// <summary>
        /// Calls the LeavingRoom event.
        /// </summary>
        /// <param name="leftRoom">The room the player just left.</param>
        /// <param name="direction">The direction the player left the room.</param>
        public void OnLeavingRoom(Room leftRoom, Direction direction)
        {
            this.RoomLeft?.Invoke(this, new LeavingRoomEventArgs(leftRoom, direction));
        }

        /// <inheritdoc/>
        public override void Damage(double damageValue, object dealer = null)
        {
            // If damaged, and cooldown is 0
            if (this.dmgCooldown == 0)
            {
                base.Damage(damageValue, dealer);

                Sounds.SoundPlayer.PlaySound("player-hurt.mp3", Sounds.SoundPlayer.SoundType.SoundEffect);
                this.dmgCooldown = DamageCooldownInSeconds;
            }
        }

        /// <inheritdoc/>
        public override void OnTick(double deltaTime)
        {
            base.OnTick(deltaTime);
            this.dmgCooldown = Math.Max(this.dmgCooldown - deltaTime, 0);
        }

        /// <summary>
        /// Called when the player leaves the floor.
        /// </summary>
        public void OnFloorLeft()
        {
            this.FloorLeft?.Invoke(this, new EventArgs());
        }

        /// <inheritdoc/>
        protected override void DoWallDetection(ref float newPosX, ref float newPosY)
        {
            bool roomLeft = false;

            if (this.Room.IsCleared)
            {
                // Detection if the player is leaving the room.
                if (this.IsCenteredHorizontally(newPosX))
                {
                    // The player is centered horizontally and going up.
                    if (newPosY + this.Hitbox.RelativeHitbox.Top < 0 && this.Room.Neighbours.GetRoomToThe(Direction.Up) != null)
                    {
                        (this as Player).OnLeavingRoom(this.Room, Direction.Up);
                        roomLeft = true;
                    }

                    // The player is centered horizontally and going down.
                    else if (newPosY > Layout.RoomHeight - this.Size.Height && this.Room.Neighbours.GetRoomToThe(Direction.Down) != null)
                    {
                        (this as Player).OnLeavingRoom(this.Room, Direction.Down);
                        roomLeft = true;
                    }
                }

                // Checking if the player is centered vertically
                if (this.IsCenteredVertically(newPosY))
                {
                    // The player is centered vertically and moving to the right.
                    if (newPosX < 0 && this.Room.Neighbours.GetRoomToThe(Direction.Left) != null)
                    {
                        (this as Player).OnLeavingRoom(this.Room, Direction.Left);
                        roomLeft = true;
                    }

                    // The player is centered vertically and moving to the left.
                    else if (newPosX > Layout.RoomWidth - this.Hitbox.Rectangle.Width && this.Room.Neighbours.GetRoomToThe(Direction.Right) != null)
                    {
                        (this as Player).OnLeavingRoom(this.Room, Direction.Right);
                        roomLeft = true;
                    }
                }
            }

            if (!roomLeft)
            {
                base.DoWallDetection(ref newPosX, ref newPosY);
            }
        }

        /// <summary>
        /// Returns whether this entity is currently centered in the room vertically.
        /// </summary>
        /// <param name="newPosY">The y position of the entity.</param>
        /// <returns>True if the entity is centered.</returns>
        private bool IsCenteredVertically(float newPosY)
        {
            return newPosY >= Math.Floor((double)Layout.RoomHeight / 2) - 1 &&
                newPosY + this.Hitbox.RelativeHitbox.Height < Math.Ceiling((double)Layout.RoomHeight / 2) - 0.8;
        }

        /// <summary>
        /// Returns whether this entity is currently centered in the room horizontally.
        /// </summary>
        /// <param name="newPosX">The x position of the entity.</param>
        /// <returns>True if the entity is centered.</returns>
        private bool IsCenteredHorizontally(float newPosX)
        {
            return newPosX >= Math.Floor((double)Layout.RoomWidth / 2) &&
                newPosX + this.Hitbox.Rectangle.Width <= Math.Ceiling((double)Layout.RoomWidth / 2);
        }
    }
}