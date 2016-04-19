using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScorePoints : MonoBehaviour {
	//holds player's total score
	public int TotalScore = 0;
	//determines how much an android defeat is worth
	public int AndroidScoreVal = 10;
	//counter for how many androids defeated
	public int AndroidDeathCount = 0;
	//determines how much a drone defeat is worth
	public int DroneScoreVal = 30;
	//counter for how many drones defeated
	public int DroneDeathCount = 0;
	//determines the score penalty, or cost for each shot
	public int PlayerShotVal = 1;
	//counter for how many times the player shot
	public int PlayerShotCount = 0;
	//called in RayCastShoot 
	public void AddPlayerShot () {
		//cost to shoot taken from score, counter adds 1
		TotalScore -= PlayerShotVal;
		PlayerShotCount++;
	}

	//NEEDS TO BE CALLED FROM ANDROID DEATH
	public void AddAndroidScore () {
		//points awarded and counter +1
		TotalScore += AndroidScoreVal;
		AndroidDeathCount++;
	}

	//NEEDS TO BE CALLED FROM DRONE DEATH
	public void AddDroneScore () {
		//points awarded and counter +1
		TotalScore += DroneScoreVal;
		DroneDeathCount++;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//reference to score display in HUD
		GameObject scoreDisplay = GameObject.Find ("ScoreDisplay");
		//Player's total score is updated to a string so score fits in a set amount of characters
		//in this case, four zeros
		string stringScore = (TotalScore).ToString("0000");
		//Displays the number in the text field
		scoreDisplay.GetComponent<Text>().text = stringScore;
	}

	//Called at the end of the level
	//so called in Timer.cs when the end timer has been set
	public void ScoreResult () {
		//reference to Final score, and its title object from HUD
		//enable both to display to player
			GameObject finalScore = GameObject.Find ("FinalScore");
			finalScore.GetComponent<Text> ().enabled = true;
			GameObject finalScoreTitle = GameObject.Find ("FinalScoreTitle");
			finalScoreTitle.GetComponent<Text> ().enabled = true;
			//display to the player all three of their counters and their final score for the level
		finalScore.GetComponent<Text> ().text += "Androids Eliminated: " + AndroidDeathCount + "\n" + "Drones Eliminated: " + DroneDeathCount + "\n" + "Shots Fired: " + PlayerShotCount + "\n" + "Final Score: " + TotalScore;
			
			//maybe a pause can be added between each of these lines being made using the IEnumerator thing
	}
}
