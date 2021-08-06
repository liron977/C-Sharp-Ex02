using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            getBoardSize(ref boardWidth, ref boardLength);
            Ex02.ConsoleUtils.Screen.Clear();
            Board theGameBoard = new Board(boardLength, boardWidth);
            Player player1 = new Player(2, 0);
            Player player2 = new Player(2, 1);
            theGameBoard.PrintBoard();
            gameManage(theGameBoard, player1, player2);
        }

        public static void gameManage(Board io_TheGameBoard, Player io_Player1, Player io_Player2)
        {
            bool isGameOver = false;
            int gameRound = 0;
            //int playerNumber = 0;
            Player player = io_Player1;
            bool isChipNotAddedTocolumn = true;
            while (!isGameOver)
            {
               
                Console.WriteLine(string.Format("Player {0} please choose column number:", player.NumberOfPlayer + 1));
                while (isChipNotAddedTocolumn)
                {
                    isChipNotAddedTocolumn = io_TheGameBoard.AddChips(getPlayerChoice(player.NumberOfPlayer, io_TheGameBoard.BoardWidth), player.PlayerLetterType);
                }
                isChipNotAddedTocolumn = true;
                    Ex02.ConsoleUtils.Screen.Clear();
                    io_TheGameBoard.PrintBoard();
               

                gameRound++;

                ChangePlayer(gameRound, ref player, io_Player1, io_Player2);
            }
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

        private static int getPlayerChoice(int i_PlayerNumber, int i_BoardWidth)
        {
            //console.writelINE();
            string playerChoice;
            bool isVaildPlayerChoice = false;
             int PlayerColumnChoice = 0;
            while(!isVaildPlayerChoice)
            {
                playerChoice = Console.ReadLine();
              
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