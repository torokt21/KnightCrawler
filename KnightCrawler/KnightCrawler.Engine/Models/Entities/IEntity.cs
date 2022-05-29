// <copyright file="IEntity.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Entities
{
    using System.Drawing;
    using KnightCrawler.Data;

    /// <summary>
    /// An entity which has a location on the map.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets the location of the entity.
        /// </summary>
        PointF Location { get; set; }

        /// <summary>
        /// Gets or sets the size of the entity.
        /// </summary>
        SizeF Size { get; set; }

        /// <summary>
        /// Gets the entity's hitbox.
        /// </summary>
        /// <returns>The hitbox rectangle.</returns>
        Hitbox Hitbox { get; }

        /// <summary>
        /// Gets or sets the direction of the last horizontal movement. Used to flip the texture when moving left.
        /// </summary>
        Direction LastHorizontalMovement { get; set; }

        /// <summary>
        /// Moves to the next frame.
        /// </summary>
        void NextFrame();

        /// <summary>
        /// Sets the entity's location to the provided x and y coordinates (1.0f = 1 tile).
        /// </summary>
        /// <param name="location">The location.</param>
        void MoveTo(PointF location);

        /// <summary>
        /// Moves the entity to the specified location (1.0f = 1 tile).
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        void MoveTo(float x, float y);

        /// <summary>
        /// Moves the entity in the given direction.
        /// </summary>
        /// <param name="dx">The movement's direction on the x axis (-1; 0; +1).</param>
        /// <param name="dy">The movement's direction on the y axis (-1; 0; +1).</param>
        /// <param name="speed">The speed of the movement.</param>
        /// <param name="deltaTime">The delta time parameter.</param>
        /// <param name="hitTest">If true, checks for collisions with tiles that restrict movement.</param>
        void MoveDirectional(int dx, int dy, float speed, double deltaTime, bool hitTest);
    }
}