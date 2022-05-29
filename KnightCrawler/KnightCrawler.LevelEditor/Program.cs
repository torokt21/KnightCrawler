// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnightCrawler.LevelEditor
{
    using System;
    using System.Windows.Forms;
    using KnightCrawler.Repository;

    /// <summary>
    /// The main entry point of the program.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            do
            {
                var repo = new JsonLayoutRepository();

                FileSelectorWindow fs = new FileSelectorWindow(repo);

                if (fs.ShowDialog() == DialogResult.OK)
                {
                    RoomEditorWindow win = new RoomEditorWindow(repo, fs.Selected);
                    win.ShowDialog();
                }
                else
                {
                    break;
                }
            }
            while (true);
        }
    }
}