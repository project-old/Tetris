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

namespace Tetris
{    
    public partial class game : Form
    {                
        public int x = 0;
        public int y = 0;
        public bool check;
        public int currentCase = 0;
        public Tiles currentTile = null;
        public Tiles nextTile = null;
        public string tileCorrente = "";
        public int timeSet = 200;

        private static System.Timers.Timer aTimer;        

        public game()
        {            
            InitializeComponent();
            SetTimer();            
        }

        public void SetTimer()
        {            
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(timeSet);
            // Hook up the Elapsed event for the timer.             
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;            
        }

        public void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            //fulcore del gioco
           // string tileCorrente = "";

            switch (currentCase)
            {
                case 0:                    
                    Tiles currentTile = new Tiles();
                    tileCorrente = currentTile.getStringa();                    
                    campoGioco4.istance(0,0,tileCorrente);
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

                    if(campoGioco4.check(x, y, tileCorrente) == true)
                    {
                        if (y == 0)
                        {
                            aTimer.Dispose();
                            MessageBox.Show("Game Over");
                        }
                        else
                        {
                            campoGioco4.swithcState();
                            currentCase = 0;
                            y = 0;
                        }
                    }
                    else
                    {
                        campoGioco4.clean();
                        campoGioco4.istance(x, y, tileCorrente);
                        y++;
                    }                    
                    break;                
            } 



            /*
            switch (currentCase)
            {
                case 0:
                    if (nextTile == null)
                    {
                        nextTile = new Tiles();

                    }

                    if (currentTile == null)
                    {
                        nextTile = new Tiles();
                    }
                    currentCase = 1;
                    
                    break;
                case 1:
                    case



                    /*switch(Convert.ToInt32(currentTile))
                    {
                        case 0:
                            campoGioco4.matrice[x, y + 1].Name = "1";
                            campoGioco4.matrice[x+1, y + 1].Name = "1";
                            campoGioco4.clean();
                            campoGioco4.refresh(x, y+1);
                            campoGioco4.refresh(x+1, y+1);
                            campoGioco4.refresh(x, y);
                            campoGioco4.refresh(x+1, y);
                            break;
                    }
                    if(y == 17)
                    {
                        campoGioco4.matrice[x, y - 1].Name = "2";
                        campoGioco4.matrice[x+1, y - 1].Name = "2";
                        campoGioco4.matrice[x+1, y].Name = "2";
                        campoGioco4.matrice[x, y].Name = "2";
                        currentCase = 2;
                    }
                    if (y < 17)
                    {
                        campoGioco4.matrice[x, y + 1].Name = "1";
                        campoGioco4.matrice[x + 1, y + 1].Name = "1";
                        campoGioco4.clean();
                        campoGioco4.refresh(x, y + 1);
                        campoGioco4.refresh(x + 1, y + 1);
                        campoGioco4.refresh(x, y);
                        campoGioco4.refresh(x + 1, y);
                    }
 
                  /*  if (y == 18 && campoGioco4.check())
                    {
                        campoGioco4.matrice[x, y-1].Name = "2";
                        currentCase = 2;                        
                    }
                    campoGioco4.clean();
                    campoGioco4.refresh(x, y);
                    
                    y++;
                        break;
                case 2:
                        x+=2;
                    y = 0;
                        currentTile = nextTile;
                        nextTile = new Tiles();
                    currentCase = 1;
                break;
            } 
        */

            }

            public void InitializeComponent()
        {
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.campoGioco5 = new Tetris.campoGioco();
            this.campoGioco4 = new Tetris.campoGioco();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(231, 374);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(163, 23);
            this.button3.TabIndex = 1;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(77, 11);
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
            this.label2.Location = new System.Drawing.Point(423, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 39);
            this.label2.TabIndex = 4;
            this.label2.Text = "Opponent";
            // 
            // campoGioco5
            // 
            this.campoGioco5.BackColor = System.Drawing.Color.White;
            this.campoGioco5.Location = new System.Drawing.Point(401, 54);
            this.campoGioco5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.campoGioco5.Name = "campoGioco5";
            this.campoGioco5.Size = new System.Drawing.Size(210, 343);
            this.campoGioco5.TabIndex = 2;
            // 
            // campoGioco4
            // 
            this.campoGioco4.BackColor = System.Drawing.Color.White;
            this.campoGioco4.Location = new System.Drawing.Point(15, 54);
            this.campoGioco4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.campoGioco4.Name = "campoGioco4";
            this.campoGioco4.Size = new System.Drawing.Size(210, 343);
            this.campoGioco4.TabIndex = 0;
            // 
            // game
            // 
            this.BackColor = System.Drawing.Color.MediumBlue;
            this.ClientSize = new System.Drawing.Size(629, 410);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.campoGioco5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.campoGioco4);
            this.Name = "game";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.game_KeyDown);            
            this.ResumeLayout(false);
            this.PerformLayout();

        }        

        private void game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("Enter key pressed");
            }
        }
    }
}
