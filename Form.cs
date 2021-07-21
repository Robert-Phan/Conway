using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using Life;

namespace GUI
{
    class GUI : Form
    {
        public GUI(int[,] board, int width = 40, int height = 40,int speed = 200, int pixelSize = 15)
        {
            InitializeComponent();
            PixelSize = pixelSize;
            SetDimensions(width, height);
            Game.CreateBoard(board);
            Controls.Add(controlLine);
            MainLoop(speed);
        }
        Timer timer = new();
        TextBox controlLine = new() {Dock = DockStyle.Bottom, AcceptsReturn = true, Multiline = true};
        private void MainLoop(int speed)
        {
            FirstDraw();

            controlLine.KeyPress += new KeyPressEventHandler(ProcessKey);

            timer.Tick += new EventHandler((Object source, EventArgs e) => {
                this.Draw();
                Game.Update();
            });
        }

        #region Pregenerated stuff
        
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
            this.Text = "Form1";
        }
        #endregion
        
        #region Processes keys
        private void ProcessKey(Object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') 
            {
                var input = controlLine.Text;
                if (Regex.Match(input, @"[sS]tart|[rR]un").Success) timer.Enabled = true;
                else if (Regex.Match(input, "[sS]top").Success) timer.Enabled = false;
                else if (Regex.Match(input, @"\d+, *\d+").Success) AddCoords(input);
                else if (Regex.Match(input, @"(\d*(b|o|\$))+").Success) RLE(input);
                controlLine.Text = "";
            }
        }

        private void AddCoords(string text)
        {
            var g = text.Split(',');
            int y = Convert.ToInt16(g[0]), x = Convert.ToInt16(g[1]);
            Game.board[x, y] = true;
            Draw();
        }

        private void RLE(string rle)
        {
            List<int[]> results = new();
            int x = 0, y = 0;

            var g = Regex.Matches(rle, @"\d*(b|o|\$)");
            foreach (Match match in g) 
            {
                string t = match.Value;
                string tag = Regex.Match(t, @"b|o|\$").Value;
                int count = Regex.IsMatch(t, @"\d+") ? 
                Convert.ToInt16(Regex.Match(t, @"\d+").Value) : 1;

                switch (tag)
                {
                    case "b": 
                        x += count;
                        break;
                    case "o":
                        for (int i = 0; i < count; i++)
                        {
                            results.Add(new int[] {x, y});
                            x++;
                        }
                        break;
                    case "$":
                        x = 0;
                        y += count;
                        break;
                }
            }

            foreach (int[] coords in results)
            {
                x = coords[1]; y = coords[0]; 
                Game.board[x, y] = true;
            }
            Draw();
        }

        #endregion

        #region Creates the grid.
        TableLayoutPanel Grid = new();
        int PixelSize;
        public void SetDimensions(int width, int height)
        {
            Game.SetBoardSize(height, width);

            Grid.Anchor = AnchorStyles.None;
            // ? The size of the grid is dependant on both
            // ? the size of the cell itself and
            // ? the extra pixels of the border
            Grid.Size = new Size((int)((PixelSize+1) * width) + 1, (int)((PixelSize+1) * height) + 1);
            Grid.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            Grid.ColumnCount = width;
            for (int i = 0; i < width; i++)
            {
                Grid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, PixelSize));
            }
            Grid.RowCount = height;
            for (int i = 0; i < height; i++)
            {
                Grid.RowStyles.Add(new RowStyle(SizeType.Absolute, PixelSize));
            }
                
            this.ClientSize = new Size((PixelSize+2) * width, (PixelSize+2) * height);
            var center = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 1
            };
            center.Controls.Add(Grid);
            Controls.Add(center);
        }
        #endregion
        
        #region Updates the board
        private void Draw()
        {
            var board = Game.board;
            // Gets empty square
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    // ? The coords account for the size of the cells and pixels
                    int x = j * (PixelSize+1) + 1;
                    int y = i * (PixelSize+1) + 1;
                    if (board[i, j])
                    {
                        Grid.CreateGraphics().FillRectangle(new SolidBrush(Color.Black), 
                        x, y, PixelSize, PixelSize);
                    } else {
                        Grid.CreateGraphics().FillRectangle(new SolidBrush(Color.LightGray), 
                        x, y, PixelSize, PixelSize);
                    }
                }
            }
        }
        private void FirstDraw()
        {
            var t = new Timer() {Interval = 1};
            t.Tick += new EventHandler((Object source, EventArgs e) => {
                t.Stop();
                this.Draw();
            });
            t.Start();
        }
        #endregion
    }
}

