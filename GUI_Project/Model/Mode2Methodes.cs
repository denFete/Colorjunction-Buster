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
using GUI_Project;

namespace GUI_Project.Model
{
    public class Mode2Methodes
    {
        double maxX = 260;
        double minX = 0;
        double maxY = 364;
        double minY = 0;
        double _rectSize = 26;
        int _numberOfColors = 4;
        int _nrOfBlocks = 14;
        int _nrOfColumns = 10;
        Queue que = new Queue();
        Queue que2 = new Queue();
        public bool boardIsFull = false;
        Rectangle _shapeSelected = null;
        Random random = new Random();
        bool IsHover = false;
        MainWindow window;
        deladeFunktioner func;
        public double _time = 15;
        public DispatcherTimer _Timer;
        Sounds _player;
        int _numberOfFalling = 0;


        public Mode2Methodes(int nrOfColors, int nrOfBlocks, int nrOfColumns, double rectSize,  MainWindow win, Sounds player)
        {
            window = win;
            _player = player;
            func = new deladeFunktioner(window);
            window.timeClock.Content = "";
            window.scoreFS.Content = window.timeClock.Content;
            window.Points.Content = "0";
            window.levelFS.Content = "";
            window.lblLevelFullscreen.Content = "";
            window.lblTimeFullscreen.Content = "Time";
            window.scoreFS.Content = window.Points.Content;
            _numberOfColors = nrOfColors;
            _nrOfBlocks = nrOfBlocks;
            _nrOfColumns = nrOfColumns;
            _rectSize = rectSize;
            window.classicGrid.Visibility = Visibility.Hidden;
            window.surivalGrid.Visibility = Visibility.Visible;
            _Timer = new DispatcherTimer();
            _Timer.Interval = new TimeSpan(0, 0, 1);
            _Timer.Tick += _Timer_Tick;
            window.fullscreenLeft.IsEnabled = true;
            window.fullscreenLeft.Opacity = 1;
            window.fullscreenRight.Opacity = 1;

            window.lblHighScoren.Content = "Survival";
            func.GetHighScore();
            if (window._inFullscreen)
            {
                func.GetHighScore();
                window.fullscreenRight.Visibility = Visibility.Visible;
                window.fullscreenLeft.Visibility = Visibility.Visible;
            }

        }

        void _Timer_Tick(object sender, EventArgs e)
        {
            window.timeClock.Foreground = Brushes.White;
            if (_time > 0)
            {
                if (_time <= 11)
                {
                    if (!(_time % 2 >= 1))
                    {
                        window.timeClock.Foreground = Brushes.Red;
                        window.timeClock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#a62a2d"));
                        window.timeFS.Foreground = window.timeClock.Foreground;
                    }
                    else
                    {
                        window.timeClock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#671b1d"));
                        window.timeFS.Foreground = window.timeClock.Foreground;
                    }

                    _time--;
                    window.timeClock.Content = string.Format("0{0}:{1}", (int)(_time / 60), (int)(_time % 60));
                    window.timeFS.Content = window.timeClock.Content;
                }
                else
                {
                    _time--;
                    window.timeFS.Foreground = Brushes.White;
                    window.timeClock.Content = string.Format("0{0}:{1}", (int)(_time / 60), (int)(_time % 60));
                    window.timeFS.Content = window.timeClock.Content;
                }
            }
            else
            {
                _Timer.Stop();
                window.playScreen.Opacity = 0.2;
                window.fullscreenLeft.Opacity = 0.2;
                window.fullscreenRight.Opacity = 0.2;
                GameOver gameover = new GameOver(window.Points.Content.ToString(), window.modeChoice);
                window.ContentArea.Content = gameover;
                window.playScreen.IsEnabled = false;
                window.fullscreenLeft.IsEnabled = false;
                window.extraWindows.Visibility = Visibility.Visible;
                //menu.IsEnabled = false;
            }
        }//_Timer_Tick

        //Fyller spelfältet med block i collumner som faller i olika hastighet.
        public void fillPlayField()
        {
            if (_nrOfColumns == 5)
            {
                _player.fallingBlocksSmall();
            }
            else if (_nrOfColumns == 10)
            {
                _player.fallingBlocks();
            }
            else
            {
                _player.fallingBlocksLarge();
            }
            window.BlockClicks.Visibility = Visibility.Visible;
            if (boardIsFull == false)
            {
                for (int i = 1; i <= _nrOfColumns; i++)
                {
                    if (i % 2 == 0)
                        fillPlayFieldRow(i, 0.1, false);
                    else if (i % 3 == 0)
                        fillPlayFieldRow(i, 0.09, false);
                    else
                        fillPlayFieldRow(i, 0.11, true);
                }
                boardIsFull = true;
            }
        }//fillPlayField

