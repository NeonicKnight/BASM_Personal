using UnityEngine;
using System.Collections;

public class WaypointBehavior : MonoBehaviour {

	//Bool for determining if waypoint has already been used or not
	//Used in old build intended to protect Player from running into the same waypoint twice, unless intended
	public bool camerWaypoint = true;

	//POSSIBLE BUG OCCURANCE, NEEDS REMEDY
	//IF GAMEOBJECT "Player" COLLIDES WITH POINTS, CODE BELOW RUNS
	//WE DO NOT WANT THAT TO HAPPEN.
	//NEED FIX SO ONLY "Pathfinder" RUNS FOLLING CODE

	//4/4/16 Added if state to only run code if col name is Pathfinder

	//When something enters the waypoint's collider
	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.name == "Pathfinder") {
			//Debug.Log ("Pathfinder has Triggered Waypoint");
			
			//Was used to test to skip the bool check
			//FlightPath.cameraMovement.NextWaypoint ();
			
			//if the waypoint's bool is true
			if (camerWaypoint == true) {
				//Debug.Log ("CameraWaypoint has been triggered");
				//This waypoint's bool was true- so now set it to false
				camerWaypoint = false;
				//Call next waypoint now that this one was been used
			
				//TEST TO SEE IF I CAN DISABLE A POINT'S COLLIDER AFTER ITS BEEN USED
				this.GetComponent<Collider> ().enabled = false;
				FlightPath.cameraMovement.NextWaypoint ();
			}
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
