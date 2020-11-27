using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using UnityEditor;
using SimpleJSON;
using System.Linq;

public class AverageTest : MonoBehaviour
{
    public InputField nameText;
    public InputField repoText;

    public static string playerName;
    public static string repoName;
    public static int totalRepository;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick()
    {
        playerName = nameText.text;
        repoName = repoText.text;

        //RestClient.Get("https://api.github.com/repos/" + playerName + "/" + repoName + "/stats/participation").Then(response =>
        //{
        //    string jsonString = response.Text;
        //    JSONNode json = JSONNode.Parse(jsonString);

        //    double sumCommit_m = 0, sumCommit_v = 0;

        //    double mean = 0;
        //    double variance = 0;
        //    double stddev = 0;

        //    for (int i = 0; i < json["owner"].Count; i++)
        //    {
        //        sumCommit_m += json["owner"][i];
        //        sumCommit_v += json["owner"][i] * json["owner"][i];
        //    }

        //    mean = sumCommit_m / json["owner"].Count;

        //    variance = (sumCommit_v / json["owner"].Count) - (mean * mean);

        //    stddev = Math.Sqrt(variance);

        //    Debug.Log(mean);
        //    Debug.Log(variance);
        //    Debug.Log(stddev);
        //});

        //一番アクティブな曜日の取得
        //RestClient.Get("https://api.github.com/repos/" + playerName + "/" + repoName + "/stats/punch_card").Then(response =>
        //{
        //    string jsonString = response.Text;
        //    JSONNode json = JSONNode.Parse(jsonString);
        //    int sunSum = 0, monSum =0, tueSum = 0, wedSum = 0, thuSum = 0, friSum = 0, satSum = 0;

        //    for(int i = 0; i <= json.Count; i++)
        //    {
        //        if(i <= 24)
        //        {
        //            sunSum += json[i][2];
        //        }else if(i <= 48)
        //        {
        //            monSum += json[i][2];
        //        }else if(i <= 72)
        //        {
        //            tueSum += json[i][2];
        //        }else if(i <= 96)
        //        {
        //            wedSum += json[i][2];
        //        }else if(i <= 120)
        //        {
        //            thuSum += json[i][2];
        //        }else if(i <= 144)
        //        {
        //            friSum += json[i][2];
        //        }else if(i <= 168)
        //        {
        //            satSum += json[i][2];
        //        }
        //    }



        //    Debug.Log(sunSum);
        //    Debug.Log(monSum);
        //    Debug.Log(tueSum);
        //    Debug.Log(wedSum);
        //    Debug.Log(thuSum);
        //    Debug.Log(friSum);
        //    Debug.Log(satSum);
        //});

        //リポジトリ数
        RestClient.Get("https://api.github.com/users/" + playerName + "/repos").Then(response =>
        {
            string jsonString = response.Text;
            JSONNode json = JSONNode.Parse(jsonString);

            totalRepository = json.Count;
            Debug.Log(totalRepository);
        });

    }
}
