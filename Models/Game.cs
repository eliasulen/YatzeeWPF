using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.Reflection;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data.SQLite;


namespace YatzeeWPF.Models
{
    class Game : INotifyPropertyChanged
    {
        int currentTurn = 0;
        Player playerOne;
        Player playerTwo;
        bool multiPlayer;

        private Player currentPlayer;

        public Player CurrentPlayer
        {
            get { return currentPlayer; }
            set { currentPlayer = value; }
        }

        public Player PlayerOne
        {
            get { return playerOne; }
            set { playerOne = value; }
        }


        public Player PlayerTwo
        {
            get { return playerTwo; }
            set { playerTwo = value; }
        }

        public int CurrentTurn
        {
            get { return currentTurn; }
            set
            {
                currentTurn = value;
                OnPropertyChanged();
            }

        }
        Die[] dice = new Die[5];
        public Die[] Dice
        {
            get { return dice; }
            set { dice = value; }
        }

        Random rnd = new Random();

        public Game()
        {
            playerOne = new Player();
            playerTwo = new Player();

            currentPlayer = playerOne;

            for (int i = 0; i < dice.Length; i++)
            {
                dice[i] = new Die(rnd);
                dice[i].Roll(); //Startvärden
            }
        }

        public void PassTurn()
        {
            if (currentTurn < 3) NextTurn();
            currentPlayer.Hand = GetHand();

        }

        public void NextTurn()
        {
            bool allLocked = true;

            int i = 1;
            foreach (Die die in Dice)
            {
                die.Roll();
                if (!die.Locked) allLocked = false;
                i++;
            }

            if (!allLocked) CurrentTurn += 1; else if (CurrentTurn > 0) MessageBox.Show("Alla tärningar låsta"); else MessageBox.Show("Du måste rulla minst en gång");


        }

        public void FinishTurn()
        {
            CurrentTurn = 0;
            ChangePlayer();
            foreach (Die die in dice)
            {
                die.Locked = false;
            }
        }

        public Hand GetHand()
        {
            int[] scoreCount = new int[5];
            for (int i = 0; i < 5; i++)
            {
                scoreCount[i] = dice[i].Value;
            }

            return new Hand(scoreCount);
        }

        public void ChangePlayer()
        {
            if (currentPlayer != playerOne) currentPlayer = playerOne;
            else currentPlayer = playerTwo;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}
