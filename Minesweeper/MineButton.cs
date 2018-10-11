using System.Windows.Controls;

namespace Minesweeper
{
    class MineButton : Button
    {
        public int x;
        public int y;

        public MineButton(int x, int y) : base()
        {
            this.x = x;
            this.y = y;
        }
    }
}
