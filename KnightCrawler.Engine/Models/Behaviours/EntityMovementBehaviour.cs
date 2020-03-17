// <copyright file="EntityMovementBehaviour.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Behaviours
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using KnightCrawler.Engine.Models.Entities;
    using KnightCrawler.Engine.Models.World;

    /// <summary>
    /// The abstract base class for movement behaviours.
    /// </summary>
    public abstract class EntityMovementBehaviour : EntityBehaviour
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityMovementBehaviour"/> class.
        /// </summary>
        /// <param name="speed">The speed of the entity.</param>
        /// /// <param name="parentEntity">The reference to the entity.</param>
        public EntityMovementBehaviour(double speed, Entity parentEntity)
            : base(parentEntity)
        {
            this.Speed = speed;
        }

        /// <summary>
        /// Gets or sets the speed of the enemy's movement.
        /// </summary>
        public double Speed { get; set; }
    }
}
