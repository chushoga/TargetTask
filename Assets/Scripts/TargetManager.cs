using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour {

	public float health;
	public float explosiveForce;
	public float explosiveLift;
	public float density;

	void OnTriggerEnter(Collider collider){

		if (collider.gameObject.tag == "OutOfBounds_Platform"){
			//Debug.Log("Out of bounds Platform");
			gameObject.transform.SetParent(null);
			//Destroy(gameObject, 2.0f); // destroy the game object after going out of bounds and the particle is done.
		}

		if (collider.gameObject.tag == "OutOfBounds_Level"){

			//Debug.Log("Out of bounds Level");
			gameObject.transform.SetParent(null);
			// add the points here
			// spawn a point particle

			Destroy(gameObject, 2.0f); // destroy the game object after going out of bounds and the particle is done.
		}
	
	}

	void OnCollisionEnter(Collision collision){
		
		//Debug.Log("COLLISION" + collision.gameObject.tag);

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
