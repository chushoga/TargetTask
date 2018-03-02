using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public float speed;
	public GameObject startPosition;
	public GameObject endPosition;

		// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//t += Time.deltaTime/timeToReachTarget;
		//transform.position = Vector3.Lerp(startPosition, target, t);
		//float step = speed * Time.deltaTime;
		//transform.position = Vector3.MoveTowards(transform.position, endPosition.transform.position, step);
	}
}
