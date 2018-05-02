using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
	
	private Vector3 LAUNCH_VELOCITY = new Vector3(20f, 80f, 0f);
	private Vector3 INITIAL_POSITION = Vector3.zero;
	private readonly Vector3 GRAVITY = new Vector3(0f, -240.0f, 0f);
	private const float DELAY_UNTIL_LAUNCH = 5f;
	private int NUM_DOTS_TO_SHOW = 10;
	private float DOT_TIME_STEP = 0.05f; // Balance this

	private bool launched = false;
	private float timeUntilLaunch = DELAY_UNTIL_LAUNCH;
	private Rigidbody rigidBody;

	public GameObject trajectoryDotPrefab;
	private MeshRenderer mr;

	private void Awake()
	{
		Physics.gravity = new Vector3(0, -240.0f, 0);
		rigidBody = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		for (int i = 0; i < NUM_DOTS_TO_SHOW; i++)
		{			
			float p;
			p = 1 - ((float)i / NUM_DOTS_TO_SHOW);
			Debug.Log(i + " / " + NUM_DOTS_TO_SHOW + " = " + p);
			GameObject trajectoryDot = Instantiate(trajectoryDotPrefab);
			Material col = trajectoryDot.GetComponent<Renderer>().material;
			trajectoryDot.GetComponent<Renderer>().material.color = new Color(col.color.r,col.color.g, col.color.b, p);
			//trajectoryDot.GetComponent<Renderer>().material.color = Color.green;
			trajectoryDot.transform.position = CalculatePosition(DOT_TIME_STEP * i);
		}
	}

	private void Update()
	{
		timeUntilLaunch -= Time.deltaTime;

		if (!launched && timeUntilLaunch <= 0)
		{
			Launch();
		}
	}

	private void Launch()
	{
		rigidBody.velocity = LAUNCH_VELOCITY;

		launched = true;
	}

	private Vector2 CalculatePosition(float elapsedTime)
	{
		return GRAVITY * elapsedTime * elapsedTime * 0.5f + LAUNCH_VELOCITY * elapsedTime + INITIAL_POSITION;
	}

}