// <copyright file="HpBottle.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using KnightCrawler.Data;
    using KnightCrawler.Engine.Models.World;

    /// <summary>
    /// A consumable that heals the player.
    /// </summary>
    public class HpBottle : Consumable
    {
        /// <summary>
        /// The droprate of health bottles.
        /// </summary>
        public const double DropRate = 0.1;

        /// <summary>
        /// Initializes a new instance of the <see cref="HpBottle"/> class.
        /// </summary>
        /// <param name="startingPos">The tile the bottle appears on.</param>
        /// <param name="room">The room the bottle appears in.</param>
        public HpBottle(PointF startingPos, Room room)
            : base(EntityType.HpBottle, startingPos, room, new SizeF(0.7f, 0.7f), new string[] { "hp-bottle", "hp-bottle-1", "hp-bottle-1", "hp-bottle" })
        {
        }

        /// <inheritdoc/>
        protected override bool CanBePickedUp => GameModel.GetInstance().Player.Health < GameModel.GetInstance().Player.Stats.MaxHealth;

        /// <inheritdoc/>
        protected override void PickedUp(Player player)
        {
            // Healing player 1 HP.
            player.Heal(1);
            Sounds.SoundPlayer.PlaySound("hp-bottle-pickup.mp3", Sounds.SoundPlayer.SoundType.SoundEffect);
        }
    }
}
