using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryTarget : MonoBehaviour {
	
	public int points; // points for destroying/hitting the target
	public int plusTime = 0; // extra time to add if hit.
	private bool hasCounted; // has the point been counted yet?
	public float rarity; // rarity of the target.
	public int hp = 0;
	private int maxHp;

	private Renderer rend;
	private Color red = Color.red;
	private Color yellow = Color.yellow;
	private Color grey = Color.grey;

	void Start(){
		rend = gameObject.GetComponent<Renderer>();

		maxHp = hp;
	}

	void OnCollisionEnter(Collision collision){

		gameObject.GetComponent<Rigidbody>().useGravity = true; // enable gravity

		// IF COLLISION IS BULLET
		if(collision.gameObject.tag == "Ammo") {
			hp -= collision.gameObject.GetComponent<AmmoManager>().damage;
			if(hp <= 0) {
				GameManager.SCORE += points;
				GameManager.TIME_REMAINING += plusTime;
				Destroy(gameObject, 0.5f);
			}

		}

		// IF COLLISION IS TARGET
		// remove half of the targets HP
		if(collision.gameObject.tag == "Target") {
			hp -= hp / 2;
			if(hp <= 0) {
				GameManager.SCORE += collision.gameObject.GetComponent<StationaryTarget>().points;
				Destroy(gameObject, 0.5f);
			}
		}

		// TEMPERARY DAMAGE INDICATOR
		if(hp >= 1 && hp <= (maxHp / 4)) {
			// less than 25% red
			rend.material.color = red;
		} else if (hp > (maxHp / 4) && hp <= (maxHp / 2)) {
			// if less than 50% yellow
			rend.material.color = yellow;
		} else if (hp >= (maxHp / 2) && hp <= (maxHp - 1)) {
			// if less than 100% grey
			rend.material.color = grey;
		}
	}	

}
