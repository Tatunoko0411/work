using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public List<GameObject> title;
    public List<GameObject> info;
    public List<GameObject> display;
    public List<GameObject> Ranking;
    public GameObject displayIcon;
    public static long UserID = 0;
    public static bool isDisplay = false;
   [SerializeField] public InputField inputField;

    void Start()
    {
        GameManager.deathTime = 0;
        UserID = 0;
        //Component��������悤�ɂ���
        inputField = inputField.GetComponent<InputField>();

    }

    void Update()
    {
        if(Input.GetKey(KeyCode.A)&& Input.GetKey(KeyCode.T)&& Input.GetKeyDown(KeyCode.U))
        {
            if (isDisplay)
            {
                isDisplay = false;
                displayIcon.SetActive(false);
            }
            else
            {
                isDisplay = true;
                displayIcon.SetActive(true);
            }
        
        }
    }

    public void InputText()
    {
        if(inputField.text != null)
        {
            UserID = int.Parse(inputField.text);
        }
        //�e�L�X�g��inputField�̓��e�𔽉f

    }
    public void LoadStage(int stage)
    {
        Initiate.Fade("StageSelectScene", Color.black, 1.0f);
    }

    public void SetInformation()
    {
        for (int i=0;i<title.Count;i++)
        {
            title[i].SetActive(false);
        }
        for(int i = 0; i < info.Count; i++)
        {
            info[i].SetActive(true);
        }
    }

    public void BackTitle()
    {
        for (int i = 0; i < title.Count; i++)
        {
            title[i].SetActive(true);
        }
        for (int i = 0; i < info.Count; i++)
        {
            info[i].SetActive(false);
        }
    }

    public void SetRanking()
    {
        for (int i = 0; i < Ranking.Count; i++)
        {
            Ranking[i].SetActive(true);
        }
        for (int i = 0; i < title.Count; i++)
        {
            title[i].SetActive(false);
        }
    }

    public void BackRanking()
    {
        for (int i = 0; i < Ranking.Count; i++)
        {
            Ranking[i].SetActive(false);
        }
        for (int i = 0; i < title.Count; i++)
        {
            title[i].SetActive(true);
        }
    }

    public void BackDisplay()
    {
        for (int i = 0; i < display.Count; i++)
        {
            display[i].SetActive(false);
        }
    }

    public void quitGame()
    {
#if UNITY_EDITOR
        if (isDisplay)
        {
            for (int i = 0; i < display.Count; i++)
            {
                display[i].SetActive(true);
            }
            Debug.Log("�W�����[�h���s���ł��A�I������ɂ͓W�����[�h���I�����Ă�������");
            return;

        }
        UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
 if (isDisplay)
        {
            for (int i = 0; i < display.Count; i++)
            {
                display[i].SetActive(true);
            }
            Debug.Log("�W�����[�h���s���ł��A�I������ɂ͓W�����[�h���I�����Ă�������");
            return;

        }
    Application.Quit();//�Q�[���v���C�I��
#endif
    }
}
