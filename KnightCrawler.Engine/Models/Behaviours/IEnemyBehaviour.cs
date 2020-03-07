using KnightCrawler.Engine.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightCrawler.Engine.Models.Behaviours
{
    public interface IEnemyBehaviour
    {
        /// <summary>
        /// Defines what the enemy does each gametick.
        /// </summary>
        void OnTick();
    }
}
