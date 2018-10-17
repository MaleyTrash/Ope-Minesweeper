using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Minesweeper
{
    class Renderer
    {
        private readonly Grid _grid;
        private Button[,] _buttons; 

        public Renderer(Grid grid)
        {
            _grid = grid;
        }

        public void Setup(int width, int height, RoutedEventHandler callback)
        {
            Clear();

            _buttons = new Button[width, height];

            for(int x = 0; x < width; x++)
            {
                _grid.ColumnDefinitions.Add(new ColumnDefinition());
                for(int y = 0; y < height; y++)
                {

                    MineButton btn = new MineButton(x, y);
                    btn.Click += callback;
                    btn.MouseRightButtonUp += Flag;

                    Grid.SetColumn(btn, x);
                    Grid.SetRow(btn, y);

                    _grid.Children.Add(btn);

                    _buttons[x, y] = btn;   
                }
            }

            for(int y = 0; y < height; y++)
            {
                _grid.RowDefinitions.Add(new RowDefinition());
            }
        }

        private void Flag(object obj, RoutedEventArgs args)
        {
            MineButton h = obj as MineButton;
            DisplayTile(h.x, h.y, -3);
        }

        private void Clear()
        {
            _grid.Children.Clear();
            _grid.ColumnDefinitions.Clear();
            _grid.RowDefinitions.Clear();
        }

        public void DisplayTile(int x, int y, int num)
        {
            Button target = _buttons[x, y];

            switch(num)
            {
                case -3:
                    target.Content = 'F';
                    break;
                case -1:
                    target.Content = 'X';
                    target.Background = Brushes.Red;
                    break;
                case 0:
                    target.Content = "";
                    target.Background = Brushes.White;
                    break;
                default:
                    target.Content = num.ToString();
                    target.Background = Brushes.White;
                    break;
            }
        }
    }
}
