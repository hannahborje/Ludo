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
           
            // Create the Grid
            Grid myGrid = new Grid();
            myGrid.Width = 800;
            myGrid.Height = 800;
;
           // myGrid.HorizontalAlignment = HorizontalAlignment.Left;
            //myGrid.VerticalAlignment = VerticalAlignment.Top;
            myGrid.ShowGridLines = true;

            // Define the Columns
            for (int i = 0; i < 5; i++) { 
                ColumnDefinition column = new ColumnDefinition();
                RowDefinition row = new RowDefinition();
                myGrid.ColumnDefinitions.Add(column);
                 myGrid.RowDefinitions.Add(row);
            }
           
            // Add the Grid as the Content of the Parent Window Object
            board.Content = myGrid;
            board.UpdateLayout();
            //board.myGrid = myGrid;
             //myGrid.Show();

            // Add the Ellipse to the StackPanel.
           // myGrid.Children.Add(myEllipse);

            //this.Content = myGrid;
            // Add the left text cell to the Grid
            myGrid.Children.Add(createGrid(1, 2, myGrid));
            myGrid.Children.Add(createGrid(2, 1, myGrid));
            myGrid.Children.Add(createGrid(2, 3, myGrid));
            myGrid.Children.Add(createGrid(3, 2, myGrid));

        }
        public Grid createGrid(int rowPos, int columnPos, Grid myGrid)
        {
            Grid smallGrid = new Grid();
            smallGrid.ShowGridLines = true;
            for (int i = 0; i < 4; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                RowDefinition row = new RowDefinition();
                smallGrid.ColumnDefinitions.Add(column);
                if (i < 3) { smallGrid.RowDefinitions.Add(row); }
            }

            
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Ellipse ell = createEllipse();
                    Grid.SetColumn(ell, j);
                    Grid.SetRow(ell, i);
                    smallGrid.Children.Add(ell);
                }
            }
            Grid.SetRow(smallGrid, rowPos);
            Grid.SetColumn(smallGrid, columnPos);
            return smallGrid;
            //myGrid.Children.Add(smallGrid);
        }

        public Ellipse createEllipse()
        {
            Ellipse myEllipse = new Ellipse();

            // Create a SolidColorBrush with a red color to fill the  
            // Ellipse with.
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();

            // Describes the brush's color using RGB values.  
            // Each value has a range of 0-255.
            mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
            myEllipse.Fill = mySolidColorBrush;
            myEllipse.StrokeThickness = 2;
            myEllipse.Stroke = Brushes.Black;

            // Set the width and height of the Ellipse.
            myEllipse.Width = 40;
            myEllipse.Height = 40;

           
            return myEllipse;
        }
           
            // This is the end
        }
    }
