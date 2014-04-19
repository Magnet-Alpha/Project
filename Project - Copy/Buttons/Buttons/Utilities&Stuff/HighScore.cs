using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Buttons
{
    public class HighScore
    {
        public long score;
        public string name;

        public HighScore(string name, long score)
        {
            this.name = name;
            this.score = score;
        }

    }
}
