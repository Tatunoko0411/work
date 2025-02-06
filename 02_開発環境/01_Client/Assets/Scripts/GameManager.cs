using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text scoreText;

    [SerializeField] GameObject HelpObj;
    [SerializeField] GameObject RemoveObj;

    //SE
    [SerializeField] AudioClip gameClearSE;
    [SerializeField] AudioClip gameOverSE;
    AudioSource audioSource;


    public static int deathTime = 0;
    const int MAX_SCORE = 9999;
    int score = 0;
    // Start is called before the first frame update

    private void Start()
    {
        if(deathTime > 10)
        {
            HelpObj.SetActive(true);
            RemoveObj.SetActive(false);
        }
       // scoreText.text = score.ToString();
        audioSource = GetComponent<AudioSource>();
    }

    public void AddScore(int val)
    {
        score += val;
        if (score > MAX_SCORE)
        {
            score = MAX_SCORE;
        }
        scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        //  gameOverText.SetActive(true);
        deathTime++;
        audioSource.PlayOneShot(gameOverSE);
        Invoke("RestartScene", 1.5f);
    }
    public void GameClear()
    {
       // gameClearText.SetActive(true);
        audioSource.PlayOneShot(gameClearSE);
        Invoke("RestartScene", 1.5f);
    }

    public void RestartScene()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }
    
    public void Retry()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        Initiate.Fade(thisScene.name, Color.black, 1.0f);
    }

    public void GoTitle()
    {
        Initiate.Fade("TitleScene",Color.black, 1.0f);
    }
}
