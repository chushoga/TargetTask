using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticleManager : MonoBehaviour {

	public int health = 0;
	public float rarity; // rarity of the target.
	public float speed;

	private bool isMoving = true;

	void FixedUpdate(){
		// move the obsticle
		if(isMoving) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
	}

	void OnTriggerEnter(Collider collider){

		if (collider.gameObject.tag == "OutOfBounds_Level"){
			// un parent it with the platform so it does not keep following the patform when hit.
			//gameObject.transform.SetParent(null); // probably not needed now.
			isMoving = false;
			Destroy(gameObject, 0.5f);
		}
	}

	void OnCollisionEnter(Collision collision){
		// TODO: like this?
		// minus some health from the obsticle.	
	    // health -= 1;
	}

}
