using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace C21_Ex02_Liron_318598380_Chen_208711978
{
    public class FourInARow
    {
        private static void getBoardSize(ref int io_BoardWidth, ref int io_BoardLength)
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

        public static void StartGame()
        {
            int boardLength = 0;
            int boardWidth = 0;
            int playerType = 0;
            getBoardSize(ref boardWidth, ref boardLength);
            Ex02.ConsoleUtils.Screen.Clear();
            Board theGameBoard = new Board(boardLength, boardWidth);
            playerType = getPlayerType();
            Player player1 = new Player(2, 0);
            Player player2 = new Player(playerType, 1);
            theGameBoard.PrintBoard();
            gameManage(theGameBoard, player1, player2);
        }

        public static void gameManage(Board io_TheGameBoard, Player io_Player1, Player io_Player2)
        {
            bool isGameOver = false;
            int gameRound = 0;
            int playerColumnChoice=0;
            Player player = io_Player1;
            bool isChipNotAddedTocolumn = true;
            int currentChipRow=0;
            bool isContinueToAnotherRound = true;
            while (isContinueToAnotherRound)
            {
                isGameOver = false;
                isContinueToAnotherRound = false;
                player = io_Player1;
                gameRound = 0;
                io_TheGameBoard.InitBoard(io_TheGameBoard.BoardLength, io_TheGameBoard.BoardWidth);
                Ex02.ConsoleUtils.Screen.Clear();
                io_TheGameBoard.PrintBoard();

                while (!isGameOver)
                {

                    Console.WriteLine(string.Format("Player {0} please choose column number:", player.NumberOfPlayer + 1));
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
                            Console.WriteLine(string.Format("Player {0} Won:", player.NumberOfPlayer + 1));
                            printScore(io_Player1, io_Player2);
                            isGameOver = true;
                        }
                        else if (io_TheGameBoard.isFullBoard())
                        {
                            Console.WriteLine("Its a tie");
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

        private static int getPlayerType()
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

        }
        private static bool isAnotherRound()
        {
            int playerChoice;
            bool isAnotherRound;
            Console.WriteLine("Would you like to play another game ? If so, press 1 .If not, press 0.");
            playerChoice=int.Parse(Console.ReadLine());
            isAnotherRound= playerChoice == 1 ? true : false;
            return isAnotherRound;

        }
        private static void printScore(Player io_Player1, Player io_Player2)
        {
            Console.WriteLine(string.Format("Player {0} score: {1}", io_Player1.NumberOfPlayer + 1, io_Player1.PlayerScore));
            Console.WriteLine(string.Format("Player {0} score: {1}", io_Player2.NumberOfPlayer + 1, io_Player2.PlayerScore));
        }
        private static bool isPlayerWon(char i_PlayerLetterType, int i_CurrentChipRow,int i_PlayerColumnChoice,Board i_TheGameBoard)
        {
            bool isPlayerWon = false;
            isPlayerWon = i_TheGameBoard.isFourInARow(i_PlayerLetterType, i_CurrentChipRow, i_PlayerColumnChoice - 1) || i_TheGameBoard.isFourInADiagonal(i_PlayerLetterType) || i_TheGameBoard.isFourInACol(i_PlayerLetterType, i_CurrentChipRow, i_PlayerColumnChoice - 1);
            return isPlayerWon;
        }
    
        private static void ChangePlayer(int io_gameRound,ref Player io_player, Player io_Player1, Player io_Player2)
        {
            if (io_gameRound % 2 == 0)
            {
                io_player = io_Player1;
            }
            else
            {
                io_player = io_Player2;
            }
        }

        private static int getRandomComputerChoice(int i_BoardWidth)
        {
            int random_number = new Random().Next(1, i_BoardWidth);
                return random_number;
        }
        private static int getPlayerChoice(int i_PlayerNumber, int i_BoardWidth)
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
        }

        private static bool isPlayerChoiceNumber(string i_PlayerChoice)
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

      

        private static bool isValidPlayerChoiceColumn(int i_BoardWidth, string i_PlayerChoice, ref int io_PlayerColumnChoice)
        {
            bool isValidPlayerChoiceColumn = true;
            io_PlayerColumnChoice = int.Parse(i_PlayerChoice);
            isValidPlayerChoiceColumn = io_PlayerColumnChoice <= i_BoardWidth;

            return isValidPlayerChoiceColumn;
        }

        private static bool isValidBoardSize(int i_input)
        {
            bool isValidInput = true;
            isValidInput = i_input >= 4 && i_input <= 8;
            return isValidInput;
        }
    }
}