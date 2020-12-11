using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEList : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] se;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ScoreSE()
    {
        Debug.Log("Active!");
        audioSource.PlayOneShot(se[0]);
    }
}
