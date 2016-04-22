using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StartScreenCamera : MonoBehaviour {

	//Instance created
	public static StartScreenCamera cameraMovement = null;
	//This array cannot be hardcoded
	//This array stores all of the waypoints for the player to pass through
	//In the inspector, the designer adds a value for the total number of waypoints
	//Then the designer drags the transform position for a waypoint, and drags it from its location in the inspector,
	//to its position in the array
	public List<Transform> waypoints;
	//Speed in which the player travels between points
	public float speed = 6f;

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

	void OnTriggerEnter(Collider col) {
		
		if (col.gameObject.name == "ScreenLook") {
			speed = 0;
		}

		//col.GetComponent<Collider> ().enabled = false;
	}
}
