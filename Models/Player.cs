using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatzeeWPF.Models
{
    class Player
    {
        Game game;
        Hand hand;
        string name;

        public string Name
        {
            get { return Name; }
            set { Name = value; }
        }

        public Hand Hand
        {
            get { return hand; }
            set { hand = value; }
        }

    }
}
