using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using dice = System.Int32;
using piece = System.Int32;

namespace LudoRules
{
    /// Ludo.LudoRules
    /// <author> Per Jonsson, Hannah Börjesson </author>
    /// Innovativ Programmering, Linköpings Universitet
    /// TDDD49: Lab 2
    /// <summary>
    /// Laborationsbeskrivning:
    /// "Bygg en regelmotor som verifierar olika drag och håller ordning på spelets tillstånd.
    /// Regelmotorn ska vara isolerad mot resten av systemet.
    /// Du ska enhetstesta din regelmotor med enhetstestprojekt i Visual Studio."
    /// </summary>
    public class RuleEngine
    {
        #region Fields
        private int numOfPlayers;
        private LudoBoard ludoBoard;
        private bool isActive;
        #endregion



        #region Constructor
        public RuleEngine()
        {
            ActiveGame = false;
            setupBoard();
            this.numOfPlayers = ludoBoard.numOfActivePlayers;
        }
        #endregion


        
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, object> State
        {
            get { return ludoBoard.State; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool ActiveGame 
        {
            get
            { 
                return this.isActive; 
            }
            set
            {
                this.isActive = value;

                string status = "\nGame is: ";
                status += (this.isActive) ? "active" : "not active";
                Debug.Write(status);
            }
        }
        #endregion



        #region Methods
        private void setupBoard()
        {
            ludoBoard = new LudoBoard();
        }

        /// <summary>
        /// This is the method that UI will call in order to get a status update
        /// </summary>
        /// <param name="gameEvent"></param>
        /// <returns></returns>
        public GameState parseEvent(GameEvent gameEvent)
        {
            // Game State not active, Decide who is going to start
            if (!ActiveGame) 
            { 
                return decideStartingPlayer(gameEvent); 
            }
            // Game State active, make active game relevant choices
            else
            {
                Colors playerID = gameEvent.Player; 
                piece chosenPieceID = gameEvent.Piece;
                Piece[][] pieces = (Piece[][]) ludoBoard.State["pieces"];
                Piece chosenPiece = pieces[(int)playerID][chosenPieceID];
                dice dice = gameEvent.Dice;
                Player[] players = (Player[]) ludoBoard.State["players"];
                Player player = players[(int)playerID];


                Debug.Write("\nDeciding action for player: " + player.PlayerID + ", with piece: " + 
                            chosenPiece.PieceID + ", player rolled: " + dice);

                if (!chosenPiece.Active) 
                { 
                    bool isPieceActivated = tryActivate(playerID, dice);
                    Debug.Write(String.Format("\nTried activating new piece: {0}", isPieceActivated));
                }
                else
                {
                    Debug.Write("\nTrying to move piece.");
                    tryMove(chosenPiece, dice);
                }
            }
            return ludoBoard.ToArray(); // return updated state to UI
        }

        /// <summary>
        /// tryActivate(Piece)
        /// </summary>
        /// <param name="dice"></param>
        /// <returns></returns>
        private bool tryActivate(Colors playerID, dice dice)
        {
            bool isPieceActivated;

            switch (dice)
            {
                case 6:
                    ludoBoard.activatePiece(playerID, out isPieceActivated);
                    break;
                default:
                    isPieceActivated = false;
                    break;
            }
            return isPieceActivated;
        }

        /// <summary>
        ///  tryMove(piece)
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="dice"></param>
        private void tryMove(Piece piece, dice dice)
        {
            bool isMoveSuccesful = false;
            ludoBoard.tryMovePiece(piece, dice, ref isMoveSuccesful); 
            string debug = (isMoveSuccesful) ? "Move succesful" : "Move not succesful";
            Debug.WriteLine("\n" + debug);
        }

        /// <summary>
        /// If there is a player with higher result than the others, make them the winner
        /// Otherwise, do nothing
        /// </summary>
        /// <param name="gameEvent"></param>
        /// <param name="winner"></param>
        /// <returns></returns>
        private GameState decideStartingPlayer(GameEvent gameEvent)
        {
            GameState gameState = new GameState();
            Colors winner;
            Dictionary<Colors, dice> diceResults = gameEvent.Dices;

            Debug.WriteLine("\ndiceResults check");
            // Create ordered List
            var orderedResults = diceResults.OrderBy(results => results.Value).ToList();
            // Check if there are two equally maximum results
            int last = orderedResults[numOfPlayers - 1].Value;
            int nextLast = orderedResults[numOfPlayers - 2].Value;
            bool isTie = (last == nextLast);

            if (isTie) 
            {
                Debug.WriteLine("\n it's a Tie");
                gameState.StartingPlayer = -1;
            }
            else
            {
                winner = orderedResults[numOfPlayers - 1].Key;
                gameState.StartingPlayer = (int)winner;
                Debug.Write("\nWinner of round robin is: " + winner);
                ActiveGame = true;  // TODO: ???
            }
            return gameState;
        }
        #endregion
    }
}
