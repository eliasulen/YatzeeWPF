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
using YatzeeWPF.Models;
using WpfPageTransitions;
using System.IO;
using System.Drawing.Imaging;
using System.Resources;
using System.Collections;
using System.Globalization;
using System.Reflection;
using YatzeeWPF.ViewModels;
using System.Data;
using System.Windows.Media.Animation;

namespace YatzeeWPF
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class GamePage : Page
    {

        private Game game;

        private GamePageViewModel gamePageViewModel;

        public GamePageViewModel GamePageViewModel
        {
            get { return gamePageViewModel; }
            set { gamePageViewModel = value; }
        }


        Image[] dieImages = new Image[5];
        BitmapImage[] diePictures = new BitmapImage[6];
        System.Drawing.Bitmap[] diePicturesSources = new System.Drawing.Bitmap[6];


        public GamePage()
        {
            game = new Game();

            InitializeComponent();
            LoadImages();

            gamePageViewModel = new GamePageViewModel();
            this.DataContext = this;

        }



        public void UpdateDieImages()
        {

            for (int i = 0; i < game.Dice.Length; i++)
            {
                dieImages[i].Source = diePictures[(game.Dice[i].Value) - 1];
                if (game.Dice[i].Locked)
                {
                    if (dieImages[i].Opacity <= 1) dieImages[i].Opacity = 0.5;
                }
                else dieImages[i].Opacity = 1;
            }

        }


        public BitmapImage StreamBitmap(System.Drawing.Bitmap bm)
        {

            Stream ms = new MemoryStream();
            bm.Save(ms, ImageFormat.Png);

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = ms;
            bitmap.EndInit();
            return bitmap;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {

            game.PassTurn();
            UpdateDieImages();
            UpdateCombinations();
            UpdateContent();
            if (game.CurrentTurn <= 3) DieRollMover();
        }

        public void UpdateCombinations()
        {

            gamePageViewModel.CombinationRowCollection.Clear();

            foreach (var combination in game.CurrentPlayer.Hand.GetCombinations())
            {
                gamePageViewModel.CombinationRowCollection.Add(combination);
            }

            if (!AllNotTaken())
            {
                GetRemainingCombinations();
            }
        }

        public void ClearCombinations()
        {
            gamePageViewModel.CombinationRowCollection.Clear();
        }

        public void UpdateContent()
        {
            TurnLabel.Content = "Rolls used: " + (game.CurrentTurn).ToString();
            if (game.CurrentPlayer == game.PlayerOne) PlayerLabel.Content = "P1 Turn"; else PlayerLabel.Content = "P2 Turn";
            if (game.CurrentTurn == 3) NextButton.IsEnabled = false; else NextButton.IsEnabled = true;

        }


        private void AssignValue_Click(object sender, RoutedEventArgs e)
        {

            AddResult();
            ClearCombinations();
            game.FinishTurn();
            UpdateDieImages();
            UpdateContent();

            if(gamePageViewModel.ScoreRowCollection.Where(x=> x.ScoreType != "Bonus").All(x => x.ScorePlayerOne != null && x.ScorePlayerTwo != null))
            {
                HighScorePrompt();
            }

        }

        public void AddResult()
        {

            setScore(gamePageViewModel.ScoreRowCollection.Where(x => x.ScoreType == gamePageViewModel.CombinationRowCollection[CombinationRowListView.SelectedIndex].ScoreType
                ).First(), gamePageViewModel.CombinationRowCollection[CombinationRowListView.SelectedIndex].Points);


            if (gamePageViewModel.ScoreRowCollection.Where(x => x.ScoreType == "1:s" ||
            x.ScoreType == "2:s" ||
            x.ScoreType == "3:s" ||
            x.ScoreType == "4:s" ||
            x.ScoreType == "5:s" ||
            x.ScoreType == "6:s").Sum(x => getScore(x)) >= 63) setScore(gamePageViewModel.ScoreRowCollection.Where(x => x.ScoreType == "Bonus").First(), 50);

            setScore(gamePageViewModel.ScoreRowCollection.Where(x => x.ScoreType == "Total score").First(), gamePageViewModel.ScoreRowCollection.Where(x => x.ScoreType != "Total score").Sum(x => getScore(x)));



        }

        private int? getScore(ScoreRow scoreRow)
        {
            if (game.CurrentPlayer == game.PlayerOne) return scoreRow.ScorePlayerOne;
            return scoreRow.ScorePlayerTwo;
        }

        private void setScore(ScoreRow scoreRow, int? points)
        {
            if (game.CurrentPlayer == game.PlayerOne) scoreRow.ScorePlayerOne = points;
            else scoreRow.ScorePlayerTwo = points;


        }

        private void dieimage_MouseUp(object sender, MouseButtonEventArgs e)
        {

            game.Dice[(Array.IndexOf(dieImages, sender))].ToggleLock();
            UpdateDieImages();
        }

        public void LoadImages()
        {

            diePicturesSources[0] = Properties.Resources.die1;
            diePicturesSources[1] = Properties.Resources.die2;
            diePicturesSources[2] = Properties.Resources.die3;
            diePicturesSources[3] = Properties.Resources.die4;
            diePicturesSources[4] = Properties.Resources.die5;
            diePicturesSources[5] = Properties.Resources.die6;

            for (int i = 0; i < diePictures.Length; i++)
            {
                diePictures[i] = StreamBitmap(diePicturesSources[i]);
            }

            dieImages[0] = dieimage1;
            dieImages[1] = dieimage2;
            dieImages[2] = dieimage3;
            dieImages[3] = dieimage4;
            dieImages[4] = dieimage5;


            UpdateDieImages();


        }

        public void DieRollMover()
        {
            Random rnd = new Random();

            int x1;
            int x2;
            int y1;
            int y2;

            for (int i = 0; i < dieImages.Length; i++)
            {
                x1 = rnd.Next(5, 5);
                x2 = rnd.Next(-5, 5);
                y1 = rnd.Next(-35, 35);
                y2 = rnd.Next(-35, 35);

                var AnimationX = new DoubleAnimation(x1, x2, TimeSpan.FromSeconds(0.15));
                var AnimationY = new DoubleAnimation(y1, y2, TimeSpan.FromSeconds(0.15));

                var Transform = new TranslateTransform();
                if (!game.Dice[i].Locked) dieImages[i].RenderTransform = Transform;

                Transform.BeginAnimation(TranslateTransform.XProperty, AnimationX);
                Transform.BeginAnimation(TranslateTransform.YProperty, AnimationY);
            }

        }

        private void CombinationRowListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (CombinationRowListView.SelectedIndex == -1) { assignValueButton.IsEnabled = false; NextButton.IsEnabled = true; } else assignValueButton.IsEnabled = true;
            if (CombinationRowListView.SelectedIndex != -1)
            {

                if (getScore(gamePageViewModel.ScoreRowCollection.Where(x => x.ScoreType == gamePageViewModel.CombinationRowCollection[CombinationRowListView.SelectedIndex].ScoreType
                   ).First()) != null) assignValueButton.IsEnabled = false;

            }




        }

        public bool AllNotTaken()
        {

            for (int i = 0; i < gamePageViewModel.CombinationRowCollection.Count(); i++)
            {
                if (getScore(gamePageViewModel.ScoreRowCollection.Where(x => x.ScoreType == gamePageViewModel.CombinationRowCollection[i].ScoreType
                   ).First()) == null) return true;

            }


            return false;
        }

        public void GetRemainingCombinations()
        {
            gamePageViewModel.CombinationRowCollection.Clear();

            for (int i = 0; i < gamePageViewModel.ScoreRowCollection.Count(); i++)
            {
                string scoreType;


                if (getScore(gamePageViewModel.ScoreRowCollection.Where(x => x.ScoreType == gamePageViewModel.ScoreRowCollection[i].ScoreType
                  ).First()) == null)
                {
                    scoreType = gamePageViewModel.ScoreRowCollection[i].ScoreType;
                    foreach (var combination in game.CurrentPlayer.Hand.GetAllCombinations())
                    {
                        if (combination.ScoreType.Equals(scoreType))
                            gamePageViewModel.CombinationRowCollection.Add(combination);
                    }
                }
            }



        }

         private void HighScorePrompt()
        {
            var totalScore = gamePageViewModel.ScoreRowCollection.Last();
            var p1won = totalScore.ScorePlayerOne > totalScore.ScorePlayerTwo;

            var draw = totalScore.ScorePlayerOne == totalScore.ScorePlayerTwo;

            if (p1won) WinnerTextBlock.Text = "Game finished, P1 won";
            else if (draw) WinnerTextBlock.Text = "Game finished, draw";
            else WinnerTextBlock.Text = "Game finished, P2 won";

            P1ScoreLabel.Content = "P1 Score: " + totalScore.ScorePlayerOne.ToString();

            P2ScoreLabel.Content = "P2 Score: " + totalScore.ScorePlayerTwo.ToString();

            HighScorePromptBox.Visibility = System.Windows.Visibility.Visible;
        }

        private void SubmitHighScore_Click(object sender, RoutedEventArgs e)
        {
     
            if (P1NameTextBox.Text.Trim().Length > 0 && P2NameTextBox.Text.Trim().Length > 0)
            {
                HighScorePromptBox.Visibility = System.Windows.Visibility.Collapsed;

                String playerOneName = P1NameTextBox.Text.Trim();
                String playerTwoName = P2NameTextBox.Text.Trim();

                P1NameTextBox.Text = String.Empty;
                P2NameTextBox.Text = String.Empty;

                try
                {
                    HighScoreManager highScoreManager = new HighScoreManager();

                    highScoreManager.AddToHighScore(new HighScoreEntry() { Name = playerOneName, Points = gamePageViewModel.ScoreRowCollection.Last().ScorePlayerOne.Value });
                    highScoreManager.AddToHighScore(new HighScoreEntry() { Name = playerTwoName, Points = gamePageViewModel.ScoreRowCollection.Last().ScorePlayerTwo.Value });
                    NavigationService.Navigate(new ScorePage());

                }
                catch (HighScoreException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else MessageBox.Show("Please enter two names");
                
            
        }

        private void DontSubmitHighScore_Click(object sender, RoutedEventArgs e)
        {
            HighScorePromptBox.Visibility = System.Windows.Visibility.Collapsed;
            NavigationService.Navigate(new ScorePage());

            P1NameTextBox.Text = String.Empty;
            P2NameTextBox.Text = String.Empty;
            
        }
    











    }
}
