using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    public Button toRegistrationButton, toBattleButton, toRankingButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnclickRegistration()
    {
        SceneManager.LoadScene("DataRegistration");
    }

    public void OnclickBattle()
    {
        SceneManager.LoadScene("Battle");
    }

    public void OnclickRanking()
    {
        SceneManager.LoadScene("Ranking");
    }

}
