using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public int SCORE;
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

	private float rotationX = 0f;

	public void TiltUp(){		
		//turret.transform.Rotate(-Vector3.right * Time.deltaTime * tiltSpeed);
		//Debug.Log(turret.transform.rotation.eulerAngles.x);

		//--------------------------------------------------------------------
		rotationX -= -1 * tiltSpeed * Time.deltaTime;

		rotationX = Mathf.Clamp(rotationX, -20, 35);

		turret.transform.localEulerAngles = new Vector3(
			-rotationX, 
			turret.transform.localEulerAngles.y, 
			turret.transform.localEulerAngles.z
		);
		//--------------------------------------------------------------------

	}



	public void TiltDown(){		
		//turret.transform.Rotate(Vector3.right * Time.deltaTime * tiltSpeed);
		//--------------------------------------------------------------------
		rotationX += -1 * tiltSpeed * Time.deltaTime;

		rotationX = Mathf.Clamp(rotationX, -20, 35);

		turret.transform.localEulerAngles = new Vector3(
			-rotationX, 
			turret.transform.localEulerAngles.y, 
			turret.transform.localEulerAngles.z
		);
		//--------------------------------------------------------------------
	}

}
