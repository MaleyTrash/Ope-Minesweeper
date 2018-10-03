using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;

namespace Minesweeper
{
    class Game
    {
        private TileType[,] tiles;

        private readonly Renderer _render;

        public Game(Grid grid)
        {
            _render = new Renderer(grid);
        }

        public void Start(int width, int height)
        {
            tiles = new TileType[width, height];

            _render.Setup(width, height, ass);
        }

        private void ass(object obj, RoutedEventArgs args)
        {
            _render.ChangeToText(1,1, TileType.Empty, "asswecan");
        }
    }
}
