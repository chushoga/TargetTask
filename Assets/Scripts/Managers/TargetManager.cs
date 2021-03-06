﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour {

	public float health;
	public int points; // points for destroying/hitting the target
	public int plusTime = 0; // extra time to add if hit.
	private bool hasCounted; // has the point been counted yet?
	public float rarity; // rarity of the target.
	public float speed; // movement speed
	public bool moveDir;

	private bool isMoving = true;

	void Start(){
		
		gameObject.GetComponent<Rigidbody>().useGravity = false; // disable gravity to start if it is true.

	}

	void FixedUpdate(){
		if(isMoving) {
			if(moveDir == false) {
				transform.position += Vector3.right * speed * Time.deltaTime;
			} else {
				transform.position += -Vector3.right * speed * Time.deltaTime;
			}
		}
	}

	void OnTriggerEnter(Collider collider){

		gameObject.transform.SetParent(null); // is this needed now??

		if (collider.gameObject.tag == "OutOfBounds_Platform"){
			// un parent it with the platform so it does not keep following the patform when hit.

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
			GameManager.SCORE += collision.gameObject.GetComponent<TargetManager>().points;
			Destroy(gameObject, 0.5f);
		}

	}	


}
