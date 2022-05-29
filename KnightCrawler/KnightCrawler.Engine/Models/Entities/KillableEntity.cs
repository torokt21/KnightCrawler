// <copyright file="KillableEntity.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Entities
{
    using System;
    using System.Drawing;
    using KnightCrawler.Data;
    using KnightCrawler.Engine.Factories;
    using KnightCrawler.Engine.Models.World;

    /// <summary>
    /// Entity with health.
    /// </summary>
    public abstract class KillableEntity : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KillableEntity"/> class.
        /// </summary>
        /// <param name="startingPos">The entity's starting position.</param>
        /// <param name="type">The type this entity is.</param>
        /// <param name="size">The size of the entity texture.</param>
        /// <param name="maxHealth">The max health of the antity.</param>
        /// <param name="room">The room the entity's in.</param>
        /// <param name="frames">The frames of the entity's animation.</param>
        public KillableEntity(EntityType type, Point startingPos, Room room, SizeF size, double maxHealth, string[] frames)
            : base(type, startingPos, room, size, frames)
        {
            this.MaxHealth = maxHealth;
            this.Health = maxHealth;
        }

        /// <summary>
        /// Called when the entity's health reaches 0.
        /// </summary>
        public event EventHandler<Models.EventArgs.DamagedEventArgs> Killed;

        /// <summary>
        /// Called when the entity's health decreases.
        /// </summary>
        public event EventHandler<Models.EventArgs.DamagedEventArgs> Damaged;

        /// <summary>
        /// Called when the entity's health increases.
        /// </summary>
        public event EventHandler Healed;

        /// <summary>
        /// Gets the healthpoints of the entity.
        /// </summary>
        public double Health { get; private set; }

        /// <summary>
        /// Gets the progress of the damage animation. Between 0.0 and 1.0.
        /// </summary>
        public double DamageAnimationProgress { get; private set; } = 1;

        /// <summary>
        /// Gets or sets the maximum healthpoints of the entity.
        /// </summary>
        public virtual double MaxHealth { get; set; }

        /// <summary>
        /// Damages the entity by the given amount.
        /// </summary>
        /// <param name="damageValue">The value of the damage.</param>
        /// <param name="dealer">The dealer of the damage.</param>
        public virtual void Damage(double damageValue, object dealer = null)
        {
            // Updating the entity's health
            this.Health = Math.Max(0, this.Health - damageValue);

            this.OnDamaged(dealer);
            this.DamageAnimationProgress = 0;

            // If the entity died, we call the Killed event
            if (this.Health == 0)
            {
                this.OnKilled(dealer);

                if (this is Enemy)
                {
                    Sounds.SoundPlayer.PlaySound("enemy-dead.wav", Sounds.SoundPlayer.SoundType.SoundEffect);

                    // Spawning HP bottle
                    if (new Random().NextDouble() < HpBottle.DropRate)
                    {
                        this.Room.Spawn(new HpBottle(this.Location, this.Room));
                    }
                }
                else if (this is Player)
                {
                    Sounds.SoundPlayer.PlaySound("player-death.mp3", Sounds.SoundPlayer.SoundType.SoundEffect);
                }
            }
        }

        /// <summary>
        /// Heals the entity the given amount.
        /// </summary>
        /// <param name="amount">The amount to heal.</param>
        public virtual void Heal(double amount)
        {
            this.Health = Math.Min(this.MaxHealth, this.Health + amount);
            this.Healed?.Invoke(this, new EventArgs());
        }

        /// <inheritdoc/>
        public override void OnTick(double deltaTime)
        {
            this.DamageAnimationProgress = Math.Min(1, this.DamageAnimationProgress + deltaTime);
            base.OnTick(deltaTime);
        }

        private void OnKilled(object killedBy)
        {
            this.Killed?.Invoke(this, new Models.EventArgs.DamagedEventArgs(this, killedBy));
        }

        private void OnDamaged(object damagedBy)
        {
            this.Damaged?.Invoke(this, new Models.EventArgs.DamagedEventArgs(this, damagedBy));
        }
    }
}