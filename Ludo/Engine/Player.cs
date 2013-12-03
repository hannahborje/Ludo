using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace LudoRules
{
    /// <summary>
    /// A player has a color and controls all the pieces with that color, by rolling the dice
    /// </summary>
    public class Player : BoardObject
    {
        #region Fields
        private bool isActive;
        #endregion



        #region Constructor
        public Player(Colors color)//, int numOfPieces)
        {
            PlayerID = color;
            isActive = false;
        }
        #endregion  



        #region Properties
        public Colors PlayerID
        {
            get { return this.color; }
            set { this.color = value; }
        }

        public bool Active
        {
            get { return isActive; }
            set { isActive = value; }
        }
        #endregion



        #region Methods
        public override string ToString()
        {
            string status = String.Format("\nPlayer: {0}", PlayerID);
            return status;
        }
        #endregion

    }
}
