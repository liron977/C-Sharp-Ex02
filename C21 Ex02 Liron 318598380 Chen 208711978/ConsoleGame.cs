using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C21_Ex02_Liron_318598380_Chen_208711978
{
    public class ConsoleGame
    {
        private readonly int r_PlayerDecidedToQuit = -1;
        private readonly string r_PlayerPausedTheGameCapitalSymbol = "Q";
        private readonly string r_PlayerPausedTheGameLowerSymbol = "q";
        private readonly int r_MaxBoardSize = 8;
        private readonly int r_MinBoardSize = 4;

        public void StartGame()
        {
            int boardLength = 0;
            int boardWidth = 0;
            int playerType = 0;
            FourInARow newGame;

            getBoardSize(ref boardLength, "length");
            getBoardSize(ref boardWidth, "width");
            Ex02.ConsoleUtils.Screen.Clear();
            playerType = getPlayerType();
            newGame = new FourInARow(boardWidth, boardLength, playerType);
            gameManage(newGame);
        }

        private void gameManage(FourInARow i_Game)
        {
            int playerChoice = 0;
            Player player;
            int currentChipRow = 0;

            i_Game.IsContinueToAnotherRound = true;
            while(i_Game.IsContinueToAnotherRound)
            {
                i_Game.InitGame();
                printBoard(i_Game);
                while(!i_Game.IsGameOver)
                {
                    player = i_Game.GetCurrentPlayer();
                    playerAttemptsForValidMove(i_Game, player, ref currentChipRow, ref playerChoice);
                    if(playerChoice == r_PlayerDecidedToQuit)
                    {
                        playerQuitTheGame(i_Game, player);
                    }
                    else
                    {
                        updateGameStatus(i_Game, currentChipRow, playerChoice);
                    }

                    i_Game.m_GameRound++;
                }

                isAnotherRound(i_Game);
            }
        }

        private void playerQuitTheGame(FourInARow i_Game, Player i_Player)
        {
            i_Player = i_Game.GetPreviousPlayer();
            i_Player.PlayerScore++;
            i_Game.IsGameOver = true;
        }

        private void playerAttemptsForValidMove(
            FourInARow i_Game,
            Player i_Player,
            ref int o_CurrentChipRow,
            ref int i_PlayerChoice)
        {
            bool isFullColumnNumber = true;

            while(isFullColumnNumber)
            {
                if(i_Player.PlayerType == Player.ePlayerType.Person)
                {
                    i_PlayerChoice = getPlayerChoice(i_Game);
                    if(i_PlayerChoice == -1)
                    {
                        break;
                    }
                }
                else
                {
                    System.Console.WriteLine("The computer turn");
                    i_PlayerChoice = i_Game.GetComputerChoice();
                }

                isFullColumnNumber = i_Game.TheGameBoard.AddChips(
                    i_PlayerChoice,
                    i_Player.PlayerLetterType,
                    ref o_CurrentChipRow);
                if(isFullColumnNumber)
                {
                    Console.WriteLine("The column is full please try again.");
                }
            }
        }

        private void updateGameStatus(FourInARow i_Game, int i_CurrentChipRow, int i_PlayerColumnChoice)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            printBoard(i_Game);
            if(i_Game.IsPlayerWon(i_CurrentChipRow, i_PlayerColumnChoice))
            {
                printTheWinner(i_Game);
            }
            else if(i_Game.TheGameBoard.IsFullBoard())
            {
                declareATie(i_Game);
            }
        }

        private void declareATie(FourInARow i_Game)
        {
            Console.WriteLine("Its a tie");
            printScore(i_Game.GetCurrentPlayer(), i_Game.GetPreviousPlayer());
            i_Game.IsGameOver = true;
        }

        private void printTheWinner(FourInARow i_Game)
        {
            string theWinner = string.Empty;

            i_Game.GetCurrentPlayer().PlayerScore++;
            theWinner = string.Format("Player {0} Won:", i_Game.GetCurrentPlayer().NumberOfPlayer);
            Console.WriteLine(theWinner);
            printScore(i_Game.GetCurrentPlayer(), i_Game.GetPreviousPlayer());
            i_Game.IsGameOver = true;
        }

        private void getBoardSize(ref int io_BoardSize, string i_TypeOfSize)
        {
            bool isValidInput = false;
            string boardInput = string.Empty;
            string messageToTheUser = string.Empty;

            messageToTheUser = String.Format("Please enter the game board {0},between 4-8", i_TypeOfSize);
            while(!isValidInput)
            {
                Ex02.ConsoleUtils.Screen.Clear();
                Console.WriteLine(messageToTheUser);
                boardInput = Console.ReadLine();
                if(boardInput != string.Empty)
                {
                    isValidInput = isFirstCharIsNotZero(boardInput) && isPlayerChoiceIsNumber(boardInput);
                    if(isValidInput)
                    {
                        io_BoardSize = int.Parse(boardInput);
                        isValidInput = isValidBoardSize(io_BoardSize);
                    }
                }

                messageToTheUser = String.Format(
                    "Invalid input!! ,Please enter the game board {0},between 4-8",
                    i_TypeOfSize);
            }
        }

        private bool isFirstCharIsNotZero(string i_PlayerChoice)
        {
            bool isFirstCharIsNotZero = true;

            if (i_PlayerChoice.Length > 0)
            {
                if (i_PlayerChoice[0] == '0')
                {
                    isFirstCharIsNotZero = false;
                }
            }

            return isFirstCharIsNotZero;
        }

        private static int getPlayerType()
        {
            bool isValidInput = false;
            string playerChoice = string.Empty;
            int playerTypeChoice = 0;
            string messageToTheUser = string.Empty;

            messageToTheUser =
                String.Format("Please choose if you want to play against computer press(1),if not press(2)");
            while(!isValidInput)
            {
                Ex02.ConsoleUtils.Screen.Clear();
                Console.WriteLine(messageToTheUser);
                messageToTheUser =
                    "Invalid input! Please choose if you want to play against computer press(1),if not press(2)";
                playerChoice = Console.ReadLine();
                isValidInput = playerChoice == "1" || playerChoice == "2";
            }

            playerTypeChoice = int.Parse(playerChoice);

            return playerTypeChoice;
        }

        private void isAnotherRound(FourInARow i_Game)
        {
            int playerChoice = -1;
            bool isValidInput = false;
            string playerChoiceAnotherRound = string.Empty;
            string messageToTheUser = string.Empty;

            i_Game.IsContinueToAnotherRound = false;
            messageToTheUser = "Would you like to play another game ? If so, press 1 .If not, press 0.";
            while(!isValidInput)
            {
                Console.WriteLine(messageToTheUser);
                messageToTheUser =
                    "Invalid input! Would you like to play another game ? If so, press 1 .If not, press 0.";
                playerChoiceAnotherRound = Console.ReadLine();
                isValidInput = isPlayerChoiceIsNumber(playerChoiceAnotherRound);
                if(playerChoiceAnotherRound != string.Empty && isValidInput)
                {
                    playerChoice = int.Parse(playerChoiceAnotherRound);
                    isValidInput = playerChoice == 1 || playerChoice == 0;
                }

                Ex02.ConsoleUtils.Screen.Clear();
            }

            i_Game.IsContinueToAnotherRound = playerChoice == 1;
        }

        private void printScore(Player i_Player1, Player i_Player2)
        {
            Console.WriteLine(string.Format("Player {0} score: {1}", i_Player1.NumberOfPlayer, i_Player1.PlayerScore));
            Console.WriteLine(string.Format("Player {0} score: {1}", i_Player2.NumberOfPlayer, i_Player2.PlayerScore));
        }

        private int getPlayerChoice(FourInARow i_Game)
        {
            string playerChoice = string.Empty;
            bool isValidPlayerChoice = false;
            int playerColumnChoice = 0;

            Console.WriteLine(
                string.Format("Player {0} please choose column number:", i_Game.GetCurrentPlayer().NumberOfPlayer));
            while(!isValidPlayerChoice)
            {
                playerChoice = Console.ReadLine();
                if((playerChoice == r_PlayerPausedTheGameCapitalSymbol)
                   || (playerChoice == r_PlayerPausedTheGameLowerSymbol))
                {
                    return r_PlayerDecidedToQuit;
                }

                isValidPlayerChoice = isFirstCharIsNotZero(playerChoice) && isPlayerChoiceIsNumber(playerChoice)
                                                                         && i_Game.IsPlayerChoiceColumnInRange(playerChoice, ref playerColumnChoice);
                if(!isValidPlayerChoice)
                {
                    Console.WriteLine("Invalid choice");
                }
            }

            return playerColumnChoice;
        }

        private bool isPlayerChoiceIsNumber(string i_PlayerChoice)
        {
            bool isPlayerChoiceNumber = true;

            for(int i = 0; i < i_PlayerChoice.Length; i++)
            {
                isPlayerChoiceNumber = (char.IsDigit(i_PlayerChoice[i]));
                if(!isPlayerChoiceNumber)
                {
                    break;
                }
            }

            return isPlayerChoiceNumber;
        }

        private bool isValidBoardSize(int i_InputBoardSize)
        {
            bool isValidInput = i_InputBoardSize >= r_MinBoardSize && i_InputBoardSize <= r_MaxBoardSize;

            return isValidInput;
        }

        private void printBoard(FourInARow i_Game)
        {
            StringBuilder theGameBoard = new StringBuilder();

            theGameBoard.Append("  ");
            for(int i = 0; i < i_Game.TheGameBoard.BoardWidth; i++)
            {
                theGameBoard.AppendFormat("{0}   ", i + 1);
            }

            theGameBoard.AppendFormat("{0}", Environment.NewLine);
            for(int i = 0; i < i_Game.TheGameBoard.BoardLength; i++)
            {
                for(int j = 0; j < i_Game.TheGameBoard.BoardWidth; j++)
                {
                    theGameBoard.AppendFormat(@"| {0} ", i_Game.TheGameBoard.TheGameBoard[i, j]);
                }

                theGameBoard.Append("|");
                theGameBoard.AppendFormat("{0}", Environment.NewLine);
                for(int k = 0; k < i_Game.TheGameBoard.BoardWidth; k++)
                {
                    theGameBoard.Append("====");
                }

                theGameBoard.Append("==");
                theGameBoard.AppendFormat("{0}", Environment.NewLine);
            }

            Console.WriteLine(theGameBoard);
        }
    }
}