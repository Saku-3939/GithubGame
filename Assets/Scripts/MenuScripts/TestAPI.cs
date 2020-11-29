using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class TestAPI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(
            getGithubCommits(
                (JSONNode JSONresponse) =>
                {
                    //問題ありそう
                    HandleAPIresponse(JSONresponse);
                }
                )
            );

    }

    private IEnumerator getGithubCommits(System.Action<JSONNode> callBack)
    {
        string RequestURL = "https://api.github.com/repos/Saku-3939/AGU_PiedPiper.github.io/stats/contributors";
        UnityWebRequest request = UnityWebRequest.Get(RequestURL);

        yield return request.SendWebRequest();

        string JSONstring = request.downloadHandler.text;
        //問題ありそう
        JSONNode JSONnode = JSON.Parse(JSONstring);

        callBack(JSONnode);
    }

    private void HandleAPIresponse(JSONNode APIresponse)
    {
        //問題ありそう
        string AccountName = APIresponse[0]["author"]["login"].Value;
        Debug.Log("Your Accont name is:" + AccountName);
        string CommitCount = APIresponse[0]["total"].Value;
        Debug.Log("Your commit count is:" + CommitCount);
 
        //string hoge = APIresponse;
        //Debug.Log(APIresponse);
    }
}
