using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace C21_Ex02_Liron_318598380_Chen_208711978
{
    public class FourInARow
    {
      
        private Player m_Player1;
        private Player m_Player2;
        private int m_GameRoundCounter = 0;
        private Board m_TheGameBoard;
        private bool m_isGameOver = false;
        private bool isContinueToAnotherRound;
        public FourInARow(int i_BoardWidth, int i_BoardLenght , int playerType)
        {
            m_TheGameBoard = new Board(i_BoardWidth, i_BoardLenght);
          
            m_Player1 = new Player(2, 1);
            m_Player2 = new Player(playerType, 2);


        }

        public Board TheGameBoard
        {
            get
            {
                return m_TheGameBoard;
            }
        }
        public bool isGameOver
        {
            get
            {
                return m_isGameOver;
            }

            set
            {
                m_isGameOver = value;
            }
        }
        public void initGame()
        {
            isGameOver = false;
            isContinueToAnotherRound = false;
            m_TheGameBoard.InitBoard(m_TheGameBoard.BoardLength, m_TheGameBoard.BoardWidth);
            Ex02.ConsoleUtils.Screen.Clear();
            m_TheGameBoard.PrintBoard();
        }
        public Player GetCurrentPlayer()
        {
            return m_GameRoundCounter % 2 == 0 ? m_Player1 : m_Player2;
        }

        public Player GetPreviousPlayer()
        {
            return m_GameRoundCounter % 2 != 0 ? m_Player1 : m_Player2;
        }

        public int m_GameRound
        {
            get
            {
                return m_GameRoundCounter;
            }

            set
            {
                m_GameRoundCounter = value;
            }
        }


        /*private static void getBoardSize(ref int io_BoardWidth, ref int io_BoardLength)
        {
            bool isValidWidth = false;
            bool isValidLength = false;
            while(!isValidWidth)
            {
                Console.WriteLine("Please enter the game board width,between 4-8");
                io_BoardWidth = int.Parse(Console.ReadLine());
                isValidWidth = isValidBoardSize(io_BoardWidth);
                if(!isValidWidth)
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    Console.WriteLine("Invalid input!! ,Please enter the game board width,,between 4-8");
                }
            }

            while(!isValidLength)
            {
                Console.WriteLine("Please enter the game board length,,between 4-8");
                io_BoardLength = int.Parse(Console.ReadLine());
                isValidLength = isValidBoardSize(io_BoardLength);
                if(!isValidLength)
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    Console.WriteLine("Invalid input!! ,Please enter the game board length,,between 4-8");
                }
            }
        }*/

        /* public static void StartGame()
         {
             int boardLength = 0;
             int boardWidth = 0;
             int playerType = 0;
             ConsoleGame gameUI;
             gameUI.getBoardSize(ref boardWidth, ref boardLength);
             Ex02.ConsoleUtils.Screen.Clear();
             Board m_TheGameBoard = new Board(boardLength, boardWidth);
             playerType = getPlayerType();
             Player player1 = new Player(2, 0);
             Player player2 = new Player(playerType, 1);
             theGameBoard.PrintBoard();
             gameManage(theGameBoard, player1, player2);
         }
 */
        /*    public  void gameManage(Board io_TheGameBoard)
        {
            bool isGameOver = false;
            int gameRound = 0;
            int playerColumnChoice=0;
            Player player = m_Player1;
            bool isChipNotAddedTocolumn = true;
            int currentChipRow=0;
            bool isContinueToAnotherRound = true;
            while (isContinueToAnotherRound)
            {
                isGameOver = false;
                isContinueToAnotherRound = false;
                player = m_Player1;
                gameRound = 0;
                io_TheGameBoard.InitBoard(io_TheGameBoard.BoardLength, io_TheGameBoard.BoardWidth);
                Ex02.ConsoleUtils.Screen.Clear();
                io_TheGameBoard.PrintBoard();

                while (!isGameOver)
                {

                    System.Console.WriteLine(string.Format("Player {0} please choose column number:", player.NumberOfPlayer + 1));
                    while (isChipNotAddedTocolumn)
                    {
                        if (player.PlayerType==Player.ePlayerType.Person)
                        {
                            playerColumnChoice = getPlayerChoice(player.NumberOfPlayer, io_TheGameBoard.BoardWidth);
                        }
                        else
 
                        {
                            Thread.Sleep(500);
                            playerColumnChoice = getRandomComputerChoice(io_TheGameBoard.BoardWidth);
                        }
                        if (playerColumnChoice == -1)
                        {
                            break;
                        }
                        isChipNotAddedTocolumn = io_TheGameBoard.AddChips(playerColumnChoice, player.PlayerLetterType, ref currentChipRow);
                    }
                    if (playerColumnChoice != -1)
                    {

                        isChipNotAddedTocolumn = true;
                        Ex02.ConsoleUtils.Screen.Clear();
                        io_TheGameBoard.PrintBoard();
                        if (isPlayerWon(player.PlayerLetterType, currentChipRow, playerColumnChoice, io_TheGameBoard))
                        {
                            player.PlayerScore++;
                            System.Console.WriteLine(string.Format("Player {0} Won:", player.NumberOfPlayer + 1));
                            printScore(io_Player1, io_Player2);
                            isGameOver = true;
                        }
                        else if (io_TheGameBoard.isFullBoard())
                        {
                            System.Console.WriteLine("Its a tie");
                            printScore(io_Player1, io_Player2);
                            isGameOver = true;
                        }
                    }
                    else
                    {
                        ChangePlayer(gameRound, ref player, io_Player1, io_Player2);
                        player.PlayerScore++;
                        isGameOver = true;
                    }
                    gameRound++;

                    ChangePlayer(gameRound, ref player, io_Player1, io_Player2);
                }

                isContinueToAnotherRound = isAnotherRound();
                
            }
        }
*/
        /* private static int getPlayerType()
         {
             bool isValidInput=false;
             string playerChoice=string.Empty;
            int  playerTypeChoice=0;
             while (!isValidInput) 
             {
                 Console.WriteLine("Please choose if you want to play against computer press(1),if not press(2)");
                 playerChoice = Console.ReadLine();
                 isValidInput = playerChoice == "1" || playerChoice == "2";

             }

             playerTypeChoice=int.Parse(playerChoice);
             return playerTypeChoice;

         }*/
        /* private static bool isAnotherRound()
         {
             int playerChoice;
             bool isAnotherRound;
             Console.WriteLine("Would you like to play another game ? If so, press 1 .If not, press 0.");
             playerChoice=int.Parse(Console.ReadLine());
             isAnotherRound= playerChoice == 1 ? true : false;
             return isAnotherRound;

         }*/
        /* private static void printScore(Player io_Player1, Player io_Player2)
         {
             Console.WriteLine(string.Format("Player {0} score: {1}", io_Player1.NumberOfPlayer + 1, io_Player1.PlayerScore));
             Console.WriteLine(string.Format("Player {0} score: {1}", io_Player2.NumberOfPlayer + 1, io_Player2.PlayerScore));
         }*/
        public  bool isPlayerWon( int i_CurrentChipRow,int i_PlayerColumnChoice)
        {
            bool isPlayerWon = false;
            isPlayerWon = m_TheGameBoard.isFourInARow(GetCurrentPlayer().PlayerLetterType, i_CurrentChipRow, i_PlayerColumnChoice - 1) || m_TheGameBoard.isFourInADiagonal(GetCurrentPlayer().PlayerLetterType) || m_TheGameBoard.isFourInACol(GetCurrentPlayer().PlayerLetterType, i_CurrentChipRow, i_PlayerColumnChoice - 1);
            return isPlayerWon;
        }
    
     /*   private static void ChangePlayer(int io_gameRound,ref Player io_player, Player io_Player1, Player io_Player2)
        {
            if (io_gameRound % 2 == 0)
            {
                io_player = io_Player1;
            }
            else
            {
                io_player = io_Player2;
            }
        }*/

        public  int getRandomComputerChoice()
        {
            int random_number = new Random().Next(1, m_TheGameBoard.BoardWidth);
                return random_number;
        }
        /*  private static int getPlayerChoice(int i_PlayerNumber, int i_BoardWidth)
          {
              //console.writelINE();
              string playerChoice;
              bool isVaildPlayerChoice = false;
               int PlayerColumnChoice = 0;
              while(!isVaildPlayerChoice)
              {
                  playerChoice = Console.ReadLine();
                if(playerChoice=="Q")
                  {
                      return -1;

                  }
                  isVaildPlayerChoice = isPlayerChoiceNumber(playerChoice) && isValidPlayerChoiceColumn(i_BoardWidth, playerChoice,ref PlayerColumnChoice);
                  if(!isVaildPlayerChoice)
                  {
                      Console.WriteLine("Invalid choice");
                  }
              }

              return PlayerColumnChoice;
          }*/

        public  bool isPlayerChoiceNumber(string i_PlayerChoice)
        {
            bool isPlayerChoiceNumber = true;
            for (int i = 0; i < i_PlayerChoice.Length; i++)
            {
                isPlayerChoiceNumber = (char.IsDigit(i_PlayerChoice[i]));
                if (!isPlayerChoiceNumber)
                {
                    break;
                }
            }

            return isPlayerChoiceNumber;
        }



        public  bool isValidPlayerChoiceColumn( string i_PlayerChoice, ref int io_PlayerColumnChoice)
        {
            bool isValidPlayerChoiceColumn = true;
            io_PlayerColumnChoice = int.Parse(i_PlayerChoice);
            isValidPlayerChoiceColumn = io_PlayerColumnChoice <= m_TheGameBoard.BoardWidth;

            return isValidPlayerChoiceColumn;
        }
      
    }
}