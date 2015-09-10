using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatzeeWPF.Models;
using System.Windows;

namespace YatzeeWPF.ViewModels
{


   public class ScorePageViewModel
    {
        HighScoreManager highScoreManager = new HighScoreManager();
        public ObservableCollection<HighScoreEntry> HighScoreEntries { get; private set; }

        public ScorePageViewModel()
        {       
            HighScoreEntries = new ObservableCollection<HighScoreEntry> { };
            try
            {

                foreach (HighScoreEntry highScoreEntry in highScoreManager.GetHighScore())
                {
                    HighScoreEntries.Add(new HighScoreEntry { Name = highScoreEntry.Name, Points = highScoreEntry.Points });
                }
            }catch (HighScoreException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public ObservableCollection<HighScoreEntry> HighScoreCollection
        {

            get { return HighScoreEntries; }
            set
            {
                HighScoreEntries = value;
            }

        }


    }
}
