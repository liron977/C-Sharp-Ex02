using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C21_Ex02_Liron_318598380_Chen_208711978
{
    class Board
    {
        private char[,] m_GameBoard;
        private int m_BoardLength;
        private int m_BoardWidth;
        public Board(int i_BoardLength, int i_BoardWidth)
        {
            m_BoardLength = i_BoardLength;
            m_BoardWidth = i_BoardWidth;
            m_GameBoard = new char[m_BoardWidth, m_BoardLength];
            InitBoard(i_BoardLength, i_BoardWidth);
         
        }

        public int BoardWidth
        {
            get
            {
                return m_BoardWidth;
            }

            set
            {
                m_BoardWidth = value;
            }
        }

        public int BoardLength
        {
            get
            {
                return m_BoardLength;
            }

            set
            {
                m_BoardLength = value;
            }
        }

        public Char[,] MatrixOfBoard
        {
            get
            {
                return m_GameBoard;
            }

            set
            {
                m_GameBoard = value;
            }
        }

        // Initialize the board when created
        private void InitBoard(int i_BoardLength, int i_BoardWidth)
        {

            for(int i = 0; i < i_BoardWidth; i++)
            {
                for(int j = 0; j < i_BoardLength; j++)
                {
                    m_GameBoard[i, j] = ' ';
                }
            }
        }
        public void AddChips(Coordinate i_ChipToAdd,char i_PlayerChip)
        {
            m_GameBoard[i_ChipToAdd.CoordinateRow, i_ChipToAdd.CoordinateCol]= i_PlayerChip;
        }

        public void PrintBoard()
        {
            StringBuilder theGameBoard = new StringBuilder();

            theGameBoard.Append("  ");
            for(int i = 0; i < m_BoardWidth; i++)
            {
                theGameBoard.AppendFormat("{0}   ", i + 1);
            }

            theGameBoard.AppendFormat("{0}", Environment.NewLine);

            for(int i = 0; i < m_BoardLength; i++)
            {
                for(int j = 0; j < m_BoardWidth; j++)
                {
                    theGameBoard.AppendFormat(@"| {0} ", m_GameBoard[j, i]);
                }

                theGameBoard.Append("|");

                theGameBoard.AppendFormat("{0}", Environment.NewLine);
                for(int k = 0; k < m_BoardWidth; k++)
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

