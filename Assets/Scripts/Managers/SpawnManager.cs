﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	private bool isSpawning = true; // keep spawning as long as this is true. If set to false all the spawing will stop.

	public float groundTargetSpawnRate; // rate at which targets spawn;
	public float airTargetSpawnRate; // rate at which the air targets spawn
	public float obsticleSpawnRate; // rate at which targets spawn;

	public GameObject groundTargetSpawnPoint; // where the targets start spawing starts on the left side of the screen
	public GameObject obsitcleSpawnPoint; // where the obsitcles start spawing starts on the left side of the screen
	public GameObject airTargetSpawnPoint; // where the obsticles start spawing in the air

	public List<GameObject> groundTargets = new List<GameObject>();
	public List<GameObject> airTargets = new List<GameObject>();
	public List<GameObject> bonusTargets = new List<GameObject>();
	public List<GameObject> obsticalTargets = new List<GameObject>();
	public List<GameObject> powerUpTargets = new List<GameObject>();

	private GameObject targetContainer; // a container for the spawned targets. This is just for keeping editor clean.

	void Start () {
		
		StartCoroutine(GroundSpawnTimer()); // start spawn timer
		StartCoroutine(AirSpawnTimer()); // start spawn timer
		StartCoroutine(ObsticleSpawnTimer()); // start spawn timer

		targetContainer = new GameObject();
		targetContainer.name = "TARGET_CONTAINER";

	}

	// ---------------------------------------------------------------------------------------------------------------------------------------------
	// GROUND TARGETS
	// ---------------------------------------------------------------------------------------------------------------------------------------------
	void SpawnGroundTargets(){
		
		GameObject go = Instantiate(groundTargets[ChooseRandomSpawn(groundTargets)], groundTargetSpawnPoint.transform.position, Quaternion.identity);
		go.transform.parent = targetContainer.transform;

	}

	public IEnumerator GroundSpawnTimer(){
		
		while(GameManager.TIME_REMAINING > 0){
			if(isSpawning){
				yield return new WaitForSeconds(groundTargetSpawnRate);
				SpawnGroundTargets();
			} else{
				yield return null;
			}
		}
	}

	// ---------------------------------------------------------------------------------------------------------------------------------------------
	// AIR TARGETS
	// ---------------------------------------------------------------------------------------------------------------------------------------------
	void SpawnAirTargets(){

		Vector3 randomX = new Vector3(airTargetSpawnPoint.transform.position.x, airTargetSpawnPoint.transform.position.y + Random.Range(-1.0f, 1), airTargetSpawnPoint.transform.position.z);

		GameObject go = Instantiate(airTargets[ChooseRandomSpawn(airTargets)], randomX, Quaternion.identity);
		go.transform.parent = targetContainer.transform;

	}

	public IEnumerator AirSpawnTimer(){
		while(isSpawning) {
			yield return new WaitForSeconds(airTargetSpawnRate);
			SpawnAirTargets();
		}
	}

	// ---------------------------------------------------------------------------------------------------------------------------------------------
	// OBSTICLES
	// ---------------------------------------------------------------------------------------------------------------------------------------------
	void SpawnObsicles(){
		
		GameObject go = Instantiate(obsticalTargets[ChooseRandomSpawn(obsticalTargets)], obsitcleSpawnPoint.transform.position, Quaternion.identity);
		go.transform.parent = targetContainer.transform;

	}

	public IEnumerator ObsticleSpawnTimer(){
		while(isSpawning){
			yield return new WaitForSeconds(obsticleSpawnRate);
			SpawnObsicles();
		}
	}

	// ---------------------------------------------------------------------------------------------------------------------------------------------
	// OTHER FUNCTIONS
	// ---------------------------------------------------------------------------------------------------------------------------------------------

	// Return an index for a random object to spawn from a list of prefabs.
	// This will return and index depending on the prefabs rarity weight.
	// Keep rarity between 0.001 - 100 on prefab stats.
	public int ChooseRandomSpawn(List<GameObject> list){

		float x = 0; // counter
		float totalRarity = 0; // tht total rarity weight of all in the list
		int index = 0; // return this index

		if(list.Count >= 0) {

			// get total rarity
			for(int i = 0; i < list.Count; i++) {

				// check if target or obsticle
				if(list[i].GetComponentsInChildren<TargetManager>().Length != 0) {
					TargetManager tmScript = list[i].GetComponentInChildren<TargetManager>();
					totalRarity += tmScript.rarity;
				} else {					
					ObsticleManager tmScript = list[i].GetComponentInChildren<ObsticleManager>();
					totalRarity += tmScript.rarity;
				}
			}

			// get random number from the total rarity weight
			x = Random.Range(0, totalRarity);

			// Step through the list and check if x is less than the rarity.
			// If x is less than the rarity then break out of the loop and 
			// return the index;
			for(int i = 0; i < list.Count; i++) {

				index = i;

				//TargetManager tmScript = list[i].GetComponentInChildren<TargetManager>();
				//float rarity = tmScript.rarity;
				float rarity = 0;

				// check if target or obsticle
				if(list[i].GetComponentsInChildren<TargetManager>().Length != 0) {
					TargetManager tmScript = list[i].GetComponentInChildren<TargetManager>();
					rarity = tmScript.rarity;
				} else {					
					ObsticleManager tmScript = list[i].GetComponentInChildren<ObsticleManager>();
					rarity = tmScript.rarity;
				}


				if(x <= rarity) {
					break;
				}
				
				x -= rarity;

			}
		}
		return index;
	}


}
