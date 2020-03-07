namespace KnightCrawler.Engine.Models.Behaviours
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the movement behaviour of an enemy.
    /// </summary>
    public interface IEnemyMovementBehaviour : IEnemyBehaviour
    {
        /// <summary>
        /// Gets or sets the movement speed of the enemy.
        /// </summary>
        double Speed { get; set; }
    }
}
