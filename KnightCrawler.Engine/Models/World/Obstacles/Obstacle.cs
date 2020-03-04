namespace KnightCrawler.Engine.Models.World.Obstacles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    class Obstacle : IObstacle
    {
        private Rect rectangle;

        public Rect Rectangle { get => rectangle; set => rectangle = value; }
    }
}
