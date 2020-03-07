namespace KnightCrawler.Engine.Models.World.Obstacles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using GalaSoft.MvvmLight;

    /// <summary>
    /// An obstacle found in rooms restricting movement.
    /// </summary>
    public class Obstacle : ObservableObject, IObstacle
    {
        private Rect rectangle;

        public Rect Rectangle { get => this.rectangle; set => this.Set(ref this.rectangle, value); }
    }
}
