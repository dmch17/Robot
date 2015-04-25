using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot
{
    public interface IView
    {
        void RenderLevel(Cell.CellStatus[,] levelInfo, Boolean isWaitAfterEachStep);
        void ShowStartMessage(String message);
        void ShowFinalMessage(String message);
        void ShowErrorMessage(String message);
    }
}
