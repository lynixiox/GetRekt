using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioControl : MonoBehaviour {

    bool playing = false;
    AudioSource Walking;

    // Use this for initialization
    void Start ()
    {
        var audioS = this.GetComponents<AudioSource>();
        Walking = audioS[0];
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(isRunning() && !playing)
        {
            Walking.Play();
            playing = true;
        }	
        else if(!isRunning())
        {
            Walking.Stop();
            playing = false;
        }
	}

    public bool isRunning()
    {
        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            return true;
        }
        return false;
    }
}
