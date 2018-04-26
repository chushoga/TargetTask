using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTurretController : MonoBehaviour {

	GameObject barrel;

	// Use this for initialization
	void Start () {
		barrel = transform.Find("Body/Barrel").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKey(KeyCode.A)){ transform.Rotate(Vector3.down, Time.deltaTime * 50.0f); }

		if(Input.GetKey(KeyCode.D)){ transform.Rotate(Vector3.up, Time.deltaTime * 50.0f); }

		if(Input.GetKey(KeyCode.W)){ barrel.transform.Rotate(Vector3.forward, Time.deltaTime * 50.0f); }

		if(Input.GetKey(KeyCode.S)){ barrel.transform.Rotate(Vector3.back, Time.deltaTime * 50.0f); }

	}
}
