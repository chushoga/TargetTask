using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour {

	public bool useGravity = true;
	public float gravityMod = 0; // how much - the gravity? 
	public float damage = 1; // damage done by the ammo
	public int bounceCount = 0; // destroy ammo after how many bounces
	private int bounceCounter = 0;


	void Start(){
		// change the gravity here

		if(useGravity){
			Physics.gravity = new Vector3(0, -gravityMod, 0);
		} else {
			gameObject.GetComponent<Rigidbody>().useGravity = false; // enable gravity
		}


	}

	void OnCollisionEnter(){
		
		bounceCounter += 1;
		if(bounceCounter > bounceCount){
			Destroy(gameObject);	
		}
		Debug.Log(bounceCounter);
	}

	void OnTriggerEnter(Collider collider){

		if (collider.gameObject.tag == "OutOfBounds_Platform"){			
			//Destroy(gameObject, 4.0f); // destroy the game object after going out of bounds and the particle is done.
		}

		if (collider.gameObject.tag == "OutOfBounds_Level"){			
			Destroy(gameObject, 20.0f); // destroy the game object after going out of bounds and the particle is done.
		}

	}

}
