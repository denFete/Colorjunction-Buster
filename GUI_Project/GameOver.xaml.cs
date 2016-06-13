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
using System.IO;
using GUI_Project.Model;

namespace GUI_Project
{
    /// <summary>
    /// Interaction logic for GameOver.xaml
    /// </summary>
    public partial class GameOver : UserControl
    {
        string points;
        string modeChoice;
        public GameOver(string point, int mode)
        {
            InitializeComponent();
            points = point;
            if (mode == 0)
            {
                modeChoice = "Mode1.txt";
            }
            else
            {
                modeChoice = "Mode2.txt";
            }
            lblPoints.Content = points;
            var highScoreList = new List<DataItem>();
            int p = readInHighScore(highScoreList);
            if (p >= Convert.ToInt32(points))
            {
                submitGrid.Visibility = Visibility.Hidden;
            }
        }//GameOver

        private void btnSubmitHighScore_Click(object sender, RoutedEventArgs e)
        {
            string myString = txbAddName.Text;
            txbAddName.BorderBrush = Brushes.Transparent;
            if (myString.Count(char.IsLetter) > 0)
            {
                updateHighScore();
                submitGrid.Visibility = Visibility.Hidden;
                lblHighscoreSaved.Content = "Highscore Saved!";
                
            }
            if (myString.Count(char.IsLetter) == 0)
            {
                txbAddName.BorderBrush = Brushes.Red;
            }
            
        }//btnSubmitHighScore_Click

        private int readInHighScore(List<DataItem> list)
        {
            string line;
            StreamReader file = new StreamReader(modeChoice, System.Text.Encoding.GetEncoding(1252));
            while ((line = file.ReadLine()) != null)
            {
                string[] Parts = line.Split('|');
                list.Add(new DataItem { Nr = Parts[0], Player = Parts[1], Score = Convert.ToInt32(Parts[2]) });
            }
            file.Close();

            if (list.Count == 10)
            {
                return list[list.Count - 1].Score;
            }
            else
            {
                return -1;
            }
        }//readInHighScore

        private int updateHighScore()
        {
            var highScoreList = new List<DataItem>();
            int p = readInHighScore(highScoreList);
            if (highScoreList.Count < 10)
            {
                highScoreList.Add(new DataItem { Nr = points, Player = txbAddName.Text.ToString(), Score = Convert.ToInt32(points) });
            }
            else if (p < Convert.ToInt32(points))
            {
                highScoreList.RemoveAt(highScoreList.Count - 1);
                highScoreList.Add(new DataItem { Nr = points, Player = txbAddName.Text.ToString(), Score = Convert.ToInt32(points) });
            }

            Comparison<DataItem> comparison = (x, y) => y.Score.CompareTo(x.Score);
            highScoreList.Sort(comparison);

            using (var stream = new FileStream(modeChoice, FileMode.Truncate))
            {
                using (var writer = new StreamWriter(stream, System.Text.Encoding.GetEncoding(1252)))
                {
                    for (int i = 0; i < highScoreList.Count; i++)
                    {
                        writer.WriteLine(i + 1 + "|" + highScoreList[i].Player + "|" + highScoreList[i].Score);
                    }
                }
            }
            return p;
        }//updateHighScore

        private void btnNewGameGO_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = Window.GetWindow(this) as MainWindow;
            
            window.ContentArea.Content = null;
            window.Points.Content = "0";
            
            if (modeChoice == "Mode1.txt")
            {
                window.timeFS.Content = "5";
                window.mode1.restart();
            }
            else
            {
                window.timeFS.Content = "";
                window.levelFS.Content = "";
            }
            window.btnNewGameMenu.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));
            window.scoreFS.Content = "0";
            
            window.scoreFS.Content = window.Points.Content;
            window.timeClock.Content = "";
            window.playScreen.IsEnabled = true;
            window.fullscreenLeft.Opacity = 1;
            window.fullscreenRight.Opacity = 1;
            window.fullscreenLeft.IsEnabled = true;
            //window.menu.IsEnabled = true;
            window.extraWindows.Visibility = Visibility.Hidden;
            deladeFunktioner func = new deladeFunktioner(window);
            func.GetHighScore();

        }//btnNewGameGO_Click

        private void btnMenuGO_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = Window.GetWindow(this) as MainWindow;
            window.ContentArea.Content = null;
            window.Points.Content = "0";
            window.scoreFS.Content = window.Points.Content;
            window.timeClock.Content = "";
            window.timeFS.Content = "";
            window.playScreen.Visibility = Visibility.Hidden;
            window.startScreen.Visibility = Visibility.Visible;
            window.btnResume.Visibility = Visibility.Collapsed;
            window.playScreen.IsEnabled = true;
            //window.menu.IsEnabled = true;
            window.extraWindows.Visibility = Visibility.Hidden;
            if (window._inFullscreen)
            {
                window.fullscreenLeft.Visibility = Visibility.Hidden;
                window.fullscreenRight.Visibility = Visibility.Hidden;
            }

        }

        private void txbAddName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txbAddName.BorderBrush = Brushes.Transparent;
        }//btnMenuGO_Click


    }
}
