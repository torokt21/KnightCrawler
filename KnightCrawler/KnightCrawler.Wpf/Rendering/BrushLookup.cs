// <copyright file="BrushLookup.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Wpf.Rendering
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Provides an easy way to get image brushes by their name.
    /// </summary>
    public class BrushLookup
    {
        private static readonly Dictionary<string, ImageBrush> SavedBrushes = new Dictionary<string, ImageBrush>();

        /// <summary>
        /// Gets a brush with the given name.
        /// </summary>
        /// <param name="name">The name of the brush to load or get.</param>
        /// <param name="flip">If true, the image will be flipped.</param>
        /// <returns>A brush instance.</returns>
        public static ImageBrush GetBrush(string name, bool flip = false)
        {
            GetBrush(name);

            if (flip)
            {
                return GetBrush(name, new ScaleTransform(-1, 1, SavedBrushes[name].Viewbox.Width / 2, 0));
            }
            else
            {
                return GetBrush(name, null);
            }
        }

        /// <summary>
        /// Gets a brush with the given transform applied to it.
        /// </summary>
        /// <param name="name">The name of the brush.</param>
        /// <param name="transform">The transform.</param>
        /// <returns>Return an imagebrush.</returns>
        public static ImageBrush GetBrush(string name, Transform transform)
        {
            GetBrush(name);

            if (transform != null)
            {
                SavedBrushes[name].RelativeTransform = transform;
            }
            else
            {
                SavedBrushes[name].RelativeTransform = null;
            }

            return SavedBrushes[name];
        }

        private static ImageBrush GetBrush(string name)
        {
            if (!SavedBrushes.ContainsKey(name))
            {
                BitmapImage bmp = new BitmapImage(new Uri($"pack://application:,,,/KnightCrawler.Wpf;component/Images/Assets/{name}.png"));
                ImageBrush ib = new ImageBrush(bmp);
                SavedBrushes.Add(name, ib);
            }

            return SavedBrushes[name];
        }
    }
}