using UnityEngine;
using System.Collections;

public class SSCamHUD : MonoBehaviour {

	Animator MyAni;

	public bool StartButtonPushed = false;

	public bool NextLevelPush = false;

	public float AlphaChangeRate = 0.4f;

	public float readWait = 8f;

	// Use this for initialization
	void Start () {
		MyAni = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		GameObject firstCanvas = GameObject.Find ("InitialScreen");
		GameObject sceneOrgan = GameObject.Find ("SceneOrcastrator");
		SceneOrganizer sceneScript = sceneOrgan.GetComponent<SceneOrganizer> ();

		if (StartButtonPushed == true) {

			firstCanvas.GetComponent<CanvasGroup> ().alpha -= (Time.deltaTime * AlphaChangeRate);

			//MyAni.Play ("StartSceneCamAni", -1, 0f);
			MyAni.SetBool("StartButtonPushed", true);
		}

		if (firstCanvas.GetComponent<CanvasGroup> ().alpha <= 0) {
			firstCanvas.GetComponent<Canvas> ().enabled = false;

			StartCoroutine (InstructionTime());
		}

		if (NextLevelPush == true) {
			if (Input.GetKey (KeyCode.Return) || Input.GetButtonDown("XboxAButton") || Input.GetButtonDown("XboxStartButton")){
				sceneScript.NextLevel();
			}
		}

		if (Input.GetButtonDown("XboxAButton") || Input.GetButtonDown("XboxStartButton")) {
			StartButtonPushed = true;
		}
	}

	public void ButtonPush() {

		if (Input.GetKey (KeyCode.Return) || Input.GetButtonDown("XboxAButton") || Input.GetButtonDown("XboxStartButton")) {
			StartButtonPushed = true;
		}
	}

	IEnumerator InstructionTime() {

		yield return new WaitForSeconds (readWait);

		GameObject pressIt = GameObject.Find ("PressCanvas");
		pressIt.GetComponent<Canvas> ().enabled = true;

		NextLevelPush = true;

	}
}
