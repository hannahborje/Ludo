using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoRules
{
    /// <summary>
    /// Nest is a stack-like construction which collects inactive pieces
    /// </summary>
    public class Nest : BoardObject
    {
        #region Fields
        private Stack<Piece> pieces;
        #endregion

        #region Constructor
        public Nest(Colors color, Piece[] pieces)
        {
            this.pieces = new Stack<Piece>();
            this.color = color;

            foreach (var piece in pieces)
            {  pushPiece(piece); }

        }
        #endregion


        #region Properties
        public int Count
        {
            get { return this.pieces.Count; }
        }        
        #endregion



        #region Methods
        public Piece popPiece()
        {
            return pieces.Pop();
        }
        public void pushPiece(Piece piece)
        {
            pieces.Push(piece);
        }

        public override string ToString()
        {
            return String.Format("\nNest, Color: {0}, pieces count: {1}", color.ToString(), Count);
        }

        #endregion  
    }
}
