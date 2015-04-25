using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot
{
    class RobotImpl : IRobot
    {
        private Dictionary<String, Cell> realLevel = new Dictionary<String,Cell>();
        private Cell currentCell;

        public RobotImpl(Cell.CellStatus[,] realLevel)
        {
            for (int currentRow = 0; currentRow < realLevel.GetLength(0); currentRow++)
            {
                for (int currentColumn = 0; currentColumn < realLevel.GetLength(1); currentColumn++)
                {
                    Cell cell = null;
                    if (realLevel[currentRow, currentColumn] == Cell.CellStatus.Robot)
                    {
                        cell = new Cell(currentColumn, currentRow, Cell.CellStatus.Empty);
                        currentCell = cell;
                    }
                    else
                    {
                        cell = new Cell(currentColumn, currentRow, realLevel[currentRow, currentColumn]);
                    }
                    this.realLevel.Add(cell.GetCurrentKey(), cell);
                }
            }
        }

        public void Move(Direction direction)
        {
            Cell nextCell = GetNextCell(direction);
            if (nextCell.CellState != Cell.CellStatus.Wall)
            {
                currentCell = nextCell;
            }
            else
            {
                throw new Exception("You are trying to move the robot into wall");
            }
        }

        public Cell.CellStatus GetCurrentCellState()
        {
            return currentCell.CellState;
        }

        public Cell.CellStatus GetNearbyCellState(Direction direction)
        {
            
            return GetNextCell(direction).CellState;
        }

        private Cell GetNextCell(Direction direction)
        {
            String nextCellCode = currentCell.GetKey(direction);
            Cell nextCell = null;
            if (nextCellCode == null)
            {
                throw new NullReferenceException("The code of next cell is empty");
            }
            else
            {
                if(realLevel.ContainsKey(nextCellCode))
                {
                    nextCell = realLevel[nextCellCode];
                }
                else
                {
                    throw new NullReferenceException(String.Format("There is no cell with coordinate {0}", nextCellCode));
                }
            }
            return nextCell;
        }
    }
}
