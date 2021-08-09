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

        private void gameManage(FourInARow newGame)
        {
            int playerChoice = 0;
            Player player;
            int currentChipRow = 0;

            newGame.isContinueToAnotherRound = true;
            while(newGame.isContinueToAnotherRound)
            {
                newGame.initGame();
                printBoard(newGame);
                while(!newGame.isGameOver)
                {
                    player = newGame.GetCurrentPlayer();
                    playerAttemptsForValidMove(newGame, player, ref currentChipRow, ref playerChoice);

                    if(playerChoice == playerDecidedToQuit)
                    {
                        playerQuitTheGame(newGame, player);
                    }
                    else
                    {
                        updateGameStatus(newGame, currentChipRow, playerChoice);
                    }

                    newGame.m_GameRound++;
                }

                isAnotherRound(newGame);
            }
        }

        private void playerQuitTheGame(FourInARow i_newGame, Player i_Player)
        {
            i_Player = i_newGame.GetPreviousPlayer();
            i_Player.PlayerScore++;
            i_newGame.isGameOver = true;
        }

        private void playerAttemptsForValidMove(
            FourInARow i_newGame,
            Player i_Player,
            ref int io_CurrentChipRow,
            ref int playerChoice)
        {
            bool isFullColumnNumber = true;
            while(isFullColumnNumber)
            {
                if(i_Player.PlayerType == Player.ePlayerType.Person)
                {
                    playerChoice = getPlayerChoice(i_newGame);
                    if(playerChoice == -1)
                    {
                        break;
                    }
                }
                else
                {
                    playerChoice = i_newGame.GetComputerChoice();
                }

                isFullColumnNumber = i_newGame.TheGameBoard.AddChips(
                    playerChoice,
                    i_Player.PlayerLetterType,
                    ref io_CurrentChipRow);
                if(isFullColumnNumber)
                {
                    Console.WriteLine("The column is full please try again.");
                }
            }
        }

        private void updateGameStatus(FourInARow i_newGame, int currentChipRow, int playerColumnChoice)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            printBoard(i_newGame);
            if(i_newGame.IsPlayerWon(currentChipRow, playerColumnChoice))
            {
                printTheWinner(i_newGame);
            }
            else if(i_newGame.TheGameBoard.IsFullBoard())
            {
                declareATie(i_newGame);
            }
        }

        private void declareATie(FourInARow i_newGame)
        {
            Console.WriteLine("Its a tie");
            printScore(i_newGame.GetCurrentPlayer(), i_newGame.GetPreviousPlayer());
            i_newGame.isGameOver = true;
        }

        private void printTheWinner(FourInARow i_newGame)
        {
            i_newGame.GetCurrentPlayer().PlayerScore++;
            string theWinner = string.Format("Player {0} Won:", i_newGame.GetCurrentPlayer().NumberOfPlayer);
            Console.WriteLine(theWinner);
            printScore(i_newGame.GetCurrentPlayer(), i_newGame.GetPreviousPlayer());
            i_newGame.isGameOver = true;
        }

        private void getBoardSize(ref int io_BoardWidth, ref int io_BoardLength)
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

        private void isAnotherRound(FourInARow i_newGame)
        {
            int playerChoice = -1;
            bool isValidInput = false;

            i_newGame.isContinueToAnotherRound = false;
            while (!isValidInput)
            {
                Console.WriteLine("Would you like to play another game ? If so, press 1 .If not, press 0.");
                playerChoice = int.Parse(Console.ReadLine());
                isValidInput = playerChoice == 1 || playerChoice == 0;
                Ex02.ConsoleUtils.Screen.Clear();
            }

            i_newGame.isContinueToAnotherRound = playerChoice == 1;
        }

        private void printScore(Player i_Player1, Player i_Player2)
        {
            Console.WriteLine(
                string.Format("Player {0} score: {1}", i_Player1.NumberOfPlayer, i_Player1.PlayerScore));
            Console.WriteLine(
                string.Format("Player {0} score: {1}", i_Player2.NumberOfPlayer, i_Player2.PlayerScore));
        }

        private int getPlayerChoice(FourInARow newGame)
        {
            string playerChoice;
            bool isValidPlayerChoice = false;
            int playerColumnChoice = 0;

            Console.WriteLine(
                string.Format("Player {0} please choose column number:", newGame.GetCurrentPlayer().NumberOfPlayer));
            while(!isValidPlayerChoice)
            {
                playerChoice = Console.ReadLine();
                if(playerChoice == "Q")
                {
                    return -1;
                }

                isValidPlayerChoice = newGame.IsPlayerChoiceIsNumber(playerChoice)
                                      && newGame.IsPlayerChoiceColumnInRange(playerChoice, ref playerColumnChoice);
                if(!isValidPlayerChoice)
                {
                    Console.WriteLine("Invalid choice");
                }
            }

            return playerColumnChoice;
        }

        private bool isValidBoardSize(int i_input)
        {
            bool isValidInput = i_input >= 4 && i_input <= 8;

            return isValidInput;
        }

        private void printBoard(FourInARow i_newGame)
        {
            StringBuilder theGameBoard = new StringBuilder();

            theGameBoard.Append("  ");
            for(int i = 0; i < i_newGame.TheGameBoard.BoardWidth; i++)
            {
                theGameBoard.AppendFormat("{0}   ", i + 1);
            }

            theGameBoard.AppendFormat("{0}", Environment.NewLine);

            for(int i = 0; i < i_newGame.TheGameBoard.BoardLength; i++)
            {
                for(int j = 0; j < i_newGame.TheGameBoard.BoardWidth; j++)
                {
                    theGameBoard.AppendFormat(@"| {0} ", i_newGame.TheGameBoard.TheGameBoard[i, j]);
                }

                theGameBoard.Append("|");
                theGameBoard.AppendFormat("{0}", Environment.NewLine);
                for(int k = 0; k < i_newGame.TheGameBoard.BoardWidth; k++)
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