using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    [SerializeField] GameObject rankItemPrefab;
    [SerializeField] GameObject parentGameObject;
    [SerializeField] Text UserIdText;
    public bool isAdd;

    int stageID;

    long userId = 0;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        if (isAdd)
        {


            userId = TitleManager.UserID;
            stageID = StageManager.stageID;
            Ranking.Rank = 0;
            time = PlayerManager.GetTime();

            if (userId <= 0)
            {
                LoadUserID();
            }
            else
            {
                UserIdText.text = $"UserID:{userId}";
                UpdateRanking();
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadRanking()
    {

        ResetRanking();
        StartCoroutine("GetRanking");
    }

    public void LoadTitleRanking(int stage)
    {
        ResetRanking();
        if (stage == 1)
        {

            StartCoroutine("GetRankingStage1");
        }
        else if (stage == 2)
        {
            StartCoroutine("GetRankingStage2");

        }
    }

    IEnumerator GetRanking()
    {


        //string stage = JsonConvert.SerializeObject(stageID);
        UnityWebRequest request = UnityWebRequest.Get($"https://functionappge202402.azurewebsites.net/api/ranking/get?stage={stageID}");
        // request.SetRequestHeader("stage", stage);
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("x-functions-key", "e-L4VqC3rter_KNKNWvL-35mdvCzH_b0ltPFxSLgfMLYAzFu4Bzu4A==");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            List<Ranking> rankingList = JsonConvert.DeserializeObject<List<Ranking>>(json);
            int rank = 0;
            foreach (Ranking ranking in rankingList)
            {
                rank++;
                GameObject textObject = Instantiate(
                    rankItemPrefab,
                    parentGameObject.transform.position,
                    Quaternion.identity,
                    parentGameObject.transform
                    );
                textObject.GetComponent<Text>().text = ranking.GetRankText(rank);
            }
        }
        else
        {
            Debug.Log("Error:GetHiscore UnityWebRequest Failed");
        }
    }

    public void UpdateRanking()
    {
        ResetRanking();
        StartCoroutine("AddHiscore");
    }

    IEnumerator AddHiscore()
    {


        // ハイスコアのインスタンスを作成してJsonシリアライズ
        User ranking = new User(userId, stageID, time);
        string json = JsonConvert.SerializeObject(ranking);

        UnityWebRequest request = UnityWebRequest.Post(
            "https://functionappge202402.azurewebsites.net/api/ranking/add",
            json,
            "applicastion/jspn");
        request.SetRequestHeader("x-functions-key", "e-L4VqC3rter_KNKNWvL-35mdvCzH_b0ltPFxSLgfMLYAzFu4Bzu4A==");

        Debug.Log("ハイスコアを送信します");

        //APIとの通信が完了するまで待機
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error:AddHiscore UnityRequest Failsd");
        }
        else
        {
            Debug.Log("ハイスコア送信完了！");
        }
        LoadRanking();

    }

    public void LoadUserID()
    {
        StartCoroutine("GetUserID");
    }

    IEnumerator GetUserID()
    {
        UnityWebRequest request = UnityWebRequest.Get($"https://functionappge202402.azurewebsites.net/api/member/get");
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("x-functions-key", "e-L4VqC3rter_KNKNWvL-35mdvCzH_b0ltPFxSLgfMLYAzFu4Bzu4A==");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            userId = JsonConvert.DeserializeObject<long>(json);
            userId++;
            UserIdText.text = $"UserID:{userId}";
        }
        else
        {
            Debug.Log("Error:GetHiscore UnityWebRequest Failed");
        }
        UpdateRanking();
    }

    public void ResetRanking()
    {
        GameObject[] Rankings = GameObject.FindGameObjectsWithTag("Ranking");

        foreach (GameObject r in Rankings)
        {
            Destroy(r);
        }
        Ranking.Rank = 0;
    }

    IEnumerator GetRankingStage1()
    {


        //string stage = JsonConvert.SerializeObject(stageID);
        UnityWebRequest request = UnityWebRequest.Get($"https://functionappge202402.azurewebsites.net/api/ranking/get?stage=1");
        // request.SetRequestHeader("stage", stage);
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("x-functions-key", "e-L4VqC3rter_KNKNWvL-35mdvCzH_b0ltPFxSLgfMLYAzFu4Bzu4A==");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            List<Ranking> rankingList = JsonConvert.DeserializeObject<List<Ranking>>(json);
            int rank = 0;
            foreach (Ranking ranking in rankingList)
            {
                rank++;
                GameObject textObject = Instantiate(
                    rankItemPrefab,
                    parentGameObject.transform.position,
                    Quaternion.identity,
                    parentGameObject.transform
                    );
                textObject.GetComponent<Text>().text = ranking.GetRankText(rank);
            }
        }
        else
        {
            Debug.Log("Error:GetHiscore UnityWebRequest Failed");
        }
    }

    IEnumerator GetRankingStage2()
    {


        //string stage = JsonConvert.SerializeObject(stageID);
        UnityWebRequest request = UnityWebRequest.Get($"https://functionappge202402.azurewebsites.net/api/ranking/get?stage=2");
        // request.SetRequestHeader("stage", stage);
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("x-functions-key", "e-L4VqC3rter_KNKNWvL-35mdvCzH_b0ltPFxSLgfMLYAzFu4Bzu4A==");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            List<Ranking> rankingList = JsonConvert.DeserializeObject<List<Ranking>>(json);
            int rank=0;
            foreach (Ranking ranking in rankingList)
            {
                rank++;
                GameObject textObject = Instantiate(
                    rankItemPrefab,
                    parentGameObject.transform.position,
                    Quaternion.identity,
                    parentGameObject.transform
                    );
                textObject.GetComponent<Text>().text = ranking.GetRankText(rank);
            }
        }
        else
        {
            Debug.Log("Error:GetHiscore UnityWebRequest Failed");
        }
    }
}
