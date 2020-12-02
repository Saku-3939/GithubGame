using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    int maxScore = 99999999;
    [SerializeField]
    int maxKill = 20;
    [SerializeField]
    Text scoreText;
    [SerializeField]
    FirstPersonAIO firstPerson;
    [SerializeField]
    FPSGunController gunController;
    [SerializeField]
    Text centerText;
    [SerializeField]
    float waitTime = 2;

    public bool isGameClear = false;
    public bool isGameOver = false;

    int score = 0;
    int kill = 0;


    public int Score
    {
        set
        {
            score = Mathf.Clamp(value, 0, maxScore);

            scoreText.text = score.ToString("D8");
        }
        get
        {
            return score;
        }
    }

    public int Kill
    {
        set
        {
            kill = value;
            if(kill >= maxKill)
            {
                StartCoroutine(GameClear());
            }
        }
        get
        {
            return kill;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        InitGame();
        StartCoroutine(GameStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitGame()
    {
        Score = 0;
        firstPerson.playerCanMove = false;
        firstPerson.enableCameraMovement = false;
        gunController.shootEnabled = false;
        Cursor.visible = true;

    }

    public IEnumerator GameStart()
    {
        yield return new WaitForSeconds(waitTime);
        centerText.text = "3";
        yield return new WaitForSeconds(1);
        centerText.text = "2";
        yield return new WaitForSeconds(1);
        centerText.text = "1";
        yield return new WaitForSeconds(1);
        centerText.text = "Go!";
        firstPerson.playerCanMove = true;
        firstPerson.enableCameraMovement = true;
        gunController.shootEnabled = true;
        yield return new WaitForSeconds(1);
        centerText.text = "";
        centerText.enabled = false;
    }

    public IEnumerator GameClear()
    {
        return null;
    }

    public IEnumerator GameOver()
    {
        isGameOver = true;
        firstPerson.playerCanMove = false;
        firstPerson.enableCameraMovement = false;
        gunController.shootEnabled = false;
        centerText.enabled = true;
        centerText.text = "GameOver!";
        yield return new WaitForSeconds(waitTime);
        DestroyEnemies();
        centerText.text = "";
        centerText.enabled = false;
        isGameOver = false;
    }
    
    void DestroyEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

}

