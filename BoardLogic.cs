using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2
{
    public class BoardLogic
    {
        private eStatus[,] m_Board;
        private readonly int m_Width;
        private readonly int m_Length;
        private int[] m_AvailableSpotEachColumn;

        public BoardLogic(int i_Length, int i_Width)
        {
            m_Width = i_Width;
            m_Length = i_Length;
            m_AvailableSpotEachColumn = new int[i_Width];
            InitializeBoard();
        }

        public eStatus[,] Board
        {
            get
            {
                return m_Board;
            }
        }

        public int Length
        {
            get
            {
                return m_Length;
            }
        }

        public int Width
        {
            get
            {
                return m_Width;
            }
        }

        public int[] AvailableSpotEachColumn
        {
            get
            {
                return m_AvailableSpotEachColumn;
            }
        }

        public void InitializeBoard()
        {
            for (int column = 0; column < m_AvailableSpotEachColumn.Length; column++)
            {
                m_AvailableSpotEachColumn[column] = m_Length - 1;
            }

            m_Board = new eStatus[m_Length, m_Width];
        }
        
        public void AddTokenToColumn(eStatus i_PlayerName, int i_RealColumn)
        {
            int matchingRow;
            eStatus k_EmptySpot = eStatus.Empty;
                        
            if (i_PlayerName != k_EmptySpot)
            {
                matchingRow = m_AvailableSpotEachColumn[i_RealColumn];
                m_Board[matchingRow, i_RealColumn] = i_PlayerName;
                m_AvailableSpotEachColumn[i_RealColumn]--;
            }
            else
            {
                m_AvailableSpotEachColumn[i_RealColumn]++;
                matchingRow = m_AvailableSpotEachColumn[i_RealColumn];
                m_Board[matchingRow, i_RealColumn] = i_PlayerName;
            }

        }

        public bool IsFullBoard()
        {
            bool isFullBoard = true;

            foreach (int column in m_AvailableSpotEachColumn)
            {
                if (column != -1)
                {
                    isFullBoard = false;
                    break;
                }

            }

            return isFullBoard;
        }
    }
}
