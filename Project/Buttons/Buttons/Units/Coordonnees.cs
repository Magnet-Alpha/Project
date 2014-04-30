using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Buttons
{
    class Coordonnees
    {
        public int xd;
        public int xf;
        public int yd;
        public int yf;

        public Coordonnees(int xd, int xf, int yd, int yf)
        {
            this.xd = xd;
            this.xf = xf;
            this.yd = yd;
            this.yf = yf;
        }

        public void Fill(ref Tower[,] towers)
        {
            int x = xd;
            while (x <= xf)
            {
                int xi = x / 2;
                int y = yd;
                if (x % 2 == 0)
                    y--;
                while (y <= yf)
                {
                    towers[xi, y] = new Tower();
                    y = y + 2;
                }
                x++;
            }
        }
    }
}
