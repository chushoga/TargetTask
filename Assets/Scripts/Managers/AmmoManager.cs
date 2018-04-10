using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour {

	// GRAVITY MODIFER
	public bool useGravity = true; // if ammo uses special gravity or not
	public float gravityMod = 0; // how much - the gravity? 

	// DAMAGE
	public float damage = 1; // damage done by the ammo

	// BOUNCE
	public int bounceCount = 0; // destroy ammo after how many bounces
	private int bounceCounter = 0; // counter for how many bounces have been done

	// EXPLOSIVE
	public float explosiveForce = 10.0f;
	public float explosiveRadius = 10.0f;

	// REFERENCE VARS
	Rigidbody rb;

	void Start(){

		// REFERENCES
		rb = GetComponent<Rigidbody>();

		// change the gravity here
		if(useGravity){
			Physics.gravity = new Vector3(0, -gravityMod, 0);
		} else {
			gameObject.GetComponent<Rigidbody>().useGravity = false; // enable gravity
		}

	}

	void Update(){
		// rotate ammo based on trajectory
		transform.rotation = Quaternion.LookRotation(rb.velocity);
	}

	void OnCollisionEnter(Collision col){
		
		bounceCounter += 1;
		if(bounceCounter >= bounceCount){

			// explode
			Vector3 explosionPos = transform.position;

			Collider[] colliders = Physics.OverlapSphere(explosionPos, explosiveRadius);

			foreach(Collider hit in colliders) {
				
				Rigidbody rb = hit.GetComponent<Rigidbody>();

				if(rb != null) {
					rb.AddExplosionForce(explosiveForce, explosionPos, explosiveRadius);
				}

			}

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
