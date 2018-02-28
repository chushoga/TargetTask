using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public int SCORE;
	public float platformSpeed; // speed of the platform
	public float shotForce;
	public float shotForceMax;

	//TURRET
	public bool isTiltUp = false;
	public bool isTiltDown = false;
	public float tiltSpeed;
	public GameObject turret;
	public Transform ammoSpawn;
	public GameObject ammo;

	// GUI
	public GameObject shotForceText;
	private Text shotForceRef;
	public bool isCharging = false;

	// Use this for initialization
	void Start () {
		shotForceRef = shotForceText.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

		if(isTiltUp){
			TiltUp();
		}

		if(isTiltDown){
			TiltDown();
		}

		if(isCharging) {
			ChargeShot();
		}

		// update the shot power text
		shotForceRef.text = "[" + Mathf.Round(shotForce) + "]";
	}

	public void Shoot(){
		GameObject bullet = Instantiate(ammo, ammoSpawn.transform.position, ammoSpawn.transform.rotation);
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * shotForce;
		// bullet.transform.Rotate(Vector3.left * 90);
		isCharging = false;
		shotForce = 0;
		Debug.Log("RESET SHOT FORCE");
	}

	public void ChargeShot(){
		if(shotForce < shotForceMax) {
			shotForce += 0.5f;
		}
	}

	public void SetIsCharging(){
		isCharging = !isCharging;
	}

	public void SetTiltUp(){
		isTiltUp = !isTiltUp;
	}

	public void SetTiltDown(){
		isTiltDown = !isTiltDown;
	}

	public void TiltUp(){		
		turret.transform.Rotate(-Vector3.right * Time.deltaTime * tiltSpeed);
	}

	public void TiltDown(){		
		turret.transform.Rotate(Vector3.right * Time.deltaTime * tiltSpeed);
	}

}
