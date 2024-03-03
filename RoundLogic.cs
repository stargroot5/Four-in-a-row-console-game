using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2
{
    public class RoundLogic
    {
        private BoardLogic m_RoundBoard;
        private readonly bool m_IsAgainstComputer;
        private ScoreBoard m_ScoreBoard;
                       
        public RoundLogic(int i_Length, int i_Width, bool i_IsAgainstComputer)
        {
            m_IsAgainstComputer = i_IsAgainstComputer;
            m_RoundBoard = new BoardLogic(i_Length, i_Width);
            m_ScoreBoard = new ScoreBoard();
        }
        
        public BoardLogic RoundBoard
        {
            get
            {
                return m_RoundBoard;
            }
        }
        
        public ScoreBoard ScoreBoard
        {
            get
            {
                return m_ScoreBoard;
            }
        }

        public bool IsAgainstComputer
        {
            get
            {
                return m_IsAgainstComputer;
            }
        }
       
        public bool IsWinner(int i_ChosenRow, int i_RealColumn, eStatus i_PlayerName)
        {
            bool isWinner;
            const int k_NumberInARow = 4;

            isWinner = IsNumberInARow(k_NumberInARow, i_ChosenRow, i_RealColumn, i_PlayerName);

            return isWinner;
        }
        
        public eStatus NextPlayer(eStatus i_CurrentPlayer)
        {
            eStatus nextPlayer = i_CurrentPlayer == eStatus.PlayerOne ? eStatus.PlayerTwo : eStatus.PlayerOne;

            return nextPlayer;
        }
        
        public int ComputerChosenColumn()
        {
            int chosenColumn;
            int matchingRow;
            bool isNumberInRow = false;
            var rand = new Random();

            chosenColumn = rand.Next(0, m_RoundBoard.Width);
            while (m_RoundBoard.AvailableSpotEachColumn[chosenColumn] == -1)
            {
                chosenColumn = rand.Next(0, m_RoundBoard.Width);
            }

            for (int numberInARow = 4; numberInARow > 1; numberInARow--)
            {
                for (int column = 0; column < m_RoundBoard.Width; column++)
                {
                    matchingRow = m_RoundBoard.AvailableSpotEachColumn[column];
                    if (matchingRow >= 0)
                    {
                        isNumberInRow = IsBestColumnOption(numberInARow, column,matchingRow);
                        if (isNumberInRow)
                        {
                            chosenColumn = column;
                            break;
                        }
                        else
                        {
                            isNumberInRow = false;
                        }

                    }
                }
                
                if (isNumberInRow)
                {
                    break;
                }

            }

            return chosenColumn;
        }

        public bool IsBestColumnOption(int i_NumberInARow, int i_TestColumn, int i_TestRow)
        {
            bool isBestColumnOption = false;
            bool isNumberInARow = false;
            int counterToVictory = 0;
            const eStatus k_ComputerPlayer = eStatus.PlayerTwo;
            const eStatus k_EmptySpot = eStatus.Empty;

            m_RoundBoard.AddTokenToColumn(k_ComputerPlayer, i_TestColumn);
            isNumberInARow = CheckNumberInARowInColumns(i_NumberInARow, i_TestColumn, ref counterToVictory, k_ComputerPlayer);
            if (isNumberInARow && (m_RoundBoard.AvailableSpotEachColumn[i_TestColumn] >= (4 - i_NumberInARow)))
            {
                isBestColumnOption = true;
            }

            isNumberInARow = CheckNumberInARowInRows(i_NumberInARow, i_TestRow, ref counterToVictory, k_ComputerPlayer);
            if ((!isBestColumnOption) && isNumberInARow && IsEnoughSpotsInRow(i_TestRow, k_ComputerPlayer))
            {
                isBestColumnOption = true;
            }

            isNumberInARow = CheckNumberInARowInFirstDiagonal(i_NumberInARow, i_TestRow, i_TestColumn, ref counterToVictory, k_ComputerPlayer);
            if ((!isBestColumnOption) && isNumberInARow)
            {
                isBestColumnOption = true;
            }

            isNumberInARow = CheckNumberInARowInSecondDiagonal(i_NumberInARow, i_TestRow, i_TestColumn, ref counterToVictory, k_ComputerPlayer);
            if ((!isBestColumnOption) && isNumberInARow)
            {
                isBestColumnOption = true;
            }

            m_RoundBoard.AddTokenToColumn(k_EmptySpot, i_TestColumn);

            return isBestColumnOption;
        }

        public bool IsEnoughSpotsInRow(int i_Row, eStatus i_PlayerName)
        {
            bool isEnoughSpotsInRow = false;
            int counterOfPotentialSpotsToVictory = 0;
            const eStatus k_EmptySpot = eStatus.Empty;

            for(int column = 0; column < m_RoundBoard.Width; column++)
            {
                if((m_RoundBoard.Board[i_Row, column]== k_EmptySpot)||(m_RoundBoard.Board[i_Row, column] == i_PlayerName))
                {
                    counterOfPotentialSpotsToVictory++;
                }
                else
                {
                    counterOfPotentialSpotsToVictory = 0;
                }

            }

            isEnoughSpotsInRow = counterOfPotentialSpotsToVictory == 4;

            return isEnoughSpotsInRow;
        }

        public bool CheckNumberInARow(int i_NumberInARow, int i_CurrentRow, int i_CurrentColumn, ref int io_counterToVictory, eStatus i_PlayerName)
        {
            bool isWinner = false;

            if (m_RoundBoard.Board[i_CurrentRow, i_CurrentColumn] == i_PlayerName)
            {
                io_counterToVictory++;
            }
            else
            {
                io_counterToVictory = 0;
            }

            if (io_counterToVictory == i_NumberInARow)
            {
                isWinner = true;
            }

            return isWinner;
        }

        public bool IsNumberInARow(int i_NumberInARow, int i_ChosenRow, int i_RealColumn, eStatus i_PlayerName)
        {
            bool isWinner = false;
            int counterToVictory = 0;

            isWinner = CheckNumberInARowInRows(i_NumberInARow, i_ChosenRow, ref counterToVictory, i_PlayerName);
                       
            isWinner = isWinner || CheckNumberInARowInColumns(i_NumberInARow, i_RealColumn, ref counterToVictory, i_PlayerName);

            isWinner = isWinner || CheckNumberInARowInFirstDiagonal(i_NumberInARow, i_ChosenRow, i_RealColumn,
                ref counterToVictory, i_PlayerName);

            isWinner = isWinner || CheckNumberInARowInSecondDiagonal(i_NumberInARow, i_ChosenRow, i_RealColumn,
                ref counterToVictory, i_PlayerName);

            return isWinner;
        }

        public bool CheckNumberInARowInRows(int i_NumberInARow, int i_ChosenRow, ref int io_CounterToVictory, eStatus i_PlayerName)
        {
            //Row
            bool isWinner = false;

            io_CounterToVictory = 0;
            for (int column = 0; column < m_RoundBoard.Width; column++)
            {
                if (isWinner)
                {
                    break;
                }

                isWinner = CheckNumberInARow(i_NumberInARow, i_ChosenRow, column, ref io_CounterToVictory, i_PlayerName);
            }

            return isWinner;
        }

        public bool CheckNumberInARowInColumns(int i_NumberInARow, int i_RealColumn, ref int io_CounterToVictory, eStatus i_PlayerName)
        {
            //Column
            bool isWinner = false;

            io_CounterToVictory = 0;
            for (int row = m_RoundBoard.Length - 1; row >= 0; row--)
            {
                if (isWinner)
                {
                    break;
                }

                isWinner = CheckNumberInARow(i_NumberInARow, row, i_RealColumn, ref io_CounterToVictory, i_PlayerName);
            }

            return isWinner;
        }

        public bool CheckNumberInARowInFirstDiagonal(int i_NumberInARow, int i_ChosenRow, int i_RealColumn, ref int io_CounterToVictory, eStatus i_PlayerName)
        {
            //Top left to bottom right
            bool isWinner = false;
            int diagonalRow, diagonalColumn;

            io_CounterToVictory = 0;
            if (i_RealColumn < i_ChosenRow)
            {
                diagonalRow = i_ChosenRow - i_RealColumn;
                diagonalColumn = 0;
            }
            else
            {
                diagonalRow = 0;
                diagonalColumn = i_RealColumn - i_ChosenRow;
            }

            while (diagonalColumn != (m_RoundBoard.Width) && diagonalRow != (m_RoundBoard.Length))
            {
                if (isWinner)
                {
                    break;
                }

                isWinner = CheckNumberInARow(i_NumberInARow, diagonalRow, diagonalColumn, ref io_CounterToVictory, i_PlayerName);
                diagonalColumn++;
                diagonalRow++;
            }

            return isWinner;
        }

        public bool CheckNumberInARowInSecondDiagonal(int i_NumberInARow, int i_ChosenRow, int i_RealColumn, ref int io_CounterToVictory, eStatus i_PlayerName)
        {
            //Top right to bottom left
            bool isWinner = false;
            int diagonalRow, diagonalColumn;

            io_CounterToVictory = 0;
            if (i_RealColumn + i_ChosenRow < m_RoundBoard.Width)
            {
                diagonalColumn = i_ChosenRow + i_RealColumn;
                diagonalRow = 0;
            }
            else
            {
                diagonalRow = i_ChosenRow - m_RoundBoard.Width + i_RealColumn + 1;
                diagonalColumn = m_RoundBoard.Width - 1;
            }

            while (diagonalColumn >= 0 && diagonalRow < (m_RoundBoard.Length))
            {
                if (isWinner)
                {
                    break;
                }

                isWinner = CheckNumberInARow(i_NumberInARow, diagonalRow, diagonalColumn, ref io_CounterToVictory, i_PlayerName);
                diagonalColumn--;
                diagonalRow++;
            }

            return isWinner;
        }
    }
}