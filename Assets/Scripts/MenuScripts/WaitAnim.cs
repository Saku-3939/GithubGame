using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitAnim : MonoBehaviour
{
    public Animator waitAnimator;
    public Animator startAnimator;
    public AudioSource audioSource;
    public AudioClip se;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(waitAnimator.GetCurrentAnimatorStateInfo(0));
    }

    // Update is called once per frame
    void Update()
    {
        //if(waitAnimator.GetCurrentAnimatorStateInfo(0) == )
    }
    
    
}
