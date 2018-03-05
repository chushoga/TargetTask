using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour {

	public float health;
	public float explosiveForce;
	public float explosiveLift;
	public float density;

	void Start(){
		gameObject.GetComponent<Rigidbody>().useGravity = false;
	}

	void OnTriggerEnter(Collider collider){
		
		if (collider.gameObject.tag == "OutOfBounds_Platform"){
			//Debug.Log("Out of bounds Platform");
			gameObject.transform.SetParent(null);
			//Destroy(gameObject, 2.0f); // destroy the game object after going out of bounds and the particle is done.
		}

		/*
		if (collider.gameObject.tag == "OutOfBounds_Level"){

			Destroy(gameObject, 3.0f); // destroy the game object after going out of bounds and the particle is done.
			Debug.Log("Out of bounds Level");
			//gameObject.transform.SetParent(null);
			// add the points here
			// spawn a point particle
		}
	*/
	}

	void OnCollisionEnter(Collision collision){
		
		gameObject.GetComponent<Rigidbody>().useGravity = true;// enable gravity

		// IF COLLISION IS BULLET
		if(collision.gameObject.tag == "Bullet"){

			// check if this item is a bomb or not.
			if(explosiveForce > 0){
				Explode(); // do the explosion here
			}
		}

	}	

	void Explode(){
		//Debug.Log("explode");
	}

}
