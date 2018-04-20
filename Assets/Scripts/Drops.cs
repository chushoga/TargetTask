using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour {

	public float health;
	public int points; // points for destroying/hitting the target
	public int plusTime = 0; // extra time to add if hit.
	private bool hasCounted; // has the point been counted yet?
	public float rarity; // rarity of the target.
	public float speed; // movement speed

	private bool isMoving = true;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		rb.useGravity = false;
		gameObject.AddComponent<ConstantForce>().force = new Vector3(0.0f, -0.2f, 0.0f); // add a constant downward force
		rb.AddForce(Vector3.down, ForceMode.Force);
	}
	
	void FixedUpdate(){
		if(isMoving) {
				//transform.position += Vector3.right * speed * Time.deltaTime;
		}
	}

	void OnTriggerEnter(Collider collider){

		if (collider.gameObject.tag == "OutOfBounds_Platform"){
			// un parent it with the platform so it does not keep following the patform when hit.
			// gameObject.transform.SetParent(null); // is this needed now??
			isMoving = false;
		}
	}

	void OnCollisionEnter(Collision collision){

		gameObject.GetComponent<Rigidbody>().useGravity = true; // enable gravity
		//gameObject.transform.SetParent(null); // clear parent? is this needed now?

		// IF COLLISION IS BULLET
		if(collision.gameObject.tag == "Ammo") {
			GameManager.SCORE += points;
			GameManager.TIME_REMAINING += plusTime;
		}

		// IF COLLISION IS TARGET
		if(collision.gameObject.tag == "Target"){
			GameManager.SCORE += collision.gameObject.GetComponent<Drops>().points;
			Destroy(gameObject, 0.5f);
		}

	}	
}
