using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public List<Transform> RespawnPos = new List<Transform>();
    public List<Transform> SniperRespwanPos = new List<Transform>();
    public GameObject player;
    public List<GameObject> snipers;

    // Start is called before the first frame update
    void Start()
    {
        player.transform.position =  RespawnPos[Random.Range(0,RespawnPos.Count)].position;

        for(int i = 0; i <= snipers.Count; i++)
        {
            snipers[i].transform.position = SniperRespwanPos[i].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
