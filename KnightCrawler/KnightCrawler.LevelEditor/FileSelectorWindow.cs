using KnightCrawler.Data;
using KnightCrawler.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace KnightCrawler.LevelEditor
{
    public partial class FileSelectorWindow : Form
    {
        private readonly ILayoutRepository repo;

        private List<Layout> Layouts;

        public Layout Selected { get; private set; }

        public FileSelectorWindow(ILayoutRepository repo)
        {
            InitializeComponent();

            this.repo = repo;

            this.RefreshLayouts();
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            string name = NewRoomTextbox.Text.Trim();
            NewRoomTextbox.Text = "";

            this.repo.Create(name);
            this.RefreshLayouts();
            RoomSelect.SelectedItem = name;
        }

        private void RefreshLayouts()
        {
            this.Layouts = repo.GetAllLayouts();

            RoomSelect.Items.Clear();
            RoomSelect.Items.AddRange(this.Layouts.Select(l => l.Name).ToArray());

            if (RoomSelect.Items.Count > 0)
                RoomSelect.SelectedIndex = 0;
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            Selected = Layouts[RoomSelect.SelectedIndex];
            this.DialogResult = DialogResult.OK;
        }
    }
}