        //Släpper ner en collumn av block. t = tid det ska ta att falla.
        public void fillPlayFieldRow(int column, double t, bool block)
        {
            Storyboard sb = new Storyboard();
            int count = 0;
            int children = 0;
            _shapeSelected = load_Figures(column);
            window.canvas1.Children.Add(_shapeSelected);
            children++;
            animationFigures(children, sb, t);
            sb.Completed += delegate
            {
                _shapeSelected = load_Figures(column);
                window.canvas1.Children.Add(_shapeSelected);
                children++;
                animationFigures(children, sb, t);
                count++;
                sb.Completed += delegate
                {
                    if (count >= _nrOfBlocks - 1)
                    {
                        sb.Stop();
                        if (block)
                        {
                            window.BlockClicks.Visibility = Visibility.Hidden;
                            window.restartFullscreen.IsEnabled = true;
                        }
                    }

                };
                sb.Begin();
            };
            sb.Begin();

        }//fillPlayFieldRow

    
        private void animationFigures(int children, Storyboard sb, double t)
        {
            Duration duration = new Duration(TimeSpan.FromSeconds(t));
            DoubleAnimation anim = new DoubleAnimation(0-_rectSize, window.canvas1.ActualHeight - ((_rectSize * (children))), duration, FillBehavior.Stop);
            anim.AccelerationRatio = 1;
            Canvas.SetTop(_shapeSelected, window.canvas1.ActualHeight - (_rectSize * (children)));
            sb.Children.Add(anim);
            Storyboard.SetTargetProperty(anim, new PropertyPath(Canvas.TopProperty));
            Storyboard.SetTarget(sb, _shapeSelected);
        }//animationFigures

        public delegate void EnabledChanged();

        //Blockerar så att man inte kan aktivera hover eller klicka på rektanglarna
        public void Blockclickproperly()
        {
            while (_numberOfFalling > 0) ;
            Thread.Sleep(25);

            if (window.BlockClicks.Dispatcher.CheckAccess())
            {
                window.BlockClicks.Visibility = Visibility.Hidden;

                while (window.BlockClicks.Visibility == Visibility.Visible);

                bool ok = func.checkForPossible(que2, _rectSize);
                if (!ok)
                {
                    _Timer.Stop();
                    window.canvas1.Children.Clear();
                    boardIsFull = false;
                    fillPlayField();
                    _Timer.Start();
                }
                window.lbl.Content = "";
            }
            else
            {
                window.BlockClicks.Dispatcher.Invoke(
                    System.Windows.Threading.DispatcherPriority.Normal,
                    new EnabledChanged(this.Blockclickproperly));
            }
        }//Blockclickproperly

        private int RandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }//RandomNumber

        //skapar angivet "antal" block som blir slumpad mellan 4 olika färger.
        public Rectangle load_Figures(int antal)
        {
            Brush[] br = new Brush[6]{ 
                new ImageBrush(new BitmapImage(new Uri(@"Bilder\red.png", UriKind.Relative))),
                new ImageBrush(new BitmapImage(new Uri(@"Bilder\blue.png", UriKind.Relative))),
                new ImageBrush(new BitmapImage(new Uri(@"Bilder\yellow.png", UriKind.Relative))),
                new ImageBrush(new BitmapImage(new Uri(@"Bilder\green.png", UriKind.Relative))),
                new ImageBrush(new BitmapImage(new Uri(@"Bilder\purple.png", UriKind.Relative))),
                new ImageBrush(new BitmapImage(new Uri(@"Bilder\deepblue.png", UriKind.Relative)))                 
                };

            Rectangle rt = new Rectangle();
            int rndNr = RandomNumber(0, _numberOfColors);
            rt.Fill = br[rndNr];
            rt.Height = _rectSize;
            rt.Width = _rectSize;
            rt.VerticalAlignment = VerticalAlignment.Center;
            rt.HorizontalAlignment = HorizontalAlignment.Center;
            rt.MouseDown += figur_Click;
            _shapeSelected = (Rectangle)rt;
            rt.MouseEnter += mouseEnter;
            rt.MouseLeave += mouseLeave;

            Canvas.SetLeft(_shapeSelected, _rectSize * antal - _rectSize);
            Canvas.SetTop(_shapeSelected, 0);

            return _shapeSelected;
        }//load_Figures

      
        //Räknar ut poängen för antalet block man klickat på. Så, blocks+nuvarandepoäng+blocks-3
        public int scorePoints(int blocks)
        {
            int pointsNow = 0;
            string tal = window.Points.Content.ToString();
            int.TryParse(tal, out pointsNow);
            return pointsNow + blocks + (blocks - 3);
        }//scorePoints

