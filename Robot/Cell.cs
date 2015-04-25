using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot
{
    public class Cell
    {
        private int x;
        private int y;
        private CellStatus cellState;

        public Cell Right { get; set; }
        public Cell Left { get; set; }
        public Cell Up { get; set; }
        public Cell Down { get; set; }

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public CellStatus CellState
        {
            set { cellState = value; }
            get { return cellState; }
        }

        public Cell(int x, int y, CellStatus cellState)
        {
            this.x = x;
            this.y = y;
            this.cellState = cellState;
        }

        public String GetCurrentKey()
        {
            return GetKeyString(x, y);
        }

        public String GetKey(Direction direction)
        {
            switch(direction)
            {
                case(Direction.Right):
                    return GetKeyString(x + 1, y);
                case(Direction.Left):
                    return GetKeyString(x - 1, y);
                case(Direction.Up):
                    return GetKeyString(x, y - 1);
                case(Direction.Down):
                    return GetKeyString(x, y + 1);
                default:
                    return null;
            }
        }

        public static String GetKeyString(int x, int y)
        {
            return String.Concat(x, " ", y);
        }

        public enum CellStatus
        {
            Wall, Exit, Empty, Robot, Unknown, AlreadyKnown
        }
    }
}
