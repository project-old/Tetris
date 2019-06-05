using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Timers;

namespace Tetris
{
    public partial class SelectMode : Form
    {
        //due arrray di oggetti da definire con l'oggetto di ogni giocatore: IP, nome, versione, modalità...

        private List<tipoGiocatore> mod0 = new List<tipoGiocatore>();
        private List<tipoGiocatore> mod1 = new List<tipoGiocatore>();

        //Timer
        private static System.Timers.Timer aTimer;

        //General variables
        private string myIp;
        private int myPort;
        private string myServerName;
        private string name;

        //private List<tipoGiocatore> mod0 = new List<tipoGiocatore> { new tipoGiocatore("seb", "192.168.0.10", "1v1"), new tipoGiocatore("dave", "192.168.0.15", "1v1"), new tipoGiocatore("cva", "192.168.0.20", "1v1") };
        //private List<tipoGiocatore> mod1 = new List<tipoGiocatore> { new tipoGiocatore("seb", "192.168.1.10", "mult"), new tipoGiocatore("dave", "192.168.1.15", "mult"), new tipoGiocatore("cva", "192.168.1.20", "mult") };

        private int arraySelected = -1;                        

        Socket socket;

        public SelectMode()
        {            
            name = "1";
            InitializeComponent();
            inizializzatore(name);
            //listBox1.Items.Add("Nome\tIndirizzo\tPort\tModalità\n");
            myIp = getIP();
            myPort = 65500;
            myServerName = "";

            //Initializing socket
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        }        
        
        private void button1_Click(object sender, EventArgs e)
        {
            button4.Visible = true;
            label2.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            button3.Visible = true;
            label4.Visible = true;
            //listBox1.Visible = true;
            //button5.Visible = true;
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

        private void button3_Click(object sender, EventArgs e)
        {
            string ipInsert = textBox1.Text;
            //IPAddress ipOpponent = textBox1.Text.tol;
            using (var createServer = new game(ipInsert, myPort, "client",textBox2.Text))
            {
                var result = createServer.ShowDialog();               
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {                            
            game createGame = new game(myIp, myPort, "host", textBox2.Text);
            var resultGame = createGame.ShowDialog();
                //if confirmed broadcast begins   
                //tipoGiocatore data = new tipoGiocatore(name, myIp.ToString(), myPort.ToString(), "1VS1");
                //mod0.Add(data);                                
        }        

        //----------------------------------------------------------------------
        //Vari methodi
        //----------------------------------------------------------------------

        private void inizializzatore(string namePlayer)
        {
            label3.Text = "Player: " + namePlayer;
        }

        private string getIP()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
            Console.WriteLine(hostName);
            // Get the IP  
            string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            return myIP;
        }

        private void createList(List<tipoGiocatore> lista)
        {
           // listBox1.Items.Clear();
           // listBox1.Items.Add("Nome\tIndirizzo\tPort\tModalità\n");

            foreach (tipoGiocatore i in lista)
            {
               // listBox1.Items.Add(i.name + "\t" + i.ip + "\t" + i.port + "\t" + i.modalità + "\n");
            }            
        }

        private void searchLAN()
        {
            //search mathces
        }
        
        //in development
        /*
        private void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(2000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        //Timer for the sending of the ip,port and name


        public void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            MessageBox.Show(myServerName);
            //Mandare IP
            IPEndPoint remoteIP = new IPEndPoint(, myPort);
            Refresh();
        }
    */
    }  
}
