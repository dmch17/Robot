using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot
{
    public class ConsoleView : IView
    {
        private readonly String wall = "#";
        private readonly String exit = "E";
        private readonly String robot = "R";
        private readonly String empty = " ";
        private readonly String alreadyKnown = "+";
        private readonly String unknown = "~";
        private int currentStep = 0;

        public void RenderLevel(Cell.CellStatus[,] levelInfo, bool isWaitAfterEachStep)
        {
            Console.WriteLine("Step {0}", ++currentStep);
            StringBuilder level = new StringBuilder();
            for (int currentRow = 0; currentRow < levelInfo.GetLength(1); currentRow++)
            {
                for (int currentColumn = 0; currentColumn < levelInfo.GetLength(0); currentColumn++)
                {
                    switch (levelInfo[currentColumn, currentRow])
                    {
                        case (Cell.CellStatus.Empty):
                            level.Append(empty);
                            break;
                        case (Cell.CellStatus.Exit):
                            level.Append(exit);
                            break;
                        case (Cell.CellStatus.Wall):
                            level.Append(wall);
                            break;
                        case (Cell.CellStatus.AlreadyKnown):
                            level.Append(alreadyKnown);
                            break;
                        case (Cell.CellStatus.Robot):
                            level.Append(robot);
                            break;
                        case (Cell.CellStatus.Unknown):
                            level.Append(unknown);
                            break;
                    }
                }
                level.Append("\n");
            }
            Console.WriteLine(level);
            if (isWaitAfterEachStep)
            {
                Console.ReadKey();
            }
        }

        public void ShowStartMessage(string message)
        {
            ShowMessage(message);
        }

        public void ShowFinalMessage(string message)
        {
            ShowMessage(message);
        }

        public void ShowErrorMessage(string message)
        {
            ShowMessage(message);
        }

        private void ShowMessage(String message)
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}
