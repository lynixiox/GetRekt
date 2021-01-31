using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quit : MonoBehaviour
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
        Debug.Log("QUUUUIT");
        Application.Quit();
    }
}

