using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Ludo
{
    class piece : INotifyPropertyChanged
    {
        private int _x, _y;
        private String color;
        public event PropertyChangedEventHandler PropertyChanged;

        public piece(String color, int startX, int startY){
            this.color = color;
            _x = startX;
            _y = startY;
     
    }
        public int x
        {
            get { return _x; }
            set
            {
                _x = value;
                OnPropertyChanged("x");
            }
        }
        public int y
        {
            get { return _y; }
            set
            {
                _y = value;
                OnPropertyChanged("y");
            }
        }
        protected void OnPropertyChanged(string property)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(property));
            }
        }
        public BitmapImage getImage()
        {
            BitmapImage image = new BitmapImage(new Uri("/images/" + color + ".jpg", UriKind.Relative));
            return image;
        }
     
    }
}
