using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YatzeeWPF.ViewModels;
using YatzeeWPF.Models;


using WpfPageTransitions;


namespace YatzeeWPF

{

    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ScorePage : Page
    {
        HighScoreManager highScoreManager = new HighScoreManager();
        private ScorePageViewModel scorePageViewModel;

        public ScorePageViewModel ScorePageViewModel
        {
            get { return scorePageViewModel; }
            set { scorePageViewModel = value; }
        }

        public ScorePage()
        {
            InitializeComponent();

            ScorePageViewModel = new ScorePageViewModel();
            this.DataContext = this;
        }




    }
}
