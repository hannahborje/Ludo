
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

        void Roll_Dice(object sender, RoutedEventArgs e)
        {
            Button b = e.Source as Button;
            Random num = new Random();
            int Number = num.Next(1, 7);
            dice newDice = new dice(Number);
            BitmapImage Img = new BitmapImage(new Uri("dice" + Number.ToString() + ".jpg", UriKind.Relative));


            imgdice.Source = Img;
            b.Foreground = Brushes.Red;
        }
        void Start_Game(object sender, RoutedEventArgs e)
        {
            Button b = e.Source as Button;
            b.Foreground = Brushes.Red;
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