﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;

namespace Minesweeper
{
    class Game
    {
        private bool started = false;
        private int mines;
        private int[,] field;

        private readonly Renderer _render;

        public Game(Grid grid)
        {
            _render = new Renderer(grid);
        }

        public void Start(int width, int height, int mines)
        {
            field = new int[width, height];
            this.mines = mines;

            _render.Setup(width, height, handleClick);
        }

        private void handleClick(object obj, RoutedEventArgs args)
        {
            MineButton h = obj as MineButton;
            ChangeField(h.x, h.y);
        }

        private void ChangeField(int x, int y)
        {
            if (!started)
            {
                GenerateField(x, y);
                ChangeEmpty(x, y);
                started = true;
                return;
            }

            if(field[x, y] >= 0)
            {
                ChangeEmpty(x, y);
            }

            if(field[x, y] == -1)
            {
                EndGame();
            }
        }

        private void EndGame()
        {
            for(int x = 0; x < field.GetLength(0); x++)
            {
                for(int y = 0; y < field.GetLength(1); y++)
                {
                    if (field[x, y] < -1) continue;
                    _render.DisplayTile(x, y, field[x, y]);
                }
            }

            MessageBox.Show("Your're are lost!", "h", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ChangeEmpty(int x, int y)
        {
            int first = field[x, y];
            if(first >= 0)
            {
                _render.DisplayTile(x, y, first);
                field[x, y] = -2;
            }

            // Cancel if first is a number
            if(first > 0)
            {
                return;
            }

            CheckNeighbours(x, y, (hitX, hitY) =>
            {
                int target = field[hitX, hitY];

                if (target == -1 || target == -2) return -1;

                _render.DisplayTile(hitX, hitY, target);
                field[hitX, hitY] = -2;
                if(target == 0)
                {
                    ChangeEmpty(hitX, hitY);
                }
                return 1;
            });
        }

        private void GenerateField(int blockX, int blockY)
        {
            GenerateMines(blockX, blockY);

            int xLength = field.GetLength(0);
            int yLength = field.GetLength(1);

            for (int x = 0; x < xLength; x++)
            {
                for(int y = 0; y < yLength; y++)
                {
                    if (field[x, y] == -1)
                    {
                        ChangeAround(x, y);
                    }
                }
            }
        }

        private void ChangeAround(int x, int y)
        {
            CheckAround(x, y, (hitX, hitY) =>
            {
                if (field[hitX, hitY] == -1) return -1;

                field[hitX, hitY] += 1;
                return 1;
            });
        }

        private void CheckAround(int x, int y, Func<int, int, int> onHit)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    int targetX = x + i;
                    int targetY = y + z;

                    if (!IsInBounds(targetX, targetY)) continue;

                    onHit(targetX, targetY);
                }
            }
        }

        private void CheckNeighbours(int x, int y, Func<int, int, int> onHit)
        {
            int[][] targets = new int[4][];

            targets[0] = new int[] { x, y - 1 };
            targets[1] = new int[] { x - 1, y };
            targets[2] = new int[] { x + 1, y };
            targets[3] = new int[] { x, y + 1 };

            foreach(int[] item in targets)
            {
                int tarX = item[0];
                int tarY = item[1];

                if (!IsInBounds(tarX, tarY)) continue;

                onHit(tarX, tarY);
            }
        }

        private bool IsInBounds(int x, int y)
        {
            if (x < 0 || x >= field.GetLength(0)) return false;
            if (y < 0 || y >= field.GetLength(1)) return false;
            return true;
        }

        private void GenerateMines(int x, int y)
        {
            Random ran = new Random();

            int xLength = field.GetLength(0);
            int yLength = field.GetLength(1);
            int maxMines = (xLength * yLength) - 1;

            int placed = 0;

            while (placed <= mines && placed <= maxMines)
            {
                int ranX = ran.Next(0, xLength);
                int ranY = ran.Next(0, yLength);

                if (ranX == x || ranY == y) continue;
                if (field[ranX, ranY] != 0) continue;

                field[ranX, ranY] = -1;
                placed++;
            }
        }
    }
}
