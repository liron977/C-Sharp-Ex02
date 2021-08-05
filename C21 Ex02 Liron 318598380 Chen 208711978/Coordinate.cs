using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C21_Ex02_Liron_318598380_Chen_208711978
{
    //public/private?
   public struct Coordinate
    {
        private int m_Row;
        private int m_Col;

        public Coordinate(int i_Row, int i_Col)
        {
            m_Row = i_Row;
            m_Col = i_Col;
        }

        public int CoordinateRow
        {
            get
            {
                return m_Row;
            }

            set
            {
                m_Row = value;
            }
        }

        public int CoordinateCol
        {
            get
            {
                return m_Col;
            }

            set
            {
                m_Col = value;
            }
        }


    }
}
