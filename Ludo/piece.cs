using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Ludo
{
    public class Piece 
    {
        private int _x, _y;
        private String color;
       

        public Piece(String color, int startX, int startY){
            this.color = color;
            _x = startX;
            _y = startY;
     
    }
        public int X
        {
            get { return _x; }
            set
            {
                _x = value;
            }
        }
        public int Y
        {
            get { return _y; }
            set
            {
                _y = value;
          
            }
        }
     
        public BitmapImage getImage()
        {
            BitmapImage image = new BitmapImage(new Uri("/images/" + color + ".png", UriKind.Relative));
            return image;
        }
        public void movePiece(int steps, Dictionary<int, List<int>> pos)
        {
            if (pos.ContainsKey(steps))
            {
                X = pos[steps][0];
                Y = pos[steps][1];
               
                
            }
        }
     
    }
}
