using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C21_Ex02_Liron_318598380_Chen_208711978
{
    class ConsoleGame
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

        public void gameManage(FourInARow newGame)
        {
            int playerChoice = 0;
            Player player;
            int currentChipRow = 0;
            bool isContinueToAnotherRound = true;

            while(isContinueToAnotherRound)
            {
               
                newGame.initGame();
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
                    player = newGame.GetCurrentPlayer();
                }

                isContinueToAnotherRound = isAnotherRound();
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
            ref int playerColumnChoice)
        {
            bool isChipNotAddedTocolumn = true;
            while(isChipNotAddedTocolumn)
            {
                if(i_Player.PlayerType == Player.ePlayerType.Person)
                {
                    playerColumnChoice = getPlayerChoice(i_Player.NumberOfPlayer, i_newGame);
                    if(playerColumnChoice == -1)
                    {
                        break;
                    }
                }
                else
                {
                    playerColumnChoice=i_newGame.getRandomComputerChoice();
                }

                isChipNotAddedTocolumn = i_newGame.TheGameBoard.AddChips(
                    playerColumnChoice,
                    i_Player.PlayerLetterType,
                    ref io_CurrentChipRow);
            }
        }

        private void updateGameStatus(FourInARow i_newGame, int currentChipRow, int playerColumnChoice)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            i_newGame.TheGameBoard.PrintBoard();
            if(i_newGame.isPlayerWon(currentChipRow, playerColumnChoice))
            {
                printTheWinner(i_newGame);
            }
            else if(i_newGame.TheGameBoard.isFullBoard())
            {
                declareAtie(i_newGame);
            }
        }

        private void declareAtie(FourInARow i_newGame)
        {
            System.Console.WriteLine("Its a tie");
            printScore(i_newGame.GetCurrentPlayer(), i_newGame.GetPreviousPlayer());
            i_newGame.isGameOver = true;
        }

        private void printTheWinner(FourInARow i_newGame)
        {
            i_newGame.GetCurrentPlayer().PlayerScore++;
            System.Console.WriteLine(string.Format("Player {0} Won:", i_newGame.GetCurrentPlayer().NumberOfPlayer));
            printScore(i_newGame.GetCurrentPlayer(), i_newGame.GetPreviousPlayer());
            i_newGame.isGameOver = true;
        }

        private void getBoardSize(ref int io_BoardWidth, ref int io_BoardLength)
        {
            bool isValidWidth = false;
            bool isValidLength = false;
            while(!isValidWidth)
            {
                System.Console.WriteLine("Please enter the game board width,between 4-8");
                io_BoardWidth = int.Parse(System.Console.ReadLine());
                isValidWidth = isValidBoardSize(io_BoardWidth);
                if(!isValidWidth)
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    System.Console.WriteLine("Invalid input!! ,Please enter the game board width,,between 4-8");
                }
            }

            while(!isValidLength)
            {
                System.Console.WriteLine("Please enter the game board length,,between 4-8");
                io_BoardLength = int.Parse(System.Console.ReadLine());
                isValidLength = isValidBoardSize(io_BoardLength);
                if(!isValidLength)
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    System.Console.WriteLine("Invalid input!! ,Please enter the game board length,,between 4-8");
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
                System.Console.WriteLine("Please choose if you want to play against computer press(1),if not press(2)");
                playerChoice = System.Console.ReadLine();
                isValidInput = playerChoice == "1" || playerChoice == "2";
            }

            playerTypeChoice = int.Parse(playerChoice);
            return playerTypeChoice;
        }

        private bool isAnotherRound()
        {
            int playerChoice;
            bool isAnotherRound;
            System.Console.WriteLine("Would you like to play another game ? If so, press 1 .If not, press 0.");
            playerChoice = int.Parse(System.Console.ReadLine());
            isAnotherRound = playerChoice == 1 ? true : false;
            return isAnotherRound;
        }

        private static void printScore(Player io_Player1, Player io_Player2)
        {
            System.Console.WriteLine(
                string.Format("Player {0} score: {1}", io_Player1.NumberOfPlayer + 1, io_Player1.PlayerScore));
            System.Console.WriteLine(
                string.Format("Player {0} score: {1}", io_Player2.NumberOfPlayer + 1, io_Player2.PlayerScore));
        }

        private int getPlayerChoice(int i_PlayerNumber, FourInARow newGame)
        {
            //console.writelINE();
            string playerChoice;
            bool isVaildPlayerChoice = false;
            int PlayerColumnChoice = 0;
            System.Console.WriteLine(
                string.Format("Player {0} please choose column number:", newGame.GetCurrentPlayer().NumberOfPlayer));
            while(!isVaildPlayerChoice)
            {
                playerChoice = System.Console.ReadLine();
                if(playerChoice == "Q")
                {
                    return -1;
                }

                isVaildPlayerChoice = newGame.isPlayerChoiceIsNumber(playerChoice) && newGame.isPlayerChoiceColumnInRange(
                                          playerChoice,
                                          ref PlayerColumnChoice);
                if(!isVaildPlayerChoice)
                {
                    System.Console.WriteLine("Invalid choice");
                }
            }

            return PlayerColumnChoice;
        }

        public bool isValidBoardSize(int i_input)
        {
            bool isValidInput = true;
            isValidInput = i_input >= 4 && i_input <= 8;
            return isValidInput;
        }
    }
}