using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


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
            m_GameRoundCounter=0;
            m_TheGameBoard.InitBoard(m_TheGameBoard.BoardLength, m_TheGameBoard.BoardWidth);
            Ex02.ConsoleUtils.Screen.Clear();
            m_TheGameBoard.PrintBoard();
        }

        public Player InitPlayer()
        {
            return m_Player1;
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


       
        public  bool isPlayerWon( int i_CurrentChipRow,int i_PlayerColumnChoice)
        {
            bool isPlayerWon = false;
            isPlayerWon = m_TheGameBoard.isFourInARow(GetCurrentPlayer().PlayerLetterType, i_CurrentChipRow, i_PlayerColumnChoice - 1) || m_TheGameBoard.isFourInADiagonal(GetCurrentPlayer().PlayerLetterType) || m_TheGameBoard.isFourInACol(GetCurrentPlayer().PlayerLetterType, i_CurrentChipRow, i_PlayerColumnChoice - 1);
            return isPlayerWon;
        }
    
        public  int getRandomComputerChoice()
        {
            System.Console.WriteLine(
                string.Format("The computer turn"));
            Thread.Sleep(500);
            int random_number = new Random().Next(1, m_TheGameBoard.BoardWidth);
                return random_number;
        }
        
        public  bool isPlayerChoiceIsNumber(string i_PlayerChoice)
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

        public  bool isPlayerChoiceColumnInRange( string i_PlayerChoice, ref int io_PlayerColumnChoice)
        {
            bool isValidPlayerChoiceColumn = true;
            io_PlayerColumnChoice = int.Parse(i_PlayerChoice);
            isValidPlayerChoiceColumn = io_PlayerColumnChoice <= m_TheGameBoard.BoardWidth;

            return isValidPlayerChoiceColumn;
        }
      
    }
}