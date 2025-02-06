using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Function2DAction
{
    internal class Ranking
    {
        public int UserId { get; set; }

        public float Time { get; set; }

        public Ranking()
        {
            UserId = 0;

            Time = 0;
        }
        public Ranking(int user, float time)
        {
            UserId = user;
            Time = time;
        }
    }
}
