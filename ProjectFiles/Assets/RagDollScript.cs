using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollScript : MonoBehaviour
{
    public GameObject RagdollIdle;
    public GameObject RagdollRun;
    public GameObject IDLEDeath;
    public GameObject RUNDeath;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SpawnRagdollIdle()
    {
        RagdollIdle.SetActive(true);
        IDLEDeath.SetActive(false);

    }
    public void SpawnRagdollRun()
    {
        RagdollRun.SetActive(true);
        RUNDeath.SetActive(false);
    }
}
