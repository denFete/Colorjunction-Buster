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

    public class Mode3Methodes
    {
        double maxX = 260;
        double minX = 0;
        double maxY = 364;
        double minY = 0;
        double _rectSize;
        int _numberOfColors;
        int _nrOfBlocks;
        int _nrOfColumns;
        Queue que = new Queue();
        Queue que2 = new Queue();
        public bool boardIsFull = false;
        Rectangle _shapeSelected = null;
        Random random = new Random();
        bool IsHover = false;
        MainWindow window;
        deladeFunktioner func;
        Sounds _player;
        int _numberOfFalling = 0;

        public Mode3Methodes(int nrOfColors, int nrOfBlocks, int nrOfColumns, double rectSize, MainWindow win, Sounds player)
        {
            window = win;
            _player = player;
            func = new deladeFunktioner(window);
            window.classicGrid.Visibility = Visibility.Hidden;
            window.surivalGrid.Visibility = Visibility.Hidden;
            _numberOfColors = nrOfColors;
            _nrOfBlocks = nrOfBlocks;
            _nrOfColumns = nrOfColumns;
            _rectSize = rectSize;
        }

        //Fyller spelfältet med block i collumner som faller i olika hastighet.
        public void fillPlayField()
        {
            if(_nrOfColumns == 5)
            {
                _player.fallingBlocksSmall();
            }
            else if(_nrOfColumns == 10)
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

        //Släpper ner en collumn av block. Collumn=rad och t = tid det ska ta att falla.
        public void fillPlayFieldRow(int rad, double t, bool block)
        {
            Storyboard sb = new Storyboard();
            int count = 0;
            int children = 0;
            _shapeSelected = load_Figures(rad);
            window.canvas1.Children.Add(_shapeSelected);
            children++;
            animationFigures(children, sb, t);

            sb.Completed += delegate
            {
                _shapeSelected = load_Figures(rad);
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
            DoubleAnimation anim = new DoubleAnimation(0, window.canvas1.ActualHeight - ((_rectSize * (children))), duration, FillBehavior.Stop);
            anim.AccelerationRatio = 1;
            Canvas.SetTop(_shapeSelected, window.canvas1.ActualHeight - (_rectSize * (children)));
            sb.Children.Add(anim);
            Storyboard.SetTargetProperty(anim, new PropertyPath(Canvas.TopProperty));
            Storyboard.SetTarget(sb, _shapeSelected);
        }//animationFigures

        public delegate void EnabledChanged();

        private void checkForHoleLeft(double left, double top, int antal)
        {
            Storyboard sb = new Storyboard();
            Point pt = new Point(left, top);
            if (pt.X < maxX && pt.X > minX && pt.Y < maxY && pt.Y > minY)
            {
                Object obj = VisualTreeHelper.HitTest(window.canvas1, pt).VisualHit;

                if (obj is Rectangle)
                {
                    sb.Completed += delegate
                    {
                        Rectangle figur = (Rectangle)obj;
                        slideTogether(pt.X - _rectSize / 2, sb, figur, (pt.X - _rectSize / 2) + _rectSize * antal, 0.01);
                        Canvas.SetLeft(figur, (pt.X - _rectSize / 2) + _rectSize * antal);
                        double X = pt.X;
                        double Y = pt.Y - _rectSize;
                        while (X < maxX && X > minX && Y < maxY && Y > minY)
                        {
                            obj = VisualTreeHelper.HitTest(window.canvas1, new Point(X, Y)).VisualHit;
                            if (obj is Rectangle)
                            {
                                Rectangle figur2 = (Rectangle)obj;
                                slideTogether(pt.X - _rectSize / 2, sb, figur2, (pt.X - _rectSize / 2) + _rectSize * antal, 0.01);
                                Canvas.SetLeft(figur2, (X - _rectSize / 2) + _rectSize * antal);
                            }
                            Y -= _rectSize;
                        }
                    };
                    sb.Begin();
                    checkForHoleLeft(pt.X - _rectSize, pt.Y, antal);
                }
                else
                {
                    checkForHoleLeft(pt.X - _rectSize, pt.Y, ++antal);
                }
            }
        }//checkForHoleLeft

        private void checkForHoleRight(double left, double top, int antal)
        {
            Storyboard sb = new Storyboard();
            Point pt = new Point(left, top);
            if (pt.X < maxX && pt.X > minX && pt.Y < maxY && pt.Y > minY)
            {
                Object obj = VisualTreeHelper.HitTest(window.canvas1, pt).VisualHit;

                if (obj is Rectangle)
                {
                    sb.Completed += delegate
                    {
                        Rectangle figur = (Rectangle)obj;
                        slideTogether(pt.X - _rectSize / 2, sb, figur, (pt.X - _rectSize / 2) - _rectSize * antal, 0.01);
                        Canvas.SetLeft(figur, (pt.X - _rectSize / 2) - _rectSize * antal);
                        double X = pt.X;
                        double Y = pt.Y - _rectSize;
                        while (X < maxX && X > minX && Y < maxY && Y > minY)
                        {
                            obj = VisualTreeHelper.HitTest(window.canvas1, new Point(X + 3, Y)).VisualHit;
                            if (obj is Rectangle)
                            {
                                Rectangle figur2 = (Rectangle)obj;
                                slideTogether(X - _rectSize / 2, sb, figur2, (pt.X - _rectSize / 2) - _rectSize * antal, 0.01);
                                Canvas.SetLeft(figur2, (X - _rectSize / 2) - _rectSize * antal);
                            }
                            Y -= _rectSize;
                        }
                    };
                    sb.Begin();
                    checkForHoleRight(pt.X + _rectSize, pt.Y, antal);
                }
                else
                {
                    checkForHoleRight(pt.X + _rectSize, pt.Y, ++antal);
                }
            }
        }//checkForHoleRight

        public void slideTogether(double start, Storyboard sb, Rectangle r, double to, double t)
        {
            Duration duration = new Duration(TimeSpan.FromSeconds(t));
            DoubleAnimation anim = new DoubleAnimation(start, to, duration, FillBehavior.Stop);
            anim.AccelerationRatio = 1;
            sb.Children.Add(anim);
            Storyboard.SetTargetProperty(anim, new PropertyPath(Canvas.LeftProperty));
            Storyboard.SetTarget(sb, r);
            //sb.Begin();
        }//fallDown

        private void centerBlocks()
        {
            Thread.Sleep(20);
            if (window.canvas1.Dispatcher.CheckAccess())
            {
                int left = 0;
                int right = 0;
                Object obj = new Object();
                Point pt = new Point(minX + 13, maxY - 13);
                while (pt.X < maxX && pt.X > minX && pt.Y < maxY && pt.Y > minY)
                {
                    HitTestResult hr = VisualTreeHelper.HitTest(window.canvas1, pt);
                    obj = hr.VisualHit;

                    if (obj is Rectangle)
                    {
                        break;
                    }
                    left++;
                    pt.X += _rectSize;
                }
                pt.X = maxX - _rectSize / 2;
                while (pt.X < maxX && pt.X > minX && pt.Y < maxY && pt.Y > minY)
                {
                    HitTestResult hr = VisualTreeHelper.HitTest(window.canvas1, pt);
                    obj = hr.VisualHit;
                    if (obj is Rectangle)
                    {
                        break;
                    }
                    right++;
                    pt.X -= _rectSize;
                }
                if ((left == 0 && right == 1) || (right == 0 && left == 1) || right == left)
                {
                    return;
                }
                pt.X = maxX - _rectSize / 2;
                if (left < right - 1)
                {
                    while (pt.X < maxX && pt.X > minX && pt.Y < maxY && pt.Y > minY)
                    {
                        HitTestResult hr = VisualTreeHelper.HitTest(window.canvas1, pt);
                        obj = hr.VisualHit;
                        if (obj is Rectangle)
                        {
                            double pos = pt.X;
                            Canvas.SetLeft(obj as Rectangle, pos + _rectSize * (right - (left + 1)));
                            double Y = pt.Y - _rectSize;
                            while (Y > minY)
                            {
                                HitTestResult hr2 = VisualTreeHelper.HitTest(window.canvas1, new Point(pt.X, Y));
                                obj = hr2.VisualHit;
                                if (obj is Rectangle)
                                {
                                    Canvas.SetLeft(obj as Rectangle, pos + _rectSize * (right - (left + 1)));
                                }
                                Y -= _rectSize;
                            }
                        }
                        pt.X -= _rectSize;
                    }
                }
                else if (left > right + 1)
                {
                    pt.X = minX + _rectSize / 2;
                    while (pt.X < maxX && pt.X > minX && pt.Y < maxY && pt.Y > minY)
                    {
                        HitTestResult hr = VisualTreeHelper.HitTest(window.canvas1, pt);
                        obj = hr.VisualHit;
                        if (obj is Rectangle)
                        {
                            double pos = pt.X;
                            Canvas.SetLeft(obj as Rectangle, pos - _rectSize * (left - (right + 1)));
                            double Y = pt.Y - _rectSize;
                            while (Y > minY)
                            {
                                window.canvas1.Dispatcher.CheckAccess();
                                HitTestResult hr2 = VisualTreeHelper.HitTest(window.canvas1, new Point(pt.X, Y));
                                obj = hr2.VisualHit;
                                if (obj is Rectangle)
                                {
                                    Canvas.SetLeft(obj as Rectangle, pos - _rectSize * (left - (right + 1)));
                                }
                                Y -= _rectSize;
                            }
                        }
                        pt.X += _rectSize;
                    }
                }
            }
            else
            {
                window.canvas1.Dispatcher.Invoke(
                     System.Windows.Threading.DispatcherPriority.Normal,
                     new EnabledChanged(this.centerBlocks));
            }
        }//centerBlocks

        public void checkForHoleThread()
        {
            if (window.canvas1.Dispatcher.CheckAccess())
            {
                if (_nrOfColumns > 5)
                {
                    checkForHoleLeft((maxX / 2 - _rectSize / 2), maxY - _rectSize / 2, 0);
                    checkForHoleRight((maxX / 2 + _rectSize / 2), maxY - _rectSize / 2, 0);
                }
                else
                {
                    checkForHoleLeft((maxX / 2 - _rectSize), maxY - _rectSize / 2, 0);
                    checkForHoleRight((maxX / 2), maxY - _rectSize / 2, 0);
                }
            }
            else
            {
                window.BlockClicks.Dispatcher.Invoke(
                    System.Windows.Threading.DispatcherPriority.Normal,
                    new EnabledChanged(this.checkForHoleThread));
            }
        }//checkForHoleThread

        //Blockerar så att man inte kan aktivera hover eller klicka på rektanglarna
        public void Blockclickproperly()
        {
            while (_numberOfFalling>0);

            //dra ihop ---------------------------------------
            Thread pullTogether = new Thread(new ThreadStart(checkForHoleThread));
            pullTogether.Start();

            while (pullTogether.ThreadState == System.Threading.ThreadState.Running) ;

            System.Threading.Thread.Sleep(70);
            //centerBlocks();
            if (window.BlockClicks.Dispatcher.CheckAccess())
            {
                window.BlockClicks.Visibility = Visibility.Hidden;
                while (window.BlockClicks.Visibility == Visibility.Visible) ;
                func.checkForPossible(que2, _rectSize);
                bool ok = func.checkForPossible(que2, _rectSize);
                if (!ok)
                {
                    window.canvas1.Children.Clear();
                    boardIsFull = false;
                    fillPlayField();
                }
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

        public void fallDown(double top, double left, Storyboard sb, Rectangle r, double bottom, double t)
        {
            Duration duration = new Duration(TimeSpan.FromSeconds(t));
            DoubleAnimation anim = new DoubleAnimation(top, bottom, duration, FillBehavior.Stop);
            anim.AccelerationRatio = 1;
            sb.Children.Add(anim);
            Canvas.SetTop(r, bottom);
            Storyboard.SetTargetProperty(anim, new PropertyPath(Canvas.TopProperty));
            Storyboard.SetTarget(sb, r);
        }//fallDown

        //tar bort blocken som man håller över och flyttar ner blocken som är över.
        private void figur_Click(object sender, RoutedEventArgs e)
        {
            if (IsHover)
            {
                window.BlockClicks.Visibility = Visibility.Visible;
            }
            List<Rectangle> columRek = new List<Rectangle>();
            rektangelInfo rekinfo = new rektangelInfo();
            for (int i = 0; i < que.Count; i++)
            {
                rekinfo = (rektangelInfo)que.Dequeue();
                Brush color = rekinfo.br;
                Rectangle rekt = rekinfo.rekt;
                que.Enqueue(rekinfo);
                window.canvas1.Children.Remove(rekt);
            }
            double rekLeft = 0;
            int queSize = que.Count;
            if (queSize > 2)
            {
                _player.dropBlock();
            }
            while (que.Count != 0)
            {
                for (int i = 0; i < queSize; i++)
                {
                    rekinfo = (rektangelInfo)que.Dequeue();
                    Rectangle rekt = rekinfo.rekt;

                    if (columRek.Count == 0 || rekLeft == Canvas.GetLeft(rekt))
                    {
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
                        fallDown(Canvas.GetTop(re), Canvas.GetLeft(re), sb, re, bottom, 0.1);
                        ++_numberOfFalling;
                        sb.Completed += delegate
                        {
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
            }
            if (IsHover)
            {
                var mChanger = new System.Threading.Thread(new System.Threading.ThreadStart(Blockclickproperly));
                mChanger.Start();
            }
            IsHover = false;
        }//figur_Click

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
