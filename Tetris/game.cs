using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Tetris
{
    //handler.Shutdown(SocketShutdown.Both);
    // handler.Close();
    public partial class game : Form
    {
        public bool richiestaNome = true;
        public int width = 11;
        public int x = 3;
        public int y = 0;
        public bool check;
        public int currentCase = 0;
        public Tiles currentTile = null;
        public Tiles nextTile = null;
        public string tileCorrente = "";
        public int timeSet = 500;
        public int timeSetC = 50;
        public int keepTimer = 0;
        public string rotazione = "left";
        private static System.Timers.Timer aTimer;
        string _name;
        string _ip, _type;
        int _port;
        private static System.Timers.Timer cTimer;        
        public Socket socket;
        Thread t;
        Thread aggiornaCampoAvversario;        
        public game(string ip, int port, string type,string name)
        {
            _name = name;
            _ip = ip;
            _port = port;
            _type = type;
            InitializeComponent();
            inizializza();
            //wait for player then:             
        }

        public void SetTimer()
        {
            try
            {
                this.label9.Visible = false;
            }
            catch { }
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(timeSet);
            // Hook up the Elapsed event for the timer.             
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        public void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            //keepTimer += 10;
            //fulcore del gioco
            // string tileCorrente = "";


            keepTimer = 0;
            switch (currentCase)
            {
                case 0:
                    Tiles currentTile = new Tiles();
                    tileCorrente = currentTile.getStringa();
                    campoGioco4.istance(x, y, tileCorrente);
                    currentCase = 1;
                    break;
                case 1:

                    //Logica del gioco da sistemare
                    //1) Controlla se la matrice è già piena prima di inserire
                    //2) Inserisci blocco 
                    //2.5) Incrementa y
                    //3) Controlla se la prossima posizione si può utilizzare
                    //3) Si --> 4 No --> 5
                    //4) Inserisci blocco più giù
                    //5) y = 0; --> 2

                    if (campoGioco4.check(x, y, tileCorrente))
                    {
                        if (y == 0)
                        {
                            aTimer.Dispose();

                            aTimer.Enabled = false;
                            aTimer.Stop();
                            cTimer.Enabled = false;
                            cTimer.Stop();
                            string s = "GAMEOVER";
                            byte[] msg = Encoding.UTF8.GetBytes(s);
                            // Blocks until send returns.
                            socket.Send(msg, 0, msg.Length, SocketFlags.None);
                            DialogResult ok = MessageBox.Show("Game Over");
                            if (ok == DialogResult.OK)
                            {
                                t.Join();
                                aggiornaCampoAvversario.Join();
                                socket.Dispose();
                                DialogResult = DialogResult.No;
                            }                        
                        }
                        else
                        {
                            campoGioco4.swithcState();
                            currentCase = 0;
                            y = 0;
                            x = 3;
                            campoGioco4.verifica = false;
                        }
                    }
                    else
                    {
                        campoGioco4.clean();
                        campoGioco4.istance(x, y, tileCorrente);
                        y++;
                        List<int> indiciRigheComplete;
                        int righeComplet = campoGioco4.checkFullLine(out indiciRigheComplete);
                        if (righeComplet != 0)
                        {
                            InvioRighe(righeComplet);
                            righeComplet = 0;
                            campoGioco4.spostaGiu(indiciRigheComplete, righeComplet);
                            campoGioco4.clean();
                        }
                    }
                    break;
            }
            //manda e ricevi  
        }
        
        private void game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
            {
                rotazione = "right";
                if (x != width - 1 && !campoGioco4.checkSide(x + 1, y, tileCorrente, rotazione) && !campoGioco4.check(x, y, tileCorrente))
                {
                    x = x + 1;
                }
                else if (campoGioco4.check(x, y, tileCorrente))
                {
                    campoGioco4.swithcState();
                    currentCase = 0;
                    y = 0;
                    x = 3;
                    campoGioco4.verifica = false;
                }
            }
            else if (e.KeyCode == Keys.A)
            {
                rotazione = "left";
                if (x >= -1 && !campoGioco4.checkSide(x - 1, y, tileCorrente, rotazione) && !campoGioco4.check(x, y, tileCorrente))
                {
                    x = x - 1;
                }
                else if(campoGioco4.check(x, y, tileCorrente))
                {
                    campoGioco4.swithcState();
                    currentCase = 0;
                    y = 0;
                    x = 3;
                    campoGioco4.verifica = false;
                }
            }
            else if (e.KeyCode == Keys.W)
            {
                rotazione = "left";
                if (tileCorrente != "0000001100011000000000000" && !campoGioco4.check(x, y, tileCorrente))
                    tileCorrente = campoGioco4.traslate(tileCorrente, rotazione, x, y);
                else if (campoGioco4.check(x, y, tileCorrente))
                {
                    campoGioco4.swithcState();
                    currentCase = 0;
                    y = 0;
                    x = 3;
                    campoGioco4.verifica = false;
                }
                //se non riesce a girarlo senza fare i vincoli ritorna lo stesso tile
            }
            else if (e.KeyCode == Keys.S)
            {            
                if (!campoGioco4.check(x, y+1, tileCorrente))
                {
                    campoGioco4.clean();
                    campoGioco4.istance(x, y, tileCorrente);
                    campoGioco4.verifica = false;
                   y++;
                }
                else
                {
                    if (!campoGioco4.check(x, y, tileCorrente))
                    {
                        campoGioco4.clean();
                        campoGioco4.istance(x, y, tileCorrente);
                    }      
                    campoGioco4.swithcState();
                    currentCase = 0;
                    y = 0;
                    x = 3;
                    tileCorrente = "0000000000000000000000000";
                    campoGioco4.verifica = false;
                }                    
                    
                
            }
            campoGioco4.clean();
            campoGioco4.istance(x, y, tileCorrente);
        }

        //Methods
        private void InvioRighe(int i)
        {
            //socket.Send(msg, 0, msg.Length, SocketFlags.None);
            string stringa = sendData(campoGioco4, i);
        }

        private void inizializza()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            aggiornaCampoAvversario = new Thread(setTimerComunicazione);
            label3.Text += "  "+_ip;
            label5.Text += "  " + _port.ToString();
            label7.Text += "  " + _type;
            if (_type == "host")
            {
                t = new Thread(HostThread);
            }
            else
            {
                t = new Thread(JoinerThread);
            }
            t.Start();
        }

        private void setTimerComunicazione()
        {
            cTimer = new System.Timers.Timer(timeSetC);
            // Hook up the Elapsed event for the timer.             
            cTimer.Elapsed += comunicazione;
            cTimer.AutoReset = true;
            cTimer.Enabled = true;
        }

        private void comunicazione(object source, ElapsedEventArgs e)
        {
            string stringa = sendData(campoGioco4, 0);
           // MessageBox.Show(""+stringa);
            if (stringa.Contains("GAMEOVER") || stringa == null)
            {
                aTimer.Enabled = false;
                aTimer.Stop();
                cTimer.Enabled = false;
                cTimer.Stop();
                socket.Dispose();
                DialogResult vinto = MessageBox.Show("HAI VINTOOOO");
                if (vinto == DialogResult.OK)
                    DialogResult = DialogResult.OK;

            }
            else if (stringa.Contains("righe"))
            {
                int i = stringa.IndexOf("righe");
                // int aggiungiRighe = stringa[5];
                //MessageBox.Show(stringa[i+5].ToString());
                campoGioco4.aggiungiSu(Convert.ToInt32(stringa[i + 5].ToString()));
            }
            else if (stringa.Contains("NAME"))
            {
                string nomeAvversario="";
                int indiceNome = stringa.IndexOf("NAME");
                int numeroLettere = Convert.ToInt32(stringa[indiceNome +4].ToString());
                for (int i = indiceNome +5; i < numeroLettere + 5; i++)
                {
                    nomeAvversario += "" + stringa[i];
                }
                label2.Text = nomeAvversario;
            }
            else if (!stringa.Contains("GAMEOVER"))
            {
                //string stringa = receiveData();
                campoGioco5.cleanAll();
                campoGioco5.convertToMatrix(stringa);
                if (!campoGioco5.convertToMatrix(stringa))
                {
                    socket.Dispose();
                    socket.Close();
                    aTimer.Enabled = false;
                    aTimer.Stop();
                    cTimer.Enabled = false;
                    cTimer.Stop();
                    cTimer.Dispose();
                    DialogResult vinto = MessageBox.Show("HAI VINTOOOO");
                    if (vinto == DialogResult.OK)
                        DialogResult = DialogResult.OK;
                }
            }            
        }       
        
        public void HostThread()
        {
            //AddToConsoleBox("Host thread started");
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = IPAddress.Parse(_ip);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, _port);            
            socket.Bind(localEndPoint);
            socket.Listen(1);
            try { socket = socket.Accept();
                SetTimer();
                aggiornaCampoAvversario.Start();
            }
            catch { }
            
            
            //AddToConsoleBox("Checkpoint 0");                        
        }
        public void JoinerThread()
        {
            //ConsoleBox.Items.Add("Joiner thread started");
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(_ip), _port);
            try
            {
                socket.Connect(remoteEP);                
                //AddToConsoleBox("Checkpoint 0");            
                SetTimer();
                aggiornaCampoAvversario.Start();
            }
            catch
            {
                MessageBox.Show("Nessuna partita presente a questo inidirizzo ip");
                
            }
        }        

        public string getStringMatrice()
        {
            string s = "";
            for (int i = 0; i < campoGioco4.matrice.GetLength(1); i++)
            {
                for (int j = 0; j < campoGioco4.matrice.GetLength(0); j++)
                {
                    s += campoGioco4.matrice[j, i].Name;
                }
            }            
            return s;
        }

        public string sendData(campoGioco matrice, int righe)
        {
            string s = getStringMatrice();
            byte[] msg;
            byte[] bytes = new byte[256];
            string scivolo = "";
            while (richiestaNome)
            {
                    msg = Encoding.UTF8.GetBytes("NAME" + _name.Length + _name);
                    int byteCount = socket.Send(msg, 0, msg.Length, SocketFlags.None);

                    // Get reply from the server.
                    byteCount = socket.Receive(bytes, 0);

                    if (byteCount > 0)
                        scivolo = Encoding.UTF8.GetString(bytes);
                    if(scivolo.Contains("NAME"))
                     richiestaNome = false;
            }
            if (!scivolo.Contains("NAME"))
            {
                try
                {
                    if (righe == 0)
                    {
                        msg = Encoding.UTF8.GetBytes(s);
                        // Blocks until send returns.
                        int byteCount = socket.Send(msg, 0, msg.Length, SocketFlags.None);

                        // Get reply from the server.
                        byteCount = socket.Receive(bytes, 0);

                        if (byteCount > 0)
                            scivolo = Encoding.UTF8.GetString(bytes);
                    }
                    else
                    {
                        msg = Encoding.UTF8.GetBytes("righe" + righe);
                        int byteCount = socket.Send(msg, 0, msg.Length, SocketFlags.None);

                        // Get reply from the server.
                        byteCount = socket.Receive(bytes, 0);

                        if (byteCount > 0)
                            scivolo = Encoding.UTF8.GetString(bytes);
                    }
                }
                catch (SocketException e)
                {
                    Console.WriteLine("{0} Error code: {1}.", e.Message, e.ErrorCode);
                }
            }
            
            return scivolo;
        }        

        /// <summary>
        /// Cose generate dal computer
        /// </summary>

        public void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(game));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.campoGioco5 = new Tetris.campoGioco();
            this.campoGioco4 = new Tetris.campoGioco();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 39);
            this.label1.TabIndex = 3;
            this.label1.Text = "YOU";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(621, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 39);
            this.label2.TabIndex = 4;
            this.label2.Text = "Opponent";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(28, 512);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 36);
            this.label3.TabIndex = 5;
            this.label3.Text = "IP:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(69, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 36);
            this.label4.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(332, 512);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 36);
            this.label5.TabIndex = 7;
            this.label5.Text = "Port:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(317, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 36);
            this.label6.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(579, 512);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 36);
            this.label7.TabIndex = 9;
            this.label7.Text = "Type:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(491, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 36);
            this.label8.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Crimson;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F);
            this.label9.Location = new System.Drawing.Point(128, 255);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(658, 63);
            this.label9.TabIndex = 11;
            this.label9.Text = "WAITING FOR PLAYER...\r\n";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(178, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(422, 297);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(301, 298);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(167, 200);
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            // 
            // campoGioco5
            // 
            this.campoGioco5.BackColor = System.Drawing.Color.White;
            this.campoGioco5.Location = new System.Drawing.Point(542, 155);
            this.campoGioco5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.campoGioco5.Name = "campoGioco5";
            this.campoGioco5.Size = new System.Drawing.Size(210, 343);
            this.campoGioco5.TabIndex = 2;
            // 
            // campoGioco4
            // 
            this.campoGioco4.BackColor = System.Drawing.Color.White;
            this.campoGioco4.Location = new System.Drawing.Point(17, 155);
            this.campoGioco4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.campoGioco4.Name = "campoGioco4";
            this.campoGioco4.Size = new System.Drawing.Size(210, 343);
            this.campoGioco4.TabIndex = 0;
            // 
            // game
            // 
            this.BackColor = System.Drawing.Color.MediumBlue;
            this.ClientSize = new System.Drawing.Size(780, 567);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.campoGioco5);
            this.Controls.Add(this.campoGioco4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "game";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.game_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.game_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void game_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                socket.Dispose();
                socket.Close();
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
