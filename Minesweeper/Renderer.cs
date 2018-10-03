using System.Windows;
using System.Windows.Controls;

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
                    _grid.RowDefinitions.Add(new RowDefinition());

                    Button btn = new Button();
                    btn.Click += callback;

                    Grid.SetColumn(btn, x);
                    Grid.SetRow(btn, y);

                    _grid.Children.Add(btn);

                    _buttons[x, y] = btn;   
                }
            }
        }

        private void Clear()
        {
            _grid.Children.Clear();
            _grid.ColumnDefinitions.Clear();
            _grid.RowDefinitions.Clear();
        }

        public void ChangeToText(int x, int y, TileType type, string text = "")
        {
            Button target = _buttons[x, y];

            _grid.Children.Remove(target);

            // Do something
        }
    }
}
