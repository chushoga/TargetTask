using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static int SCORE = 0;
	public float shotForce;
	public float shotForceMax;

	//TURRET
	public bool isTiltUp = false;
	public bool isTiltDown = false;
	public bool isTiltLeft = false;
	public bool isTiltRight = false;

	public float tiltSpeed;
	public float rotSpeed;
	public GameObject turretBody;
	public GameObject turretArm;

	public Transform ammoSpawn;
	public GameObject ammo;

	// GUI
	public GameObject shotForceText;
	private Text shotForceRef;
	public bool isCharging = false;
	public GameObject powerGauge;
	private Image powerGaugeRef;
	                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
	// Use this for initialization
	void Start () {
		shotForceRef = shotForceText.GetComponent<Text>();
		powerGaugeRef = powerGauge.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(isTiltUp){
			TiltUp();
		}

		if(isTiltDown){
			TiltDown();
		}

		if(isTiltLeft){
			TiltLeft();
		}

		if(isTiltRight){
			TiltRight();
		}

		if(isCharging) {
			ChargeShot();
		}

		// update the shot power text
		shotForceRef.text = Mathf.Round(shotForce) + "";
	}

	public void Shoot(){
		GameObject bullet = Instantiate(ammo, ammoSpawn.transform.position, ammoSpawn.transform.rotation);
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * (shotForce / 2);
		// bullet.transform.Rotate(Vector3.left * 90);
		isCharging = false;
		shotForce = 0;
		powerGaugeRef.fillAmount = shotForce/200; // RESET SHOT FORCE

		//Play shot particle
		ParticleSystem ammoPart = ammoSpawn.GetComponentInChildren<ParticleSystem>();
		ammoPart.Play();
	}

	public void ChargeShot(){
		if(shotForce < shotForceMax) {
			shotForce += 1f;
			powerGaugeRef.fillAmount = shotForce/200;
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

	public void SetTiltLeft(){
		isTiltLeft = !isTiltLeft;
	}

	public void SetTiltRight(){
		isTiltRight = !isTiltRight;
	}

	private float rotationX = 0f;
	private float rotationZ = 0f;

	public void TiltUp(){
		rotationX -= -1 * tiltSpeed * Time.deltaTime;
		rotationX = Mathf.Clamp(rotationX, -20, 35);
		turretBody.transform.localEulerAngles = new Vector3(
			-rotationX, 
			turretBody.transform.localEulerAngles.y, 
			turretBody.transform.localEulerAngles.z
		);
	}

	public void TiltDown(){
		rotationX += -1 * tiltSpeed * Time.deltaTime;
		rotationX = Mathf.Clamp(rotationX, -20, 35);
		turretBody.transform.localEulerAngles = new Vector3(
			-rotationX, 
			turretBody.transform.localEulerAngles.y, 
			turretBody.transform.localEulerAngles.z
		);
	}

	public void TiltLeft(){
		rotationZ += -1 * rotSpeed * Time.deltaTime;
		rotationZ = Mathf.Clamp(rotationZ, -30, 30);

		//Debug.Log(rotationZ);
		if(rotationZ > -30 && rotationZ < 30){
			turretArm.transform.Rotate(-Vector3.forward * rotSpeed * Time.deltaTime);	
		}


		/*
		turretArm.transform.localEulerAngles = new Vector3(
			turretArm.transform.localEulerAngles.x, 
			turretArm.transform.localEulerAngles.y,
			-rotationZ
		);
*/
	}


	public void TiltRight(){
		rotationZ -= -1 * rotSpeed * Time.deltaTime;
		rotationZ = Mathf.Clamp(rotationZ, -30, 30);

		//Debug.Log(rotationZ);
		if(rotationZ > -30 && rotationZ < 30){
			turretArm.transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);	
		}
		//turretArm.transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);


		/*
		turretArm.transform.localEulerAngles = new Vector3(
			turretArm.transform.localEulerAngles.x, 
			turretArm.transform.localEulerAngles.y,
			-rotationZ 
		);
		*/
	}


}
