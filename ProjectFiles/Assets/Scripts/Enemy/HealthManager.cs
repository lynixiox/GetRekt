using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {


    public float HealthLeft;
    public Text ST;
    private float cooldown = 0;


    

    // Use this for initialization
    void Update()
    {
        if (cooldown <= 0)
        {
            cooldown = 0.3f;
            if (ST == null)
                ST = GameObject.FindGameObjectWithTag("Health").GetComponent<Text>();
            else
                ST.text = "Health " + HealthLeft.ToString();
        }
        else
            cooldown -= Time.deltaTime;
    }
    public void AddScore(int amount)
    {
        HealthLeft -= amount;

    }
}
