using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using UnityEditor;
using SimpleJSON;

public class PlayerScores : MonoBehaviour
{
    public Text scoreText;
    public Text collaboText;
    public Text contributeText;
    public InputField nameText;
    public InputField repoText;

    private System.Random random = new System.Random();

    User user = new User();

    public static int playerScore;
    public static int contributeCount;
    public static string playerName;
    public static string repoName;
    public static string collaboName;


    // Start is called before the first frame update
    void Start()
    {
        //playerScore = random.Next(0, 101);
        scoreText.text = "Score:" + playerScore;

        //RestClient.Get("https://api.github.com/repos/Saku-3939/TechTrain_githubTutorial/stats/contributors").Then(response => {
        //    string JsonString = response.Text;
        //    JSONNode JsonNode = JSON.Parse(JsonString);
        //    EditorUtility.DisplayDialog("Response", JsonNode[0]["total"] , "Ok");
        //});
    }

    public void OnGetScore()
    {
        playerName = nameText.text;
        repoName = repoText.text;
        RetriveFromDatabase();
    }

    private void UpdateScore()
    {
        //playerScore = user.userScore;
        //scoreText.text = "Score:" + user.userScore;
        collaboText.text = collaboName;
    }

    public void OnSubmit()
    {
        playerName = nameText.text;
        contributeCount = int.Parse(contributeText.text);
        PostToDatabase();
    }

    private void PostToDatabase()
    {
        User user = new User();
        
        RestClient.Put("https://apigame-39.firebaseio.com/" + playerName + ".json", user);
    }

    private void RetriveFromDatabase()
    {
        RestClient.Get("https://api.github.com/repos/" + playerName + "/" + repoName + "/stats/contributors").Then(response =>
        {
            var json = JsonUtility.ToJson(response);
            string JsonString = response.Text;
            JSONNode JsonNode = JSON.Parse(JsonString);

            Debug.Log(json.Length);

            //user.userScore = JsonNode[0]["total"];
            collaboName = JsonNode[0]["author"]["login"];
            UpdateScore();
        });
    }
}
