namespace KnightCrawler.Engine.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// The player class.
    /// </summary>
    public class Player : LivingEntity
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
        /// Gets the name of the current player.
        /// </summary>
        public string Name { get; }
    }
}
