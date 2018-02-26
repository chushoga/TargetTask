using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	float t;
	public float timeToReachTarget;
	public Vector3 startPosition;
	public Vector3 target;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		t += Time.deltaTime/timeToReachTarget;
		transform.position = Vector3.Lerp(startPosition, target, t);
	}

	public void SetDestination(Vector3 destination, float time){
		t = 0;
		startPosition = transform.position;
		timeToReachTarget = time;
		target = destination;
	}
}
