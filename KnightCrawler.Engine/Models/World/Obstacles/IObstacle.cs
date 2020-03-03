namespace KnightCrawler.Engine.Models.World.Obstacles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    public interface IObstacle
    {
        Rect Rectangle { get; set; }
    }
}
