
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Ludo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private List<Piece> pieces;
        public List<Piece> Pieces
        {
            get { return pieces;  }
            set { pieces = value; OnPropertyChanged("Pieces"); }

        }
        protected void OnPropertyChanged(string property)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(property));
            }
        }

        void Roll_Dice(object sender, RoutedEventArgs e)
        {
            Button b = e.Source as Button;
            Dice newDice = new Dice();
            BitmapImage Img = new BitmapImage(new Uri("/images/dice" + newDice.roll_dice() + ".jpg", UriKind.Relative));
            imgdice.Source = Img;

           
        }
        void Start_Game(object sender, RoutedEventArgs e)
        {
            pieces = new List<Piece>();

            Piece blue1 = new Piece("blue",1,1);
            pieces.Add(blue1);
            Piece blue2 = new Piece("blue",2,1 );
            pieces.Add(blue2);
            Piece blue3 = new Piece("blue",1,2);
            pieces.Add(blue3);
            Piece blue4 = new Piece("blue",2,2);
            pieces.Add(blue4);

            Piece lavender1 = new Piece("lavender", 8, 1);
            pieces.Add(lavender1);
            Piece lavender2 = new Piece("lavender", 9, 1);
            pieces.Add(lavender2);
            Piece lavender3 = new Piece("lavender", 8, 2);
            pieces.Add(lavender3);
            Piece lavender4 = new Piece("lavender", 9, 2);
            pieces.Add(lavender4);

            Piece lemon1 = new Piece("lemon", 8, 8);
            pieces.Add(lemon1);
            Piece lemon2 = new Piece("lemon", 9, 8);
            pieces.Add(lemon2);
            Piece lemon3 = new Piece("lemon", 8, 9);
            pieces.Add(lemon3);
            Piece lemon4 = new Piece("lemon", 9, 9);
            pieces.Add(lemon4);

            Piece green1 = new Piece("green", 1, 8);
            pieces.Add(green1);
            Piece green2 = new Piece("green", 2, 8);
            pieces.Add(green2);
            Piece green3 = new Piece("green", 1, 9);
            pieces.Add(green3);
            Piece green4 = new Piece("green", 2, 9);
            pieces.Add(green4);
             
            Pieces = pieces;
           
            BitmapImage blueImg1 = blue1.getImage();
            blueStart1.Source = blueImg1;
            BitmapImage blueImg2 = blue2.getImage();
            blueStart2.Source = blueImg2;
            BitmapImage blueImg3 = blue3.getImage();
            blueStart3.Source = blueImg3;
            BitmapImage blueImg4 = blue4.getImage();
            blueStart4.Source = blueImg4;
         
            BitmapImage lavenderImg1 = lavender1.getImage();
            lavenderStart1.Source = lavenderImg1;
            BitmapImage lavenderImg2 = lavender2.getImage();
            lavenderStart2.Source = lavenderImg2;
            BitmapImage lavenderImg3 = lavender3.getImage();
            lavenderStart3.Source = lavenderImg3;
            BitmapImage lavenderImg4 = lavender4.getImage();
            lavenderStart4.Source = lavenderImg4;

            BitmapImage lemonImg1 = lemon1.getImage();
            lemonStart1.Source = lemonImg1;
            BitmapImage lemonImg2 = lemon2.getImage();
            lemonStart2.Source = lemonImg2;
            BitmapImage lemonImg3 = lemon3.getImage();
            lemonStart3.Source = lemonImg3;
            BitmapImage lemonImg4 = lemon4.getImage();
            lemonStart4.Source = lemonImg4;

            BitmapImage greenImg1 = green1.getImage();
            greenStart1.Source = greenImg1;
            BitmapImage greenImg2 = green2.getImage();
            greenStart2.Source = greenImg2;
            BitmapImage greenImg3 = green3.getImage();
            greenStart3.Source = greenImg3;
            BitmapImage greenImg4 = green4.getImage();
            greenStart4.Source = greenImg4;
            
        }
        void Pause_Game(object sender, RoutedEventArgs e)
        {
            Button b = e.Source as Button;
            b.Foreground = Brushes.Red;
        }
        void Quit_Game(object sender, RoutedEventArgs e)
        {
            Button b = e.Source as Button;
            b.Foreground = Brushes.Red;
        }
        void Highscore(object sender, RoutedEventArgs e)
        {
            Button b = e.Source as Button;
            b.Foreground = Brushes.Red;
        }



    }
}