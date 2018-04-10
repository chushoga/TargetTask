using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

	public static int SCORE = 0;
	public float levelBaseTime = 2.0f;
	public static float TIME_REMAINING = 2.0f;
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
	private LineRenderer trajectoryLine;

	// GUI
	public GameObject shotForceText;
	private Text shotForceRef;
	private bool isCharging = false;
	public GameObject powerGauge;
	private Image powerGaugeRef;

	// buttons highlight
	public Image fireBtnDown;
	public Image directionDown;


	// MANAGERS
	public LevelManager lm;

	// Use this for initialization
	void Start () {
		
		shotForceRef = shotForceText.GetComponent<Text>();
		powerGaugeRef = powerGauge.GetComponent<Image>();

		// TRAJECTORY HELPER START

		trajectoryLine = ammoSpawn.GetComponent<LineRenderer>();
		trajectoryHelper = Instantiate(trajectoryHelper, trajectoryHelper.transform.position, trajectoryHelper.transform.rotation);

		// TRAJECTORY HELPER END

		TIME_REMAINING = levelBaseTime;
		// Rest the static time remaining countdown to the base level time
		StartCoroutine("TimeRemaining"); // start game timer

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
			//print("transform forward: " + ammoSpawn.transform.forward);
			//print("Found an object - distance: " + hit.transform.position);
			//Debug.DrawLine(ammoSpawn.transform.position, hit.point, Color.green);

			trajectoryHelper.transform.position = hit.point;
			trajectoryLine.SetPosition(0, ammoSpawn.transform.position);
			trajectoryLine.SetPosition(1, hit.point);

		}


		// update the shot power text
		shotForceRef.text = Mathf.Round(shotForce) + "%";
	}

	public void Shoot(){
		GameObject bullet = Instantiate(ammo, ammoSpawn.transform.position, ammoSpawn.transform.rotation);
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * (shotForce);

		isCharging = false;
		shotForce = 0;
		powerGaugeRef.fillAmount = 0.06f; // RESET SHOT FORCE

		//Play shot particle
		ParticleSystem ammoPart = ammoSpawn.GetComponentInChildren<ParticleSystem>();
		ammoPart.Play();
	}

	public void ChargeShot(){
		if(shotForce < shotForceMax) {
			shotForce += 1f;
			if(shotForce / 224 < 0.06){
				powerGaugeRef.fillAmount = 0.06f;
			} else {
				powerGaugeRef.fillAmount = shotForce / 224;
			}
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

		lm.ShowGameOver(); // show the game over screen

	}


}
