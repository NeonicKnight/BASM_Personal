using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {


	//Checks for layer IDs
	public LayerMask whatIsPlayer;
	public LayerMask whatIsObstacle;
	private bool targetFound;

	//Controls movement
	private Rigidbody rbody;
	public float wallCheckDist = 0f;
	private Vector3 moveDir;

	public float rotationSpeed = 0.1f;


	//Controls Shooting
	public float shootingRate = 0f;
	private float shootTimeStamp = 0f;
	public GameObject playerObject;
	public float distanceFromTarget = 0f;
	public float safeDistance = 0f;
	private bool shotFired = false;

	public GameObject bullet;
	public Transform gun;
	public float shootForce = 0f;

	//---Sees player
	//Number of degrees centered on forward for enemy sight
	public float fieldOfViewAngle = 180f;
	//Reference to the sphere collider trigger component
	private SphereCollider col;
	//where the player was last sighted
	private Vector3 previousSighting;

	private PlayerHealth playerHealth;
	private EnemyHealth enemyHealth;

	public Animator anim;

	public AudioSource shootSound;

	void Awake(){

		col = GetComponent<SphereCollider> ();
		playerObject = GameObject.Find ("Player");

		enemyHealth = gameObject.GetComponent<EnemyHealth> ();

		//----check what this does
		//playerHealth = playerObject.GetComponent<playerHealth> ();

		anim = GetComponent<Animator>();

		shootSound = GetComponent<AudioSource> ();


	}

	// Use this for initialization
	void Start () {
		
		//Assigns rbody as the rigidbody component
		rbody = GetComponent<Rigidbody> ();


	}

	//Uses Collider to check if player inside
	//3 conditions : He is within trigger , infront of enemy and nothing is blocking view of player
	void OnTriggerStay (Collider other) {


		//If the player has entered the collider
		if (other.gameObject.name == "Player") 
		{
			//Debug.Log ("Player has entered Enemy SearchRadious");
			//Set to false as default if other fields are not met
			targetFound = false;

			//Takes two vector 3's and returns the angle between them
			//if angle is less than half of field of view then its in fieldOfview
			//Vector from enemy to the player and stores the angle between it and forward

			//Vector3 direction = other.transform.position - transform.position;
			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle (direction, transform.forward);

			//Checks if our angle is less than our field of view angle
			if (angle < fieldOfViewAngle * 0.5f) 
			{
				//Debug.Log ("The angle is less then the fieldofviewangle *0.5");
				RaycastHit hit;

				//Position,Direction, raycast variable, distance of it (collider)
				//ELIMINATED COL.RADIUS, SHOULD HAVE REMOVED MAX DISTANCE WHICH SHOULD BE OKAY AS 
				//PREVIOUS LINES CHECK FOR PLAYER COLLISION AND LATER LINES STOP THIS FX, SO LIMITS SHOULD NOT BE NEEDED
				if (Physics.Raycast (transform.position, direction, out hit)) 
				{
					//Debug.Log ("physics.raycast has been used");
					transform.LookAt (hit.transform.position);
					//transform.rotation = Quaternion.Slerp(transform.forward,playerLocation.position, Time.time * rotationSpeed);

					//If it hits the player
					if (hit.collider.gameObject == playerObject) 
					{
						//Debug.Log ("Player is about to be shot at!");
						if(enemyHealth.CurrentHealth > 0){
							targetFound = true;

							//wait to shoot until stage start, stop when stage ends

							if (other.gameObject.GetComponent<PlayerMovement> ().activateControls == true) {
								Shoot ();
							}
						}
					}
				}
			}
		}

	}

	void OnTriggerExit (Collider other)
	{
		//If the player leaves the trigger zone
		if (other.gameObject == playerObject) {
			//player not in view
			targetFound = false;
			anim.Play ("Idle", -1, 0f);

		}
	}

	// Update is called once per frame
	void Update () {

		if (targetFound)
		{

			//Shoot();
			//AimBeforeShoot();
		} 
		else 
		{
			LookForTarget();

		}

		//measures distance from enemy to players position
		distanceFromTarget = Vector3.Distance (transform.position, playerObject.transform.position);

	}

	void Shoot(){

		if (!shotFired) {
			//If shot has not been fired then fire
			//Sets fire rate --- change to the other fire rate
			if (Time.time > shootTimeStamp) {

				anim.Play ("Firing", -1, 0f);

				shootSound.Play ();


				//----------------------------Play shoot sound
				//deadSound.Play();

				GameObject go = (GameObject)Instantiate (bullet, gun.position, gun.rotation);

				//makes the bullet move forward
				go.GetComponent<Rigidbody> ().AddForce (gun.forward * shootForce);

				//Says that the enemy did fire
				shotFired = true;

				shootTimeStamp = Time.time + shootingRate;

				//AndroidAnimation.instance.anim.Play ("Firing", -1, 0f);
			}
		} 
		//If already shot run and hide
		else {

			Hide ();

		}

	}


	void Hide(){
		shotFired = false;

		//targetFound = false;
		//moveDir = ChooseDirection ();
		//transform.rotation = Quaternion.LookRotation (moveDir);

	}
//	//Moves enemy and looks for player using raycast
	void LookForTarget(){
//
//		Move ();
//
//		//Shoots from enemy location, forward, distance, looking for
		//targetFound = Physics.Raycast (transform.position, transform.forward, fieldOfViewAngle, whatIsPlayer);

	}

}
