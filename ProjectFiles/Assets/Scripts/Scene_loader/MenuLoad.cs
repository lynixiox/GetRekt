using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuLoad : MonoBehaviour
{

    public Button yourButton;
    public GameObject[] gameobjects;
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
        foreach (GameObject u in gameobjects)
            Destroy(u);
        Application.LoadLevel("Main_Menu");
    }
}
