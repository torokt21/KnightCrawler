// <copyright file="Entity.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Entities
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Windows;
    using KnightCrawler.Data;
    using KnightCrawler.Engine.Models.World;
    using KnightCrawler.Engine.Models.World.Tiles;

    /// <summary>
    /// An entity that can be drawn on the map.
    /// </summary>
    public abstract class Entity : IEntity
    {
        private int currentFrameCounter = 0;
        private FloorTile tileUnderCenter;

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        /// <param name="startingPos">The starting position of the entity.</param>
        /// <param name="type">The type of the entity.</param>
        /// <param name="frames">The frames of the entity's texture.</param>
        /// <param name="room">The room the entity's in.</param>
        /// <param name="size">The size of the enemy hitbox.</param>
        public Entity(EntityType type, PointF startingPos, Room room, SizeF size, string[] frames)
        {
            this.Frames = frames;
            this.Size = size;
            this.Room = room;
            this.EntityType = type;

            this.Hitbox = new Hitbox(this);

            this.Location = new PointF(
                (float)(startingPos.X + 0.5f - (this.Hitbox.RelativeHitbox.Width / 2)),
                (float)(startingPos.Y - this.Hitbox.RelativeHitbox.Top + (0.5f - (this.Hitbox.RelativeHitbox.Height / 2))));

            this.MoveTo(this.Location);
        }

        /// <summary>
        /// called, when the player leaves a room.
        /// </summary>
        public event EventHandler CenterTileChanged;

        /// <inheritdoc/>
        public PointF Location { get; set; }

        /// <inheritdoc/>
        public SizeF Size { get; set; }

        /// <summary>
        /// Gets the type of the entity.
        /// </summary>
        public EntityType EntityType { get; }

        /// <inheritdoc/>
        public Hitbox Hitbox { get; set; }

        /// <summary>
        /// Gets the name of the current frame.
        /// </summary>
        public string CurrentFrame { get => this.Frames[this.currentFrameCounter]; }

        /// <summary>
        /// Gets or sets the tile the entity's center is over.
        /// </summary>
        public FloorTile TileUnderCenter
        {
            get => this.tileUnderCenter;
            set
            {
                if (this.tileUnderCenter != value)
                {
                    this.tileUnderCenter = value;
                    this.CenterTileChanged?.Invoke(value, new EventArgs());
                }
            }
        }

        /// <inheritdoc/>
        public Direction LastHorizontalMovement { get; set; }

        /// <summary>
        /// Gets or sets the room the entity is in.
        /// </summary>
        internal Room Room { get; set; }

        /// <summary>
        /// Gets or sets the frame of frames of the entity's animation.
        /// </summary>
        protected string[] Frames { get; set; }

        /// <inheritdoc/>
        public void MoveTo(float x, float y)
        {
            // Storing horizontal movement direction.
            if (x != this.Location.X)
            {
                this.LastHorizontalMovement = (x < this.Location.X) ? Direction.Left : Direction.Right;
            }

            this.Location = new PointF(x, y);
            this.Hitbox.UpdateHitbox();

            if (this.Room != null)
            {
                this.TileUnderCenter = this.Room[(int)this.Hitbox.Center.X, (int)this.Hitbox.Center.Y];
            }
        }

        /// <summary>
        /// Gets called every gametick.
        /// </summary>
        /// <param name="deltaTime">The time elapsed since the last tick.</param>
        public virtual void OnTick(double deltaTime)
        {
            // Calling all tiles the entity is standing on.
            this.Room.TilesNear(this.Hitbox.Rectangle)
                .Where(t => this.Hitbox.Rectangle.IntersectsWith(t.Rectangle))
                .ToList()
                .ForEach(t => t.SteppedOn(this, deltaTime));

            // Getting nearby enemies, and handling collisions.
            this.Room.EntitiesNear(this)
                .Where(e => e.Hitbox.Rectangle.IntersectsWith(this.Hitbox.Rectangle))
                .ToList()
                .ForEach(e => this.CollideWith(e));
        }

        /// <inheritdoc/>
        public void MoveTo(PointF location) => this.MoveTo(location.X, location.Y);

        /// <inheritdoc/>
        public void NextFrame()
        {
            this.currentFrameCounter = this.Frames.Length == 1 ? 0 : (this.currentFrameCounter + 1) % (this.Frames.Length - 1);
        }

        /// <inheritdoc/>
        public void MoveDirectional(int dx, int dy, float speed, double deltaTime, bool hitTest)
        {
            float realSpeed = SpeedFromDirection(dx, dy, speed) * (float)deltaTime;

            // Calculating the target position
            float newPosX = this.Location.X + (dx * realSpeed);
            float newPosY = this.Location.Y + (dy * realSpeed);

            Room room = this.Room;
            this.DoWallDetection(ref newPosX, ref newPosY);

            if (this.Room != room)
            {
                return;
            }

            if (hitTest)
            {
                Rect hitboxRect = new Rect(newPosX + this.Hitbox.RelativeHitbox.Left, newPosY + this.Hitbox.RelativeHitbox.Top, this.Hitbox.RelativeHitbox.Width, this.Hitbox.RelativeHitbox.Height);

                // Calculating the new positions's hitbox center, to find nearby obstacles.
                PointF centerPoint = new PointF(
                    (float)(hitboxRect.Left + (hitboxRect.Width / 2)),
                    (float)(hitboxRect.Top + (hitboxRect.Height / 2)));

                // Hittesting with nearby obstacles
                if (this.Room.TilesNear(this.Hitbox.Rectangle).Any(t => t.BlocksEntities && hitboxRect.IntersectsWith(t.Rectangle)))
                {
                    // If moving diagonally, we split the movement into two slower movements.
                    if (dx != 0 && dy != 0)
                    {
                        this.MoveDirectional(dx, 0, realSpeed, 1, true);
                        this.MoveDirectional(0, dy, realSpeed, 1, true);
                    }

                    return;
                }
            }

            this.MoveTo(newPosX, newPosY);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.EntityType.ToString();
        }

        /// <summary>
        /// Called every tick the entity collides with an entity.
        /// </summary>
        /// <param name="entity">The entity this enity collided with.</param>
        public virtual void CollideWith(Entity entity)
        {
        }

        /// <summary>
        /// Takes care of wall detection, and leaving the room.
        /// </summary>
        /// <param name="newPosX">The reference of the new x position.</param>
        /// <param name="newPosY">The reference of the new y position.</param>
        protected virtual void DoWallDetection(ref float newPosX, ref float newPosY)
        {
            newPosY = Math.Min(Math.Max((float)-this.Hitbox.RelativeHitbox.Top, newPosY), Layout.RoomHeight - this.Size.Height);
            newPosX = Math.Min(Math.Max(0, newPosX), Layout.RoomWidth - this.Size.Width);
        }

        /// <summary>
        /// Scales to speed to diagonal speed.
        /// </summary>
        /// <param name="dx">Horizontal movement (-1; 0; 1).</param>
        /// <param name="dy">Vertical movement (-1; 0; 1).</param>
        /// <param name="speed">The speed.</param>
        /// <returns>Returns a scaled speed.</returns>
        private static float SpeedFromDirection(int dx, int dy, float speed)
        {
            if (dx != 0 && dy != 0)
            {
                return speed * (float)Math.Sin(Math.PI / 4);
            }

            return speed;
        }
    }
}