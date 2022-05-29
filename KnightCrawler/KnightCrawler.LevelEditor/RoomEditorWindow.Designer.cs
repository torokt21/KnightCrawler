namespace KnightCrawler.LevelEditor
{
    partial class RoomEditorWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DifficultyNumbericUpdown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.roomNameLabel = new System.Windows.Forms.Label();
            this.roomPanel = new System.Windows.Forms.Panel();
            this.SaveButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.enemySelectPanel = new System.Windows.Forms.Panel();
            this.tileSelectPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DifficultyNumbericUpdown)).BeginInit();
            this.panel1.SuspendLayout();
            this.enemySelectPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // DifficultyNumbericUpdown
            // 
            this.DifficultyNumbericUpdown.Location = new System.Drawing.Point(103, 17);
            this.DifficultyNumbericUpdown.Margin = new System.Windows.Forms.Padding(5);
            this.DifficultyNumbericUpdown.Name = "DifficultyNumbericUpdown";
            this.DifficultyNumbericUpdown.Size = new System.Drawing.Size(147, 27);
            this.DifficultyNumbericUpdown.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "Difficulty:";
            // 
            // roomNameLabel
            // 
            this.roomNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.roomNameLabel.Location = new System.Drawing.Point(384, 17);
            this.roomNameLabel.Name = "roomNameLabel";
            this.roomNameLabel.Size = new System.Drawing.Size(310, 22);
            this.roomNameLabel.TabIndex = 2;
            this.roomNameLabel.Text = "Room name";
            this.roomNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // roomPanel
            // 
            this.roomPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.roomPanel.Location = new System.Drawing.Point(0, 52);
            this.roomPanel.Name = "roomPanel";
            this.roomPanel.Size = new System.Drawing.Size(652, 489);
            this.roomPanel.TabIndex = 3;
            this.roomPanel.SizeChanged += new System.EventHandler(this.roomPanel_Resize);
            // 
            // SaveButton
            // 
            this.SaveButton.ForeColor = System.Drawing.Color.Black;
            this.SaveButton.Location = new System.Drawing.Point(734, 16);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(152, 27);
            this.SaveButton.TabIndex = 4;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.enemySelectPanel);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Location = new System.Drawing.Point(658, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(249, 489);
            this.panel1.TabIndex = 4;
            // 
            // enemySelectPanel
            // 
            this.enemySelectPanel.AutoSize = true;
            this.enemySelectPanel.Controls.Add(this.tileSelectPanel);
            this.enemySelectPanel.Controls.Add(this.label3);
            this.enemySelectPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.enemySelectPanel.Location = new System.Drawing.Point(0, 22);
            this.enemySelectPanel.Name = "enemySelectPanel";
            this.enemySelectPanel.Size = new System.Drawing.Size(249, 22);
            this.enemySelectPanel.TabIndex = 0;
            // 
            // tileSelectPanel
            // 
            this.tileSelectPanel.AutoSize = true;
            this.tileSelectPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tileSelectPanel.Location = new System.Drawing.Point(0, 22);
            this.tileSelectPanel.Name = "tileSelectPanel";
            this.tileSelectPanel.Size = new System.Drawing.Size(249, 0);
            this.tileSelectPanel.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 22);
            this.label3.TabIndex = 0;
            this.label3.Text = "Tiles (left click)";
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(249, 22);
            this.panel3.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.AutoSize = true;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 22);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(249, 0);
            this.panel4.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(171, 22);
            this.label4.TabIndex = 0;
            this.label4.Text = "Enemies (right click)";
            // 
            // RoomEditorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.ClientSize = new System.Drawing.Size(907, 541);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.roomPanel);
            this.Controls.Add(this.roomNameLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DifficultyNumbericUpdown);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "RoomEditorWindow";
            this.Text = "Level Editor - Room";
            ((System.ComponentModel.ISupportInitialize)(this.DifficultyNumbericUpdown)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.enemySelectPanel.ResumeLayout(false);
            this.enemySelectPanel.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown DifficultyNumbericUpdown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label roomNameLabel;
        private System.Windows.Forms.Panel roomPanel;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel enemySelectPanel;
        private System.Windows.Forms.Panel tileSelectPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label4;
    }
}