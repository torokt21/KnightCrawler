namespace KnightCrawler.Engine.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using KnightCrawler.Engine.Models.World;

    /// <summary>
    /// The floor factory, creating instances of the floor class.
    /// </summary>
    internal static class FloorFactory
    {
        /// <summary>
        /// Generates a floor.
        /// </summary>
        /// <returns>Return a new instance of a floor.</returns>
        public static Floor GenerateFloor()
        {
            Floor floor = new Floor();

            // TODO generate floor - here or in constructor?

            return floor;
        }
    }
}
