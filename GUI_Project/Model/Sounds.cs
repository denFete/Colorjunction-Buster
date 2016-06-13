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
using System.Windows.Media;
using Microsoft.Win32;

namespace GUI_Project.Model
{
    public class Sounds
    {
        MediaPlayer _player;
        public Sounds()
        {
             _player = new MediaPlayer();
        }

        public void dropBlock()
        {
            _player.Stop();
            _player.Open(new Uri(@"Sounds/Tick-DeepFrozenApps-397275646.mp3",UriKind.Relative));
            _player.Play();
        }
        public void fallingBlocks()
        {
            _player.Stop();
            _player.Open(new Uri(@"Sounds/Tick-DeepFrozenFall.mp3", UriKind.Relative));
            _player.Play();
        }
        public void fallingBlocksSmall()
        {
            _player.Stop();
            _player.Open(new Uri(@"Sounds/Tick-DeepFrozenFallSmall.mp3", UriKind.Relative));
            _player.Play();
        }
        public void fallingBlocksLarge()
        {
            _player.Stop();
            _player.Open(new Uri(@"Sounds/Tick-DeepFrozenFallLarge.mp3", UriKind.Relative));
            _player.Play();
        }
        public void volumeUp()
        {
            _player.Volume = 100;
        }
        public void volumeDown()
        {
            _player.Volume = 0;
        }
        public void menuClick()
        {
            _player.Stop();
            _player.Open(new Uri(@"Sounds/MenuClick.mp3", UriKind.Relative));
            _player.Play();
        }
        public void menuClickFail()
        {
            _player.Stop();
            _player.Open(new Uri(@"Sounds/MenuClickFail.mp3", UriKind.Relative));
            _player.Play();
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