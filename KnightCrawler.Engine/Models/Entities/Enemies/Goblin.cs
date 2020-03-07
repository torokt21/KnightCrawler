using KnightCrawler.Engine.Models.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightCrawler.Engine.Models.Entities.Enemies
{
    public class Goblin : Enemy
    {
        public Goblin(IEnemyMovementBehaviour movementBehaviour, IEnemyBehaviour attackBehaviour)
            : base(movementBehaviour, attackBehaviour)
        {
            
        }
    }
}
