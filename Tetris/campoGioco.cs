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
        public bool verifica = false;
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
        public void cleanAll()
        {
            int righe = matrice.GetLength(0);
            int colonne = matrice.GetLength(1);

            for (int x = 0; x < righe; x++)
            {
                for (int y = 0; y < colonne; y++)
                {                    
                        matrice[x, y].BackColor = System.Drawing.Color.Black;
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
        public string traslate(string tile, string r, int xCampo, int yCampo)
        {
            int indexTile = 0;
            string tmpnewtile = "";
            string newtile = "";
            int xx = 0;
            int yy = 0;
            string[] newTile1 = new string[1];
            string[,] tmp = new string[5, 5];
            string[,] tmp1 = new string[5, 5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (tile[indexTile] == '1')
                        tmp[j, i] = "1";
                    else
                        tmp[j, i] = "0";
                    indexTile++;
                }

            }
            if (r == "left")
            {
                indexTile = 0;
                for (int x = 4; x > -1; x--)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        if (tmp[x, y] == "1")
                        {
                            tmp1[xx, yy] = tmp[x, y];
                        }
                        else
                        {
                            tmp1[xx, yy] = "0";
                        }
                        indexTile++;
                        xx++;
                    }
                    yy++;
                    xx = 0;
                }
            }            
            for (int i = 0; i < tmp.GetLength(0); i++)
            {
                for (int j = 0; j < tmp.GetLength(0); j++)
                {
                    tmpnewtile = tmp1[j, i];
                    newtile += tmpnewtile;
                }
            }
            //se supera il check allora torna la nuova forma altrimenti ritorna la stessa
            if (check(xCampo, yCampo, newtile))
                return tile;
            else
                return newtile;
        }

        public bool check(int x, int y, string tile)
        {
            int indexTile = 0;
            bool verificadown = false;
            for (int yy = 0; yy <= 4; yy++)
            {
                for (int xx = 0; xx <= 4; xx++)
                {
                    if (tile[indexTile] == '1')
                    {                        
                        try
                        {
                            if (matrice[x + xx, y + yy].Name == "2" || matrice[x + xx, y + yy].Name == "3" && verificadown == false)
                            {
                                verificadown = true;
                            }
                        }
                        catch
                        {
                            verificadown = true;
                        }                                                

                    }
                    indexTile++;
                }
            }
            return verificadown;
        }       

        public bool checkSide(int x, int y, string tile, string r)
        {            
                       
            int indexTile = 0;
            bool verifica = false;
            for (int yy = 0; yy <= 4; yy++)
            {
                for (int xx = 0; xx <= 4; xx++)
                {
                    if (tile[indexTile] == '1')
                    {
                        try
                        {
                            if (matrice[x + xx, y+yy].Name == "2" || matrice[x + xx, y+yy].Name == "3")
                            {
                                verifica = true;
                            }
                        }
                        catch
                        {

                            verifica = true;
                        }                        
                    }
                    indexTile++;
                }

            }
            return verifica;
        }


        public void istance(int x, int y, string tile)
        {
            int indexTile = 0;
            for (int yy = 0; yy <= 4; yy++)
            {
                for (int xx = 0; xx <= 4; xx++)
                {
                    try
                    {
                        if (tile[indexTile] == '1' && matrice[x + xx, y + yy].Name != "2")
                        {
                            matrice[x + xx, y + yy].BackColor = System.Drawing.Color.Red;
                            matrice[x + xx, y + yy].Name = "1";
                        }
                    }
                    catch { }
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
                    if (matrice[x, y].Name == "1")
                    {
                        matrice[x, y].Name = "2";
                    }
                }
            }
        }

        public void aggiungiSu(int righe)
        {
            for (int w = 0; w < righe; w++)
            {
                for (int i = 0; i < matrice.GetLength(1) - 1; i++)
                {
                    for (int j = 0; j < matrice.GetLength(0); j++)
                    {
                        if (matrice[j, i + 1].Name == "2" || matrice[j, i + 1].Name == "1")
                        {
                            matrice[j, i].BackColor = System.Drawing.Color.Red;
                            matrice[j, i].Name = matrice[j, i + 1].Name;
                        }
                        else if (matrice[j, i + 1].Name == "0")
                        {
                            matrice[j, i].BackColor = System.Drawing.Color.Black;
                            matrice[j, i].Name = matrice[j, i + 1].Name;
                        }
                        else if (matrice[j, i + 1].Name == "3")
                        {
                            matrice[j, i].BackColor = System.Drawing.Color.Purple;
                            matrice[j, i].Name = matrice[j, i + 1].Name;
                        }

                        //matrice[j, i].Name = matrice[j, i + 1].Name;
                        //matrice[j, i].BackColor = matrice[j, i+1].BackColor;
                    }
                }
                for (int z = 0; z < matrice.GetLength(0); z++)
                {
                    matrice[z, 17].Name = "3";
                    matrice[z, 17].BackColor = System.Drawing.Color.Purple;
                }
            }

        }

        public void spostaGiu(List<int> lista, int righe)
        {
            foreach (int item in lista)
            {
                for (int i = item; i > 0; i--)
                {
                    for (int j = 0; j < matrice.GetLength(0); j++)
                    {
                        matrice[j, i].Name = matrice[j, i - 1].Name;
                        if (matrice[j, i].Name == "2")
                            matrice[j, i].BackColor = System.Drawing.Color.Red;
                        else if (matrice[j, i].Name != "3")
                            matrice[j, i].BackColor = System.Drawing.Color.Black;
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
            this.Load += new System.EventHandler(this.campoGioco_Load);
            this.ResumeLayout(false);

        }

        public bool convertToMatrix(string s)
        {
            //ritornare valore che dice se la matrice c'è, se non c'è viene stoppata la connessione
            //CIAO
            //SPERO CHE TI SIA ANDATA BENE
            //
            int contatore = 0;
            try
            {
                for (int y = 0; y < matrice.GetLength(1); y++)
                {
                    for (int x = 0; x < matrice.GetLength(0); x++)
                    {
                        if (s[contatore].ToString() == "1" || s[contatore].ToString() == "2")
                        {
                            matrice[x, y].BackColor = System.Drawing.Color.Red;
                            matrice[x, y].Name = "1";
                        }
                        else if(s[contatore].ToString() == "3")
                        {
                            matrice[x, y].BackColor = System.Drawing.Color.Purple;
                            matrice[x, y].Name = "3";
                        }
                        else
                        {
                            matrice[x, y].BackColor = System.Drawing.Color.Black;
                            matrice[x, y].Name = "0";
                        }
                            contatore++;
                    }
                    //contatore++;              
                }
                return true;
            }
            catch
            {
               // MessageBox.Show("Player is disconnected from the server");
                return false;
            }


        }

        public int checkFullLine(out List<int> lista){
            int tmp =0;
            int rigaCompleta = 0;
            lista = new List<int>();
            for (int i = 0; i < matrice.GetLength(1); i++)
            {
                for (int j = 0; j < matrice.GetLength(0); j++)
                {
                    if (matrice[j, i].Name == "2")
                    {
                        tmp++;
                    }                  
                }
                if (tmp == 11)
                {
                    rigaCompleta++;
                    lista.Add(i);
                }
                tmp = 0;
            }
            return rigaCompleta;
        }
        private void campoGioco_Load(object sender, EventArgs e)
        {

        }
    }
}
