using System.Threading;

namespace C21_Ex02_Liron_318598380_Chen_208711978
{
    public class FourInARow
    {
        private readonly Player r_Player1;
        private readonly Player r_Player2;
        private int m_GameRoundCounter;
        private readonly Board r_TheGameBoard;
        private bool m_isGameOver;
        private bool m_isContinueToAnotherRound;

        public FourInARow(int i_BoardWidth, int i_BoardLength, int playerType)
        {
            r_TheGameBoard = new Board(i_BoardWidth, i_BoardLength);

            r_Player1 = new Player(2, 1);
            r_Player2 = new Player(playerType, 2);
            m_GameRoundCounter = 0;
            m_isGameOver = false;
        }

        public Board TheGameBoard
        {
            get
            {
                return r_TheGameBoard;
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
        public bool isContinueToAnotherRound
        {
            get
            {
                return m_isContinueToAnotherRound;
            }

            set
            {
                m_isContinueToAnotherRound = value;
            }
        }

        public void initGame()
        {
            isGameOver = false;
            m_isContinueToAnotherRound = true;
            m_GameRoundCounter = 0;
            r_TheGameBoard.InitBoard(r_TheGameBoard.BoardLength, r_TheGameBoard.BoardWidth);
            Ex02.ConsoleUtils.Screen.Clear();
        }

        public Player GetCurrentPlayer()
        {
            return m_GameRoundCounter % 2 == 0 ? r_Player1 : r_Player2;
        }

        public Player GetPreviousPlayer()
        {
            return m_GameRoundCounter % 2 != 0 ? r_Player1 : r_Player2;
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

        public bool IsPlayerWon(int i_CurrentChipRow, int i_PlayerColumnChoice)
        {
            bool isPlayerWon = false;
            isPlayerWon =
                r_TheGameBoard.IsFourInARow(
                    GetCurrentPlayer().PlayerLetterType,
                    i_CurrentChipRow,
                    i_PlayerColumnChoice - 1) || r_TheGameBoard.IsFourInADiagonal(GetCurrentPlayer().PlayerLetterType)
                                              || r_TheGameBoard.IsFourInACol(
                                                  GetCurrentPlayer().PlayerLetterType,
                                                  i_CurrentChipRow,
                                                  i_PlayerColumnChoice - 1);
            return isPlayerWon;
        }

        public int GetComputerChoice()
        {
            System.Console.WriteLine("The computer turn");
            Thread.Sleep(500);
            int computerChoice = r_TheGameBoard.GetFirstAvailableColumn();
            return computerChoice;
        }

        public bool IsPlayerChoiceIsNumber(string i_PlayerChoice)
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

        public bool IsPlayerChoiceColumnInRange(string i_PlayerChoice, ref int io_PlayerColumnChoice)
        {
            bool isValidPlayerChoiceColumn = true;

            io_PlayerColumnChoice = int.Parse(i_PlayerChoice);
            isValidPlayerChoiceColumn = io_PlayerColumnChoice <= r_TheGameBoard.BoardWidth;

            return isValidPlayerChoiceColumn;
        }
    }
}