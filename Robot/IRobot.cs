using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot
{
    public interface IRobot
    {
        void Move(Direction direction);
        Cell.CellStatus GetCurrentCellState();
        Cell.CellStatus GetNearbyCellState(Direction direction);
    }
}
