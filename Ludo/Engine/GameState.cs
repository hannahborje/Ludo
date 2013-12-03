using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LudoRules
{
    /// <summary>
    /// The UI will receive this as a "board status update" from the Rule engine
    /// It may use this information however it pleases
    /// </summary>
    public struct GameState
    {
        public int[][] Squares;
        public int[][] ExitSquares;
        public int[] Nests;
        public int StartingPlayer;
    }
}
