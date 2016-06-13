//BBBBBBBBBBBBBBBBB                                               tttt                                                  
//B::::::::::::::::B                                           ttt:::t                                                  
//B::::::BBBBBB:::::B                                          t:::::t                                                  
//BB:::::B     B:::::B                                         t:::::t                                                  
//  B::::B     B:::::Buuuuuu    uuuuuu      ssssssssss   ttttttt:::::ttttttt        eeeeeeeeeeee    rrrrr   rrrrrrrrr   
//  B::::B     B:::::Bu::::u    u::::u    ss::::::::::s  t:::::::::::::::::t      ee::::::::::::ee  r::::rrr:::::::::r  
//  B::::BBBBBB:::::B u::::u    u::::u  ss:::::::::::::s t:::::::::::::::::t     e::::::eeeee:::::eer:::::::::::::::::r 
//  B:::::::::::::BB  u::::u    u::::u  s::::::ssss:::::stttttt:::::::tttttt    e::::::e     e:::::err::::::rrrrr::::::r
//  B::::BBBBBB:::::B u::::u    u::::u   s:::::s  ssssss       t:::::t          e:::::::eeeee::::::e r:::::r     r:::::r
//  B::::B     B:::::Bu::::u    u::::u     s::::::s            t:::::t          e:::::::::::::::::e  r:::::r     rrrrrrr
//  B::::B     B:::::Bu::::u    u::::u        s::::::s         t:::::t          e::::::eeeeeeeeeee   r:::::r            
//  B::::B     B:::::Bu:::::uuuu:::::u  ssssss   s:::::s       t:::::t    tttttte:::::::e            r:::::r            
//BB:::::BBBBBB::::::Bu:::::::::::::::uus:::::ssss::::::s      t::::::tttt:::::te::::::::e           r:::::r            
//B:::::::::::::::::B  u:::::::::::::::us::::::::::::::s       tt::::::::::::::t e::::::::eeeeeeee   r:::::r            
//B::::::::::::::::B    uu::::::::uu:::u s:::::::::::ss          tt:::::::::::tt  ee:::::::::::::e   r:::::r            
//BBBBBBBBBBBBBBBBB       uuuuuuuu  uuuu  sssssssssss              ttttttttttt      eeeeeeeeeeeeee   rrrrrrr   


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
using System.Windows.Media.Animation;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Resources;
using System.Collections;
using System.IO;
using GUI_Project.Model;

