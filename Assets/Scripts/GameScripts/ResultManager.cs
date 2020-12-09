using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using UnityEditor;
using SimpleJSON;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class ResultManager : MonoBehaviour
{
    private DatabaseReference _FirebaseDB;

    public GameObject errorMessage;
    public GameObject successMessage;
    public Text resultWave;
    public Text damageText;
    public Text scoreText;

    public InputField nameText;
    public int waveCount;
    public float totalDamage;
    public float totalTakenDamage;
    public string userName;

    

    public static string identifyName;
    public static int gameScore;


    // Start is called before the first frame update
    void Start()
    {
        _FirebaseDB = FirebaseDatabase.DefaultInstance.GetReference("/Ranking");

        userName = StatusSetManage.registrationUser;
        waveCount = GameFlowManager.WaveCount;
        totalTakenDamage = Damageable.totalPlayerTakenDamage;
        totalDamage = Damageable.totalPlayerDamage;
        

        gameScore = waveCount * 10 + (100 - (int)totalTakenDamage / 10) + (int)totalDamage /10;

        resultWave.text = waveCount.ToString();
        damageText.text = totalTakenDamage.ToString();
        scoreText.text = gameScore.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnclickRegistration()
    {
        identifyName = nameText.text;

        if(identifyName != "")
        {
            _FirebaseDB.Child(PlayerPrefs.GetString("PlayerId")).Child("identifyName").SetValueAsync(identifyName);
            _FirebaseDB.Child(PlayerPrefs.GetString("PlayerId")).Child("gameScore").SetValueAsync(gameScore);

            successMessage.SetActive(true);
        }
        else
        {
            Debug.Log("ランキングに登録する名前を入力してください");
            errorMessage.SetActive(true);
        }


        //User user = new User();



        //RestClient.Put("https://apigame-39.firebaseio.com/Ranking/" + PlayerPrefs.GetString("PlayerId") + ".json", user);

    }

    public void GotoRanking()
    {
        Damageable.totalPlayerTakenDamage = 0;
        Damageable.totalPlayerDamage = 0;
        GameFlowManager.WaveCount = 1;
        SceneManager.LoadScene("Ranking");
        
    }

    public void Retry()
    {
        Damageable.totalPlayerTakenDamage = 0;
        Damageable.totalPlayerDamage = 0;
        GameFlowManager.WaveCount = 1;
        SceneManager.LoadScene("MainGameScene");
    }
}
