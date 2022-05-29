namespace KnightCrawler.LevelEditor
{
    using KnightCrawler.Data;
    using KnightCrawler.Repository;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    public partial class RoomEditorWindow : Form
    {
        private readonly ILayoutRepository repo;

        private Layout RoomLayout { get; set; }

        public RoomEditorWindow(ILayoutRepository repo, Layout layout)
        {
            InitializeComponent();

            this.repo = repo;
            this.RoomLayout = layout;
            this.roomNameLabel.Text = layout.Name;
            this.DifficultyNumbericUpdown.Value = layout.Difficulty;

            // Adding tiles
            RadioButton[] tileRadios = Enum.GetNames(typeof(FloorTileType))
                .Select(t => new RadioButton()
                {
                    Dock = DockStyle.Top,
                    Text = t,
                    BackColor = this.ColorByTile((FloorTileType)Enum.Parse(typeof(FloorTileType), t)), // I know this is ugly, but the program is only for us
                    Checked = t == Enum.GetName(typeof(FloorTileType), FloorTileType.Empty),
                }).ToArray();

            tileSelectPanel.Controls
                .AddRange(tileRadios);

            // Adding enemies
            enemySelectPanel.Controls
                .AddRange(Enum.GetNames(typeof(EntityType))
                .ToList()
                .Where(e => e.ToLower() != "player")
                .OrderBy(e => e)
                .Select(t => new RadioButton()
                {
                    Dock = DockStyle.Top,
                    Text = t,
                    Checked = t == Enum.GetName(typeof(EntityType), EntityType.Goblin),
                })
                .ToArray());

            enemySelectPanel.Controls.Add(new RadioButton() { Dock = DockStyle.Top, Text = "Remove" });

            RefreshLayout();
        }

        private void tileRadioButton_MouseUp(object sender, MouseEventArgs e)
        {
            // Getting the tile coordinates from the button tag
            int[] spl = (sender as Control).Tag.ToString()
                .Split(';')
                .Select(n => Convert.ToInt32(n))
                .ToArray();
            int x = spl[0];
            int y = spl[1];

            if (e.Button == MouseButtons.Left)
            {
                RadioButton selected = tileSelectPanel.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
                this.RoomLayout.Tiles[x, y] = (FloorTileType)Enum.Parse(typeof(FloorTileType), (selected as Control).Text);
                RefreshLayout();
            }
            else if (e.Button == MouseButtons.Right)
            {
                RadioButton selected = enemySelectPanel.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);

                EntityStartingInfo startingInfoThere = this.RoomLayout.EntityStartInfos
                    .FirstOrDefault(p => p.StartingPosition.Equals(new Point(x, y)));
                if (startingInfoThere != null || selected.Text == "Remove")
                {
                    this.RoomLayout.EntityStartInfos.Remove(startingInfoThere);
                    RefreshLayout();
                    if (selected.Text == "Remove")
                    {
                        return;
                    }
                }

                EntityType type = (EntityType)Enum.Parse(typeof(EntityType), (selected as Control).Text);
                EntityStartingInfo startingInfo = new EntityStartingInfo(new Point(x, y), type);
                this.RoomLayout.EntityStartInfos.Add(startingInfo);
                RefreshLayout();
            }
        }

        public void RefreshLayout()
        {
            int tileButtonSize = Convert.ToInt32(Math.Min(roomPanel.Width / Data.Layout.RoomWidth, roomPanel.Height / Data.Layout.RoomHeight));

            roomPanel.Controls.Clear();

            for (int y = 0; y < Data.Layout.RoomHeight; y++)
            {
                for (int x = 0; x < Data.Layout.RoomWidth; x++)
                {
                    EntityStartingInfo entityThere = this.RoomLayout.EntityStartInfos.FirstOrDefault(e => e.StartingPosition.Equals(new Point(x, y)));

                    Button tileButton = new Button()
                    {
                        Width = tileButtonSize,
                        Height = tileButtonSize,
                        Location = new Point(x * tileButtonSize, y * tileButtonSize),
                        Tag = $"{x};{y}",
                        BackColor = this.ColorByTile(this.RoomLayout.Tiles[x, y]),
                        Text = entityThere == default ? "" : Enum.GetName(typeof(EntityType), entityThere.Type),
                    };

                    roomPanel.Controls.Add(tileButton);
                    tileButton.MouseUp += tileRadioButton_MouseUp;
                }
            }
        }

        private Color ColorByTile(FloorTileType type)
        {
            switch (type)
            {
                default:
                case FloorTileType.Empty:
                    return Color.FromArgb(44, 44, 44);

                case FloorTileType.Obstacle:
                    return Color.Brown;

                case FloorTileType.Chest:
                    return Color.Gold;

                case FloorTileType.Spike:
                    return Color.Gray;

                case FloorTileType.Exit:
                    return Color.SandyBrown;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            this.RoomLayout.Difficulty = Convert.ToInt32(this.DifficultyNumbericUpdown.Value);
            repo.Save(this.RoomLayout);
            System.Media.SystemSounds.Exclamation.Play();
        }

        private void roomPanel_Resize(object sender, EventArgs e)
        {
            RefreshLayout();
        }
    }
}