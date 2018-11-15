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
    public partial class SelectMode : Form
    {
        //due arrray di oggetti da definire con l'oggetto di ogni giocatore: IP, nome, versione, modalità...

        private List<tipoGiocatore> mod0 = new List<tipoGiocatore>();
        private List<tipoGiocatore> mod1 = new List<tipoGiocatore>();

        //private List<tipoGiocatore> mod0 = new List<tipoGiocatore> { new tipoGiocatore("seb", "192.168.0.10", "1v1"), new tipoGiocatore("dave", "192.168.0.15", "1v1"), new tipoGiocatore("cva", "192.168.0.20", "1v1") };
        //private List<tipoGiocatore> mod1 = new List<tipoGiocatore> { new tipoGiocatore("seb", "192.168.1.10", "mult"), new tipoGiocatore("dave", "192.168.1.15", "mult"), new tipoGiocatore("cva", "192.168.1.20", "mult") };

        private int arraySelected = -1;

        public string name;
        public bool mode;

        public SelectMode(string name)
        {     
            InitializeComponent();
            inizializzatore(name);
        }        

        private void button1_Click(object sender, EventArgs e)
        {
            //quale array si sta visualizzando
            arraySelected = 0;
            //search in the LAN if there are players for 1 VS 1
            searchLAN();
            //refresh list
            createList(mod0);            
        }


        //button for multiplayer mode
        private void button2_Click(object sender, EventArgs e)
        {
            /*
            //quale array si sta visualizzando
            arraySelected = 1;
            //search in the LAN if there are players Multiplayer
            searchLAN();
            //refresh list  
            createList(mod1);                        
            */

            MessageBox.Show("Mode not implemented yet");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //search in the LAN if there are players Multiplayer
            switch (arraySelected){
                case 0:
                    createList(mod0);
                    break;
                case 1:
                    createList(mod1);
                    break;
                }            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var createServer = new game())
            {
                var result = createServer.ShowDialog();
                if (result == DialogResult.OK)
                {        
                    
                }else
                {

                }
            }
        }

        //----------------------------------------------------------------------
        //Vari methodi
        //----------------------------------------------------------------------

        private void inizializzatore(string namePlayer)
        {
            label3.Text = "Player: " + namePlayer;
        }

        private void createList(List<tipoGiocatore> lista)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("Nome\tIndirizzo\tModalità\n");

            foreach (tipoGiocatore i in lista)
            {
                listBox1.Items.Add(i.name + "\t" + i.ip + "\t" + i.modalità + "\n");
            }            
        }

        private void searchLAN()
        {
            //search mathces
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (var createServer = new CreateGame())
            {
                var result = createServer.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string val = createServer.ReturnValue1;
                    bool selected = createServer.ReturnValue2;                    
                }
            }
        }
    }
}
