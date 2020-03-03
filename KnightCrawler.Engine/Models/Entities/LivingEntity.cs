namespace KnightCrawler.Engine.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// An entity with health, and the ability to move.
    /// </summary>
    public class LivingEntity : KillableEntity
    {
        private Point location;

        /// <summary>
        /// Gets or sets the location of the entity.
        /// </summary>
        public Point Location { get => this.location; set => this.location = value; }
    }
}
