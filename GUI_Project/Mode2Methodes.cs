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

namespace GUI_Project.Methodes
{
    public class Mode2Methodes
    {
        double maxX = 248;
        double minX = 0;
        double maxY = 355;
        double minY = 0;
        int _rectSize = 26;
        Queue que = new Queue();
        Queue que2 = new Queue();
        public bool boardIsFull = false;
        Rectangle _shapeSelected = null;
        Random random = new Random();
        bool IsHover = false;
        MainWindow window;



        //Fyller spelfältet med block i collumner som faller i olika hastighet.
        public void fillPlayField(MainWindow win)
        {
            window = win;
            window.BlockClicks.Visibility = Visibility.Visible;
            if (boardIsFull == false)
            {
                for (int i = 1; i <= 10; i++)
                {
                    if (i % 2 == 0)
                        fillPlayFieldRow(i, 0.1, false, window);
                    else if (i % 3 == 0)
                        fillPlayFieldRow(i, 0.09, false, window);
                    else
                        fillPlayFieldRow(i, 0.11, true, window);
                }
                boardIsFull = true;
            }
        }//fillPlayField

        //Släpper ner en collumn av block. Collumn=rad och t = tid det ska ta att falla.
        public void fillPlayFieldRow(int rad, double t, bool block, MainWindow win)
        {
            Storyboard sb = new Storyboard();
            int count = 0;
            int children = 0;
            _shapeSelected = load_Figures(rad);
            win.canvas1.Children.Add(_shapeSelected);
            children++;
            animationFigures(children, sb, t, win);

            sb.Completed += delegate
            {
                _shapeSelected = load_Figures(rad);
                win.canvas1.Children.Add(_shapeSelected);
                children++;
                animationFigures(children, sb, t, win);
                count++;
                sb.Completed += delegate
                {
                    if (count >= 14 - 1)
                    {
                        sb.Stop();
                        if (block)
                        {
                            win.BlockClicks.Visibility = Visibility.Hidden;
                        }
                    }

                };
                sb.Begin();
            };
            sb.Begin();

        }//fillPlayFieldRow

        private void animationFigures(int children, Storyboard sb, double t, MainWindow win)
        {
            double fallHeight = win.canvas1.ActualHeight - ((_rectSize * (children)));
            Duration duration = new Duration(TimeSpan.FromSeconds(t));
            DoubleAnimation anim = new DoubleAnimation(0, win.canvas1.ActualHeight - ((_rectSize * (children))), duration, FillBehavior.Stop);
            anim.AccelerationRatio = 1;
            Canvas.SetTop(_shapeSelected, win.canvas1.ActualHeight - (_rectSize * (children)));
            sb.Children.Add(anim);
            Storyboard.SetTargetProperty(anim, new PropertyPath(Canvas.TopProperty));
            Storyboard.SetTarget(sb, _shapeSelected);
        }//animationFigures

        public delegate void EnabledChanged();

