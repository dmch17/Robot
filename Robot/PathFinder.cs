using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot
{
    public class PathFinder
    {
        private IView view = new ConsoleView();
        private IRobot robot;
        private SquareLinkedCells sensorLevel;
        private Stack<Direction> memory = new Stack<Direction>();
        private readonly Boolean isWaitAfterEachStep = false;

        public PathFinder(IRobot robot)
        {
            this.robot = robot;
            sensorLevel = new SquareLinkedCells(new Cell(0, 0, Cell.CellStatus.AlreadyKnown));

        }

        public void PathFind()
        {
            view.ShowStartMessage("Searching starts!");
            Direction currentDirection = Direction.Up;
            while(true)
            {
                Boolean isTryToFindDirection = true;
                foreach(Direction direction in LookOut(currentDirection))
                {
                    Cell.CellStatus nextCellStatus = robot.GetNearbyCellState(direction);
                    if (nextCellStatus == Cell.CellStatus.Wall)
                    {
                        if (sensorLevel.GetCell(direction) == null)
                        {
                            sensorLevel.Add(direction, nextCellStatus);
                            RenderLevel();
                        }
                    }
                    else if (nextCellStatus == Cell.CellStatus.Exit)
                    {
                        sensorLevel.Add(direction, Cell.CellStatus.Exit);
                        memory.Push(direction);
                        RenderLevel();
                        sensorLevel.ChangeCell(direction);
                        view.ShowFinalMessage("Exit is found!");
                        return;
                    }
                    else if (nextCellStatus == Cell.CellStatus.Empty)
                    {
                        if (sensorLevel.GetCell(direction) == null)
                        {
                            sensorLevel.Add(direction, Cell.CellStatus.AlreadyKnown);
                            memory.Push(direction);
                            sensorLevel.ChangeCell(direction);
                            robot.Move(direction);
                            RenderLevel();
                            currentDirection = direction;
                            isTryToFindDirection = false;
                            break;
                        }
                    }
                }
                if (isTryToFindDirection)
                {
                    if (memory.Count == 0)
                    {
                        String message = "There is no exit";
                        view.ShowErrorMessage(message);
                        throw new Exception(message);
                    }
                    Direction directionFromMemory = GetOppositeDirection(memory.Pop());
                    sensorLevel.ChangeCell(directionFromMemory);
                    robot.Move(directionFromMemory);
                    RenderLevel();
                }
            }
                
        }

        private void RenderLevel()
        {
            view.RenderLevel(sensorLevel.ToArray(), isWaitAfterEachStep);
        }

        private Direction ChangeDirection(Direction currentDirection)
        {
            switch(currentDirection)
            {
                case(Direction.Up):
                    return Direction.Right;
                case(Direction.Right):
                    return Direction.Down;
                case(Direction.Down):
                    return Direction.Left;
                case(Direction.Left):
                    return Direction.Up;
                default:
                    throw new Exception("There is a direction of unknown type");
            }
        }

        private Direction GetOppositeDirection(Direction direction)
        {
            switch(direction)
            {
                case(Direction.Up):
                    return Direction.Down;
                case(Direction.Right):
                    return Direction.Left;
                case(Direction.Down):
                    return Direction.Up;
                case(Direction.Left):
                    return Direction.Right;
                default:
                    throw new Exception("There is a direction of unknown type");
            }
        }

        private IEnumerable<Direction> LookOut(Direction direction)
        {
            Direction nextDirection = direction;
            do
            {
                yield return nextDirection;
                nextDirection = ChangeDirection(nextDirection);
            } while (nextDirection != direction);
        }
    }
}
