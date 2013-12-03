using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LudoRules
{
    /// <summary>
    /// A very important part of the game
    /// </summary>
    public class Piece : BoardObject
    {
        #region Fields
        private bool isAlive;
        private bool isActive;
        private int ID;
        private int steps;
        private int position;
        private const int boardSize = 40;
        #endregion



        #region Constructor         
        public Piece(Colors color, int ID) 
        {
            Color = color;
            PieceID = ID;
            Alive = true;
            Active = false;
            Steps = 0;
            Position = 0;
        }
        #endregion



        #region Properties
        public bool Active
        {
            get { return this.isActive; }
            set { this.isActive = value; }
        }

        public bool Alive
        {
            get { return this.isAlive;  }
            set { this.isAlive = value; }
        }

        public int PieceID
        {
            get { return this.ID; }
            set { this.ID = value; }
        }

        public int Steps
        {
            get { return this.steps; }
            set { this.steps = value; }
        }

        public int Position
        {
            get { return this.position; }
            set 
            { 
                this.position = value;
                if (this.position >= boardSize && steps < boardSize) 
                { 
                    this.position -= boardSize;
                } // wrap arund
            }
        }

        public Colors Color
        {
            get { return this.color; }
            set { this.color = value; }
        }
        #endregion


        #region Methods
        public override string ToString()
        {
            return  String.Format("Piece, number: {0}, active: {1}, color: {2}, position: {3}",
                                    PieceID, Active, Color, Position);
        }
        #endregion  
    

    }
}
