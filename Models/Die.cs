using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace YatzeeWPF.Models
{
    class Die
    {

        //Referens
        Game game;
        Random rnd;

        public Die(Random r)
        {
            rnd = r;
        }
     
        bool locked;

        public bool Locked
        {
            get { return locked; }
            set { locked = value; }
        }

        int value;

        public int Value
        {
            get { return this.value; }
            set { this.value = value; }
        }      
        

         public void ToggleLock() { 
         Locked = !Locked;
        }

        public void Roll()
        {
            if(!locked)this.value = rnd.Next(1, 7);
        }







         

       

    }
}
