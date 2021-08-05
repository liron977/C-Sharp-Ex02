using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C21_Ex02_Liron_318598380_Chen_208711978
{
    public class FourInARow
    {
        private static void getBoardSize(ref int io_boardWidth, ref int io_boardLength)
        {
            bool isValidWidth = false;
            bool isValidLength = false;
            while(!isValidWidth)
            {
                Console.WriteLine("Please enter the game board width,between 4-8");
                io_boardWidth = int.Parse(Console.ReadLine());
                isValidWidth = isValidBoardSize(io_boardWidth);
                if(!isValidWidth)
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    Console.WriteLine("Invalid input!! ,Please enter the game board width,,between 4-8");
                }
            }

            while(!isValidLength)
            {
                Console.WriteLine("Please enter the game board length,,between 4-8");
                io_boardLength = int.Parse(Console.ReadLine());
                isValidLength = isValidBoardSize(io_boardLength);
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
        }

        public void gameManage(Board io_TheGameBoard, Player io_Player1, Player io_Player2)
        {
            bool isGameOver = false;
            int gameRound = 0;
            while(!isGameOver)
            {
                if(gameRound % 2 == 0)
                {
                }
            }
        }

        private bool isValidPlayerChoice(int i_BoardWidth, string i_PlayerChoice)
        {
            bool isValidPlayerChoiceColumn = true;
            int i_PlayerColumnChoice;
            for(int i = 0; i < i_PlayerChoice.Length; i++)
            {
                isValidPlayerChoiceColumn = (char.IsDigit(i_PlayerChoice[i]));
                if(!isValidPlayerChoiceColumn)
                {
                    break;
                }
            }

            if(isValidPlayerChoiceColumn)
            {
                i_PlayerColumnChoice = int.Parse(i_PlayerChoice);

                isValidPlayerChoiceColumn = (i_PlayerColumnChoice < 0 || i_PlayerColumnChoice > i_BoardWidth);
            }

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