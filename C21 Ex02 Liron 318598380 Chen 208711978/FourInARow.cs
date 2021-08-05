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
            while (!isValidWidth)
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
             int boardLength=0;
             int boardWidth=0;
            getBoardSize(ref boardWidth, ref boardLength);
            Ex02.ConsoleUtils.Screen.Clear();
            Board theGameBoard = new Board(boardLength, boardWidth);
            theGameBoard.PrintBoard();
        }


       private static bool isValidBoardSize(int i_input)
        {
            bool isValidInput = true;
            isValidInput = i_input >= 4 && i_input <= 8;
            return isValidInput;
        }
    }
}
