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
    public partial class campoGioco : UserControl
    {
        public Square[,] matrice;
        public Square quadratino;      

        public campoGioco()
        {
            matrice = new Square[11, 18];
            InitializeComponent();
            addSquares();            
        }

        private void addSquares()
        {
            int righe = matrice.GetLength(0);
            int colonne = matrice.GetLength(1);

            for (int x = 0; x < righe; x++)
            {
                for (int y = 0; y < colonne; y++)
                {
                    int size = 18;
                    matrice[x, y] = new Square(size);
                    matrice[x, y].Location = new Point(1 + (size * x) + (1 * x), 1 + (size * y) + (1 * y));
                    matrice[x, y].Parent = this;
                    matrice[x, y].Name = "0";
                }
            }
        }

        public void clean()
        {
            int righe = matrice.GetLength(0);
            int colonne = matrice.GetLength(1);

            for (int x = 0; x < righe; x++)
            {
                for (int y = 0; y < colonne; y++)
                {
                    if (matrice[x, y].Name == "1")
                    {
                        matrice[x, y].BackColor = System.Drawing.Color.Black;
                        matrice[x, y].Name = "0";
                    }
    
                }
            }
        }

        public void istance(int x, int y,string tile)
        {
            int indexTile = 0;       
            for (int yy = 0; yy <= 3; yy++)
            {
                for (int xx = 0; xx <= 3; xx++)
                {
                    if (tile[indexTile] == '1' && matrice[x+xx,y+yy].Name != "2")
                    {
                        matrice[x + xx, y + yy].BackColor = System.Drawing.Color.Red;
                        matrice[x + xx, y + yy].Name = "1";
                    }
                   
                    indexTile++;
                }
               
            }            
        }                

        public void swithcState()
        {
            int righe = matrice.GetLength(0);
            int colonne = matrice.GetLength(1);

            for (int x = 0; x < righe; x++)
            {
                for (int y = 0; y < colonne; y++)
                {
                    if (matrice[x, y].BackColor == System.Drawing.Color.Red)
                    {                        
                        matrice[x, y].Name = "2";
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // campoGioco
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Name = "campoGioco";
            this.Size = new System.Drawing.Size(242, 402);
            this.ResumeLayout(false);
            //this.sparta=!cipolla
        }
    }
}
