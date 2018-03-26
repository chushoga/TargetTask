using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour {

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag != "Ammo") {			
			Destroy(col.gameObject, 5.0f);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
