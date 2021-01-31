using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MenuController : MonoBehaviour
{

    string name = "";
    public int score;
    public bool fromgame = false;
    List<Scores> highscore;
    public Button AddScoreButton;
    public Text Score;
    public Text Name;
    public GameObject AddScoreButtonn;
    public GameObject GoToMenuButtonn;
    public Text[] Names;
    public Text[] Scores;



    // Use this for initialization
    void Start()
    {
        //EventManager._instance._buttonClick += ButtonClicked;
        if (GameObject.Find("DontDestroyScore").GetComponent<DontDestroyOnLoadScore>().loadScore)
            score = GameObject.Find("DontDestroyScore").GetComponent<DontDestroyOnLoadScore>().score;
        else
        {
            score = 0;
            ActivateGoToMainMenuButton();
        }
        highscore = new List<Scores>();
        AddScoreButton.onClick.AddListener(AddScoreee);
                    highscore = HighScoreManager._instance.GetHighScore();
        for (int i = 0; i < highscore.Count; i++)
        {
            if (i < 10)
            {
                Names[i].text = highscore[i].name;
                Scores[i].text = highscore[i].score.ToString();
            }

        }
        Score.text = "Score: " + score.ToString();

    }


    void ButtonClicked(GameObject _obj)
    {
        print("Clicked button:" + _obj.name);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {


     //   GUILayout.BeginHorizontal();
        //GUILayout.Label("500000000");

   //     GUILayout.EndHorizontal();



   //     if (GUILayout.Button("Get LeaderBoard"))
     //   {
          //  highscore = HighScoreManager._instance.GetHighScore();
   //     }

 //       if (GUILayout.Button("Clear Leaderboard"))
  //      {
  //          HighScoreManager._instance.ClearLeaderBoard();
   //     }

       /// GUILayout.Space(190);

      //  GUILayout.BeginHorizontal();
    //    GUILayout.Label("Name", GUILayout.Width(Screen.width / 2));
  //      GUILayout.Label("Scores", GUILayout.Width(Screen.width / 2));
//        GUILayout.EndHorizontal();

   //     GUILayout.Space(25);

    //    foreach (Scores _score in highscore)
   //     {
     //       GUILayout.BeginHorizontal();
     //       GUILayout.Label(_score.name, GUILayout.Width(Screen.width / 2));
    //        GUILayout.Label("" + _score.score, GUILayout.Width(Screen.width / 2));
     //       GUILayout.EndHorizontal();
   //     }
    }
    public void AddScoreee()
    {
        name = Name.text;
        HighScoreManager._instance.SaveHighScore(name, score);
        highscore = HighScoreManager._instance.GetHighScore();
        for (int i = 0; i < highscore.Count; i++)
        {
            if (i < 10)
            {
                Names[i].text = highscore[i].name;
                Scores[i].text = highscore[i].score.ToString();
            }

        }
        ActivateGoToMainMenuButton();
    }
    public void ActivateGoToMainMenuButton()
    {
        AddScoreButtonn.SetActive(false);
        GoToMenuButtonn.SetActive(true);
    }
}