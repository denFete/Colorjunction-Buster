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
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace GUI_Project
{
    class WindowAspectRatio
    {
        private double _ratio;

        private WindowAspectRatio(Window window)
        {
            _ratio = window.Width / window.Height;
            ((HwndSource)HwndSource.FromVisual(window)).AddHook(DragHook);
        }

        public static void Register(Window window)
        {            
            new WindowAspectRatio(window);
        }

        internal enum WM
        {
            WINDOWPOSCHANGING = 0x0046,
        }

        [Flags()]
        public enum SWP
        {
            NoMove = 0x2,
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct WINDOWPOS
        {
            public IntPtr hwnd;
            public IntPtr hwndInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public int flags;
        }

        private IntPtr DragHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handeled)
        {
            if ((WM)msg == WM.WINDOWPOSCHANGING)
            {
                WINDOWPOS position = (WINDOWPOS)Marshal.PtrToStructure(lParam, typeof(WINDOWPOS));

                if ((position.flags & (int)SWP.NoMove) != 0 ||
                    HwndSource.FromHwnd(hwnd).RootVisual == null) return IntPtr.Zero;

                position.cx = (int)(position.cy * _ratio);

                Marshal.StructureToPtr(position, lParam, true);
                handeled = true;
            }

            return IntPtr.Zero;
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