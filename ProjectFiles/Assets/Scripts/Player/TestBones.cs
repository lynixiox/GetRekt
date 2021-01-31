using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBones : MonoBehaviour {
    public Transform[] bones;

    // Use this for initialization
    void Start () {
        bones = GetComponent<SkinnedMeshRenderer>().bones;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = bones[3].position;
        transform.rotation = bones[3].rotation;
		
	}
}
