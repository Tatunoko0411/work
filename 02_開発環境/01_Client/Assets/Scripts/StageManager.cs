using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static int stageID=1;
    public static long TempUserID=0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadStage1()
    {
        stageID = 1;
        Initiate.Fade("GameScene", Color.black, 1.0f);
    }

    public void LoadStage2()
    {
        stageID = 2;
        Initiate.Fade("Stage2", Color.black, 1.0f);
    }

    public void BackScene()
    {
        Initiate.Fade("TitleScene", Color.black, 1.0f);
    }
}
