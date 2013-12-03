using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using dice = System.Int32;

namespace LudoRules
{
    /// <summary>
    /// A GameEvent is what the Rule engine will receive from above from the UI
    /// It's the "API"
    /// </summary>
    public struct GameEvent
    {
        public Colors Player;
        public int Piece;
        public dice Dice;
        public Dictionary<Colors, dice> Dices;
    }
}
