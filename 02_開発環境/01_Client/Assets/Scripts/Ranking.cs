using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranking
{
    public static int Rank;
    public long UserId { get; set; }

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
    // Start is called before the first frame update
    public string GetRankText(int rank)
        {
            
            return $"{rank}ˆÊ {this.UserId} {this.Time}•b";

        }
    }
