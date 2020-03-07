using KnightCrawler.Engine.Models.Behaviours;
using KnightCrawler.Engine.Models.Entities;
using KnightCrawler.Engine.Models.Entities.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightCrawler.Engine.Factories
{
    static class EnemyFactory
    {
        /// <summary>
        /// Creates an instance of a
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Entity CreateInstance(char c)
        {
            switch (c.ToString().ToLower()[0])
            {
                case 'g':
                    return new Goblin(new AStarPathFinding(), null);
            }

            throw new Exception($"Enemy letter '{c}' not found");
        }
    }
}
