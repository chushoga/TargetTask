using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour {

	public float health;
	public float explosiveForce;
	public float explosiveLift;
	public float density;

	void Start(){
		
		gameObject.GetComponent<Rigidbody>().useGravity = false; // disable gravity to start if it is true.

	}

	void OnTriggerEnter(Collider collider){
		
		if (collider.gameObject.tag == "OutOfBounds_Platform"){
			// un parent it with the platform so it does not keep following the patform when hit.
			gameObject.transform.SetParent(null);

		}
	}

	void OnCollisionEnter(Collision collision){
		
		gameObject.GetComponent<Rigidbody>().useGravity = true; // enable gravity

		// IF COLLISION IS BULLET
		if(collision.gameObject.tag == "Bullet"){

			// check if this item is a bomb or not.
			if(explosiveForce > 0){
				Explode(); // do the explosion here
			}
		}

		// IF COLLISION IS BULLET
		if(collision.gameObject.tag == "Target"){
			Destroy(gameObject, 0.5f);
		}

	}	

	void Explode(){
		//Debug.Log("explode");
	}

}
