using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoRules
{
    /// <summary>
    /// There are two types of squares, just to make it easier to handle abstract-ly
    /// </summary>
    public class Square : BoardObject
    {
        protected int SquareID;
        protected Piece occupant;

        public Square(Colors color, int number)
        {
            this.color = color;
            this.SquareID = number;
            this.occupant = null;
        }

        public Piece Occupant
        {
            get { return this.occupant; }
            set { this.occupant = value; }
        }

        public override string ToString()
        {
            string status = (occupant != null) ?
                String.Format("\nSquare, color: {0}, id: {1}, is occupied by: {2}", 
                this.color, this.SquareID, this.occupant.ToString()) :
                String.Format("\nSquare, color: {0}, id: {1}, is occupied by: {2}", 
                this.color, this.SquareID, "none");
            return status;
        }
    }



    public class ExitSquare : Square
    {
        public ExitSquare(Colors color, int number) : base(color, number) {}
        public override string ToString() {
            return base.ToString() + "(exit)";
        }
    }
}
