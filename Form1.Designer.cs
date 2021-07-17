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

namespace Conway
{
    class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetDimensions(20, 20);
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
            this.ClientSize = new System.Drawing.Size(300, 400);
            this.Text = "Form1";
        }

        public void SetDimensions(int width, int height )
        {
            if (width <= 8 || height <= 8 )
            {
                throw new Exception("No dimensions value under 8 are allowed.");
            }
            Game.SetBoardSize(width, height);

            var grid = new TableLayoutPanel();
            // grid.Padding = new Padding(0, 0, 0, 20);
            // grid.Anchor = (AnchorStyles.None|AnchorStyles.None);
            grid.Dock = DockStyle.Fill;

            grid.ColumnCount = width;
            for (int i = 0; i < width; i++)
            {
                grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1));
            }

            grid.RowCount = height;
            for (int i = 0; i < height; i++)
            {
                grid.RowStyles.Add(new RowStyle(SizeType.Percent, 1));
            }

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var t = new Label() {BackColor = Color.Black, Size = new Size(20, 20),
                    Anchor = (AnchorStyles.None|AnchorStyles.None)};
                    grid.Controls.Add(t, i, j);
                }
            }

            this.ClientSize = new Size(25 * width, 25 * height);
            Controls.Add(grid);
        }

    }
}