namespace GUI_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public Mode1Methodes mode1;
        public Mode2Methodes mode2;
        public Mode3Methodes mode3;
        public int modeChoice;
        public bool _inFullscreen = false;
        int nrOfColors = 4;
        int nrOfColumns = 10;
        int nrOfBlocks = 14;
        double rectSize = 26;
        Sounds player;
        int pictureNumber=1;
        public MainWindow()
        {
            try
            {
            InitializeComponent();
            }
            catch (Exception e)
            {
                MessageBox.Show(Convert.ToString(e));
                throw;
            }
            player = new Sounds();
            btnFullscreenOff_Click(null, null);
            btnCol2.IsEnabled = false;
            btnFreeColor2.IsEnabled = false;
            btnFreePanel2.IsEnabled = false;
            btnDif2.IsEnabled = false;
            btnCol1.Foreground = Brushes.White;
            btnFreeColor1.Foreground = Brushes.White;
            btnCol3.Foreground = Brushes.White;
            btnFreeColor3.Foreground = Brushes.White;
            this.KeyDown += new KeyEventHandler(KeyEvent);
            btnSoundOn.IsEnabled = false;

            

        }//MainWindow      

       
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            playScreen.Visibility = Visibility.Hidden;
            startScreen.Visibility = Visibility.Visible;
            btnResume.Visibility = Visibility.Visible;
            btnResume.IsEnabled = true;
            if (modeChoice == 1)
            {
                mode2._Timer.Stop();
            }
            if(_inFullscreen)
            {
                fullscreenLeft.Visibility = Visibility.Hidden;
                fullscreenRight.Visibility = Visibility.Hidden;
            }
            
        }//btnPlay_Click

        private void Window_SourceInitialized(object sender, EventArgs ea)
        {
            WindowAspectRatio.Register((Window)sender);
        }//Window_SourceInitialized

        private void newGame_click(object sender, RoutedEventArgs e)
        {
            playScreen.Opacity = 1;
            
            canvas1.Children.Clear();
            if (modeChoice == 0)
            {
                mode1.boardIsFull = false;
                mode1.fillPlayField();
                Points.Content = 0;
                scoreFS.Content = 0;
                tries.Content = 5;
                level.Content = 1;
            }
            else if (modeChoice == 1)
            {
                mode2._Timer.Stop();
                timeClock.Foreground = Brushes.Black;
                timeClock.Content = "00:00:15";
                Points.Content = "0";
                mode2.boardIsFull = false;
                mode2.fillPlayField();
                mode2._time = 16;
                mode2._Timer.Start();
            }
            else
            {
                mode3.boardIsFull = false;
                mode3.fillPlayField();
            }
            
        }//newGame_click

        private void Exit_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }//Exit_click

        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            player.menuClick();
            playScreen.Opacity = 1;
            chooseMode mode = new chooseMode(this, nrOfColors, nrOfColumns, nrOfBlocks, rectSize, player, _inFullscreen);
            ContentArea.Content = mode;
            startScreen.IsEnabled = false;
            extraWindows.Visibility = Visibility.Visible;
            //menu.IsEnabled = false;            
        }//btnStartGame_Click

        private void btnResume_Click(object sender, RoutedEventArgs e)
        {
            player.menuClick();
            playScreen.Opacity = 1;
            startScreen.Visibility = Visibility.Hidden;
            playScreen.Visibility = Visibility.Visible;
            btnResume.Visibility = Visibility.Visible;
            if (modeChoice == 1)
            {
                mode2._Timer.Start();
            }
            if (_inFullscreen)
            {
                if (modeChoice < 2)
                {
                    fullscreenRight.Visibility = Visibility.Visible;
                    fullscreenLeft.Visibility = Visibility.Visible;
                }
            }
        }//btnResume_Click

        private void btnHighScore_Click(object sender, RoutedEventArgs e)
        {
            player.menuClick();
            startScreen.Visibility = Visibility.Hidden;
            highScores.Visibility = Visibility.Visible;
            btnMode2.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7F3E3E3E"));
            highScoreList.Items.Clear();
            string line;
            StreamReader file = new StreamReader("Mode1.txt", System.Text.Encoding.GetEncoding(1252));
            while ((line = file.ReadLine()) != null)
            {
                string[] nameParts = line.Split('|');
                highScoreList.Items.Add(new DataItem { Nr = nameParts[0], Player = nameParts[1], Score = Convert.ToInt32(nameParts[2]) });
            }
            file.Close();
            btnMode1.IsEnabled = false;
            btnMode1.Foreground = Brushes.White;
            btnMode2.IsEnabled = true;
            
        }//btnHighScore_Click
              
        private void btnMode1_Click(object sender, RoutedEventArgs e)
        {
            highScoreList.Items.Clear();
            btnMode1.IsEnabled = false;
            btnMode1.Foreground = Brushes.White;
            btnMode2.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7F3E3E3E"));
            btnMode2.IsEnabled = true;
            string line;
            StreamReader file = new StreamReader("Mode1.txt", System.Text.Encoding.GetEncoding(1252));
            while ((line = file.ReadLine()) != null)
            {
                string[] nameParts = line.Split('|');
                highScoreList.Items.Add(new DataItem { Nr = nameParts[0], Player = nameParts[1], Score = Convert.ToInt32(nameParts[2]) });
            }
            file.Close();
        }

        private void btnMode2_Click(object sender, RoutedEventArgs e)
        {
            highScoreList.Items.Clear(); 
            btnMode2.IsEnabled = false;
            btnMode2.Foreground = Brushes.White;
            btnMode1.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7F3E3E3E"));
            btnMode1.IsEnabled = true;
            string line;
            StreamReader file = new StreamReader("Mode2.txt", System.Text.Encoding.GetEncoding(1252));
            while ((line = file.ReadLine()) != null)
            {
                string[] nameParts = line.Split('|');
                highScoreList.Items.Add(new DataItem { Nr = nameParts[0], Player = nameParts[1], Score = Convert.ToInt32(nameParts[2]) });
            }
            file.Close();
        }//btnMode2_Click

        private void btnHighScoreBack_Click(object sender, RoutedEventArgs e)
        {
            player.menuClick();
            highScores.Visibility = Visibility.Hidden;
            settings.Visibility = Visibility.Hidden;
            startScreen.Visibility = Visibility.Visible;            
        }//btnHighScoreBack_Click

         private void btnDif1_Click(object sender, RoutedEventArgs e)
        {
            rectSize = 26 * 2;
            nrOfColumns = 10 / 2;
            nrOfBlocks = 14 / 2;
            btnDif1.IsEnabled = false;
            btnFreePanel1.IsEnabled = false;
            btnDif1.Foreground = Brushes.White;
            btnFreePanel1.Foreground = Brushes.White;
            btnDif2.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7F3E3E3E"));
            btnFreePanel2.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7F3E3E3E"));
            btnDif3.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7F3E3E3E"));
            btnFreePanel3.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7F3E3E3E"));
            btnDif2.IsEnabled = true;
            btnFreePanel2.IsEnabled = true;
            btnDif3.IsEnabled = true;
            btnFreePanel3.IsEnabled = true;
        }//btnDif1_Click
        private void btnDif2_Click(object sender, RoutedEventArgs e)
        {
            rectSize = 26;
            nrOfColumns = 10;
            nrOfBlocks = 14;
            btnDif2.IsEnabled = false;
            btnFreePanel2.IsEnabled = false;
            btnDif2.Foreground = Brushes.White;
            btnFreePanel2.Foreground = Brushes.White;
            btnDif1.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7F3E3E3E"));
            btnFreePanel1.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7F3E3E3E"));
            btnDif3.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7F3E3E3E"));
            btnFreePanel3.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7F3E3E3E"));
            btnDif1.IsEnabled = true;
            btnFreePanel1.IsEnabled = true;
            btnDif3.IsEnabled = true;
            btnFreePanel3.IsEnabled = true;
        }//btnDif2_Click
        private void btnDif3_Click(object sender, RoutedEventArgs e)
        {
            rectSize = 26 / 2;
            nrOfColumns = 10 * 2;
            nrOfBlocks = 14 * 2;
            btnDif3.IsEnabled = false;
            btnFreePanel3.IsEnabled = false;
            btnDif3.Foreground = Brushes.White;
            btnFreePanel3.Foreground = Brushes.White;
            btnDif1.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7F3E3E3E"));
            btnFreePanel1.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7F3E3E3E"));
            btnDif2.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7F3E3E3E"));
            btnFreePanel2.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7F3E3E3E"));
            btnDif1.IsEnabled = true;
            btnFreePanel1.IsEnabled = true;
            btnDif2.IsEnabled = true;
            btnFreePanel2.IsEnabled = true;
        }//btnDif3_Click

        private void btnCol1_Click(object sender, RoutedEventArgs e)
        {
            nrOfColors = Convert.ToInt16(btnCol2.Content);
            if (nrOfColors > 2)
            {
                btnCol3.IsEnabled = true;
                btnFreeColor3.IsEnabled = true;
                btnCol1.IsEnabled = true;
                btnFreeColor1.IsEnabled = true;
                btnCol2.Content = --nrOfColors;
                btnFreeColor2.Content = nrOfColors;
                btnFreeColor3.Foreground = Brushes.White;
                btnCol3.Foreground = Brushes.White;
                btnFreeColor3.Foreground = Brushes.White;
                if (nrOfColors == 2)
                {
                    btnCol1.IsEnabled = false;
                    btnCol1.Foreground = Brushes.Black;
                    btnFreeColor1.IsEnabled = false;
                    btnFreeColor1.Foreground = Brushes.Black;

                }
            }
        }//btnCol1_Click
        private void btnCol2_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnCol3_Click(object sender, RoutedEventArgs e)
        {
            nrOfColors = Convert.ToInt16(btnCol2.Content);
            if (nrOfColors < 6)
            {
                btnCol1.IsEnabled = true;
                btnFreeColor1.IsEnabled = true;
                btnCol3.IsEnabled = true;
                btnFreeColor3.IsEnabled = true;
                btnCol2.Content = ++nrOfColors;
                btnFreeColor2.Content = nrOfColors;
                btnCol1.Foreground = Brushes.White;
                btnFreeColor1.Foreground = Brushes.White;
                if (nrOfColors == 6)
                {
                    btnCol3.IsEnabled = false;
                    btnCol3.Foreground = Brushes.Black;
                    btnFreeColor3.Foreground = Brushes.Black;
                    btnFreeColor3.IsEnabled = false;
                }
            }
        }//btnCol3_Click

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            player.menuClick();
            startScreen.Visibility = Visibility.Hidden;
            settings.Visibility = Visibility.Visible;           
        }//btnSettings_Click
        private void btnSoundOff_Click(object sender, RoutedEventArgs e)
        {
            btnSoundOff.IsEnabled = false;
            btnSoundOff.Foreground = Brushes.White;
            btnSoundOn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7F3E3E3E"));
            btnSoundOn.IsEnabled = true;
            player.volumeDown();

        }//btnSoundOff_Click
        private void btnSoundOn_Click(object sender, RoutedEventArgs e)
        {
            btnSoundOn.IsEnabled = false;
            btnSoundOn.Foreground = Brushes.White;
            btnSoundOff.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7F3E3E3E"));
            btnSoundOff.IsEnabled = true;
            player.volumeUp();
        }//btnSoundOff_Click
        private void CenterWindowOnScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }//CenterWindowOnScreen

        private void btnFullscreenOff_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Normal;
            WindowStyle = WindowStyle.SingleBorderWindow;
            ResizeMode = ResizeMode.CanResize;
            btnFullscreenOff.IsEnabled = false;
            btnFullscreenOff.Foreground = Brushes.White;
            btnFullscreenOn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7F3E3E3E"));
            btnFullscreenOn.IsEnabled = true;
            CenterWindowOnScreen();
            _inFullscreen = false;
            fullscreenRight.Visibility = Visibility.Collapsed;
            fullscreenLeft.Visibility = Visibility.Collapsed;
            topPanel.Visibility = Visibility.Visible;
            topPanelFullScreen.Visibility = Visibility.Collapsed;
            if (modeChoice == 2)
            {
                topPanel.Visibility = Visibility.Collapsed;
                topPanelFullScreen.Visibility = Visibility.Collapsed;
                topPanelFree.Visibility = Visibility.Visible;
            }
        }//btnFullscreenOff_Click

        private void btnFullscreenOn_Click(object sender, RoutedEventArgs e)
        {
            if (playScreen.Visibility == Visibility.Visible)
            {
                if (modeChoice < 2)
                {
                    fullscreenRight.Visibility = Visibility.Visible;
                    fullscreenLeft.Visibility = Visibility.Visible;
                } 
            }
                

            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;
            //Topmost = true;
            ResizeMode = ResizeMode.CanMinimize;
            btnFullscreenOn.IsEnabled = false;
            btnFullscreenOn.Foreground = Brushes.White;
            btnFullscreenOff.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7F3E3E3E"));
            btnFullscreenOff.IsEnabled = true;
            _inFullscreen = true;
            topPanel.Visibility = Visibility.Collapsed;
            topPanelFullScreen.Visibility = Visibility.Visible;
            if (modeChoice == 2)
            {
                topPanel.Visibility = Visibility.Collapsed;
                topPanelFullScreen.Visibility = Visibility.Collapsed;
                topPanelFree.Visibility = Visibility.Visible;
            }
        }//btnFullscreenOn_Click

        //Få fullscreen på F12 klick, Startscreen!
        private void KeyEvent(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F12 && WindowState == WindowState.Maximized && _inFullscreen)
            {
                btnFullscreenOff_Click(null, null);
                topPanelFullScreen.Visibility = Visibility.Collapsed;
            }
            else if(e.Key == Key.F12)
            {
                btnFullscreenOn_Click(null, null);
                if (modeChoice != 2)
                    topPanelFullScreen.Visibility = Visibility.Visible;
            }
        }//Window_KeyDownF12

        private void btnSettingsBack_Click(object sender, RoutedEventArgs e)
        {
            player.menuClick();
            settings.Visibility = Visibility.Hidden;
            startScreen.Visibility = Visibility.Visible;
        }//btnSettingsBack_Click

        
        private void btnRulseBack_Click(object sender, RoutedEventArgs e)
        {
            player.menuClick();
            startScreen.Visibility = Visibility.Visible;
            screenRules.Visibility = Visibility.Hidden;
        }//btnRulseBack_Click

        public void hideHidescore()
        {
            fullscreenRight.Visibility = Visibility.Collapsed;
            fullscreenLeft.Visibility = Visibility.Collapsed;

        }

        private void btnNewGameGO_Click(object sender, RoutedEventArgs e)
        {
            if (!BlockClicks.IsVisible)
            {
                restartFullscreen.IsEnabled = false;
                if (modeChoice != 2)
                {
                    deladeFunktioner func = new deladeFunktioner(this);
                    ContentArea.Content = null;
                    Points.Content = "0";
                    scoreFS.Content = Points.Content;
                    if (Convert.ToString(lblHighScoren.Content) == "Survival")
                    {
                        timeClock.Content = "";
                        timeFS.Content = timeClock.Content;
                        levelFS.Content = "";
                    }
                    else
                    {
                        tries.Content = 5;
                        timeFS.Content = tries.Content;
                        levelFS.Content = 1;
                        mode1 = new Mode1Methodes(this, player);
                        modeChoice = 0;
                    }
                    func.GetHighScore();
                }
                else
                {
                    mode3 = new Mode3Methodes(nrOfColors, nrOfBlocks, nrOfColumns, rectSize, this, player);
                }
                btnNewGameMenu.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));
                playScreen.IsEnabled = true;
                //window.menu.IsEnabled = true;
                extraWindows.Visibility = Visibility.Hidden;
            }
        }

        private void btnRules_Click(object sender, RoutedEventArgs e)
        {
            player.menuClick();
            btnMode1Rules_Click(null, null);
            startScreen.Visibility = Visibility.Hidden;
            screenRules.Visibility = Visibility.Visible;
            rulesClassic.Visibility = Visibility.Visible;
            btnMode2Rules.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7F3E3E3E"));
            btnMode1Rules.IsEnabled = false;
            btnMode1Rules.Foreground = Brushes.White;
            btnMode2Rules.IsEnabled = true;
        }//btnRules_Click

        private void btnMode1Rules_Click(object sender, RoutedEventArgs e)
        {
            pictureNumber = 0;
            lblStep.Content = "Step " + (pictureNumber + 1);
            rulesClassic.Visibility = Visibility.Visible;
            btnMode1Rules.IsEnabled = false;
            btnMode2Rules.IsEnabled = true;
            btnLeftRules_Click(null, null);
            btnMode1Rules.Foreground = Brushes.White;
            btnMode2Rules.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7F3E3E3E"));
            
        }

        private void btnMode2Rules_Click(object sender, RoutedEventArgs e)
        {
            pictureNumber = 0;
            lblStep.Content = "Step " + (pictureNumber + 1);
            btnMode2Rules.IsEnabled = false;
            btnMode1Rules.IsEnabled = true;
            btnLeftRules_Click(null, null);
            btnMode2Rules.Foreground = Brushes.White;
            btnMode1Rules.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7F3E3E3E"));
            
        }

        private void btnLeftRules_Click(object sender, RoutedEventArgs e)
        {
            btnRulesRight.Opacity = 1;
            btnRulesRight.IsEnabled = true;
            string bilden="";
            if (pictureNumber > 0)
            {              
                pictureNumber--;
                lblStep.Content = "Step " + (pictureNumber+1);
                bilden = "/bilder/rules" + pictureNumber + ".png";
                picture.Source = new BitmapImage(new Uri(@bilden, UriKind.Relative));
            }
            if(btnMode1Rules.IsEnabled == false)
            {
                if (pictureNumber == 0)
                {
                    btnRulesLeft.Opacity = 0.2;
                    btnRulesLeft.IsEnabled = false;
                    bilden = "/bilder/rules" + pictureNumber + "c.png";
                    picture.Source = new BitmapImage(new Uri(@bilden, UriKind.Relative));
                }
            }
            else
            {
                if (pictureNumber == 0)
                {
                    btnRulesLeft.Opacity = 0.2;
                    btnRulesLeft.IsEnabled = false;
                    bilden = "/bilder/rules" + pictureNumber + "s.png";
                    picture.Source = new BitmapImage(new Uri(@bilden, UriKind.Relative));
                }
            }

        }

        private void btnRulesRight_click(object sender, RoutedEventArgs e)
        {
            btnRulesLeft.IsEnabled = true;
            btnRulesLeft.Opacity = 1;
            if (pictureNumber < 3)
            {
                pictureNumber++;
                lblStep.Content = "Step " + (pictureNumber+1);
                string bilden = "/bilder/rules" + pictureNumber + ".png";
                picture.Source = new BitmapImage(new Uri(@bilden, UriKind.Relative));                
            }
            if (pictureNumber == 3)
            {
                btnRulesRight.Opacity = 0.3;
                btnRulesRight.IsEnabled = false;
            }

        }//btnNewGameGO_Click

        

    }    

}


