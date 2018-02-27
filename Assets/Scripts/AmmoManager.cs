using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour {

	void OnTriggerEnter(Collider collider){

		if (collider.gameObject.tag == "OutOfBounds_Platform"){			
			Destroy(gameObject, 4.0f); // destroy the game object after going out of bounds and the particle is done.
		}

		if (collider.gameObject.tag == "OutOfBounds_Level"){			
			Destroy(gameObject, 20.0f); // destroy the game object after going out of bounds and the particle is done.
		}

	}

}
