using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatzeeWPF.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Windows;


namespace YatzeeWPF.Models
{
    public class ScoreRow : INotifyPropertyChanged
    {
        private string scoreType;
        private int? scorePlayerOne;
        private int? scorePlayerTwo;

        public string ScoreType
        {
            get { return scoreType; }
            set
            {
                scoreType = value;
                OnPropertyChanged();
            }
        }

        public int? ScorePlayerOne
        {
            get { return scorePlayerOne; }
            set
            {
                scorePlayerOne = value;
                OnPropertyChanged();
            }
        }

        public int? ScorePlayerTwo
        {
            get { return scorePlayerTwo; }
            set
            {
                scorePlayerTwo = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

   
       protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
           if(PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
    }

