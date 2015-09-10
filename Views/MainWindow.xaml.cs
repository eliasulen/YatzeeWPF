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
using System.Windows.Shapes;
using System.Windows.Navigation;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using YatzeeWPF.Models;
using WpfPageTransitions;

namespace YatzeeWPF
{
    /// <summary>
    /// Interaction logic for MainFrame.xaml
    /// </summary>
    public partial class MainWindow
    {
        MainIntro mainIntro = new MainIntro();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void HighScore_Click(object sender, RoutedEventArgs e)
        {
            mainIntro.Visibility = Visibility.Hidden;
            mainFrame.NavigationService.Navigate(new ScorePage());
        }

        private void CloseApplication_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Windows[0].Close();
        }
        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate("");
            ShowIntro();
        }

        private void VersusComputer_Click(object sender, RoutedEventArgs e)
        {         
           mainIntro.Visibility = Visibility.Hidden;
           mainFrame.NavigationService.Navigate(new GamePage());
        }

        private void VersusPlayer_Click(object sender, RoutedEventArgs e)
        {
            mainIntro.Visibility = Visibility.Hidden;
            mainFrame.NavigationService.Navigate(new GamePage());
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ShowIntro();
        }

        public void ShowIntro()
        {   
            mainIntro = new MainIntro();
            pageTransitionControl.ShowPage(mainIntro);
        }

     
        
    }
}
