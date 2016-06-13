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
    public class deladeFunktioner
    {
        MainWindow window;
        double maxX = 260;
        double minX = 0;
        double maxY = 364;
        double minY = 0;

        public deladeFunktioner(MainWindow win)
        {
            window = win;
        }

        public void GetHighScore()
        {
            if (Convert.ToString(window.lblHighScoren.Content) == "Classic")
            {
                window.highScoreListFullscreen.Items.Clear();
                string line;
                StreamReader file = new StreamReader("Mode1.txt", System.Text.Encoding.GetEncoding(1252));
                while ((line = file.ReadLine()) != null)
                {
                    string[] nameParts = line.Split('|');
                    window.highScoreListFullscreen.Items.Add(new DataItem { Nr = nameParts[0], Player = nameParts[1], Score = Convert.ToInt32(nameParts[2]) });
                }
                file.Close();
            }
            else
            {
                window.highScoreListFullscreen.Items.Clear();
                string line;
                StreamReader file = new StreamReader("Mode2.txt", System.Text.Encoding.GetEncoding(1252));
                while ((line = file.ReadLine()) != null)
                {
                    string[] nameParts = line.Split('|');
                    window.highScoreListFullscreen.Items.Add(new DataItem { Nr = nameParts[0], Player = nameParts[1], Score = Convert.ToInt32(nameParts[2]) });
                }
                file.Close();
            }
        }//GetHighScore

        //Kollar ifall det finns fler möjliga moves.
        public bool checkForPossible(Queue que2, double _rectSize)
        {
            foreach (Rectangle childs in window.canvas1.Children)
            {
                que2.Clear();
                bool ok = checkForNeighbor((ImageBrush)childs.Fill, Canvas.GetLeft(childs) + _rectSize / 2, Canvas.GetTop(childs) + _rectSize / 2, que2, _rectSize);
                if (ok)
                    return true;
            }
            return false;
        }//checkForPossible

        //Kollar ifall blocket har 3 eller fler grannar.
        public bool checkForNeighbor(ImageBrush pressedColor, double left, double top, Queue que2, double _rectSize)
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
                        checkForNeighbor(pressedColor, pt.X + _rectSize, pt.Y, que2, _rectSize);
                        checkForNeighbor(pressedColor, pt.X, pt.Y + _rectSize, que2, _rectSize);
                        checkForNeighbor(pressedColor, pt.X, pt.Y - _rectSize, que2, _rectSize);
                        checkForNeighbor(pressedColor, pt.X - _rectSize, pt.Y, que2, _rectSize);
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

        public void connectedColors(ImageBrush pressedColor, double ptX, double ptY, double _rectSize, Queue que)
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
                    connectedColors(pressedColor, pt.X + _rectSize, pt.Y, _rectSize, que);
                    connectedColors(pressedColor, pt.X - _rectSize, pt.Y, _rectSize, que);
                    connectedColors(pressedColor, pt.X, pt.Y + _rectSize, _rectSize, que);
                    connectedColors(pressedColor, pt.X, pt.Y - _rectSize, _rectSize, que);
                }
            }
        }//connectedColors
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