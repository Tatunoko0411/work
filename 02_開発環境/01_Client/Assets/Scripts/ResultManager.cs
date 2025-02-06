using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField] Text timeText;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        time = PlayerManager.GetTime();
        timeText.text = ($"Your Time:{time.ToString("F2")}sec");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadTitle()
    {
        Initiate.Fade("TitleScene", Color.black, 1.0f);
    }
}
