using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public int SCORE;
	public bool isTiltUp = false;
	public bool isTiltDown = false;

	public float tiltSpeed;
	public GameObject turret;
	public Transform ammoSpawn;
	public GameObject ammo;
	public float shotForce;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(isTiltUp){
			TiltUp();
		}
		if(isTiltDown){
			TiltDown();
		}
	}

	public void Shoot(){
		GameObject bullet = Instantiate(ammo, ammoSpawn.transform.position, ammoSpawn.transform.rotation);
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * shotForce;
		// bullet.transform.Rotate(Vector3.left * 90);
	}

	public void SetTiltUp(){
		isTiltUp = !isTiltUp;
	}

	public void SetTiltDown(){
		isTiltDown = !isTiltDown;
	}

	public void TiltUp(){
		Debug.Log("up");
		turret.transform.Rotate(-Vector3.right * Time.deltaTime * tiltSpeed);
	}

	public void TiltDown(){
		Debug.Log("down");
		turret.transform.Rotate(Vector3.right * Time.deltaTime * tiltSpeed);
	}

}
