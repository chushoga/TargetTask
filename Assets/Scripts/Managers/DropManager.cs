using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour {

	public bool isSpawning = true;
	public float dropRadius = 10.0f;
	public float spawnRate = 1.0f;
	public List<GameObject> itemsToDrop = new List<GameObject>();

	public GameObject spawnPoint;

	private GameObject dropContainer; // a container for the spawned targets. This is just for keeping editor clean.

	// Use this for initialization
	void Start () {
		dropContainer = new GameObject();
		dropContainer.name = "DROP_CONTAINER";

		StartCoroutine(SpawnDropsTimer()); // start spawn timer
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// ---------------------------------------------------------------------------------------------------------------------------------------------
	// AIR TARGETS
	// ---------------------------------------------------------------------------------------------------------------------------------------------
	void SpawnDrops(){

		Vector3 randomX = new Vector3(spawnPoint.transform.position.x + Random.Range(-dropRadius, dropRadius), spawnPoint.transform.position.y, spawnPoint.transform.position.z + Random.Range(-dropRadius, dropRadius));

		GameObject go = Instantiate(itemsToDrop[ChooseRandomSpawn(itemsToDrop)], randomX, Quaternion.identity);
		go.transform.parent = dropContainer.transform;

	}

	public IEnumerator SpawnDropsTimer(){
		while(isSpawning) {
			yield return new WaitForSeconds(spawnRate);
			SpawnDrops();
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
				if(list[i].GetComponentsInChildren<Drops>().Length != 0) {
					Drops tmScript = list[i].GetComponentInChildren<Drops>();
					totalRarity += tmScript.rarity;
				} else {
					totalRarity += 0;
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
				if(list[i].GetComponentsInChildren<Drops>().Length != 0) {
					Drops tmScript = list[i].GetComponentInChildren<Drops>();
					rarity = tmScript.rarity;
				} else {	
					rarity = 0;
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
