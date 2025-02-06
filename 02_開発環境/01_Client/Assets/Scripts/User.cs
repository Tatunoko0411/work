using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : Ranking
{
    // Start is called before the first frame update
    public int StageId { get; set; }

    public User()
    {
        UserId = 0;
        StageId = 0;
        Time = 0;
    }
    public User(long user, int stage, float time)
    {
        UserId = user;
        StageId = stage;
        Time = time;
    }
}
