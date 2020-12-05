using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using UnityEditor;
using SimpleJSON;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    public Text resultWave;
    public InputField nameText;
    public int waveCount;
    public string userName;

    public static string identifyName;
    public static int gameScore;

    // Start is called before the first frame update
    void Start()
    {
        
        userName = StatusSetManage.registrationUser;
        waveCount = GameFlowManager.WaveCount;
        resultWave.text = "Your Result : Wave" + waveCount;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnclickRegistration()
    {
        identifyName = nameText.text;
        gameScore = waveCount;

        User user = new User();

        RestClient.Put("https://apigame-39.firebaseio.com/Ranking/" + PlayerPrefs.GetString("PlayerId") + ".json", user);
        
    }

    public void GotoRanking()
    {
        GameFlowManager.WaveCount = 1;
        SceneManager.LoadScene("Ranking");
        
    }

    public void Retry()
    {
        GameFlowManager.WaveCount = 1;
        SceneManager.LoadScene("MainGameScene");
    }
}
