using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("STARTUP");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision){
		Debug.Log("COLLIDED");
		Time.fixedDeltaTime = 0.02f;
	}
}
