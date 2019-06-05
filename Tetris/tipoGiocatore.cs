using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class tipoGiocatore
    {
        public string name;
        public string ip;
        public string port;
        public string modalità;        

        public tipoGiocatore(string _name, string _ip, string _port, string _modalità)
        {
            this.name = _name;
            this.ip = _ip;
            this.modalità = _modalità;
            this.port = _port;
        }
    }
}
