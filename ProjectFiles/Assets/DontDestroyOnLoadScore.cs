using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadScore : MonoBehaviour
{
    public int score;
    public bool loadScore;
    public void Awake()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
