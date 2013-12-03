using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace LudoRules
{
    /// <summary>
    /// We want to make sure that board contains mathods and attributes
    /// to keep track of the actual state of of what's taking place
    /// and also services to change its state
    /// </summary>
    public class LudoBoard
    {       
        #region Fields
        private bool isActive = false;

        public int numOfActivePlayers;
        private const int numOfPlayers = 4;
        private const int numOfPiecesPerPlayer = 4;
        private const int numOfSquaresPerSide = 10;
        private const int numOfExitSquaresPerSide = 4;
        private const int numOfMaximumStepsPerLap = numOfSquaresPerSide * numOfPlayers;
        private const int numOfStepsToExit = numOfMaximumStepsPerLap + numOfExitSquaresPerSide;

        private Player[] players;
        private Piece[][] pieces;
        private Nest[] nests;
        private Square[][] squares;
        private Square[][] exitSquares;
        #endregion



        #region Constructor
        /// <summary>
        /// By default, assume players are 4
        /// </summary>
        public LudoBoard() : this(4) { } 
        public LudoBoard(int numOfPlayers)
        {
            this.numOfActivePlayers = numOfPlayers;

            generateBoard();

            Debug.Write("\nBoard constructed");
        }
        #endregion



        #region Properties
        public bool Active
        {
            get { return isActive;  }
            set { isActive = value;  }
        }

        /// <summary>
        /// The rule engine and the test engine will make good use of this information contained here
        /// </summary>
        public Dictionary<string, object> State
        {
            get
            {
                Dictionary<string, object> gameState = new Dictionary<string, object>();

                gameState.Add("board", this.ToString());
                gameState.Add("players", players);
                gameState.Add("pieces", pieces);
                gameState.Add("nests", nests);
                gameState.Add("squares", squares);
                gameState.Add("exitsquares", exitSquares);

                return gameState;
            }
        }
        #endregion



        #region Methods
        /// <summary>
        /// Initialize
        /// </summary>
        private void generateBoard()
        {
            generatePlayersAndPieces();
            generateNests();
            generateSquares();
            Active = true;
        }
        private void generateNests()
        {
            nests = new Nest[numOfPlayers];

            for (int i = 0; i < numOfPlayers; i++)
            {
                nests[i] = new Nest((Colors)i, pieces[i]);
            }
        }
        private void generatePlayersAndPieces()
        {
            players = new Player[numOfPlayers];
            pieces = new Piece[numOfPlayers][];

            for (int i = 0; i < numOfPlayers; i++)
            {
                players[i] = new Player((Colors)i);
                generatePieces((Colors)i);
            }
        }
        private void generatePieces(Colors color)
        {
            pieces[(int)color] = new Piece[numOfPiecesPerPlayer];
            for (int i = 0; i < numOfPiecesPerPlayer; i++)
            {
                pieces[(int)color][i] = new Piece(color, i);
            }
        }
        private void generateSquares()
        {
            squares = new Square[numOfPlayers][];
            exitSquares = new ExitSquare[numOfPlayers][];

            for (int side = 0; side < numOfPlayers; side++)
            {
                squares[side] = new Square[numOfSquaresPerSide];
                for (int position = 0; position < numOfSquaresPerSide; position++)
                {
                    int squarePosition = position + (side * numOfSquaresPerSide);
                    squares[side][position] = new Square((Colors)side, squarePosition);
                }

                exitSquares[side] = new ExitSquare[numOfExitSquaresPerSide];
                for (int position = 0; position < numOfExitSquaresPerSide; position++)
                {
                    exitSquares[side][position] = new ExitSquare((Colors)side, position);
                }
            }
        }

        public void deActivatePiece(Piece piece)
        {
            nests[(int)piece.Color].pushPiece(piece);
            piece.Active = false;
        }

        /// <summary>
        /// The nest works like a stack
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private Piece popPieceFromNest(Colors color)
        {
            return nests[(int)color].popPiece();
        }
        /// <summary>
        /// Stack-like behaviour
        /// </summary>
        /// <param name="piece"></param>
        private void pushPieceToNest(Piece piece)
        {
            nests[(int)piece.Color].pushPiece(piece);
        }

        /// <summary>
        /// Pop a piece but push it back if there's a collision
        /// </summary>
        /// <param name="color"></param>
        /// <param name="isPieceActivated"></param>
        public void activatePiece(Colors color, out bool isPieceActivated)
        {
            Piece piece = popPieceFromNest(color);
            isPieceActivated = tryIntroducePiece(piece);
            piece.Active = isPieceActivated;
            piece.Position = 0 + ((int)color * numOfSquaresPerSide);
            if (!isPieceActivated)  
            {
                pushPieceToNest(piece);
            }
            else      
            { 
                players[(int)color].Active = true;
            }
        }

        /// <summary>
        /// By moving through the goal the piece gets killed
        /// </summary>
        /// <param name="piece"></param>
        private bool killPiece(Piece piece)
        {
            Square currentPosition = locateSquare(piece.Steps, piece.Position, piece.Color);
            currentPosition.Occupant = null;
            piece.Alive = false;
            piece.Active = false;
            return true;
        }

        private bool allPiecesDead(Colors color)
        {
            foreach (var piece in pieces[(int)color])
            { 
                if (piece.Alive) { return false;  }
            }
            return true;
        }
        
        /// <summary>
        /// Here we need to consider 3 scenarios:
        /// - If we are about to roll the dice and have a chance at moving to the exit squares 
        /// - chance at moving through the goal
        /// - "normal" behaviour
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="steps"></param>
        /// <param name="isMoveSuccesful"></param>
        public void tryMovePiece(Piece piece, int steps, ref bool isMoveSuccesful)
        {
            int requestedSteps = piece.Steps + steps;

            // 3 scenarios //
            if (requestedSteps == numOfStepsToExit)
            {
                Debug.Write("\nExiting");
                isMoveSuccesful = killPiece(piece);
                checkPlayerDeActivation(piece.Color);
            }
            // Almost quitting
            else if (requestedSteps > numOfStepsToExit)
            {
                Debug.Write(String.Format(
                            "\nGoing to the exit squares. Steps walked: {0}, position: {1}, trying new pos: {2}",
                            piece.Steps, piece.Position, piece.Position + steps));

                // We will have to step back x steps from the goal square (overflow)
                Square currentPosition = locateSquare(piece.Steps, piece.Position, piece.Color);
                calculateNewStepsAfterMissedTarget(piece.Position, requestedSteps, ref steps);
                Square requestedPosition = locateSquare(requestedSteps, piece.Position + steps, piece.Color);
               
                isMoveSuccesful = tryMove(currentPosition, requestedPosition, piece, steps, requestedSteps);

            }
            else
            {
                Debug.Write(String.Format(
                    "\nSteps walked: {0}, position: {1}, trying new pos: {2}", 
                     piece.Steps, piece.Position, piece.Position + steps));

                Square currentPosition = locateSquare(piece.Steps, piece.Position, piece.Color);
                Square requestedPosition = locateSquare(requestedSteps, piece.Position + steps, piece.Color);

                isMoveSuccesful = tryMove(currentPosition, requestedPosition, piece, steps, requestedSteps);
            }
        }

        private bool checkPlayerDeActivation(Colors color)
        {
            foreach (var piece in pieces[(int)color])
            {
                    if (piece.Alive) 
                    { 
                        return false;  
                    }
            }
            players[(int)color].Active = false;
            return true;
        }

        /// <summary>
        /// This is a collision detector. 
        /// You need to get an exact match with the dice in order to exit
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <param name="requestedSteps"></param>
        /// <returns></returns>
        private void calculateNewStepsAfterMissedTarget(int currentPosition, int requestedSteps, ref int steps)
        {
            int overflow = requestedSteps - numOfStepsToExit;
            int newPosition = numOfStepsToExit - overflow;
            int numOfNewSteps = newPosition - currentPosition;
            steps = numOfNewSteps;
        }

        /// <summary>
        /// 2 scenarios; two types of squares
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="position"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private Square locateSquare(int steps, int position, Colors color)
        {
            if (steps >= numOfMaximumStepsPerLap) // look in exitSquares
            {
                int side = (int)color;
                int squareID = position - (numOfPlayers * numOfSquaresPerSide);
                return exitSquares[side][squareID];
            }
            else // look in normal squares
            {
                int side = position / numOfSquaresPerSide;
                int squareID = position - (side * numOfSquaresPerSide);
                if (side == 4) { side = 0; } // wrap around array
                return squares[side][squareID];
            }    
        }

        /// <summary>
        /// To introduce: move a player from nest to square
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        private bool tryIntroducePiece(Piece piece)
        {
            Square firstSquare = squares[(int)piece.Color][0];
            bool isIntroduced = tryMove(null, firstSquare, piece, 0, 0);
            return isIntroduced;
        }

        /// <summary>
        /// We would like to achieve the following: 
        /// - replacing our last position with null
        /// - knocking pieces out or passing depending on collision with ourselves or others
        /// </summary>
        /// <param name="fromSquare"></param>
        /// <param name="toSquare"></param>
        /// <param name="piece"></param>
        /// <param name="steps"></param>
        /// <param name="requestedSteps"></param>
        /// <returns></returns>
        private bool tryMove(Square fromSquare, Square toSquare, Piece piece, int steps, int requestedSteps)
        {
            if (toSquare.Occupant == null)
            {
                Debug.Write("\nMoving piece to square " );
                move(piece, toSquare, fromSquare, steps);
                return true;
            }
            else
            {
                Piece occupyingPiece = toSquare.Occupant;
                Debug.Write("\nSquare  is occupied");
                if (occupyingPiece.Color == piece.Color)
                {
                    // Two pieces of the same color cannot occupy the same space (pass)
                    return false;
                }
                else
                {
                    // We will now deActivate the colliding piece, since it's not our own
                    // ... and insert our own piece
                    Debug.Write("\nKnocking out piece ");
                    move(piece, toSquare, fromSquare, steps);
                    deActivatePiece(occupyingPiece);
                    if (nests[(int)occupyingPiece.Color].Count == 4)
                    { players[(int)occupyingPiece.Color].Active = false; }

                    return true;
                }
            }
        }
        private void move(Piece piece, Square toSquare, Square fromSquare, int steps)
        {
            if (fromSquare != null) { fromSquare.Occupant = null; }
            toSquare.Occupant = piece;
            piece.Steps += steps;
            piece.Position += steps;
        }

        /// <summary>
        /// Exports status to int[]-friendly API handling
        /// </summary>
        /// <returns></returns>
        public GameState ToArray()
        {
            GameState gameState = new GameState();

            gameState.Nests = nestsToArray();
            gameState.Squares = squaresToArray(squares, numOfSquaresPerSide);
            gameState.ExitSquares = squaresToArray(exitSquares, numOfExitSquaresPerSide);
            return gameState;
        }
        public int[] nestsToArray()
        {
            int[] nestsArray = new int[numOfPiecesPerPlayer];
            int i = 0;
            foreach (var nest in nests)
            { nestsArray[i++] = nest.Count; }
            return nestsArray;
        }
        public int[][] squaresToArray(Square[][] squaresToConvert, int squaresPerSide)
        {
            int[][] squaresArray = new int[numOfPlayers][];

            for (int i = 0; i < numOfPlayers; i++)
            {
                squaresArray[i] = new int[squaresPerSide]; 

                for (int j = 0; j < squaresPerSide; j++)
                {
                    squaresArray[i][j] = (squaresToConvert[i][j].Occupant == null) ? -1 :
                                          (int)squaresToConvert[i][j].Occupant.Color;
                }
            }
            return squaresArray;
        }

        public override string ToString()
        {
            return (Active) ? "Board is active" : "Board is not active";
        }         
        #endregion
    }
}
