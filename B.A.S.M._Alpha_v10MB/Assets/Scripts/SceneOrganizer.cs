using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneOrganizer : MonoBehaviour {

	public int CurrentScene = 0;

	public int GrandScore = 0;

	public int GrandShots = 0;

	public int GrandAndCount = 0;

	public int GrandDroCount = 0;

	public float OverTimer = 16f;

	public bool DoOnce = true;

	public static SceneOrganizer sceneOrgan;

	void Start () {
		DontDestroyOnLoad (this.gameObject);
	}

	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene ().name == "StartScene") {
			CurrentScene = 0;
		}
		if (SceneManager.GetActiveScene ().name == "Tut_Level") {
			CurrentScene = 1;
		}
		if (SceneManager.GetActiveScene ().name == "Level_1_FullEnvironment") {
			CurrentScene = 2;
		}
		if (SceneManager.GetActiveScene ().name == "Level_2") {
			CurrentScene = 3;
		}
		if (SceneManager.GetActiveScene ().name == "EndScene") {
			CurrentScene = -1;
			GameOverScore ();
		}
	}

	public void RestartLevel () {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 5.3
		//Application.LoadLevel(Application.loadedLevelName); // 5.2
	}

	public void NextLevel () {
		GameObject scoreDisplay = GameObject.Find ("ScoreDisplay");
		ScorePoints totalAllScores = scoreDisplay.GetComponent<ScorePoints> ();

		GrandScore += totalAllScores.TotalScore;
		GrandShots += totalAllScores.PlayerShotCount;
		GrandAndCount += totalAllScores.AndroidDeathCount;
		GrandDroCount += totalAllScores.DroneDeathCount;


		SceneManager.LoadScene (CurrentScene + 1);
		//Application.LoadLevel (CurrentScene + 1);
	}

	public void GameOverScore() {

		//Reference to the Timer script
		GameObject stageTimer = GameObject.Find ("StageTimer");
		//display the stage timer text area from HUDCanvas
		stageTimer.GetComponent<Text> ().enabled = true;
		//subtract time
		OverTimer -= Time.deltaTime;
		//rounds the time to a whole number, then adds it to a string to keep the seconds in a two digit form
		string stringTime = Mathf.Round (OverTimer).ToString ("00");
		//display end of level text with time remaining
		stageTimer.GetComponent<Text> ().text = "Game Ends: " + "00:" + stringTime;

		if (DoOnce == true) {
			//reference to Final score, and its title object from HUD
			//enable both to display to player
			GameObject finalScore = GameObject.Find ("FinalScore");
			finalScore.GetComponent<Text> ().enabled = true;
			GameObject finalScoreTitle = GameObject.Find ("FinalScoreTitle");
			finalScoreTitle.GetComponent<Text> ().enabled = true;
			//display to the player all three of their counters and their final score for the level
			finalScore.GetComponent<Text> ().text += "Androids Eliminated: " + GrandAndCount + "\n" + "Drones Eliminated: " + GrandDroCount + "\n" + "Shots Fired: " + GrandShots + "\n" + "Final Score: " + GrandScore;
			
			//maybe a pause can be added between each of these lines being made using the IEnumerator thing

			DoOnce = false;
		}

		StartCoroutine (GameFinallyOver ());
	}

	IEnumerator GameFinallyOver() {
		//Wait TimeTillEnd seconds before quitting the game
		yield return new WaitForSeconds (OverTimer);
		Application.Quit ();
	}
}