//    ,▄███▄     ,,,,╓╓gg╓µ,,,,            ,gg,                                   
//  g█▓▓██▒▒███▀▀▀²`²````▒▒▒▒▓▓▓▓█████▄▄██▓▓▓▓▓█▄                                 
//,█▓▓▓▓██▒▒▒▒▒╕        ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒███▓▓▓▓▓▓█y                               
//▀▀²▀▓▒▒█▒▒▒▒▒▒       .▒▒▒▒███████▒▒▒▒██▒▒▒██▓▓▓▓▓µ                              
//     ▓▓█▓▓█▒▒╛       ]▒██▓▓▓▓▓▓▓▓▓▓▒▒▒▒▒▒▒▒█▓▀▓▓▓▓                              
//    ╟▓▓▓²▓▓▓█╓µggggg¿╓█▓▓▓▓▓▓²▓▓▓▓▓█▒▒▒▒▒█▓▒    ▀▀                g▄▄,          
//    █▓▓▓▓▓▀▒▓▓▓▒▒█▓▓▒░░``²▀▓▓█▓▓▓▓▓▓▒▒▒▒▒▌²▀█▄               ,g▄▄█▓█▓▓▌         
//    ▓▓▓▀¡░▒▒▒▓▓▓▓▓▓▒▒▄░.     ▀▓▓▓▓▓▓▒▒▒▒▒▌   `▀█▄µ      ,g▄██▓▒▒▒▒▓███▓▌        
//    ▓█▓ .░▒█▓████████▓░        ▓▓▓▓▒▒▒▒▒█`      ▒▓▓█████▓▒▒▒▒▒▒▒█████▓▓▓µ       
//    ▓▓▌  `░█▒▒▒▒▒▒▒▒▒█`        ╘▓▓█▒▒▒▒▒Ñ      ¡▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒█████▒▓▌      
//    ▐▓    g▒░░░░░░░░░▐▌         ▀▓▓▒▒▒▀╛      ╓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒█▒███▓▌     
//   .▓▓   ╒▌. ''`````'.▀µ         ▓▓█▀╝      .@▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██▒▓⌐    
//   █▌▒   ▌    .     .  ▀         ▐▌       ,@▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒███▓▌    
//  ]▓,▒  É▀╧▄▄g,,,,,,,╓g▄▌         ▌     ,@▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓█    
//  ██▒▒▌gg@▀      ``     ╙▄      ╓≡`   y╫▒▒▒▒▒▒▒█▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██▓    
//  ▓▒▒▒▒▒@,                ╩BB╧╩`   ,@▒▒▒▒▒▒▒▒▒▒▒█▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▌    
// ▐▓▒▒▒▒▒▒▒@,                    .@▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒█▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▌    
// ██▒▒▒▒▒▒▒▒▒                    ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▒▒▒▒▒▒▒▒▒▒▒▒▒█▓    
// █▒▒▒▒▒▒▒█▒▒`                   ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒█▒▒▒▒▒▒▒█▓▀▓▒▒▒▒▒▒▒▒▒▒▒▒▒▓█   
// █▒▒▒▒▒▒▒▒▒█,                   ╙▒▒▒▒█▒▒▒▒▒▒▒▒▒▒▒▒█▒▒▒Ñ╬▄█▀  ▐▓▒▒▒▒▒▒▒▒▒▒▒██▓█▄ 
// ██▒▒▒▒▒▒▒Ñ╣▒▌µ                   `╩Ñ▒█▒▒▒▒▒▒▒▒▒▒▒▒░ g▄▓╝     ╘▓█▒▒▒▒▒▒▒██████▓█
// ▐▓▒▒▒▒▒▒▒ ╙▒▓▀▀▄µ                    ▐▒▒▒▒▒▒▒▒▒▒▒█▌▒▒▓Ñ        ²▀███▒████████▓M
//  ▓▒▒▒▒▒▒▒  ]▓   ²▀▀▄▄g,               █▒▒▒▒▒▒▒▒▒▒█▒▒▒▓             ▓█████████▓ 
//  ▐▓▒▒▒▒▒▒  ╘▓        `²▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▓▒▒▒▒▒▒▒▒▒▒█▒▒█▌             ▓▀████▀▀▀▓▌ 
//   ▀█▀▒▒▒▒   ▓▌                       ▐▓Ñ▒▒▒▒▒▀╝`▐▌▒▒▓▌            ▐▓       .▓  
//    ▀█       `▓w                      ▐▌        ,█▒▒▒▓▌           ┌▓`       ▐▓  
//     ▀█,      ╘▓µ                     ▓`        █▀▀▀▀`           ╓▓░       ,▄▌  
//      ╘█▄µ,   ,g▓p                   ▄▌        █▌                 ▀▀▀▀▀▀▀▀▀▀`   
//         ²▀▀▀▀▀²                     ▀█▄▄▄▄▄▄▌▀▀        
