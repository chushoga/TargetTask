using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	public bool isSpawning = true;

	public float platformSpeed; // speed at which the platform moves
	public float groundTargetSpawnRate; // rate at which targets spawn;
	public float airTargetSpawnRate; // rate at which the air targets spawn
	public float obsticleSpawnRate; // rate at which targets spawn;

	public GameObject groundTargetSpawnPoint; // where the targets start spawing starts on the left side of the screen
	public GameObject obsitcleSpawnPoint; // where the obsitcles start spawing starts on the left side of the screen
	public GameObject airTargetSpawnPoint; // where the obsticles start spawing in the air
	public GameObject platform;
	public List<GameObject> groundTargets = new List<GameObject>();
	public List<GameObject> airTargets = new List<GameObject>();
	public List<GameObject> bonusTargets = new List<GameObject>();
	public List<GameObject> obsticalTargets = new List<GameObject>();
	public List<GameObject> powerUpTargets = new List<GameObject>();

	// Use this for initialization
	void Start () {
		StartCoroutine(GroundSpawnTimer());
		StartCoroutine(AirSpawnTimer());
		StartCoroutine(ObsticleSpawnTimer());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Move the platform to the right
		// TODO: think of putting this option 	
		float step = platformSpeed * Time.deltaTime;
		platform.transform.position += Vector3.right * step;
	}


	void SpawnGroundTargets(){
		//Debug.Log(ChooseRandomSpawn(groundTargets) + " <-->" + groundTargets[ChooseRandomSpawn(groundTargets)].name);

		GameObject go = Instantiate(groundTargets[ChooseRandomSpawn(groundTargets)], groundTargetSpawnPoint.transform.position, Quaternion.identity);
		go.transform.parent = platform.transform;
	}

	void SpawnAirTargets(){
		GameObject go = Instantiate(airTargets[0], airTargetSpawnPoint.transform.position, Quaternion.identity);
		go.transform.parent = platform.transform;
	}

	void SpawnObsicles(){
		GameObject go = Instantiate(obsticalTargets[0], obsitcleSpawnPoint.transform.position, Quaternion.identity);
		go.transform.parent = platform.transform;
	}

	public IEnumerator GroundSpawnTimer(){
		while(isSpawning){
			yield return new WaitForSeconds(groundTargetSpawnRate);
			SpawnGroundTargets();
		}
	}

	public IEnumerator AirSpawnTimer(){
		while(isSpawning) {
			yield return new WaitForSeconds(airTargetSpawnRate);
			SpawnAirTargets();
		}
	}

	public IEnumerator ObsticleSpawnTimer(){
		while(isSpawning){
			yield return new WaitForSeconds(obsticleSpawnRate);
			SpawnObsicles();
		}
	}


	// Return a random object to pawn from the list.
	public int ChooseRandomSpawn(List<GameObject> list){

		int index = 0;
		int x = 0;
		int startFrom;

		// get random number from result start
		startFrom = Random.Range(0, TotalRarity(list));
		x -= startFrom;
		for(int i = 0; i < list.Count; i++){

			index = i;

			TargetManager tmScript = list[i].GetComponentInChildren<TargetManager>();

			int rarity = tmScript.rarity;

			if(x < 0){
				break;
			}
			
			x -= rarity;

		}
	
		return index;
	}

	// get sum of all Rarities for the Random Spawner
	private int TotalRarity(List<GameObject> list){
		
		int x = 0;

		for(int i = 0; i < list.Count; i++){
			
			TargetManager tmScript = list[i].GetComponentInChildren<TargetManager>();
			int rarity = tmScript.rarity;

			x += rarity;

		}

		return x;
	}

}
