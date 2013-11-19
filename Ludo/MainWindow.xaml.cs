
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

namespace Ludo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private List<piece> pieces;
        public List<piece> Pieces
        {
            get { return pieces; }
            set { pieces = value; }

        }

        void Roll_Dice(object sender, RoutedEventArgs e)
        {
            Button b = e.Source as Button;
            dice newDice = new dice();
            BitmapImage Img = new BitmapImage(new Uri("/images/dice" + newDice.roll_dice() + ".jpg", UriKind.Relative));
            imgdice.Source = Img;

           
        }
        void Start_Game(object sender, RoutedEventArgs e)
        {
            pieces = new List<piece>();

            piece red1 = new piece("red",1,1);
            pieces.Add(red1);
            piece red2 = new piece("red",2,1 );
            pieces.Add(red2);
            piece red3 = new piece("red",1,2);
            pieces.Add(red3);
            piece red4 = new piece("red",2,2);
            pieces.Add(red4);


            Binding columnBinding = new Binding("x");
            Binding rowBinding = new Binding("y");
            columnBinding.Source = red1;
            rowBinding.Source = red1;
        
            blueStart1.SetBinding(Grid.ColumnProperty, columnBinding);
            blueStart1.SetBinding(Grid.RowProperty, rowBinding);
            BitmapImage redImg = red1.getImage();
            blueStart1.Source = redImg;

       
           
           
           
            /*
            piece red1 = new piece("red");
            blueStart1.Source= red1.getImage();
            piece red2 = new piece("red");
            blueStart1.Source = red2.getImage();
            piece red3 = new piece("red");
            blueStart1.Source = red3.getImage();
            piece red4 = new piece("red");
            blueStart1.Source = red4.getImage();
            
            piece yellow1 = new piece("yellow");
            lavenderStart1.Source = yellow1.getImage();
            piece yellow2 = new piece("yellow");
            lavenderStart2.Source = yellow2.getImage();
            piece yellow3 = new piece("yellow");
            lavenderStart3.Source = yellow3.getImage();
            piece yellow4 = new piece("yellow");
            lavenderStart4.Source = yellow4.getImage();
            */
           
            //Button b = e.Source as Button;
            //b.Foreground = Brushes.Red;
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