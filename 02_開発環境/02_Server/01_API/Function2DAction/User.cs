using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Function2DAction
{
    internal class User:Ranking
    {
        public int StageId { get; set; }

        public User()
        {
            UserId = 0;
            StageId = 0;
            Time = 0;
        }
        public User(int user, int stage, float time)
        {
            UserId = user;
            StageId = stage;
            Time = time;
        }
    }
}
