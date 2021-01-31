using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToHighScoreSheet : MonoBehaviour {
    public bool FromGame = false;
    public Button yourButton;
    public GameObject[] gameobjects;
    public ScoreManager score;
    // Use this for initialization
    void Start()
    {
        yourButton = this.GetComponent<Button>();
        yourButton.onClick.AddListener(PerformRestart);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void PerformRestart()
    {

        Application.LoadLevel("HighScore");
        GameObject.Find("DontDestroyScore").GetComponent<DontDestroyOnLoadScore>().loadScore = FromGame;
        if (FromGame)
            GameObject.Find("DontDestroyScore").GetComponent<DontDestroyOnLoadScore>().score = score.Score; 
        
    }
}
