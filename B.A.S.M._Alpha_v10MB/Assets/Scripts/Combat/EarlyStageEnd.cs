using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class EarlyStageEnd : MonoBehaviour {

	//public static EarlyStageEnd ESE;

	public List<GameObject> Andys;

	public int EndBlock = 10;

	//public int CurrentStageNum = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		GameObject pathy = GameObject.Find ("Pathfinder");
		FlightPath flightPath = pathy.GetComponent<FlightPath> ();

		if (SceneManager.GetActiveScene ().name == "Tut_Level") {

		}

		if (SceneManager.GetActiveScene ().name == "Level_1_FullEnvironment") {


			if (flightPath.CurrentStageNum == 1) {
				//Debug.Log ("some shit");
				if (EndBlock != flightPath.CurrentStageNum) {
					Debug.Log (flightPath.CurrentStageNum);
					if (Andys [0].gameObject.activeSelf == false) {
						if (Andys [1].gameObject.activeSelf == false) {
							if (Andys [2].gameObject.activeSelf == false) {
								Debug.Log ("About to stop the clock");
								flightPath.StopTheClock ();
								EndBlock = flightPath.CurrentStageNum;
								Debug.Log (EndBlock);
								//CurrentStageNum++;
							}
						}
					}
				}

			}

			if (flightPath.CurrentStageNum == 2) {
				if (EndBlock != flightPath.CurrentStageNum) {
					Debug.Log (flightPath.CurrentStageNum);
					if (Andys [3].gameObject.activeSelf == false) {
						if (Andys [4].gameObject.activeSelf == false) {
							Debug.Log ("About to stop the clock");
							flightPath.StopTheClock ();
							EndBlock = flightPath.CurrentStageNum;
							Debug.Log (EndBlock);
							//CurrentStageNum++;
						}
					}
				}
			}

			if (flightPath.CurrentStageNum == 3) {
				if (EndBlock != flightPath.CurrentStageNum) {
					Debug.Log (flightPath.CurrentStageNum);
					if (Andys [5].gameObject.activeSelf == false) {
						if (Andys [6].gameObject.activeSelf == false) {
							Debug.Log ("About to stop the clock");
							flightPath.StopTheClock ();
							EndBlock = flightPath.CurrentStageNum;
							Debug.Log (EndBlock);
							//CurrentStageNum++;
						}
					}
				}
			}

			if (flightPath.CurrentStageNum == 4) {
				if (EndBlock != flightPath.CurrentStageNum) {
					Debug.Log (flightPath.CurrentStageNum);
					if (Andys [7].gameObject.activeSelf == false) {
						if (Andys [8].gameObject.activeSelf == false) {
							Debug.Log ("About to stop the clock");
							flightPath.StopTheClock ();
							EndBlock = flightPath.CurrentStageNum;
							Debug.Log (EndBlock);
							//CurrentStageNum++;
						}
					}
				}
			}

			if (flightPath.CurrentStageNum == 5) {
				if (EndBlock != flightPath.CurrentStageNum) {
					Debug.Log (flightPath.CurrentStageNum);
					if (Andys [9].gameObject.activeSelf == false) {
						if (Andys [10].gameObject.activeSelf == false) {
							if (Andys [11].gameObject.activeSelf == false) {
								Debug.Log ("About to stop the clock");
								flightPath.StopTheClock ();
								EndBlock = flightPath.CurrentStageNum;
								Debug.Log (EndBlock);
								//CurrentStageNum++;
							}
						}
					}
				}
			}

			if (flightPath.CurrentStageNum == 6) {
				if (EndBlock != flightPath.CurrentStageNum) {
					Debug.Log (flightPath.CurrentStageNum);
					if (Andys [12].gameObject.activeSelf == false) {
						if (Andys [13].gameObject.activeSelf == false) {
							Debug.Log ("About to stop the clock");
							flightPath.StopTheClock ();
							EndBlock = flightPath.CurrentStageNum;
							Debug.Log (EndBlock);
							//CurrentStageNum++;
						}
					}
				}
			}

		}


		if (SceneManager.GetActiveScene ().name == "Level_2") {

		}
	}
}
