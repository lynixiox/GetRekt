using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour {

    public  int Score; 
    public Text ST;
	// Use this for 
	
	// Update is called once per frame
	void Update ()
    {
        if (ST == null)
            ST = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        else
            ST.text = "Score: " + Score.ToString();
	}
    public void AddScore(int amount)
    {
        Score += amount;
        
    }
}
