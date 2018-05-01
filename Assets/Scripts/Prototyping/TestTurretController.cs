using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTurretController : MonoBehaviour {

	GameObject barrel;
	public Transform ammoSpawn;
	public GameObject ammo;
	public float shotForce = 80.0f;	
	public Text powerTxt;


	private Vector3 LAUNCH_VELOCITY = new Vector3(20f, 80f, 0f);
	private Vector3 INITIAL_POSITION = Vector3.zero;
	private readonly Vector3 GRAVITY = new Vector3(0f, -240.0f, 0f);
	private const float DELAY_UNTIL_LAUNCH = 20f;
	private int NUM_DOTS_TO_SHOW = 10;
	private float DOT_TIME_STEP = 0.025f; // Balance this

	private bool launched = false;
	private float timeUntilLaunch = DELAY_UNTIL_LAUNCH;
	private Rigidbody rigidBody;

	public GameObject trajectoryDotPrefab;
	private MeshRenderer mr;

	public GameObject trajectoryMaster;

	public Vector3 LAUNCH_VELOCITY_NEW;

	// Use this for initialization
	void Start () {
		barrel = transform.Find("Body/Barrel").gameObject;
		Physics.gravity = new Vector3(0, -240.0f, 0);
		rigidBody = GetComponent<Rigidbody>();
		trajectoryMaster = new GameObject();
		trajectoryMaster.name = "trajectoryMaster";
	}

	void Update () {
		
		//if(Input.GetKey(KeyCode.A)){ transform.Rotate(Vector3.down, Time.deltaTime * 50.0f); }

		//if(Input.GetKey(KeyCode.D)){ transform.Rotate(Vector3.up, Time.deltaTime * 50.0f); }

		if(Input.GetKey(KeyCode.W)){ barrel.transform.Rotate(Vector3.forward, Time.deltaTime * 50.0f); }

		if(Input.GetKey(KeyCode.S)){ barrel.transform.Rotate(Vector3.back, Time.deltaTime * 50.0f); }

		if(Input.GetKeyDown(KeyCode.Space)){ Shoot(); }

		powerTxt.text = shotForce + "p";
	}

	public void Shoot(){
		GameObject bullet = Instantiate(ammo, ammoSpawn.transform.position, ammoSpawn.transform.rotation);
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * (shotForce);
		Debug.Log(bullet.transform.forward * (shotForce) + "<--------");
		//Debug.Log(bullet.GetComponent<Rigidbody>().velocity);
	}

	public void IncreasePower(){
		shotForce += 2;
		DrawTrajectory();
	}

	public void DecreasePower(){
		shotForce -= 2;
	}

	public void DrawTrajectory(){
		Debug.Log("child count = " + trajectoryMaster.transform.childCount);

		if(trajectoryMaster.transform.childCount == 0) {
			for (int i = 0; i < NUM_DOTS_TO_SHOW; i++) {			
					float p;
					p = 1 - ((float)i / NUM_DOTS_TO_SHOW);
					//Debug.Log(i + " / " + NUM_DOTS_TO_SHOW + " = " + p);
					GameObject trajectoryDot = Instantiate(trajectoryDotPrefab);
					trajectoryDot.transform.parent = trajectoryMaster.transform;
					Material col = trajectoryDot.GetComponent<Renderer>().material;
					trajectoryDot.GetComponent<Renderer>().material.color = new Color(col.color.r,col.color.g, col.color.b, p);
					//trajectoryDot.GetComponent<Renderer>().material.color = Color.green;
					trajectoryDot.transform.position = CalculatePosition(DOT_TIME_STEP * i);
			}

		} else {
			// transform the position of the dots here!!!
			for (int i = 0; i < trajectoryMaster.transform.childCount; i++) {			
				float p;
				p = 1 - ((float)i / NUM_DOTS_TO_SHOW);

				GameObject trajectoryDot = trajectoryMaster.transform.GetChild(i).gameObject;

				Material col = trajectoryDot.GetComponent<Renderer>().material;
				trajectoryDot.GetComponent<Renderer>().material.color = new Color(col.color.r,col.color.g, col.color.b, p);
				trajectoryDot.transform.position = CalculatePosition(DOT_TIME_STEP * i);
			}
		}
	}

	private Vector2 CalculatePosition(float elapsedTime)
	{
		//return GRAVITY * elapsedTime * elapsedTime * 0.5f + LAUNCH_VELOCITY * elapsedTime + INITIAL_POSITION;
		//Debug.Log(barrel.transform.rotation * (shotForce) + "<--");

		float angleInDegrees;
		Vector3 rotationAxis;
		barrel.transform.rotation.ToAngleAxis(out angleInDegrees, out rotationAxis);

		Vector3 angularDisplacement = rotationAxis * angleInDegrees * Mathf.Deg2Rad;    
		Vector3 angularSpeed = angularDisplacement / Time.deltaTime;

		//Debug.Log(angularSpeed + " *-------*");

		LAUNCH_VELOCITY_NEW = ammoSpawn.transform.forward * (shotForce);
		Debug.Log(LAUNCH_VELOCITY_NEW + " *-------*");

		return GRAVITY * elapsedTime * elapsedTime * 0.5f + LAUNCH_VELOCITY_NEW * elapsedTime + barrel.transform.position;
	}
		
	private void Launch()
	{
		rigidBody.velocity = LAUNCH_VELOCITY;

		launched = true;
	}
}
