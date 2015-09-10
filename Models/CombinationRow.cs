using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using YatzeeWPF.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Windows;

namespace YatzeeWPF.Models
{
public class CombinationRow : INotifyPropertyChanged
    {
        private string scoreType;
        private string key;
        private int points;

        public string ScoreType
        {
            get { return scoreType; }
            set
            {
                scoreType = value;
                OnPropertyChanged();
            }
        }

        public string Key
        {
            get { return key; }
            set
            {
                key = value;
                OnPropertyChanged();
            }
        }

        public int Points
        {
            get { return points; }
            set
            {
                points = value;
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
