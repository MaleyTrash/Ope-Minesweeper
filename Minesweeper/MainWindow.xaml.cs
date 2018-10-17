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

namespace Minesweeper
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Normalier_Click(object sender, RoutedEventArgs e)
        {
            NewGame(5, 5, 3);
        }

        private void Normal_Click(object sender, RoutedEventArgs e)
        {
            NewGame(10, 10, 10);
        }

        private void Normaler_Click(object sender, RoutedEventArgs e)
        {
            NewGame(15, 15, 15);
        }

        private void Normalest_Click(object sender, RoutedEventArgs e)
        {
            NewGame(30, 30, 200);
        }

        private void NewGame(int width, int height, int mines)
        {
            Game h = new Game(MainGrid);

            h.Start(width, height, mines);
        }
    }
}
