// <copyright file="JsonLayoutRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.Repository
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using KnightCrawler.Data;
    using Newtonsoft.Json;

    /// <summary>
    /// A repository that handles the layouts in json formatted files.
    /// </summary>
    public class JsonLayoutRepository : ILayoutRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonLayoutRepository"/> class.
        /// </summary>
        public JsonLayoutRepository()
        {
            this.Directory = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Layouts");

            // Making sure the directory exists.
            if (!System.IO.Directory.Exists(this.Directory))
            {
                System.IO.Directory.CreateDirectory(this.Directory);
            }

            this.Directory = this.Directory;
        }

        /// <summary>
        /// Gets or sets the directory the layouts are stored in.
        /// </summary>
        private string Directory { get; set; }

        /// <summary>
        /// Gets or sets stores all the loaded layouts, to save loading times.
        /// </summary>
        private List<Layout> LoadedLayouts { get; set; }

        /// <inheritdoc/>
        public Layout Create(string name)
        {
            Layout newLayout = new Layout(name, 1);

            this.Save(newLayout);

            return newLayout;
        }

        /// <inheritdoc/>
        public List<Layout> GetAllLayouts()
        {
            if (this.LoadedLayouts == null)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(this.Directory);
                FileInfo[] files = dirInfo.GetFiles("*.json");
                this.LoadedLayouts = files.Select(file => this.Load(Path.GetFileNameWithoutExtension(file.FullName))).ToList();
            }

            return this.LoadedLayouts;
        }

        /// <inheritdoc/>
        public Layout Load(string name)
        {
            using (StreamReader reader = new StreamReader(Path.Combine(this.Directory, name + ".json")))
            {
                Layout layout = JsonConvert.DeserializeObject<Layout>(reader.ReadToEnd());
                reader.Close();
                return layout;
            }
        }

        /// <inheritdoc/>
        public void Save(Layout layout)
        {
            using (StreamWriter writer = new StreamWriter(Path.Combine(this.Directory, layout.Name + ".json")))
            {
                writer.Write(JsonConvert.SerializeObject(layout, Formatting.Indented));
                writer.Close();
            }
        }
    }
}