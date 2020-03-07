using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KnightCrawler.Engine.Models.Entities
{
    public class Entity : IEntity
    {
        public Point Location { get ; set; }
        public Size Size { get; set; }
    }
}
