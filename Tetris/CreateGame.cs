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
    public partial class CreateGame : Form
    {
        public string ReturnValue1 { get; set; }
        public bool ReturnValue2 { get; set; }
        public CreateGame()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ReturnValue1 = textBox1.Text;
            this.ReturnValue2 = radioButton1.Checked;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = (this.textBox1.Text != "");
        }
    }
}
