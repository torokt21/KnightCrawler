// <copyright file="Player.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Entities
{
    /// <summary>
    /// The player class.
    /// </summary>
    public class Player : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        public Player(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets the current score of the player.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Gets the name of the current player.
        /// </summary>
        public string Name { get; }
    }
}
