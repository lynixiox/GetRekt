using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuckingKillerBunny : MonoBehaviour {

    public float MaxDistance;
    public float MinDistance;
    public float Speed;
    public GameObject Player;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Vector3.Distance(Player.transform.position, transform.position) < MaxDistance && Vector3.Distance(Player.transform.position, transform.position) > MinDistance)
        {
            Vector3 targetHeading = Player.transform.position - transform.position;
            Vector3 targetDirection = targetHeading.normalized;

            //rotate to look at the player

            transform.rotation = Quaternion.LookRotation(targetDirection); // Converts target direction vector to Quaternion
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            //move towards the player
            transform.position += this.transform.forward * Speed * Time.deltaTime;
        }
	}
}
