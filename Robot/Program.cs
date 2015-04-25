using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Robot
{
    class Program
    {        static void Main(string[] args)
        {
            String level = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"LevelDescription.txt"));
            PathFinder pathFinder = new PathFinder(new RobotImpl(ReadLevel(level)));
            try
            {
                pathFinder.PathFind();
            }
            catch (Exception) { }
        }

        private static Cell.CellStatus[,] ReadLevel(String level)
        {
            String[] rows = Regex.Split(level, "\r\n");
            int numberRows = rows.Length;
            int numberColumns = rows[0].Length;
            Cell.CellStatus[,] result = new Cell.CellStatus[numberRows, numberColumns];
            for (int currentRow = 0; currentRow < numberRows; currentRow++)
            {
                for (int currentColumn = 0; currentColumn < numberColumns; currentColumn++)
                {
                    switch (rows[currentRow][currentColumn])
                    {
                        case ('#'):
                            result[currentRow, currentColumn] = Cell.CellStatus.Wall;
                            break;
                        case (' '):
                            result[currentRow, currentColumn] = Cell.CellStatus.Empty;
                            break;
                        case ('E'):
                            result[currentRow, currentColumn] = Cell.CellStatus.Exit;
                            break;
                        case ('R'):
                            result[currentRow, currentColumn] = Cell.CellStatus.Robot;
                            break;
                    }
                }
            }
            return result;
        }
    }
}
