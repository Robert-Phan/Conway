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
            SetDimensions(25, 25);
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

        public void SetDimensions(int width, int height )
        {
            if (width <= 8 || height <= 8 )
            {
                throw new Exception("No dimensions value under 8 are allowed.");
            }
            Game.SetBoardSize(width, height);

            var grid = new TableLayoutPanel();
            grid.Size = new Size(21 * width, 21 * height);
            grid.Anchor = AnchorStyles.None;
            // grid.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

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
                    var t = new Label() {BackColor = Color.Black, Size = new Size(16, 16),
                    Anchor = (AnchorStyles.None|AnchorStyles.None)};
                    grid.Controls.Add(t, i, j);
                }
            }

            this.ClientSize = new Size((int) (21.75 * width),(int) (21.75 * height));
            var center = new TableLayoutPanel() {Dock = DockStyle.Fill, 
            ColumnCount = 1, RowCount = 1};
            center.Controls.Add(grid);
            Controls.Add(center);
        }

    }
}

