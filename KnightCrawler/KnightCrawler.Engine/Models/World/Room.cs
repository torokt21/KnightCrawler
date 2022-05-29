// <copyright file="Room.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.World
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using KnightCrawler.Data;
    using KnightCrawler.Engine.Factories;
    using KnightCrawler.Engine.Models.Entities;
    using KnightCrawler.Engine.Models.World.Tiles;

    /// <summary>
    /// The game room object.
    /// </summary>
    public partial class Room
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Room"/> class.
        /// </summary>
        /// <param name="layout">The information about the room layout.</param>
        /// <param name="offset">The offset of the room from the starting room.</param>
        public Room(Layout layout, System.Drawing.Point offset)
        {
            // Storing room info
            this.Layout = layout;
            this.Offset = offset;

            // Building tiles.
            this.Tiles = new FloorTile[Layout.RoomWidth, Layout.RoomHeight];
            for (int y = 0; y < Layout.RoomHeight; y++)
            {
                for (int x = 0; x < Layout.RoomWidth; x++)
                {
                    this.Tiles[x, y] = TileFactory.CreateInstance(layout.Tiles[x, y], x, y);
                }
            }

            // Spawning entities
            layout.EntityStartInfos
                .Select(e => EntityFactory.GetInstance(e, this))
                .ToList()
                    .ForEach(a =>
                    {
                        this.Spawn(a);
                    });

            this.Type = layout.Type;
        }

        /// <summary>
        /// Gets the initial layout info of the room.
        /// </summary>
        public Layout Layout { get; }

        /// <summary>
        /// Gets or sets the room type.
        /// </summary>
        public RoomType Type { get; set; }

        /// <summary>
        /// Gets a value indicating whether the room has any enemies left.
        /// </summary>
        public bool IsCleared => this.Entities.OfType<Enemy>().Count() == 0 && this.IsDiscovered;

        /// <summary>
        /// Gets a value indicating whether the room is visible on the minimap.
        /// </summary>
        public bool IsVisibleOnMinimap => this.IsDiscovered || this.Neighbours.GetAll().Any(r => r.IsDiscovered);

        /// <summary>
        /// Gets or sets a value indicating whether the player has ever entered the room.
        /// </summary>
        public bool IsDiscovered { get; set; }

        /// <summary>
        /// Gets the list of entities in the room.
        /// </summary>
        public List<Entity> Entities { get; } = new List<Entity>();

        /// <summary>
        /// Gets the tiles in the room.
        /// </summary>
        public FloorTile[,] Tiles { get; private set; }

        /// <summary>
        /// Gets or sets the neigbours of the room.
        /// </summary>
        public RoomNeigbours Neighbours { get; set; } = new RoomNeigbours();

        /// <summary>
        /// Gets or sets the offset of this room from the starting room.
        /// </summary>
        public System.Drawing.Point Offset { get; set; }

        /// <summary>
        /// Gets or sets the tile in the specified index of the room.
        /// </summary>
        /// <param name="y">The y location.</param>
        /// <param name="x">The x location.</param>
        /// <returns>The floor tile.</returns>
        public FloorTile this[int y, int x]
        {
            get => this.Tiles[y, x];
            set => this.Tiles[y, x] = value;
        }

        /// <summary>
        /// Spawns the entity in the room. Must be called.
        /// </summary>
        /// <param name="entity">The entity to spawn.</param>
        public void Spawn(Entity entity)
        {
            // If the player is entering the room, we mark the room as discovered.
            if (entity is Player)
            {
                this.IsDiscovered = true;
            }
            else if (entity is KillableEntity)
            {
                (entity as KillableEntity).Killed += this.Entity_Killed;
            }
            else if (entity is Consumable)
            {
                (entity as Consumable).OnPickedUp += this.Consumable_PickedUp;
            }

            entity.Room = this;
            this.Entities.Add(entity);
        }

        /// <summary>
        /// Gets the list of tiles that are close to the fiven point.
        /// </summary>
        /// <param name="rectangle">The rectangle to search around.</param>
        /// <param name="radius">The radius of the search.</param>
        /// <returns>List of tiles.</returns>
        public List<FloorTile> TilesNear(Rect rectangle, float radius = 1)
        {
            List<FloorTile> tiles = new List<FloorTile>();

            int xMin = Convert.ToInt32(Math.Max(0, Math.Floor(rectangle.Left) - radius));
            int xMax = Convert.ToInt32(Math.Min(Math.Ceiling(rectangle.Right) + radius, Layout.RoomWidth));

            int yMin = Convert.ToInt32(Math.Max(0, Math.Floor(rectangle.Top) - radius));
            int yMax = Convert.ToInt32(Math.Min(Math.Ceiling(rectangle.Bottom) + radius, Layout.RoomHeight));

            for (int y = yMin; y < yMax; y++)
            {
                for (int x = xMin; x < xMax; x++)
                {
                    tiles.Add(this.Tiles[x, y]);
                }
            }

            return tiles;
        }

        /// <summary>
        /// Gets the entities near the given rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle to search around.</param>
        /// <param name="radius">The radius of the search.</param>
        /// <returns>Returns the list of entities.</returns>
        public List<Entity> EntitiesNear(Rect rectangle, float radius = 1)
        {
            double xMin = rectangle.Left - radius;
            double yMin = rectangle.Top - radius;

            double xMax = rectangle.Right + radius;
            double yMax = rectangle.Bottom + radius;

            // Growing the original rectangle by the radius.
            Rect bigRect = new Rect(xMin, yMin, xMax, yMax);

            var l = this.Entities.Where(e => e.Hitbox.Rectangle.IntersectsWith(bigRect)).ToList();

            return l;
        }

        /// <summary>
        /// Gets the list of entities near an entity.
        /// </summary>
        /// <param name="entity">The entity to searchg around.</param>
        /// <param name="radius">The radius around the entity hitbox.</param>
        /// <returns>List of entities.</returns>
        public List<Entity> EntitiesNear(Entity entity, float radius = 1) => this.EntitiesNear(entity.Hitbox.Rectangle, radius);

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Room \"{this.Layout.Name}\"" + (this.IsCleared ? " - cleared" : string.Empty);
        }

        private void Entity_Killed(object sender, Models.EventArgs.DamagedEventArgs e)
        {
            GameModel.GetInstance().Player.Stats.AddScore(100);
            KillableEntity entity = sender as KillableEntity;

            this.Entities.Remove(entity);
        }

        private void Consumable_PickedUp(object sender, EventArgs e)
        {
            this.Entities.Remove(sender as Consumable);
        }
    }
}