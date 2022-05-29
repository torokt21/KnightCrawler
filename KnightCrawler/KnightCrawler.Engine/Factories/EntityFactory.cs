// <copyright file="EntityFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Factories
{
    using System.Drawing;
    using KnightCrawler.Data;
    using KnightCrawler.Engine.Models.Behaviours;
    using KnightCrawler.Engine.Models.Entities;
    using KnightCrawler.Engine.Models.World;

    /// <summary>
    /// The factory class producing enemy entities.
    /// </summary>
    public static class EntityFactory
    {
        /// <summary>
        /// Creates an instance of an entity base on the letter representing it.
        /// </summary>
        /// <param name="startingInfo">The information required to create the entity.</param>
        /// <param name="room">The room the entity is in.</param>
        /// <returns>New instance.</returns>
        public static Entity GetInstance(EntityStartingInfo startingInfo, Room room)
        {
            switch (startingInfo.Type)
            {
                case EntityType.Goblin:
                    return new Enemy(
                        startingInfo.Type,
                        startingInfo.StartingPosition,
                        room,
                        new SizeF(0.9f, 0.9f),
                        1.8,
                        MovementType.AStarPathfinding,
                        null,
                        new string[] { "goblin_run_anim_f0", "goblin_run_anim_f1", "goblin_run_anim_f2", "goblin_run_anim_f3" },
                        2.4);

                case EntityType.Skeleton:
                    return new Enemy(
                        startingInfo.Type,
                        startingInfo.StartingPosition,
                        room,
                        new SizeF(0.9f, 0.9f),
                        5,
                        MovementType.AStarPathfinding,
                        null,
                        new string[] { "skelet_run_anim_f0", "skelet_run_anim_f1", "skelet_run_anim_f2", "skelet_run_anim_f3" },
                        2);

                case EntityType.Spectre:
                    return new Enemy(
                        startingInfo.Type,
                        startingInfo.StartingPosition,
                        room,
                        new SizeF(0.7f, 0.8f),
                        4,
                        MovementType.Spectral,
                        null,
                        new string[] { "necromancer_run_anim_f0", "necromancer_run_anim_f1", "necromancer_run_anim_f2", "necromancer_run_anim_f3" },
                        1.6);

                case EntityType.Zombie:
                    return new Enemy(
                        startingInfo.Type,
                        startingInfo.StartingPosition,
                        room,
                        new SizeF(0.9f, 0.9f),
                        5,
                        MovementType.AStarPathfinding,
                        null,
                        new string[] { "zombie_run_anim_f0", "zombie_run_anim_f1", "zombie_run_anim_f2", "zombie_run_anim_f3" },
                        1.3);

                case EntityType.Ogre:
                    return new Enemy(
                        startingInfo.Type,
                        startingInfo.StartingPosition,
                        room,
                        new SizeF(1.9f, 1.9f),
                        12,
                        MovementType.Chasing,
                        null,
                        new string[] { "ogre_run_anim_f0", "ogre_run_anim_f1", "ogre_run_anim_f2", "ogre_run_anim_f3" },
                        1);

                case EntityType.MaskedOrc:
                    return new Enemy(
                        startingInfo.Type,
                        startingInfo.StartingPosition,
                        room,
                        new SizeF(0.9f, 0.9f),
                        8,
                        MovementType.Chasing,
                        null,
                        new string[] { "masked_orc_run_anim_f0", "masked_orc_run_anim_f1", "masked_orc_run_anim_f2", "masked_orc_run_anim_f3" },
                        1.7);

                case EntityType.OrcWarrior:
                    return new Enemy(
                        startingInfo.Type,
                        startingInfo.StartingPosition,
                        room,
                        new SizeF(0.9f, 0.9f),
                        5,
                        MovementType.Chasing,
                        null,
                        new string[] { "orc_warrior_run_anim_f0", "orc_warrior_run_anim_f1", "orc_warrior_run_anim_f2", "orc_warrior_run_anim_f3" },
                        1.9);

                case EntityType.Swampy:
                    return new Enemy(
                        startingInfo.Type,
                        startingInfo.StartingPosition,
                        room,
                        new SizeF(0.9f, 0.9f),
                        4,
                        MovementType.Chasing,
                        null,
                        new string[] { "swampy_run_anim_f0", "swampy_run_anim_f1", "swampy_run_anim_f2", "swampy_run_anim_f3" },
                        2.1);

                case EntityType.TinyZombie:
                    return new Enemy(
                        startingInfo.Type,
                        startingInfo.StartingPosition,
                        room,
                        new SizeF(0.8f, 0.8f),
                        1,
                        MovementType.AStarPathfinding,
                        null,
                        new string[] { "tiny_zombie_run_anim_f0", "tiny_zombie_run_anim_f1", "tiny_zombie_run_anim_f2", "tiny_zombie_run_anim_f3" },
                        2.6);

                // Consumable
                case EntityType.HpBottle:
                    return new HpBottle(startingInfo.StartingPosition, room);
            }

            throw new System.Exception("Invalid entity type in factory!");
        }
    }
}