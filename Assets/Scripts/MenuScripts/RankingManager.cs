using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using UnityEditor;
using SimpleJSON;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class RankingManager : MonoBehaviour
{
    private DatabaseReference _FirebaseDB;

    public Text[] ranking;
    public Text[] rankingScore;
    public Text[] rankingNumber;

    public Text rankingNameText;
    public Text scoreText;

    public List<string> userList = new List<string>();
    public List<int> scoreList = new List<int>();

    public static int totalContribute;
    public static string rankingName;
    public static int followerCount;

    public int rank = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        // Firebase RealtimeDatabase接続初期設定
        //FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://apigame-39.firebaseio.com/");

        // Databaseの参照先設定
        _FirebaseDB = FirebaseDatabase.DefaultInstance.GetReference("/Ranking");

        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

    }

    public void GetFollowerData()
    {


        //コントリビュート数が一番多い人を取得
        _FirebaseDB.OrderByChild("followers").GetValueAsync().ContinueWith(response =>
        {
            //動いてる
            userList.Clear();
            scoreList.Clear();

            if (response.IsFaulted)
            {
                Debug.Log("エラーが発生しました");
            }else if (response.IsCompleted)
            {
                //動いてる
                Debug.Log("成功しました");


                Debug.Log(response.Result.ChildrenCount);

                DataSnapshot snapshot = response.Result;
                IEnumerator<DataSnapshot> result = snapshot.Children.GetEnumerator();

                //snapshotが3つだけ
                Debug.Log(snapshot.ChildrenCount);

                JSONNode json = JSONNode.Parse(snapshot.GetRawJsonValue());




                while (result.MoveNext())
                {

                    DataSnapshot data = result.Current;
                    string name = (string)data.Child("identifyName").GetValue(true);
                    //Firebaseの数値データはLong型となっているので、一度longで受け取った後にintにキャスト
                    int score = (int)(long)data.Child("followers").GetValue(true);

                    userList.Add(name);
                    scoreList.Add(score);

                }
                
            }
        });
    }

    //標準偏差を持ってくる
    public void GetstddevData()
    {
        //コントリビュート数が一番多い人を取得
        _FirebaseDB.OrderByChild("stddev").GetValueAsync().ContinueWith(response =>
        {
            userList.Clear();
            scoreList.Clear();
            if (response.IsFaulted)
            {
                Debug.Log("エラーが発生しました");
            }
            else if (response.IsCompleted)
            {
                Debug.Log("成功しました");


                DataSnapshot snapshot = response.Result;
                IEnumerator<DataSnapshot> result = snapshot.Children.GetEnumerator();
                string jsonString = response.ToString();



                JSONNode json = JSONNode.Parse(snapshot.GetRawJsonValue());




                while (result.MoveNext())
                {

                    DataSnapshot data = result.Current;
                    string name = (string)data.Child("identifyName").GetValue(true);
                    //Firebaseの数値データはLong型となっているので、一度longで受け取った後にintにキャスト
                    double stddev = (double)data.Child("stddev").GetValue(true);

                    double sub = 100 * stddev;

                    int score = 100 - (int)sub;

                    userList.Add(name);
                    scoreList.Add(score);

                }
                userList.Reverse();
                scoreList.Reverse();
            }

        });
    }

    //コントリビュート数を持ってくる
    public void GetContributeData()
    {
        //コントリビュート数が一番多い人を取得
        _FirebaseDB.OrderByChild("totalContribute").GetValueAsync().ContinueWith(response =>
        {
            userList.Clear();
            scoreList.Clear();
            if (response.IsFaulted)
            {
                Debug.Log("エラーが発生しました");
            }
            else if (response.IsCompleted)
            {
                Debug.Log("成功しました");



                DataSnapshot snapshot = response.Result;
                IEnumerator<DataSnapshot> result = snapshot.Children.GetEnumerator();
                string jsonString = response.ToString();


                JSONNode json = JSONNode.Parse(snapshot.GetRawJsonValue());



                while (result.MoveNext())
                {

                    DataSnapshot data = result.Current;
                    string name = (string)data.Child("identifyName").GetValue(true);
                    //Firebaseの数値データはLong型となっているので、一度longで受け取った後にintにキャスト
                    int score = (int)(long)data.Child("totalContribute").GetValue(true);


                    userList.Add(name);
                    scoreList.Add(score);

                }
            }
        });
    }

    //リポジトリ数を持ってくる
    public void GetRepositoryData()
    {
        //コントリビュート数が一番多い人を取得
        _FirebaseDB.OrderByChild("repositoryCount").GetValueAsync().ContinueWith(response =>
        {
            userList.Clear();
            scoreList.Clear();
            if (response.IsFaulted)
            {
                Debug.Log("エラーが発生しました");
            }
            else if (response.IsCompleted)
            {
                Debug.Log("成功しました");
                DataSnapshot snapshot = response.Result;
                IEnumerator<DataSnapshot> result = snapshot.Children.GetEnumerator();
                string jsonString = response.ToString();


                JSONNode json = JSONNode.Parse(snapshot.GetRawJsonValue());




                while (result.MoveNext())
                {

                    DataSnapshot data = result.Current;
                    string name = (string)data.Child("identifyName").GetValue(true);
                    //Firebaseの数値データはLong型となっているので、一度longで受け取った後にintにキャスト
                    int score = (int)(long)data.Child("repositoryCount").GetValue(true);

                    userList.Add(name);
                    scoreList.Add(score);

                }
            }
        });
    }

    public void GetGameScore()
    {

        _FirebaseDB.OrderByChild("gameScore").GetValueAsync().ContinueWith(response =>
        {
            userList.Clear();
            scoreList.Clear();
            if (response.IsFaulted)
            {
                Debug.Log("エラーが発生しました");
            }
            else if (response.IsCompleted)
            {
                Debug.Log("成功しました");


                DataSnapshot snapshot = response.Result;
                IEnumerator<DataSnapshot> result = snapshot.Children.GetEnumerator();
                string jsonString = response.ToString();


                JSONNode json = JSONNode.Parse(snapshot.GetRawJsonValue());

                


                while (result.MoveNext())
                {

                    DataSnapshot data = result.Current;
                    string name = (string)data.Child("identifyName").GetValue(true);
                    //Firebaseの数値データはLong型となっているので、一度longで受け取った後にintにキャスト
                    int score = (int)(long)data.Child("gameScore").GetValue(true);

                    

                    userList.Add(name);
                    scoreList.Add(score);

                }
            }
        });
    }

    public void SetRanking()
    {
        userList.Reverse();
        scoreList.Reverse();

        for (int i = 0; i <= ranking.Length - 1 ; i++)
        {
            //ranking[i].text =　(i+1) + " : " + userList[i] + "  Score : " + scoreList[i];
            
            ranking[i].text = userList[i];
            rankingScore[i].text = scoreList[i].ToString();
            Debug.Log("ランキングの長さ　:" + ranking.Length);
            Debug.Log("ユーザーリストの長さ" + userList.Count);
            Debug.Log("スコアリストの長さ" + scoreList.Count);
        }
    }

    public void RenderRanking()
    {
        StartCoroutine(WaitRetrive());
    }

    IEnumerator WaitRetrive()
    {
        yield return new WaitForSeconds(1f);

        SetRanking();
        
    }


    public void OnClickTitle()
    {
        SceneManager.LoadScene("Title");
    }

}
