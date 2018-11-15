using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Tiles
    {    
        string[] vector = new string[] { "0000000001001110", "0000000010001110", "1000100010001000", "0000010001100010", "0000000011001100" };


        /*Leo
    public int tiles()
    {            
        // get a random number 
        Random rnd = new Random();

        return rnd.Next(6);

        //return the n-position of the vector from the random nuber just generated            
        //return vector[rnd.Next(6)];

    }*/

        public void tiles()
        {
            // get a random number             
        }

        public string getStringa()
        {
            Random rnd = new Random();

            //return the n-position of the vector from the random nuber just generated            
            return vector[rnd.Next(5)];            
        }
    }
}
