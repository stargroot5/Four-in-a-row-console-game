using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2
{
    public class ScoreBoard
    {
        private int m_FirstPlayerPoints;
        private int m_SecondPlayerPoints;

        public ScoreBoard() 
        {
            m_FirstPlayerPoints = 0;
            m_SecondPlayerPoints = 0;
        }

        public int FirstPlayerPoints
        {
            get
            {
                return m_FirstPlayerPoints;
            }
        }
        
        public int SecondPlayerPoints
        {
            get
            {
                return m_SecondPlayerPoints;
            }
        }

        public void UpdateScoreBoard(eStatus i_winnerPlayer)
        {
            if (i_winnerPlayer == eStatus.PlayerOne)
            {
                m_FirstPlayerPoints++;
            }
            else if (i_winnerPlayer == eStatus.PlayerTwo)
            {
                m_SecondPlayerPoints++;
            }

        }
    }
}
