// <copyright file="Utilities.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Data
{
    using System.ComponentModel;

    /// <summary>
    /// The 4 main directions.
    /// </summary>
    public enum Direction
    {
        /// <summary>
        /// The left direction.
        /// </summary>
        Left,

        /// <summary>
        /// The right direction.
        /// </summary>
        Right,

        /// <summary>
        /// The up direction.
        /// </summary>
        Up,

        /// <summary>
        /// The down direction.
        /// </summary>
        Down,
    }

    /// <summary>
    /// Utility functions.
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Reverses the given direction.
        /// </summary>
        /// <param name="direction">The direction to reverse.</param>
        /// <returns>Returns the opposite direction.</returns>
        public static Direction ReverseDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    return Direction.Right;

                case Direction.Right:
                    return Direction.Left;

                case Direction.Up:
                    return Direction.Down;

                case Direction.Down:
                    return Direction.Up;
            }

            throw new InvalidEnumArgumentException("You broke physics and found a new direction!");
        }

        /// <summary>
        /// Returns the number the X coordinate changes when moving 1 unit in the given direction.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <returns>Returns -1 0 or +1.</returns>
        public static short DirectionToXOffset(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                case Direction.Down:
                    return 0;

                case Direction.Right:
                    return 1;

                case Direction.Left:
                    return -1;
            }

            throw new InvalidEnumArgumentException(nameof(direction), (int)direction, typeof(Direction));
        }

        /// <summary>
        /// Returns the number the Y coordinate changes when moving 1 unit in the given direction.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <returns>Returns -1 0 or +1.</returns>
        public static short DirectionToYOffset(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                case Direction.Right:
                    return 0;

                case Direction.Down:
                    return 1;

                case Direction.Up:
                    return -1;
            }

            throw new InvalidEnumArgumentException(nameof(direction), (int)direction, typeof(Direction));
        }
    }
}