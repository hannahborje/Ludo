
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
        public Dictionary<int, List<int>> positions = new Dictionary<int, List<int>>(){

                {1, new List<int> {0,4}}, {2, new List<int> {1,4}}, {3, new List<int> {2,4}}, {4, new List<int> {3,4}},       
                {5, new List<int> {4,4}}, {6, new List<int> {4,3}}, {7, new List<int> {4,2}}, {8, new List<int> {4,1}},
                {9, new List<int> {4,0}}, {10, new List<int> {5,0}}, {11, new List<int> {6,0}}, {12, new List<int> {6,1}},               
                {13, new List<int> {6,2}}, {14, new List<int> {6,3}}, {15, new List<int> {6,4}}, {16, new List<int> {7,4}},
                {17, new List<int> {8,4}}, {18, new List<int> {9,4}}, {19, new List<int> {10,4}}, {20, new List<int> {10,5}},                                                                                              
                {21, new List<int> {10,6}}, {22, new List<int> {9,6}}, {23, new List<int> {8,6}}, {24, new List<int> {7,6}},                                               
                {25, new List<int> {6,6}}, {26, new List<int> {6,7}}, {27, new List<int> {6,8}}, {28, new List<int> {6,9}},
                {29, new List<int> {6,10}}, {30, new List<int> {5,10}}, {31, new List<int> {4,10}}, {32, new List<int> {4,9}},                                                                                           
                {33, new List<int> {4,8}}, {34, new List<int> {4,7}}, {35, new List<int> {4,6}}, {36, new List<int> {3,6}},                                               
                {37, new List<int> {2,6}}, {38, new List<int> {1,6}}, {39, new List<int> {0,6}}, {40, new List<int> {0,5}},
             
                {41, new List<int> {5,1}},
                {42, new List<int> {5,2}},
                {43, new List<int> {5,3}},
                {44, new List<int> {5,4}},

                {45, new List<int> {9,5}},
                {46, new List<int> {8,5}},
                {47, new List<int> {7,5}},
                {48, new List<int> {6,5}},

                {49, new List<int> {5,9}},
                {50, new List<int> {5,8}},
                {51, new List<int> {5,7}},
                {52, new List<int> {5,6}},

                {53, new List<int> {1,5}},
                {54, new List<int> {2,5}},
                {55, new List<int> {3,5}},
                {56, new List<int> {4,5}},

                
        };

    
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
            int number = newDice.roll_dice();
            BitmapImage Img = new BitmapImage(new Uri("/images/dice" + number.ToString() + ".jpg", UriKind.Relative));
            imgdice.Source = Img;

            Pieces[2].movePiece(number, positions);
            Console.WriteLine("Värde x: " + Pieces[2].X + " värde y: " + Pieces[2].Y);
            Pieces = pieces;
           
        }
        void Start_Game(object sender, RoutedEventArgs e)
        {
            pieces = new List<Piece>();

            //Adding every piece and positioning them in nest
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
           
            //Show images of pieces on the board
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