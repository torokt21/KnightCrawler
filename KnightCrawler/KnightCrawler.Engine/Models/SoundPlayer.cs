// <copyright file="SoundPlayer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Engine.Sounds
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Windows.Media;

    /// <summary>
    /// The sound player.
    /// </summary>
    public static class SoundPlayer
    {
        private const bool MuteMusic = false;

        /// <summary>
        /// The types of sounds in the game.
        /// </summary>
        public enum SoundType
        {
            /// <summary>
            /// Music.
            /// </summary>
            Music,

            /// <summary>
            /// General sound effect.
            /// </summary>
            SoundEffect,
        }

        /// <summary>
        /// Gets the list of sounds currently playing.
        /// </summary>
        private static List<PlayingSoundInfo> PlayingSounds { get; } = new List<PlayingSoundInfo>();

        private static Dictionary<string, PlayingSoundInfo> StoredPlayers { get; } = new Dictionary<string, PlayingSoundInfo>();

        /// <summary>
        /// Plays a specified sound file.
        /// </summary>
        /// <param name="filename">The name of the file.</param>
        /// <param name="type">The type of sound.</param>
        /// <param name="loop">If true, the song will loop.</param>
        public static void PlaySound(string filename, SoundType type, bool loop = false)
        {
            string folder = default;
            switch (type)
            {
                case SoundType.Music:
                    if (MuteMusic && Debugger.IsAttached)
                    {
                        return;
                    }

                    folder = "Sounds/Music/";
                    break;

                case SoundType.SoundEffect:
                    folder = "Sounds/SoundEffects/";
                    break;
            }

            string path = Path.Combine(folder, filename);

            PlayingSoundInfo info;

            if (StoredPlayers.ContainsKey(path))
            {
                info = (PlayingSoundInfo)StoredPlayers[path].Clone();
            }
            else
            {
                var newInfo = new PlayingSoundInfo(path, type);
                StoredPlayers.Add(path, newInfo);
                info = (PlayingSoundInfo)newInfo.Clone();
            }

            PlayingSounds.Add(info);

            if (loop)
            {
                info.Player.MediaEnded += Player_MediaEndedLoop;
            }
            else
            {
                info.Player.MediaEnded += Player_MediaEnded;
            }

            info.Player.Play();
        }

        /// <summary>
        /// Stops all sounds.
        /// </summary>
        public static void StopAll()
        {
            PlayingSounds.ForEach(p => p.Player.Stop());
            PlayingSounds.Clear();
        }

        private static void Player_MediaEnded(object sender, EventArgs e)
        {
            PlayingSounds.Remove(PlayingSounds.First(p => p.Player.Source == (sender as MediaPlayer).Source));
        }

        private static void Player_MediaEndedLoop(object sender, System.EventArgs e)
        {
            MediaPlayer player = sender as MediaPlayer;
            if (player == null)
            {
                return;
            }

            player.Position = new TimeSpan(0);
            player.Play();
        }

        /// <summary>
        /// The basic info of a sound that is played.
        /// </summary>
        public class PlayingSoundInfo : ICloneable
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="PlayingSoundInfo"/> class.
            /// </summary>
            /// <param name="path">The path of the sound file.</param>
            /// <param name="type">The type of the sound.</param>
            public PlayingSoundInfo(string path, SoundType type)
            {
                this.Path = path;
                this.Type = type;

                this.Player = new MediaPlayer();
                this.Player.Open(new System.Uri(path, UriKind.Relative));
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="PlayingSoundInfo"/> class.
            /// </summary>
            public PlayingSoundInfo()
            {
            }

            /// <summary>
            /// Gets or sets the type of the sound.
            /// </summary>
            public SoundType Type { get; set; }

            /// <summary>
            /// Gets or sets the media player that plays the sound.
            /// </summary>
            public MediaPlayer Player { get; set; }

            /// <summary>
            /// Gets or sets tHe path to the sound file.
            /// </summary>
            public string Path { get; set; }

            /// <inheritdoc/>
            public object Clone()
            {
                MediaPlayer player = (MediaPlayer)this.Player.Clone();
                player.Position = TimeSpan.FromSeconds(0);
                return new PlayingSoundInfo()
                {
                    Path = this.Path,
                    Player = player,
                    Type = this.Type,
                };
            }
        }
    }
}