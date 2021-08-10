using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace C21_Ex02_Liron_318598380_Chen_208711978
{
    public class ConsoleGame
    {
        private readonly int playerDecidedToQuit = -1;

        public void StartGame()
        {
            int boardLength = 0;
            int boardWidth = 0;
            int playerType = 0;

            getBoardSize(ref boardWidth, ref boardLength);
            Ex02.ConsoleUtils.Screen.Clear();
            playerType = getPlayerType();
            FourInARow newGame = new FourInARow(boardWidth, boardLength, playerType);
            gameManage(newGame);
        }

        private void gameManage(FourInARow i_Game)
        {
            int playerChoice = 0;
            Player player;
            int currentChipRow = 0;

            i_Game.isContinueToAnotherRound = true;
            while(i_Game.isContinueToAnotherRound)
            {
                i_Game.initGame();
                printBoard(i_Game);
                while(!i_Game.isGameOver)
                {
                    player = i_Game.GetCurrentPlayer();
                    playerAttemptsForValidMove(i_Game, player, ref currentChipRow, ref playerChoice);

                    if(playerChoice == playerDecidedToQuit)
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
            i_Game.isGameOver = true;
        }

        private void playerAttemptsForValidMove(
            FourInARow i_Game,
            Player i_Player,
            ref int io_CurrentChipRow,
            ref int playerChoice)
        {
            bool isFullColumnNumber = true;
            while(isFullColumnNumber)
            {
                if(i_Player.PlayerType == Player.ePlayerType.Person)
                {
                    playerChoice = getPlayerChoice(i_Game);
                    if(playerChoice == -1)
                    {
                        break;
                    }
                }
                else
                {
                    playerChoice = i_Game.GetComputerChoice();
                }

                isFullColumnNumber = i_Game.TheGameBoard.AddChips(
                    playerChoice,
                    i_Player.PlayerLetterType,
                    ref io_CurrentChipRow);
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
            i_Game.isGameOver = true;
        }

        private void printTheWinner(FourInARow i_Game)
        {
            i_Game.GetCurrentPlayer().PlayerScore++;
            string theWinner = string.Format("Player {0} Won:", i_Game.GetCurrentPlayer().NumberOfPlayer);
            Console.WriteLine(theWinner);
            printScore(i_Game.GetCurrentPlayer(), i_Game.GetPreviousPlayer());
            i_Game.isGameOver = true;
        }

        private void getBoardSize(ref int io_BoardWidth, ref int io_BoardLength)
        {
            bool isValidWidth = false;
            bool isValidLength = false;
            string boardWidthInput = string.Empty;
            string boardLengthInput = string.Empty;

            while (!isValidWidth)
            {
                Console.WriteLine("Please enter the game board width,between 4-8");
                boardWidthInput = Console.ReadLine();
                if(boardWidthInput != string.Empty)
                {
                    io_BoardWidth = int.Parse(boardWidthInput);
                    isValidWidth = isValidBoardSize(io_BoardWidth);
                }

                if(!isValidWidth)
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    Console.WriteLine("Invalid input!! ,Please enter the game board width,,between 4-8");
                }
            }

            while(!isValidLength)
            {
                Console.WriteLine("Please enter the game board length,,between 4-8");
                boardLengthInput = Console.ReadLine();
                if(boardLengthInput != string.Empty)
                {
                    io_BoardLength = int.Parse(boardLengthInput);
                    isValidLength = isValidBoardSize(io_BoardLength);
                }

                if(!isValidLength)
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    Console.WriteLine("Invalid input!! ,Please enter the game board length,,between 4-8");
                }
            }
        }

        private static int getPlayerType()
        {
            bool isValidInput = false;
            string playerChoice = string.Empty;
            int playerTypeChoice = 0;

            while(!isValidInput)
            {
                Console.WriteLine("Please choose if you want to play against computer press(1),if not press(2)");
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
            string playerChoiseAnotherRound;

            i_Game.isContinueToAnotherRound = false;
            while (!isValidInput)
            {
                Console.WriteLine("Would you like to play another game ? If so, press 1 .If not, press 0.");
                playerChoiseAnotherRound = Console.ReadLine();
                if(playerChoiseAnotherRound != string.Empty)
                {
                    playerChoice = int.Parse(playerChoiseAnotherRound);
                    isValidInput = playerChoice == 1 || playerChoice == 0;
                }

                Ex02.ConsoleUtils.Screen.Clear();
            }

            i_Game.isContinueToAnotherRound = playerChoice == 1;
        }

        private void printScore(Player i_Player1, Player i_Player2)
        {
            Console.WriteLine(
                string.Format("Player {0} score: {1}", i_Player1.NumberOfPlayer, i_Player1.PlayerScore));
            Console.WriteLine(
                string.Format("Player {0} score: {1}", i_Player2.NumberOfPlayer, i_Player2.PlayerScore));
        }

        private int getPlayerChoice(FourInARow i_Game)
        {
            string playerChoice;
            bool isValidPlayerChoice = false;
            int playerColumnChoice = 0;

            Console.WriteLine(
                string.Format("Player {0} please choose column number:", i_Game.GetCurrentPlayer().NumberOfPlayer));
            while(!isValidPlayerChoice)
            {
                playerChoice = Console.ReadLine();
                if(playerChoice == "Q")
                {
                    return -1;
                }

                isValidPlayerChoice = i_Game.IsPlayerChoiceIsNumber(playerChoice)
                                      && i_Game.IsPlayerChoiceColumnInRange(playerChoice, ref playerColumnChoice);
                if(!isValidPlayerChoice)
                {
                    Console.WriteLine("Invalid choice");
                }
            }

            return playerColumnChoice;
        }

        private bool isValidBoardSize(int i_InputBoardSize)
        {
            bool isValidInput = i_InputBoardSize >= 4 && i_InputBoardSize <= 8;

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