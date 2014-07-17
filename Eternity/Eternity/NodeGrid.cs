using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eternity
{
    public class NodeGrid
    {
        int cell_countX, cell_countY;
        
        TerrainManager m_tMgr;

        public NodeGrid(int x, int y)
        {
            cell_countX = x;
            cell_countY = y;
            m_tMgr = TerrainManager.GetInstance();
        }

        public List<Tuple<int, int>> CalculateNeighbourhood(int i, int j)
        {
            List<Tuple<int, int>> tempNhood = new List<Tuple<int, int>>(0);
            int xoff = -1;
            int yOff = -1;

            for (int num = 0; num < cell_countX; ++num)
            {
                for (int num2 = 0; num2 < cell_countY; ++num2)
                {
                    tempNhood.Add(new Tuple<int, int>(i + xoff, j + yOff));
                    xoff++;
                }
                xoff = -1;
                yOff++;
            }
            return tempNhood;
        }

        public List<Tuple<int, int>> CalculateNeighbourhood(ref Entity e)
        {
            int x = (int)m_tMgr.EntityToNodePosition(ref e).X;
            int y = (int)m_tMgr.EntityToNodePosition(ref e).Y;
            return CalculateNeighbourhood(x, y);
        }
    }
}
