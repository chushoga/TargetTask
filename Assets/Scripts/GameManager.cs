using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static int SCORE = 0;
	public static float TIME_REMAINING = 60.0f;
	public bool IS_PAUSED = false;
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

	public GameObject trajectoryHelper;

	// GUI
	public GameObject shotForceText;
	private Text shotForceRef;
	private bool isCharging = false;
	public GameObject powerGauge;
	private Image powerGaugeRef;
	                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
	// Use this for initialization
	void Start () {
		shotForceRef = shotForceText.GetComponent<Text>();
		powerGaugeRef = powerGauge.GetComponent<Image>();

		// TEMP
		trajectoryHelper = Instantiate(trajectoryHelper, trajectoryHelper.transform.position, trajectoryHelper.transform.rotation);
		// TEMP

		// start game timer
		StartCoroutine("TimeRemaining");
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

		RaycastHit hit;
		// do the raycast here for the target aiming.
		if(Physics.Raycast(ammoSpawn.transform.position, ammoSpawn.transform.forward, out hit)){
			//print("Found an object - distance: " + hit.distance);
			print("Found an object - distance: " + hit.transform.position);
			Debug.DrawLine(ammoSpawn.transform.position, hit.transform.position, Color.green);

			trajectoryHelper.transform.position = hit.transform.position;

		}


		// update the shot power text
		shotForceRef.text = Mathf.Round(shotForce) + "";
	}

	public void Shoot(){
		GameObject bullet = Instantiate(ammo, ammoSpawn.transform.position, ammoSpawn.transform.rotation);
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * (shotForce / 2);

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
		rotationX = Mathf.Clamp(rotationX, -20, 45);
		turretBody.transform.localEulerAngles = new Vector3(
			-rotationX, 
			turretBody.transform.localEulerAngles.y, 
			turretBody.transform.localEulerAngles.z
		);
	}

	public void TiltDown(){
		rotationX += -1 * tiltSpeed * Time.deltaTime;
		rotationX = Mathf.Clamp(rotationX, -20, 45);
		turretBody.transform.localEulerAngles = new Vector3(
			-rotationX, 
			turretBody.transform.localEulerAngles.y, 
			turretBody.transform.localEulerAngles.z
		);
	}

	public void TiltLeft(){
		rotationZ += -1 * rotSpeed * Time.deltaTime;
		rotationZ = Mathf.Clamp(rotationZ, -30, 35);

		//Debug.Log(rotationZ);
		if(rotationZ > -30 && rotationZ < 35){
			turretArm.transform.Rotate(-Vector3.forward * rotSpeed * Time.deltaTime);	
		}

	}


	public void TiltRight(){
		rotationZ -= -1 * rotSpeed * Time.deltaTime;
		rotationZ = Mathf.Clamp(rotationZ, -30, 35);

		//Debug.Log(rotationZ);
		if(rotationZ > -30 && rotationZ < 35){
			turretArm.transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);	
		}

	}

	private IEnumerator TimeRemaining(){
		
		while(TIME_REMAINING > 0) {
			
			if(!IS_PAUSED) {
				yield return new WaitForSeconds(1);
				TIME_REMAINING -= 1;
			} else {
				yield return null;
			}

		}

	}


}
