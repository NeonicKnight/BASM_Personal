using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneOrganizer : MonoBehaviour {

	public int CurrentScene = 0;

	public int GrandScore = 0;

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
			CurrentScene = -1;
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

		SceneManager.LoadScene (CurrentScene + 1);
		//Application.LoadLevel (CurrentScene + 1);


	}
}
