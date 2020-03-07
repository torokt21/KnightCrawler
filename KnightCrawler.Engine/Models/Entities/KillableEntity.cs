namespace KnightCrawler.Engine.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Entity with health.
    /// </summary>
    public abstract class KillableEntity
    {
        /// <summary>
        /// Called when the entity's health reaches 0.
        /// </summary>
        public event EventHandler<Models.EventArgs.KilledEventArgs> Killed;

        /// <summary>
        /// Gets or sets the healthpoints of the entity.
        /// </summary>
        public double Health { get; set; }

        /// <summary>
        /// Damages the entity by the given amount.
        /// </summary>
        /// <param name="damageValue">The value of the damage.</param>
        /// <param name="dealer">The dealer of the damage.</param>
        public void Damage(double damageValue, object dealer = null)
        {
            // Updating the entity's health
            this.Health = Math.Max(0, this.Health - damageValue);

            // If the entity died, we call the Killed event
            if (this.Health == 0)
            {
                this.OnKilled(dealer);
            }
        }

        private void OnKilled(object killedBy)
        {
            this.Killed?.Invoke(this, new Models.EventArgs.KilledEventArgs() { KilledBy = killedBy });
        }
    }
}
