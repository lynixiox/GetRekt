using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCount : MonoBehaviour {
    public int Enemies;
    public int EnemyMax = 150;
    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void AddEnemy()
    {
        Enemies++;
    }
    public void DeleteEnemy()
    {
        Enemies--;
    }
}
