using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot
{
    public class SquareLinkedCells
    {
        private Cell currentCell;
        private Cell initialCell;
        private int minX;
        private int maxX;
        private int minY;
        private int maxY;
        private Dictionary<String, Cell> cells = new Dictionary<String,Cell>();

        public int MinX { get { return minX; } }
        public int MaxX { get { return maxX; } }
        public int MinY { get { return minY; } }
        public int MaxY { get { return maxY; } }

        public Cell CurrentCell { get { return currentCell; } }
        public Cell InitialCell { get { return initialCell; } }
        public Dictionary<String, Cell> Cells { get { return cells; } }

        public void Add(Direction direction, Cell.CellStatus cellState)
        {
            if (GetCell(direction) != null)
            {
                throw new Exception();
            }
            else
            {
                Cell addedCell = null;
                switch (direction)
                {
                    case (Direction.Up):
                        currentCell.Up = new Cell(currentCell.X, currentCell.Y - 1, cellState);
                        addedCell = currentCell.Up;
                        addedCell.Down = currentCell;
                        minY = Math.Min(currentCell.Y - 1, minY);
                        break;
                    case (Direction.Down):
                        currentCell.Down = new Cell(currentCell.X, currentCell.Y + 1, cellState);
                        maxY = Math.Max(currentCell.Y + 1, maxY);
                        addedCell = currentCell.Down;
                        addedCell.Up = currentCell;
                        break;
                    case (Direction.Right):
                        currentCell.Right = new Cell(currentCell.X + 1, currentCell.Y, cellState);
                        maxX = Math.Max(currentCell.X + 1, maxX);
                        addedCell = currentCell.Right;
                        addedCell.Left = currentCell;
                        break;
                    case (Direction.Left):
                        currentCell.Left = new Cell(currentCell.X - 1, currentCell.Y, cellState);
                        minX = Math.Min(currentCell.X - 1, minX);
                        addedCell = currentCell.Left;
                        addedCell.Right = currentCell;
                        break;
                }
                cells.Add(addedCell.GetCurrentKey(), addedCell);
            }
        }

        public void ChangeCell(Direction direction)
        {
            Cell nextCell = GetCell(direction);
            if (nextCell != null)
            {
                currentCell = nextCell;
            }
            else
            {
                throw new Exception();
            }
        }

        public Cell GetCell(Direction direction)
        {
            Cell result = null;
            if (cells.ContainsKey(currentCell.GetKey(direction)))
            {
                result = cells[currentCell.GetKey(direction)];
            }
            return result;
        }

        public SquareLinkedCells(Cell initialCell)
        {
            this.initialCell = initialCell;
            cells.Add(initialCell.GetCurrentKey(), initialCell);
            currentCell = initialCell;
        }

        public Cell.CellStatus[,] ToArray()
        {
            int numberRows = maxX - minX + 1;
            int numberColumns = maxY - minY + 1;
            Cell.CellStatus[,] result = new Cell.CellStatus[numberRows, numberColumns];
            for (int currentRow = 0; currentRow < numberRows; currentRow ++)
            {
                for (int currentColumn = 0; currentColumn < numberColumns; currentColumn++)
                {
                    Cell cell = null;
                    if (cells.ContainsKey(Cell.GetKeyString(minX + currentRow, minY + currentColumn)))
                    {
                        cell = cells[Cell.GetKeyString(minX + currentRow, minY + currentColumn)];
                    }
                    result[currentRow, currentColumn] = cell == null ? Cell.CellStatus.Unknown : (cell==currentCell ? Cell.CellStatus.Robot : cell.CellState);
                }
            }
            return result;
        }
    }
}
