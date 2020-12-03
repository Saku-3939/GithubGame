﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using UnityEditor;
using SimpleJSON;
using UnityEngine.SceneManagement;

public class StatusSetManage : MonoBehaviour
{
 
    public static int userFollowerCount;
    public static int userRepositoryCount;
    public static double userStandardDev;
    public static int userTotalContribute;

    public static string registrationUser;

    User user = new User();

    //public string userName;
    //public int totalContribute;
    //public double stddev;
    //public int followers;
    //public int repositoryCount;
    //public string identifyName;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnGetScore()
    {
        RetrieveFromDatabase();
    }

    private void UpdateScore()
    {
    //    Debug.Log("followers :" + user.followers);
    //    Debug.Log("Repository数 :" + user.repositoryCount);
    //    Debug.Log("標準偏差 :" + user.stddev);
    //    Debug.Log("コントリビュート数 :" + user.totalContribute);

        userFollowerCount = user.followers;
        userRepositoryCount = user.repositoryCount;
        userStandardDev = user.stddev;
        userTotalContribute = user.totalContribute;

        SceneManager.LoadScene("MainGameScene");
    }

    private void RetrieveFromDatabase()
    {
       
        if(PlayerPrefs.HasKey("PlayerId"))
        {
            RestClient.Get<User>("https://apigame-39.firebaseio.com/Ranking/" + PlayerPrefs.GetString("PlayerId") + ".json").Then(response =>
            {
                user = response;
                UpdateScore();
            });
        }
        else
        {
            SceneManager.LoadScene("DataRegistration");
        }

    }

    public void ClearPrefs()
    {
        PlayerPrefs.DeleteKey("PlayerId");
    }
}