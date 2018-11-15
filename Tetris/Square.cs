using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Square : UserControl
    {
        public Square(int size)
        {
            InitializeComponent(size);            
        }
    }
}
