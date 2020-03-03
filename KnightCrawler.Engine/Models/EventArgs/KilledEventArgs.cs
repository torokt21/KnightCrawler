namespace KnightCrawler.Engine.Models.EventArgs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Contains the additional data for the death of an entity.
    /// </summary>
    public class KilledEventArgs : System.EventArgs
    {
        /// <summary>
        /// Gets or sets the object the entity was killed by.
        /// </summary>
        public object KilledBy { get; set; }
    }
}