        //Blockerar så att man inte kan aktivera hover eller klicka på rektanglarna
        public void Blockclickproperly()
        {
            System.Threading.Thread.Sleep(160);
            if (window.BlockClicks.Dispatcher.CheckAccess())
            {
                window.BlockClicks.Visibility = Visibility.Hidden;
                System.Threading.Thread.Sleep(10);
                bool ok = checkForPossible();
                if (!ok)
                {
                    window._Timer.Stop();
                    window.canvas1.Children.Clear();
                    boardIsFull = false;
                    fillPlayField(window);
                    window._Timer.Start();
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
            Brush[] br = new Brush[4]{ 
                new ImageBrush(new BitmapImage(new Uri(@"Bilder\blue.png", UriKind.Relative))),
                new ImageBrush(new BitmapImage(new Uri(@"Bilder\yellow.png", UriKind.Relative))),
                new ImageBrush(new BitmapImage(new Uri(@"Bilder\green.png", UriKind.Relative))),
                new ImageBrush(new BitmapImage(new Uri(@"Bilder\red.png", UriKind.Relative))),

                };

            Rectangle rt = new Rectangle();
            int rndNr = RandomNumber(0, 4);
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

        //Kollar ifall det finns fler möjliga moves.
        public bool checkForPossible()
        {
            foreach (Rectangle childs in window.canvas1.Children)
            {
                que2.Clear();
                bool ok = checkForNeighbor((ImageBrush)childs.Fill, Canvas.GetLeft(childs) + _rectSize / 2, Canvas.GetTop(childs) + _rectSize / 2);
                if (ok)
                    return true;
            }
            return false;
        }//checkForPossible

        //Kollar ifall blocket har 3 eller fler grannar.
        public bool checkForNeighbor(ImageBrush pressedColor, double left, double top)
        {
            Point pt = new Point(left, top);
            object obj = new object();

            if (pt.X < maxX && pt.X > minX && pt.Y < maxY && pt.Y > minY)
            {
                HitTestResult hr = VisualTreeHelper.HitTest(window.canvas1, pt);
                obj = hr.VisualHit;

                if (obj is Rectangle && !que2.Contains(pt))
                {
                    Rectangle figur = (Rectangle)obj;
                    ImageBrush figurImage = (ImageBrush)figur.Fill;
                    string pressedImagestring = Convert.ToString(pressedColor.ImageSource);
                    string figurImageString = Convert.ToString(figurImage.ImageSource);
                    if (figurImageString == pressedImagestring)
                    {
                        que2.Enqueue(pt);
                        checkForNeighbor(pressedColor, pt.X + _rectSize, pt.Y);
                        checkForNeighbor(pressedColor, pt.X, pt.Y + _rectSize);
                        checkForNeighbor(pressedColor, pt.X, pt.Y - _rectSize);
                        checkForNeighbor(pressedColor, pt.X - _rectSize, pt.Y);
                    }
                }
            }
            if (que2.Count > 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }//checkForNeighbor

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
                time = Math.Pow((antal - 2), 1.2);
            }
            else if (points < 200 && points >= 100)
            {
                time = (Math.Pow((antal - 2), 1.2)) * 0.7;
            }
            else if (points < 300 && points >= 200)
            {
                time = (Math.Pow((antal - 2), 1.2)) * 0.5;
            }
            else
            {
                time = (Math.Pow((antal - 2), 1.2)) * 0.2;
            }
            return time;
        }//addTime

        public void fallDown(double top, double left, Storyboard sb, Rectangle r, double bottom, double t)
        {
            double fallHeight = window.canvas1.ActualHeight + top;
            Duration duration = new Duration(TimeSpan.FromSeconds(t));
            DoubleAnimation anim = new DoubleAnimation(top, bottom, duration, FillBehavior.Stop);
            anim.AccelerationRatio = 1;
            sb.Children.Add(anim);
            Canvas.SetTop(r, bottom);
            Storyboard.SetTargetProperty(anim, new PropertyPath(Canvas.TopProperty));
            Storyboard.SetTarget(sb, r);
        }//fallDown

        public void connectedColors(ImageBrush pressedColor, double ptX, double ptY)
        {
            Point pt = new Point();
            object obj = new object();
            pt.X = ptX;
            pt.Y = ptY;

            if (pt.X < maxX && pt.X > minX && pt.Y < maxY && pt.Y > minY)
            {
                HitTestResult hr = VisualTreeHelper.HitTest(window.canvas1, pt);
                obj = hr.VisualHit;
            }

            if (obj is Rectangle)
            {
                Rectangle figur = (Rectangle)obj;
                ImageBrush figurImage = (ImageBrush)figur.Fill;
                string pressedImagestring = Convert.ToString(pressedColor.ImageSource);
                string figurImageString = Convert.ToString(figurImage.ImageSource);
                if (figurImageString == pressedImagestring)
                {
                    rektangelInfo rekinfo = new rektangelInfo();
                    rekinfo.br = pressedColor;
                    rekinfo.rekt = figur;
                    que.Enqueue(rekinfo);
                    double top = Canvas.GetTop(figur);
                    double left = Canvas.GetLeft(figur);
                    string str = "";
                    str = figurImageString.Remove(figurImageString.Length - 4, 4) + "H.png";
                    figur.Fill = new ImageBrush(new BitmapImage(new Uri(@str, UriKind.Relative)));
                    connectedColors(pressedColor, pt.X + _rectSize, pt.Y);
                    connectedColors(pressedColor, pt.X - _rectSize, pt.Y);
                    connectedColors(pressedColor, pt.X, pt.Y + _rectSize);
                    connectedColors(pressedColor, pt.X, pt.Y - _rectSize);
                }
            }
        }//connectedColors

        //tar bort blocken som man håller över och flyttar ner blocken som är över.
        private void figur_Click(object sender, RoutedEventArgs e)
        {
            if (IsHover)
            {
                window.BlockClicks.Visibility = Visibility.Visible;
                window.Points.Content = scorePoints(que.Count);
                window._time += addTime(que.Count);
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
            int queee = que.Count;
            while (que.Count != 0)
            {

                for (int i = 0; i < queee; i++)
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
                queee = que.Count;
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
            connectedColors(enteredImage, left + _rectSize / 2, top + _rectSize / 2);
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