        //Utökar tiden Beroende på hur många block som man har tagit bort och hur mycket poäng man har.
        public double addTime(int antal)
        {
            double time = 0;
            int points = (int)window.Points.Content;
            if (points < 100)
            {
                time = Math.Pow((antal - 2), 1.1);
            }
            else if (points < 400 && points >= 100)
            {
                time = (Math.Pow((antal - 2), 1.2)) * 0.3;
            }
            else if (points < 600 && points >= 400)
            {
                time = (Math.Pow((antal - 2), 1.2)) * 0.2;
            }
            else if (points < 700 && points >= 600)
            {
                time = (Math.Pow((antal - 2), 1.2)) * 0.15;
            }
            else
            {
                time = (Math.Pow((antal - 2), 1.2)) * 0.1;
            }
            return time;
        }//addTime

        
        public void ShowLblTime(double time)
        {

            window.lbl.Content = Convert.ToInt32(time).ToString() + "s";
            if (time < 1)
                window.lbl.Content = Convert.ToString(string.Format("{0:0.00}", time)) + "s";
            window.lbl.Foreground = Brushes.White;
            Point p = Mouse.GetPosition(window.canvas2);
            Canvas.SetTop(window.lbl, p.Y - 20);
            Canvas.SetLeft(window.lbl, p.X-10);


            Storyboard sb = new Storyboard();
            Duration duration = new Duration(TimeSpan.FromSeconds(1));
            DoubleAnimation anim = new DoubleAnimation(p.Y-20, p.Y-80, duration, FillBehavior.Stop);
            sb.Children.Add(anim);
            Storyboard.SetTargetProperty(anim, new PropertyPath(Canvas.TopProperty));
            Storyboard.SetTarget(sb, window.lbl);
            sb.Begin();

        }
        //tar bort blocken som man håller över och flyttar ner blocken som är över.
        private void figur_Click(object sender, RoutedEventArgs e)
        {
            if (IsHover)
            {
                window.BlockClicks.Visibility = Visibility.Visible;
                window.Points.Content = scorePoints(que.Count);
                window.scoreFS.Content = window.Points.Content;
                double timeTemp = _time;
                _time += addTime(que.Count);
                timeTemp = _time - timeTemp;
                ShowLblTime(timeTemp);
                updateTime();
            }
            
            rektangelInfo rekinfo = new rektangelInfo();
            for (int i = 0; i < que.Count; i++)
            {
                
                rekinfo = (rektangelInfo)que.Dequeue();
                Rectangle rekt = rekinfo.rekt;
                window.canvas1.Children.Remove(rekt);
                que.Enqueue(rekinfo);
                
               
            }
            double rekLeft = 0;
            int queSize = que.Count;

            if (queSize > 2)
            {
                _player.dropBlock();
            }
            List<Rectangle> columRek = new List<Rectangle>();
            while (que.Count != 0)
            {

                for (int i = 0; i < queSize; i++)
                {
                    rekinfo = (rektangelInfo)que.Dequeue();
                    Rectangle rekt = rekinfo.rekt;

                    if (columRek.Count == 0 || rekLeft == Canvas.GetLeft(rekt))
                    {
                        window.canvas1.Children.Remove(rekt);
                        rekLeft = Canvas.GetLeft(rekt);
                        columRek.Add(rekt);
                    }
                    else
                    {
                        que.Enqueue(rekinfo);
                    }
                }
                queSize = que.Count;
                int columnCount = columRek.Count;
                double bottom = 0;
                for (int i = 0; i < columnCount; i++)
                {
                    Rectangle tangel = columRek[i];

                    if (bottom < Canvas.GetTop(tangel))
                    {
                        bottom = Canvas.GetTop(tangel);
                    }
                }
                Point pt = new Point(rekLeft + 1, bottom - _rectSize + 1);
                columRek.Clear();
                while (pt.X < maxX && pt.X > minX && pt.Y < maxY && pt.Y > minY)
                {
                    HitTestResult hr = VisualTreeHelper.HitTest(window.canvas1, pt);

                    if (hr.VisualHit is Rectangle)
                    {
                        Rectangle re = (Rectangle)hr.VisualHit;
                        Storyboard sb = new Storyboard();
                        fallDown(Canvas.GetTop(re), Canvas.GetLeft(re), sb, re, bottom, 0.15);

                        ++_numberOfFalling;

                        sb.Completed += delegate {
                            --_numberOfFalling;
                        };
                        sb.Begin();
                        bottom -= _rectSize;
                        pt.Y -= _rectSize;
                    }
                    else
                    {
                        pt.Y -= _rectSize;
                        
                    }
                }
                int stepsDown = columnCount;
                for (int i = 0; i < columnCount; i++)
                {
                    _shapeSelected = load_Figures(Convert.ToInt32((pt.X) / _rectSize + 1));
                    window.canvas1.Children.Add(_shapeSelected);
                    Storyboard sb = new Storyboard();
                    animationFigures((_nrOfBlocks+1) - stepsDown, sb, 0.15);

                    sb.Completed += delegate
                    {
                        
                    };
                    sb.Begin(); 

                    sb.Begin();

                    stepsDown--;
                }
            }

            if (IsHover)
            {
                var mChanger = new System.Threading.Thread(new System.Threading.ThreadStart(Blockclickproperly));
                mChanger.Start();
            }
            IsHover = false;
        }//figur_Click

      

