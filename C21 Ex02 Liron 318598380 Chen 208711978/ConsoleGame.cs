using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace C21_Ex02_Liron_318598380_Chen_208711978
{
    class ConsoleGame
    {
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
          
            int playerColumnChoice = 0;
            Player player;
            bool isChipNotAddedTocolumn = true;
            int currentChipRow = 0;
            bool isContinueToAnotherRound = true;
            while(isContinueToAnotherRound)
            {
                newGame.initGame();
                while(!newGame.isGameOver)
                {

                    /*System.Console.WriteLine(
                        string.Format("Player {0} please choose column number:", newGame.GetCurrentPlayer().NumberOfPlayer));*/
                    while(isChipNotAddedTocolumn)
                    {
                        if(newGame.GetCurrentPlayer().PlayerType == Player.ePlayerType.Person)
                        {
                            System.Console.WriteLine(
                                string.Format("Player {0} please choose column number:", newGame.GetCurrentPlayer().NumberOfPlayer));
                            playerColumnChoice = getPlayerChoice(newGame.GetCurrentPlayer().NumberOfPlayer, newGame);
                        }
                        else

                        {
                            System.Console.WriteLine(
                                string.Format("The computer turn"));
                            Thread.Sleep(500);
                            playerColumnChoice = newGame.getRandomComputerChoice();
                        }

                        if(playerColumnChoice == -1)
                        {
                            break;
                        }

                        isChipNotAddedTocolumn = newGame.TheGameBoard.AddChips(
                            playerColumnChoice,
                            newGame.GetCurrentPlayer().PlayerLetterType,
                            ref currentChipRow);
                    }

                    if(playerColumnChoice != -1)
                    {

                        isChipNotAddedTocolumn = true;
                        Ex02.ConsoleUtils.Screen.Clear();
                        newGame.TheGameBoard.PrintBoard();
                        if(newGame.isPlayerWon(currentChipRow, playerColumnChoice))
                        {
                            newGame.GetCurrentPlayer().PlayerScore++;
                            System.Console.WriteLine(string.Format("Player {0} Won:", newGame.GetCurrentPlayer().NumberOfPlayer));
                            printScore(newGame.GetCurrentPlayer(),newGame.GetPreviousPlayer());
                            newGame.isGameOver = true;
                        }
                        else if(newGame.TheGameBoard.isFullBoard())
                        {
                            System.Console.WriteLine("Its a tie");
                            printScore(newGame.GetCurrentPlayer(), newGame.GetPreviousPlayer());
                            newGame.isGameOver = true;
                        }
                       
                    }
                    else
                    {
                        player = newGame.GetPreviousPlayer();
                        player.PlayerScore++;
                        newGame.isGameOver = true;
                    }

                    newGame.m_GameRound++;

                    player = newGame.GetCurrentPlayer();
                }

                isContinueToAnotherRound = isAnotherRound();

            }
        }

        private  void getBoardSize(ref int io_BoardWidth, ref int io_BoardLength)
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

        private  int getPlayerChoice(int i_PlayerNumber, FourInARow newGame)
        {
            //console.writelINE();
            string playerChoice;
            bool isVaildPlayerChoice = false;
            int PlayerColumnChoice = 0;
            while(!isVaildPlayerChoice)
            {
                playerChoice = System.Console.ReadLine();
                if(playerChoice == "Q")
                {
                    return -1;

                }

                isVaildPlayerChoice = newGame.isPlayerChoiceNumber(playerChoice) && newGame.isValidPlayerChoiceColumn(
                                         
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
