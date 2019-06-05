using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class StartGame : Form
    {            
        public StartGame()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Construct an image object from a file in the local directory.
            // ... This file must exist in the solution.
            Image image = Image.FromFile("F:/Tecnologie/Tetris/Tetris/img/Tetris-Logo.png");
            // Set the PictureBox image property to this image.
            // ... Then, adjust its height and width properties.
            pictureBox1.Image = image;
            pictureBox1.Height = image.Height;
            pictureBox1.Width = image.Width;
        }       

        private void button1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("No page for rules done yet, add to TODO list", "Tetris", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                SelectMode schermata2 = new SelectMode();
                schermata2.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Insert a valid name", "Tetris");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Game by Sebastian and Leonardo 5AIS 2018/2019", "Tetris", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
