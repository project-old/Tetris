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
    public partial class nextBlock : UserControl
    {
        public Square[,] matrice;
        public int size = 30;

        public nextBlock()
        {
            matrice = new Square[4, 4];
            InitializeComponent();
            addSquares();
        }        

        void addSquares()
        {
            int righe = matrice.GetLength(0);
            int colonne = matrice.GetLength(1);

            for (int x = 0; x < righe; x++)
            {
                for (int y = 0; y < colonne; y++)
                {
                    Square quadratino = new Square(size);
                    quadratino.Location = new Point(1 + (size * x) + (1 * x), 1 + (size * y) + (1 * y));
                    quadratino.Parent = this;
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // nextBlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Name = "campoGioco";
            this.Size = new System.Drawing.Size(65, 65);
            this.ResumeLayout(false);
        }
    }
}
