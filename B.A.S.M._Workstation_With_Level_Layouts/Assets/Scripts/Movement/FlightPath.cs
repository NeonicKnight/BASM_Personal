using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class FlightPath : MonoBehaviour {

	//Instance created
	public static FlightPath cameraMovement = null;
	//This array cannot be hardcoded
	//This array stores all of the waypoints for the player to pass through
	//In the inspector, the designer adds a value for the total number of waypoints
	//Then the designer drags the transform position for a waypoint, and drags it from its location in the inspector,
	//to its position in the array
	public List<Transform> waypoints;
	//Speed in which the player travels between points
	public float speed = 6f;
	//This is used to determine how long the player stays in a stage
	public float stopTime = 8f;
	//Once the player hits the last waypoint: LevelEnd, this is how long the player waits until an action occurs
	public float TimeTillEnd = 13f;
	//This string reads in each "StageStart"s TAG
	//WE CAN SET TAGS AS THE LENGTH OF TIME IN SECONDS THAT WE WISH THE PLAYER TO REMAIN AT THAT STAGE
	//EXAMPLE: If one StageStart has a tag of "12", then the player will wait there for 12 seconds
	//If another different StageStart has a tag of "5", then the player will wait there for 5 seconds
	//IF NO TAG IS USED BEFORE THIS IS SET, CODE USES WHATS STORED IN THE FLOAT ON AWAKE
	//IF NO TAG IS USED AFTER STAGENUM HAS BEEN UTILIZED, THEN THE LAST FLOAT STORED IN STOPTIME WILL BE USED
	public string StageNum;

	//Returns a Vector3 called moveVector
	//This function should be just a placeholder
	//sowhatwhocareswannafightaboutit?
	public Vector3 GetMovementVector()
	{
		Vector3 moveVector = Vector3.zero;
		if (waypoints.Count != 0)
			moveVector = Vector3.Normalize(transform.position - waypoints [0].transform.position);
		return moveVector;
	}

	//This is actually called from the script: WaypointBehavior on each waypoint
	public void NextWaypoint()
	{
		//Debug.Log ("NexWaypoint has been called");
		//What happens is that the waypoint in the index position 0, is removed
		//Now what was in index 1 is 0, and everything moves down one
		waypoints.RemoveAt(0);
		//Call Rotate() to make sure the player rotates to the next point
		StartCoroutine (Rotate ());
	}

	//This rotates the player to the next point
	//Jorge is actually Mexican.
	IEnumerator Rotate()
	{
		bool repeat = true;
		while(repeat && waypoints.Count != 0)
		{
			Quaternion originalRotation = transform.rotation;
			Vector3 targetDirection = waypoints[0].position - transform.position;
			//To change how quickly the player faces the next target in its rotation, divide TIME by a large number
			//To have the player quickly look at the target, divide TIME by a small number
			Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, Time.deltaTime / 1, 0);
			transform.rotation = Quaternion.LookRotation(newDirection);

			if(originalRotation != transform.rotation)
				yield return new WaitForEndOfFrame();
			else
				repeat = false;
		}
	}

	// Use this for initialization
	void Start()
	{
		//Instance, and makes Pathfinder look at the first waypoint
		cameraMovement = this;
		waypoints [0].LookAt (transform.position);
		for (int x = 1; x < waypoints.Count; x++) {
			//This should be what makes the Pathfinder look at every waypoint when needed to
			waypoints [x].LookAt (waypoints [x - 1].transform.position);
		}
	}

	// Update is called once per frame
	void Update()
	{
		// make the camera move
		//As long as there is at least 1 waypoint
		//No waypoints? The player doesn't look at anything
		if (waypoints.Count != 0)
		{
			float step = speed * Time.deltaTime;
			Vector3 moveVector = Vector3.Normalize (transform.position - waypoints [0].transform.position);
			transform.position = transform.position - moveVector *step;
		}
	}

	//Checks each collider for its name and evaluates what happends
	//Triggered collider is passed as col
	void OnTriggerEnter (Collider col)
	{
		GameObject instructions = GameObject.Find ("Instructions");
		GameObject scoreInst = GameObject.Find ("ScoreInst");


		if (col.gameObject.name == "Point_InstructionEnd") {
			instructions.GetComponent<Text> ().enabled = false;
			scoreInst.GetComponent<Text> ().enabled = true;
		}

		if (col.gameObject.name == "Point_ScoreInstEnd") {
			scoreInst.GetComponent<Text> ().enabled = false;
		}

		//If the Pathfinder collided with StageStart
		if (col.gameObject.name == "StageStart") 
		{
			//Debug.Log ("Player has hit StageStart");
			//MOVED INTO STAGEPLAY() TO ALLOW TIMER TO BE SET BEFORE ACTIVATING
			//enable player conrols
			//PlayerMovement.shipMovement.activateControls = true;

			//This was an idea to lock the camera and the player inbetween stages, but I haven't got it to work yet
			//GameObject.Find ("Main Camera").GetComponent<MichaelsMouseLook> ().FreeLook = true;

			//The idea is to disable the StageStart's collider so player wont collide with them multiple times
			//Gets the component of Collider from object "col" and disables it
			col.GetComponent<Collider> ().enabled = false;

			//StageNum stores the TAG of the StageStart object
			//This TAG functions as the duration in seconds the player will remain at that stage
			StageNum = col.gameObject.tag;
			//checks to prevent errors
			//if StageNum is null, or "Untagged" this code will be skipped to prevent crashing
			if (StageNum != null && StageNum != "Untagged") {
				//if StageNum is a string that can parse into a float, that number is stored in public float stopTime
				stopTime = float.Parse (StageNum);
			}

			//Reference to the Timer script
			GameObject stageTimer = GameObject.Find ("StageTimer");
			Timer timerClass = stageTimer.GetComponent<Timer> ();
			//calls SetStageTimer() in Timer.cs
			//This sets stopTime as the Stage's Timer for HUD
			timerClass.SetStageTimer ();

			//Call StagePlay() with a delayed timer
			StartCoroutine (StagePlay ());
		}

		//If the col belongs to "LevelEnd"
		if (col.gameObject.name == "LevelEnd") 
		{

			//Debug.Log ("LevelEnd has been hit, starting end timer");

			//Reference to Timer.cs
			GameObject stageTimer = GameObject.Find ("StageTimer");
			Timer timerClass = stageTimer.GetComponent<Timer> ();
			//calls SetEndTimer() in Timer.cs to set public float TimeTillEnd as the Stage's Timer for HUD
			timerClass.SetEndTimer ();

			//start the end timer before the game quits
			StartCoroutine (EndGame ());
		}
	}

	//This is whats called when player begins a stage
	IEnumerator StagePlay ()
	{
		//MOVED HERE TO TRIGGER TIMER script AFTER TIMER HAS BEEN SET
		//enable player conrols
		PlayerMovement.shipMovement.activateControls = true;

		//hold temporarily stores wtv the speed is
		//brakes sets speed to 0
		float brakes = 0;
		float hold;

		hold = speed;
		speed = brakes;

		Debug.Log (StageNum);

		//This line waits for a set amount of seconds before continuing
		//The amount of time is held in the variable "stopTime"
		yield return new WaitForSeconds (stopTime);

		//hold is put back into speed
		//THIS IS DONE TO AVOID HARDCODED VARIABLES
		//Jorge has an elephant trunk for a penis
		speed = hold;

		//Lock player's controls in the instance of class PlayerMovement
		PlayerMovement.shipMovement.activateControls = false;

		//This is a test line for trying to lock the player's camera
		//GameObject.Find ("Main Camera").GetComponent<MichaelsMouseLook> ().FreeLook = false;

	}

	//Called when the player hits the last waypoint, the one named "LevelEnd"
	IEnumerator EndGame()
	{
		//Wait TimeTillEnd seconds before quitting the game
		yield return new WaitForSeconds (TimeTillEnd);
		Application.Quit ();
	}
}
