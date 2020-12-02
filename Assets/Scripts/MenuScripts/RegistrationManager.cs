using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using UnityEditor;
using SimpleJSON;
using UnityEngine.SceneManagement;

public class RegistrationManager : MonoBehaviour
{
    public InputField nameText;
    public InputField repoText;
    public InputField identifyText;

    public static string playerName;
    public static string repoName;
    public static int totalCommit;
    public static int totalRepository;
    public static double standartDeviation;
    public static int followerCount;
    

    User user = new User();

    public void OnclickRegistration()
    {
        playerName = nameText.text;
        repoName = repoText.text;
        

        StartCoroutine(GetData());
        
    }

    IEnumerator GetData()
    {
        GetFromGithub();
        yield return new WaitForSeconds(3);

        PostToDataBase();
    }

    public void GetFromGithub()
    {
        //リポジトリへのコミット数
        RestClient.Get("https://api.github.com/repos/" + playerName + "/" + repoName + "/stats/contributors").Then(response =>
        {
            string jsonString = response.Text;
            JSONNode json = JSONNode.Parse(jsonString);


            for (int i = 0; i < json.Count; i++)
            {
                if (json[i]["author"]["login"] == playerName)
                {
                    totalCommit = json[i]["total"];
                }

            }

        });

        //52週間でのコミット数の標準偏差
        RestClient.Get("https://api.github.com/repos/" + playerName + "/" + repoName + "/stats/participation").Then(response =>
        {
            string jsonString = response.Text;
            JSONNode json = JSONNode.Parse(jsonString);

            double sumCommit_m = 0, sumCommit_v = 0;

            double mean = 0;
            double variance = 0;
            double stddev = 0;

            for (int i = 0; i < json["owner"].Count; i++)
            {
                sumCommit_m += json["owner"][i];
                sumCommit_v += json["owner"][i] * json["owner"][i];
            }

            mean = sumCommit_m / json["owner"].Count;

            variance = (sumCommit_v / json["owner"].Count) - (mean * mean);

            stddev = Math.Sqrt(variance);

            standartDeviation = stddev;

            Debug.Log("平均"+mean);
            Debug.Log("分散"+variance);
            Debug.Log("標準偏差"+stddev);
        });

        //曜日ごとのコミット数
        RestClient.Get("https://api.github.com/repos/" + playerName + "/" + repoName + "/stats/punch_card").Then(response =>
        {
            string jsonString = response.Text;
            JSONNode json = JSONNode.Parse(jsonString);
            int sunSum = 0, monSum = 0, tueSum = 0, wedSum = 0, thuSum = 0, friSum = 0, satSum = 0;

            for (int i = 0; i <= json.Count; i++)
            {
                if (i <= 24)
                {
                    sunSum += json[i][2];
                }
                else if (i <= 48)
                {
                    monSum += json[i][2];
                }
                else if (i <= 72)
                {
                    tueSum += json[i][2];
                }
                else if (i <= 96)
                {
                    wedSum += json[i][2];
                }
                else if (i <= 120)
                {
                    thuSum += json[i][2];
                }
                else if (i <= 144)
                {
                    friSum += json[i][2];
                }
                else if (i <= 168)
                {
                    satSum += json[i][2];
                }
            }

            Debug.Log("日曜日"+sunSum);
            Debug.Log("月曜日"+monSum);
            Debug.Log("火曜日"+tueSum);
            Debug.Log("水曜日"+wedSum);
            Debug.Log("木曜日"+thuSum);
            Debug.Log("金曜日"+friSum);
            Debug.Log("土曜日"+satSum);
        });

        //レポジトリ数
        RestClient.Get("https://api.github.com/users/" + playerName + "/repos").Then(response =>
        {
            string jsonString = response.Text;
            JSONNode json = JSONNode.Parse(jsonString);

            totalRepository = json.Count;
            Debug.Log(totalRepository);
        });

        RestClient.Get("https://api.github.com/users/" + playerName + "/followers").Then(response =>
        {
            string jsonString = response.Text;
            JSONNode json = JSONNode.Parse(jsonString);

            followerCount = json.Count;
        });

    }

    public void PostToDataBase()
    {
        User user = new User();

        RestClient.Put("https://apigame-39.firebaseio.com/Ranking/" + playerName + ".json", user);

        
    }

    public void GoToHome()
    {
        SceneManager.LoadScene("Home");
    }

}
