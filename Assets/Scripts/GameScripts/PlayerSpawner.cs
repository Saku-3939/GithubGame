using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public List<Transform> RespawnPos = new List<Transform>();
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player.transform.position =  RespawnPos[Random.Range(0,RespawnPos.Count)].position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
