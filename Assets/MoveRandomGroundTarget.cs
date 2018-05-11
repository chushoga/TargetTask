using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// requires a sphere collider for the collision detection.
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class MoveRandomGroundTarget : MonoBehaviour {

	// test object to spawn and see the location of newDir
	public GameObject spawnTo;

	[SerializeField] float wanderRange = 10f;
	[SerializeField] float wanderSpeed = 2.5f;
	[SerializeField] float turnSpeed = 50f;

	// collision bounds(make larger than the character)
	// this is a required component
	SphereCollider collisionBarrier;

	// required component.
	Rigidbody rb;

	// target position
	Vector3 targetPos;

	// is moving check
	bool isMoving = true;

	// is rotating?
	bool isRotating = false;

	// collision barrior cooldown.
	float collisionCooldownTimer = 2.0f;

	Animator ani;

	// Use this for initialization
	void Start() {

		//ani = GetComponent<Animator>();

		// get Rigidbody
		rb = GetComponent<Rigidbody>();

		//lock rotaiton
		rb.constraints = RigidbodyConstraints.FreezeRotation;

		// grab the sphere collider
		collisionBarrier = GetComponent<SphereCollider>(); 

		// initialize the collision barrier as isTrigger(incase forget to set it)
		collisionBarrier.isTrigger = true;

		targetPos = RandomDirection();

		LookTowards(); // look at new direction
	}

	// Update is called once per frame
	void Update() {

		// TODO RESTART HERE >>>>
		// check animation stat here. if moving then do the moving animation. else do something else
		if(rb.velocity == Vector3.zero) {

		}

		// TODO RESTART HERE >>>>>
		/*
		if(isMoving == true || isRotating == true) {
			ani.SetBool("isMoving", true);
		} else {
			ani.SetBool("isMoving", false);
		}
		*/

		// do a random check if should choose new direction or stand still for a random abount of time before moving.
		if(isMoving == true && isRotating == false) {

			// update movement
			transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * wanderSpeed);

		} else {
			// stand still
		}


		if(Mathf.Round(transform.position.x) == Mathf.Round(targetPos.x) && Mathf.Round(transform.position.z) == Mathf.Round(targetPos.z)) {
			//Debug.Log("reached pos");
			targetPos = RandomDirection();

		}
		LookTowards(); // look at new direction

		// TODO: have the gameobject move forward towards they way they are facing.
		if(Input.GetKey(KeyCode.W)) {
			transform.position += transform.forward * Time.deltaTime * wanderSpeed;
		} else if(Input.GetKey(KeyCode.S)) {
			rb.position -= transform.forward * Time.deltaTime * wanderSpeed;
		}

		if(Input.GetKey(KeyCode.D)) {
			transform.Rotate(0, Time.deltaTime * turnSpeed, 0);
		} else if(Input.GetKey(KeyCode.A)) {
			transform.Rotate(0, Time.deltaTime * -turnSpeed, 0);
		}
	}

	// pick a random direction and go
	Vector3 RandomDirection() {

		Vector3 position = new Vector3(Random.Range(-wanderRange, wanderRange), 0, Random.Range(-wanderRange, wanderRange));

		// uncomment this if you want to add something to spawn in like a particle effect at the target position.
		//Instantiate(spawnTo, position, Quaternion.identity);
		ChooseMoveType(); // choose a movement type. Standing or moving.

		Collider[] hitColliders = Physics.OverlapSphere(position, 1f);

		int i = 0;
		while(i < hitColliders.Length) {
			if(hitColliders[i].tag != "Ground") {
				// recheck position and put a new position.
				position = new Vector3(Random.Range(-wanderRange, wanderRange), 0, Random.Range(-wanderRange, wanderRange));
			} 
			i++;
		}

		return position;

	}

	void LookTowards() {

		isRotating = true;

		Vector3 targetDir = new Vector3(targetPos.x - transform.position.x, 0, targetPos.z - transform.position.z);
		float step = turnSpeed * Time.deltaTime;
		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 20.0F);

		//checking if still rotating.
		Quaternion rotation = Quaternion.LookRotation(targetDir);

		// check the target direction and see if it equels the current rotation.
		if(transform.rotation == rotation) {
			isRotating = false;
		}

		transform.rotation = Quaternion.LookRotation(newDir);

		//Debug.DrawRay(transform.position, newDir, Color.red);

	}

	// If Object collides with the proximity trigger then
	// choose a new direction and face it.
	void OnTriggerEnter(Collider collision) {

		if(collision.gameObject.tag != "Ground") {

			if(isMoving == true) {

				StartCoroutine(CollisionCooldown());

				//Debug.Log("reached pos");
				targetPos = RandomDirection();
				LookTowards(); // look at new direction
				//Debug.Log(collision.gameObject.tag);


			}

			/*END WORK HERE -----------------------------------------------------------*/
		}

	}

	// If object touches directly with body collier then
	// choose a new direction and face it.
	void OnCollisionEnter(Collision collision) {



		if(collision.gameObject.tag != "Ground") {
			//Debug.Log("reached pos");

			targetPos = RandomDirection();
			LookTowards(); // look at new direction

		}

	}

	// choose if moving or stopped.
	// 0 = stopped
	// 1 = moving
	void ChooseMoveType() {
		float rnd = Random.Range(0.0f, 100.0f);

		if(rnd >= 50) {
			isMoving = true;
		} else {
			isMoving = false;
			StartCoroutine(MovementPause());
		}

		//Debug.Log("move type:" + rnd);
	}

	IEnumerator MovementPause() {

		isMoving = false;

		float time = Random.Range(10.0f, 20.0f);

		yield return new WaitForSeconds(time);

		// chose new random directio to face
		targetPos = RandomDirection();

		// start moving
		isMoving = true;
	}

	IEnumerator CollisionCooldown() {


		collisionBarrier.enabled = false;

		yield return new WaitForSeconds(collisionCooldownTimer);

		collisionBarrier.enabled = true;

	}
}