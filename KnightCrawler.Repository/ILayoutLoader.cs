using KnightCrawler.Engine.Models.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightCrawler.Repository
{
    public interface ILayoutLoader
    {
        IList<Layout> GetAllLayouts();
    }
}
