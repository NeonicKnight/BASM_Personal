using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

	public static Timer timerClass = null;

	//BUG: CODE DOES NOT RUN THE NUMBER STORED HERE, IT RUNS THE NUMBER IN INSPECTOR
	//MAKE SURE THE INSPECTOR SHOWS THE DESIRED AMOUNT OF SECONDS WISHED TO BE STORED IN LevelTimeCounter
	//Stores how long the level is in SECONDS as float
	public float LevelTimeCounter;

	//Stores how much time is in a STAGE
	public float StageTimeLeft;

	//Stores how much time is in the LevelEnd Stage (last point)
	public float EndTimer;

	//bool to tell when to display the End Timer
	public bool StartEndTimer = false;

	//This is called from FlightPath.cs
	public void SetStageTimer (){
		//Finds the reference
		GameObject Pathfinder = GameObject.Find ("Pathfinder");
		FlightPath flightPath = Pathfinder.GetComponent<FlightPath> ();
		//Stores the variable "stopTime" into StageTimeLeft
		StageTimeLeft = flightPath.stopTime;

	}

	public void SetEndTimer (){
		//finds the reference
		GameObject Pathfinder = GameObject.Find ("Pathfinder");
		FlightPath flightPath = Pathfinder.GetComponent<FlightPath> ();
		//Stores the variable "TimeTillEnd" into EndTimer
		EndTimer = flightPath.TimeTillEnd;

		//Sets bool to TRUE so script can start the end timer
		StartEndTimer = true;

		GameObject scoreDisplay = GameObject.Find ("ScoreDisplay");
		ScorePoints scorePoints = scoreDisplay.GetComponent<ScorePoints> ();

		//Calls the final score function in ScorePoints.cs
		scorePoints.ScoreResult();
	}

	// Use this for initialization
	void Start () {
		
		GameObject stageTimer = GameObject.Find ("StageTimer");
		stageTimer.GetComponent<Text>().enabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//List of references, without these variables couldnt be found
		GameObject stageTimer = GameObject.Find ("StageTimer");
		GameObject levelTimer = GameObject.Find ("LevelTimer");

		//GameObject Pathfinder = GameObject.Find ("Pathfinder");
		//FlightPath flightPath = Pathfinder.GetComponent<FlightPath> ();

		//THIS TIMER IS USED FIRST TO SEE HOW LONG THE LEVEL IS
		//COMMENT OUT IF COUNT DOWN TIMER IS IN USE
		//Count Up Timer
		//LevelTimeCounter += Time.deltaTime;

		//THIS TIMER IS USED SECOND TO COUNT DOWN FROM THE NUMBER FOUND IN "Count Up Timer"
		//COMMENT OUT IF COUNT UP TIMER IS IN USE
		//Count Down Timer
		LevelTimeCounter -= Time.deltaTime;
		//Math for displaying the Level Time
		//Calculates minutes and seconds and stores them as strings
		string minutes = Mathf.Floor(LevelTimeCounter / 60).ToString("00");
		string seconds = Mathf.Floor (LevelTimeCounter % 60).ToString ("00");
		//compile strings into the text for LevelTimer in HUDCanvas
		levelTimer.GetComponent<Text> ().text = minutes + ":" + seconds;

		//Indicates the start of a STAGE
		if (PlayerMovement.shipMovement.activateControls == true) {

			//enables the StageTimer text on HUDCanvas
			stageTimer.GetComponent<Text>().enabled = true;
			//Subtracts time from the number stored in StageTimeLeft
			StageTimeLeft -= Time.deltaTime;
			//rounds the time to a whole number, then adds it to a string to keep the seconds in a two digit form
			string stringTime = Mathf.Round (StageTimeLeft).ToString("00");
			//Displays the number in the text field
			stageTimer.GetComponent<Text>().text = "Destroy Hostiles: " + "00:" + stringTime;
		}

		//end of a stage
		if (PlayerMovement.shipMovement.activateControls == false) {
			//disable the stage timer
			stageTimer.GetComponent<Text>().enabled = false;

		}

		//Indicates that the LevelEnd point has been reached
		if (StartEndTimer == true) {
			
			//display the stage timer text area from HUDCanvas
			stageTimer.GetComponent<Text> ().enabled = true;
			//subtract time
			EndTimer -= Time.deltaTime;
			//rounds the time to a whole number, then adds it to a string to keep the seconds in a two digit form
			string stringTime = Mathf.Round (EndTimer).ToString("00");
			//display end of level text with time remaining
			stageTimer.GetComponent<Text>().text = "Mission Ends: " + "00:" + stringTime;
		}
	}
}
