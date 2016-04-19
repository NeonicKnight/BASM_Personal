using UnityEngine;
using System.Collections;

public class RayCastShoot : MonoBehaviour {

	//How fast Player can shoot and distance of bullet
	public float fireRate = .25f;
	public float range = 50f;

	//Renders the particle system from barrel of gun 
	public ParticleSystem smokeParticles;

	//Renders the particle system from where the bullet hits 
	public GameObject hitParticles;

	//Mussle Flash
	public GameObject shootFlare;

	//Gun Damage
	public int damage = 1;

	public Transform gunEnd;

	private Camera fpsCamera;

	//Draws a visible line to make a laser(Raycast if i dont want a laser)
	private LineRenderer lineRenderer;

	//Coroutine how long it waits
	private WaitForSeconds shotLength = new WaitForSeconds (0.07f);

	//Plays shoot sound
	private AudioSource source;

	//Controls how many times you can fire
	private float nextFireTime;

	void Awake ()
	{
		//Uses GetComponent to assign values
		lineRenderer = GetComponent<LineRenderer> ();

		source = GetComponent<AudioSource> ();

		//Uses InParent to use the camera in PlayerObject
		fpsCamera = GetComponentInParent<Camera> ();
	}


	// Update is called once per frame
	void Update () 
	{
		//Shoots and detects if it hit and returns info about what it hit on update
		RaycastHit hit;
		//Sets a point in the middle of the camera using x,y,z coordinates
		Vector3 rayOrigin = fpsCamera.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, 0));


		//checks left mouse button is down and checks if enough time has passed since last shot
		if (PlayerMovement.shipMovement.activateControls == true) {
			//if (Input.GetButtonDown ("Fire1") && Time.time > nextFireTime) {
				
			//XBox Controller support, if the right trigger is used at all, this condition is met
			if (Input.GetAxis("RightTrigger") > 0 && Time.time > nextFireTime) {
					
				//timer so player can't spam the fire button
				nextFireTime = Time.time + fireRate;

				//Counter in ScorePoints class to keep track of how many shots fired
				GameObject scoreDisplay = GameObject.Find ("ScoreDisplay");
				ScorePoints scorePoints = scoreDisplay.GetComponent<ScorePoints> ();
				scorePoints.AddPlayerShot ();

				//Rayorigin where we are casting from
				//fps.forwars where we are casting towards
				//Out to get additional info and store it. passes data as reference and stores it in variable
				//Range range till we are casting the raycast
				if (Physics.Raycast (rayOrigin, fpsCamera.transform.forward, out hit, range)) {

					//Uses hit to see what we hit and check for what action can be performed with that object
					IDamageable dmgScript = hit.collider.gameObject.GetComponent<EnemyHealth> ();

					//Checks for damage script
					if (dmgScript != null) {
						//Passes damage and where the damage happened
						dmgScript.Damage (damage, hit.point);
					}


					//Michael- I commented out the force on hit to protect the player's frame, 
					//also idk if we want to push over the enemies
//					if (hit.rigidbody != null) {
//						//Adds force away from the face the the object was hit(if it has a rigidbody)
//						hit.rigidbody.				AddForce (-hit.normal * 100f);
//					}

					//Start drawing of our line
					lineRenderer.SetPosition (0, gunEnd.position);
					//End of line
					lineRenderer.SetPosition (1, hit.point);

					//Creates particles at hit.point
					//Instantiate (hitParticles, hit.point, Quaternion.identity);
				}

				//When button pressed down it calls function
				StartCoroutine (ShotEffect ());
			}
		}


	}

	//Does Effects of gun firing
	private IEnumerator ShotEffect()
	{
		lineRenderer.enabled = true;
		source.Play ();
		//smokeParticles.Play ();
		//shootFlare.SetActive (true);

		//Makes it wait 0.7 seconds
		yield return shotLength;

		//Turns these effects off
		lineRenderer.enabled = false;
		//shootFlare.SetActive (false);
	}
}
