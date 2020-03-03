namespace KnightCrawler.Engine.Models.World
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// The game room object.
    /// </summary>
    public class Room : BaseNotificationClass
    {
        /// <summary>
        /// Gets or sets a value indicating whether the room has been cleared.
        /// </summary>
        public bool IsCleared { get; set; }

        /// <summary>
        /// Loads the layout of the room from file.
        /// </summary>
        public void LoadLayout()
        {
            // TODO load room file
        }
    }
}
