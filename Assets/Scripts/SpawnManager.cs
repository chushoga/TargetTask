using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	public bool isSpawning = true;

	public float platformSpeed; // speed at which the platform moves
	public float targetSpawnRate; // rate at which targets spawn;
	public float obsticleSpawnRate; // rate at which targets spawn;

	public GameObject targetSpawnPoint; // where the targets start spawing starts on the left side of the screen
	public GameObject obsitcleSpawnPoint; // where the obsitcles start spawing starts on the left side of the screen
	public GameObject platform;
	public List<GameObject> groundTargets = new List<GameObject>();
	public List<GameObject> airTargets = new List<GameObject>();
	public List<GameObject> bonusTargets = new List<GameObject>();
	public List<GameObject> obsticalTargets = new List<GameObject>();
	public List<GameObject> powerUpTargets = new List<GameObject>();

	// Use this for initialization
	void Start () {
		StartCoroutine(SpawnTimer());
		StartCoroutine(ObsticleSpawnTimer());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Move the platform to the right
		// TODO: think of putting this option 	
		float step = platformSpeed * Time.deltaTime;
		platform.transform.position += Vector3.right * step;
	}



	void SpawnTargets(){
		GameObject go = Instantiate(groundTargets[0], targetSpawnPoint.transform.position, Quaternion.identity);
		go.transform.parent = platform.transform;
	}

	void SpawnObsicles(){
		GameObject go = Instantiate(obsticalTargets[0], obsitcleSpawnPoint.transform.position, Quaternion.identity);
		go.transform.parent = platform.transform;
	}

	public IEnumerator SpawnTimer(){
		while(isSpawning){
			yield return new WaitForSeconds(targetSpawnRate);
			SpawnTargets();
		}
	}

	public IEnumerator ObsticleSpawnTimer(){
		while(isSpawning){
			yield return new WaitForSeconds(obsticleSpawnRate);
			SpawnObsicles();
		}
	}



}
