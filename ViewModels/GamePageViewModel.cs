using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using YatzeeWPF.Models;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Windows;



namespace YatzeeWPF.ViewModels
{
    public class GamePageViewModel
    {
        private ObservableCollection<ScoreRow> scoreRows;
        private ObservableCollection<CombinationRow> combinationRow;
        public GamePageViewModel()
        {
        scoreRows = new ObservableCollection<ScoreRow> { };
        combinationRow = new ObservableCollection<CombinationRow> { };

        ScoreRowCollection.Add(new ScoreRow { ScoreType = "1:s"});
        ScoreRowCollection.Add(new ScoreRow { ScoreType = "2:s"});
        ScoreRowCollection.Add(new ScoreRow { ScoreType = "3:s" });
        ScoreRowCollection.Add(new ScoreRow { ScoreType = "4:s" });
        ScoreRowCollection.Add(new ScoreRow { ScoreType = "5:s" });
        ScoreRowCollection.Add(new ScoreRow { ScoreType = "6:s" });
        ScoreRowCollection.Add(new ScoreRow { ScoreType = "Bonus" });
        ScoreRowCollection.Add(new ScoreRow { ScoreType = "Pair" });
        ScoreRowCollection.Add(new ScoreRow { ScoreType = "Two pairs" });
        ScoreRowCollection.Add(new ScoreRow { ScoreType = "Three of a kind" });
        ScoreRowCollection.Add(new ScoreRow { ScoreType = "Four of a kind" });
        ScoreRowCollection.Add(new ScoreRow { ScoreType = "Full house" });
        ScoreRowCollection.Add(new ScoreRow { ScoreType = "Small straight" });
        ScoreRowCollection.Add(new ScoreRow { ScoreType = "Large straight" });
        ScoreRowCollection.Add(new ScoreRow { ScoreType = "Chance" });
        ScoreRowCollection.Add(new ScoreRow { ScoreType = "Yatzee" });
        ScoreRowCollection.Add(new ScoreRow { ScoreType = "Total score" });

         }
        public ObservableCollection<ScoreRow> ScoreRowCollection
        {
           
            get { return scoreRows; }
            set
            {
                scoreRows = value;
            }

        }

        public ObservableCollection<CombinationRow> CombinationRowCollection
        {

            get { return combinationRow; }
            set
            {
                combinationRow = value;
            }

        }
    }
}
