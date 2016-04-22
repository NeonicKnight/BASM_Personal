using UnityEngine;
using System.Collections;

public class EnemyDroneAI : MonoBehaviour {


	//---Sees player
	//Number of degrees centered on forward for enemy sight
	public float fieldOfViewAngle = 180f;
	//Reference to the sphere collider trigger component
	private SphereCollider col;
	//where the player was last sighted
	private Vector3 previousSighting;

	private PlayerHealth playerHealth;
	private EnemyHealth enemyHealth;

	public GameObject playerObject;
	//True or false if target was found
	private bool targetFound;

	//---Drone Controls
	public float enemyMovementSpeed;

	Rigidbody droneBody;

	public GameObject lightningTrail;
	public GameObject smoke;
	public GameObject enemyObject;

	public AudioSource deadSound;

	// Use this for initialization
	void Awake () {

		col = GetComponent<SphereCollider> ();
		playerObject = GameObject.Find ("Player");

		droneBody = GetComponent<Rigidbody> ();

		deadSound = GetComponent<AudioSource> ();


	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	//Uses Collider to check if player inside
	//3 conditions : He is within trigger , infront of enemy and nothing is blocking view of player
	void OnTriggerStay (Collider other) {

		//If the player has entered the collider
		if (other.gameObject == playerObject) 
		{
			//Set to false as default if other fields are not met
			targetFound = false;

			//Takes two vector 3's and returns the angle between them
			//if angle is less than half of field of view then its in fieldOfview
			//Vector from enemy to the player and stores the angle between it and forward

			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle (direction, transform.forward);

			//Checks if our angle is less than our field of view angle
			if (angle < fieldOfViewAngle * 0.5f) 
			{

				RaycastHit hit;

				//Position,Direction, raycast variable, distance of it (collider)
				if (Physics.Raycast (transform.position, direction, out hit, col.radius)) 
				{

					transform.LookAt (hit.transform.position);
					//transform.rotation = Quaternion.Slerp(transform.forward,playerLocation.position, Time.time * rotationSpeed);

					//If it hits the player
					if (hit.collider.gameObject == playerObject) 
					{
						
							targetFound = true;
							DroneCharge ();


					}
				}
			}
		}

	}

	void DroneCharge (){

		droneBody.AddForce (transform.forward * enemyMovementSpeed);

	}

	void OnCollisionEnter(Collision collision){

		//if (other.gameObject.name == "Player") {

			Debug.Log("Drone has been hit yo");


			//Deals damage to player
			playerObject.GetComponent<PlayerHealth> ().health -= 30;
			//Plays particle effects on death of Android
			Instantiate (lightningTrail, enemyObject.transform.position, enemyObject.transform.rotation);
			Instantiate (smoke, enemyObject.transform.position, enemyObject.transform.rotation);


		deadSound.Play ();

			gameObject.SetActive (false);

		//}

	}



}
