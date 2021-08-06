using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C21_Ex02_Liron_318598380_Chen_208711978
{
    public class Board
    {
        private char[,] m_GameBoard;
        private int m_BoardLength;
        private int m_BoardWidth;
        public Board(int i_BoardLength, int i_BoardWidth)
        {
            m_BoardLength = i_BoardLength;
            m_BoardWidth = i_BoardWidth;
            m_GameBoard = new char[m_BoardLength, m_BoardWidth];
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
       public void InitBoard(int i_BoardLength, int i_BoardWidth)
        {

            for (int i = 0; i < i_BoardLength; i++)
            {
                for (int j = 0; j < i_BoardWidth; j++)
                {
                    m_GameBoard[i, j] = ' ';
                }
            }
        }
        public bool AddChips(int i_ColumnChipToAdd, char i_PlayerChip, ref int io_currentChipRow)
        {
            bool isFullColumnNumber = isFullColumn(i_ColumnChipToAdd - 1);
            if (!isFullColumnNumber)
            {
                for (int i = m_BoardLength - 1; i >= 0; i--)
                {
                    if (isEmptyPanel(i, i_ColumnChipToAdd - 1))
                    {
                        m_GameBoard[i, i_ColumnChipToAdd - 1] = i_PlayerChip;
                        io_currentChipRow = i;
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("The column is full please try again.");

            }
            return isFullColumnNumber;

        }

        private bool isEmptyPanel(int i_Row, int i_Col)
        {
            bool isEmptyPanel = m_GameBoard[i_Row, i_Col] == ' ';
            return isEmptyPanel;
        }

        public bool isFullColumn(int i_ColumnChipToAdd)
        {
            bool isFullColumn = false;
            isFullColumn = !(isEmptyPanel(0, i_ColumnChipToAdd));
            return isFullColumn;
        }
        public void PrintBoard()
        {
            StringBuilder theGameBoard = new StringBuilder();

            theGameBoard.Append("  ");
            for (int i = 0; i < m_BoardWidth; i++)
            {
                theGameBoard.AppendFormat("{0}   ", i + 1);
            }

            theGameBoard.AppendFormat("{0}", Environment.NewLine);

            for (int i = 0; i < m_BoardLength; i++)
            {
                for (int j = 0; j < m_BoardWidth; j++)
                {
                    theGameBoard.AppendFormat(@"| {0} ", m_GameBoard[i, j]);
                }

                theGameBoard.Append("|");

                theGameBoard.AppendFormat("{0}", Environment.NewLine);
                for (int k = 0; k < m_BoardWidth; k++)
                {
                    theGameBoard.Append("====");
                }

                theGameBoard.Append("==");
                theGameBoard.AppendFormat("{0}", Environment.NewLine);
            }

            Console.WriteLine(theGameBoard);
        }
        public bool isFourInARow(char i_PlayerChip, int i_ChipRowLocation, int i_ChipColLocation)
        {
            int count = 1;
            bool isFourInARow = false;
            for (int i = i_ChipColLocation; i > 0; i--)
            {
                if ((m_GameBoard[i_ChipRowLocation, i] != m_GameBoard[i_ChipRowLocation, i - 1]) || (count == 4))
                {
                    break;
                }
                if (m_GameBoard[i_ChipRowLocation, i] == i_PlayerChip)
                {
                    count++;

                }
            }
            for (int i = i_ChipColLocation; i < BoardWidth - 1; i++)
            {
                if ((m_GameBoard[i_ChipRowLocation, i] != m_GameBoard[i_ChipRowLocation, i + 1]) || (count == 5))
                {
                    break;
                }
                if (m_GameBoard[i_ChipRowLocation, i] == i_PlayerChip)
                {
                    count++;

                }
            }
            if (count >= 4)
            {
                isFourInARow = true;
            }
            return isFourInARow;


        }


    
    public bool isFourInACol(char i_PlayerChip, int i_ChipRowLocation, int i_ChipColLocation)
    {
        int count = 1;
        bool isFourInACol = false;
        for (int i = i_ChipRowLocation; i > 0; i--)
        {
            if ((m_GameBoard[i, i_ChipColLocation] != m_GameBoard[i-1, i_ChipColLocation]) || (count == 4))
            {
                break;
            }
            if (m_GameBoard[i, i_ChipColLocation] == i_PlayerChip)
            {
                count++;

            }
        }
        for (int i = i_ChipRowLocation; i < m_BoardLength- 1; i++)
        {
            if ((m_GameBoard[i, i_ChipColLocation] != m_GameBoard[i+1, i_ChipColLocation]) || (count == 5))
            {
                break;
            }
            if (m_GameBoard[i, i_ChipColLocation] == i_PlayerChip)
            {
                count++;

            }
        }
        if (count >= 4)
        {
                isFourInACol = true;
        }
        return isFourInACol;


    }
        public bool isFullBoard()
        {
            bool isFullBoard = true;
            for (int c = 0; c < m_BoardWidth; c++)
            {
                if(m_GameBoard[0,c]==' ')
                {
                    isFullBoard = false;
                    break;
                }
            }
            return isFullBoard;
        }

        private bool isFourInADiagonalTopLeftToBottomRightIncludeMiddle(char i_PlayerChip)
        {
            int match = 0;
            bool isFourInADiagonal=false;
            for (int r = 0; r <= m_BoardLength - 4; r++)
            {
                int rowPosition = r;
                match = 0;
                for (int column = 0; column < m_BoardWidth && rowPosition < m_BoardLength; column++)
                {
                    char currentValue = m_GameBoard[rowPosition, column];
                    if (currentValue == i_PlayerChip)
                        match++;
                    else match = 0;
                    if (match ==4)
                    {
                        isFourInADiagonal = true;
                        break;
                    }
                    rowPosition++;
                }
                if (isFourInADiagonal) break;
            }
            return isFourInADiagonal;
        }
        private bool isFourInADiagonalbBottomLeftToTopRightIncludeMiddle(char i_PlayerChip)
        {
            int match = 0;
            bool isFourInADiagonal = false;
            for (int r = m_BoardLength - 1; r >= m_BoardLength - 4; r--)
            {
                int rowPosition = r;
                match = 0;
                for (int column = 0; column < m_BoardWidth && rowPosition < m_BoardLength && rowPosition >= 0; column++)
                {
                    Char currentValue = m_GameBoard[rowPosition, column];
                    if (currentValue == i_PlayerChip)
                        match++;
                    else match = 0;
                    if (match == 4)
                    {
                        isFourInADiagonal = true;
                        break;
                    }
                    rowPosition--;
                }
                if (isFourInADiagonal) break;
            }
            return isFourInADiagonal;
        }
        private bool isFourInADiagonalbBottomLeftToTopRightAfterMiddle(char i_PlayerChip)
        {
            int match = 0;
            bool isFourInADiagonal = false;
            for (int c = 1; c < m_BoardWidth; c++)
            {
                int columnPosition = c;
                match = 0;
                for (int row = m_BoardLength - 1; row < m_BoardLength && columnPosition < m_BoardLength && columnPosition >= 1; row--)
                {
                    char currentValue = m_GameBoard[row, columnPosition];
                    if (currentValue == i_PlayerChip)
                        match++;
                    else match = 0;
                    if (match == 4)
                    {
                        isFourInADiagonal = true;
                        break;
                    }
                    columnPosition++;
                }
                if (isFourInADiagonal) break;
            }
            return isFourInADiagonal;

        }
        private bool isFourInADiagonalTopLeftToBottomRightAfterMiddle(char i_PlayerChip)
        {
            int match = 0;
            bool isFourInADiagonal = false;
            for (int c = 1; c <= m_BoardWidth - 4; c++)
            {
                int columnPosition = c;
                match = 0;
                for (int row = 0; row < m_BoardLength && columnPosition < m_BoardWidth; row++)
                {
                    char currentValue = m_GameBoard[row, columnPosition];
                    if (currentValue == i_PlayerChip)
                        match++;
                    else match = 0;
                    if (match == 4)
                    {
                        isFourInADiagonal = true;
                        break;
                    }
                    columnPosition++;
                }
                if (isFourInADiagonal) break;
            }
            return isFourInADiagonal;
        }
            public bool isFourInADiagonal(char i_PlayerChip)
        {
            bool isFourInADiagonal = false;
            isFourInADiagonal = (isFourInADiagonalTopLeftToBottomRightAfterMiddle(i_PlayerChip) || isFourInADiagonalbBottomLeftToTopRightAfterMiddle(i_PlayerChip)|| isFourInADiagonalbBottomLeftToTopRightIncludeMiddle(i_PlayerChip)|| isFourInADiagonalTopLeftToBottomRightIncludeMiddle(i_PlayerChip));
                return isFourInADiagonal;
        }
        }
}

