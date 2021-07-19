using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Life;

namespace GUI
{
    class GUI : Form
    {
        public GUI()
        {
            InitializeComponent();
            SetDimensions(25, 25);
            Game.CreateBoard(new int[,] {
                {1, 0},
                {2, 0},
                {0, 1},
                {1, 1},
                {2, 2}
            });

            var t = new Timer() {Interval = 1};
            t.Tick += new EventHandler((Object source, EventArgs e) => {
                t.Stop();
                this.Draw(Game.board);
            });
            t.Start();

            var timer = new Timer() {Interval = 100};
            Grid.Click += new EventHandler((Object source, EventArgs e) => {
                timer.Enabled = timer.Enabled ? false : true;
            });
            timer.Tick += new EventHandler((Object source, EventArgs e) => {
                this.Draw(Game.board);
                Game.Update();
            });
        }
        private void MainLoop()
        {
            Game.CreateBoard(new int[,] {
                {1, 0},
                {2, 0},
                {0, 1},
                {1, 1},
                {2, 2}
            });
            var timer = new Timer() {Interval = 250};
            Grid.Click += new EventHandler((Object source, EventArgs e) => {
                timer.Enabled = timer.Enabled ? false : true;
            });
            timer.Tick += new EventHandler((Object source, EventArgs e) => {
                this.Draw(Game.board);
                Game.Update();
            });
        }

        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.LightGray;
            this.ClientSize = new System.Drawing.Size(600, 600);
            this.Text = "Form1";
        }
        TableLayoutPanel Grid = new();
        public void SetDimensions(int width, int height)
        {
            Game.SetBoardSize(width, height);

            Grid.Anchor = AnchorStyles.None;
            // ? The size of the grid is dependant on both
            // ? the size of the cell itself and
            // ? the extra pixels of the border
            Grid.Size = new Size((int)(20 * width) + 1, (int)(20 * height) + 1);
            Grid.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            Grid.ColumnCount = width;
            for (int i = 0; i < width; i++)
            {
                Grid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 19));
            }
            Grid.RowCount = height;
            for (int i = 0; i < height; i++)
            {
                Grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 19));
            }

            this.ClientSize = new Size((int)(23 * width), (int)(23 * height));
            var center = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 1
            };
            center.Controls.Add(Grid);
            Controls.Add(center);
        }
        private void Draw(bool[,] board)
        {
            // Gets empty square
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    // ? The coords account for the size of the cells and pixels
                    int x = j * 20 + 1, y = i * 20 + 1;
                    if (board[i, j])
                    {
                        Grid.CreateGraphics().FillRectangle(new SolidBrush(Color.Black), 
                        x, y, 19, 19);
                    } else {
                        Grid.CreateGraphics().FillRectangle(new SolidBrush(Color.LightGray), 
                        x, y, 19, 19);
                    }
                }
            }
        }
    }
}

