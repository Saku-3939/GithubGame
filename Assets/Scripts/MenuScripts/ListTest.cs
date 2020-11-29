using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using UnityEditor;
using SimpleJSON;

public class ListTest : MonoBehaviour
{
    public List<string> nameList = new List<string>();
    public InputField nameText;
    public InputField repoText;
    public Text scoreText;

    public static string playerName;
    public static string repoName;
    public static int contributeCount;


    public void onGetName()
    {
        playerName = nameText.text;
        repoName = repoText.text;
        RetriveFromDatabase();
    }

    public void RenderResult()
    {
        for (int i = 0; i < nameList.Count; i++)
        {
            string name = nameList[i].ToString();
        }
        Debug.Log(nameList.Count);

        scoreText.text = contributeCount.ToString();
        
    }

    private void RetriveFromDatabase()
    {
        RestClient.Get("https://api.github.com/repos/" + playerName + "/" + repoName + "/stats/contributors").Then(response => {

            string JsonString = response.Text;
            JSONNode json = JSONNode.Parse(JsonString);

            for(int i = 0; i < json.Count; i++)
            {
                    if (json[i]["author"]["login"] == playerName)
                    {
                        contributeCount = json[i]["total"];
                    }

            }

            RenderResult();

        });
    }
}