        public void fallDown(double top, double left, Storyboard sb, Rectangle r, double bottom, double t)
        {
            Duration duration = new Duration(TimeSpan.FromSeconds(t));
            DoubleAnimation anim = new DoubleAnimation(top, bottom, duration, FillBehavior.Stop);
            sb.Children.Add(anim);
            Canvas.SetTop(r, bottom);
            Storyboard.SetTargetProperty(anim, new PropertyPath(Canvas.TopProperty));
            Storyboard.SetTarget(sb, r);
        }//fallDown

        private void mouseClick(Rectangle rec, Storyboard sb)
        {
            Rectangle re = rec;
            ScaleTransform scale = new ScaleTransform(1.0, 1.0);
            re.RenderTransformOrigin = new Point(0.5, 0.5);
            re.RenderTransform = scale;

            Duration mytime = new Duration(TimeSpan.FromSeconds(0.15));

            //DoubleAnimation danim1 = new DoubleAnimation(1, 0, mytime, FillBehavior.Stop);
            DoubleAnimation danim2 = new DoubleAnimation(1, 0, mytime);
            //sb.Children.Add(danim1);
            sb.Children.Add(danim2);

            //Storyboard.SetTarget(danim1, re);
            //Storyboard.SetTargetProperty(danim1, new PropertyPath("RenderTransform.(ScaleTransform.ScaleX)"));

            Storyboard.SetTarget(danim2, re);
            Storyboard.SetTargetProperty(danim2,
                new PropertyPath("RenderTransform.(ScaleTransform.ScaleY)"));

            //sb.Begin();
        }
        
        private void mouseEnter(object sender, MouseEventArgs e)
        {
            IsHover = true;
            Rectangle entered = (Rectangle)sender;
            double top = Canvas.GetTop(entered);
            double left = Canvas.GetLeft(entered);
            ImageBrush enteredImage = (ImageBrush)entered.Fill;
            func.connectedColors(enteredImage, left + _rectSize / 2, top + _rectSize / 2, _rectSize, que);
            if (que.Count < 3)
            {
                object obj = new object();
                int queee = que.Count;
                for (int i = 0; i < queee; i++)
                {
                    rektangelInfo rekinfo = new rektangelInfo();
                    rekinfo = (rektangelInfo)que.Dequeue();
                    Brush color = rekinfo.br;
                    Rectangle rekt = rekinfo.rekt;
                    rekt.Fill = color;
                    IsHover = false;
                }
            }
        }//mouseEnter

        private void mouseLeave(object sender, MouseEventArgs e)
        {
            int queee = que.Count;
            for (int i = 0; i < queee; i++)
            {
                rektangelInfo rekinfo = new rektangelInfo();
                rekinfo = (rektangelInfo)que.Dequeue();
                Brush color = rekinfo.br;
                Rectangle rekt = rekinfo.rekt;
                rekt.Fill = color;
            }
        }//mouseLeave
        private void updateTime()
        {
            window.timeClock.Content = string.Format("0{0}:{1}", (int)(_time / 60), (int)(_time % 60));
            window.timeFS.Content = window.timeClock.Content;
            if (_time > 9)
            {
                window.timeFS.Foreground = Brushes.White;
                window.timeClock.Foreground = Brushes.White;
            }
        }
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
