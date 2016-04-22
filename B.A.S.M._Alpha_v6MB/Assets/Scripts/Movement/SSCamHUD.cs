using UnityEngine;
using System.Collections;

public class SSCamHUD : MonoBehaviour {

	Animator MyAni;

	public bool StartButtonPushed = false;

	// Use this for initialization
	void Start () {
		MyAni = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (StartButtonPushed == true) {
			//MyAni.Play ("StartSceneCamAni", -1, 0f);
			MyAni.SetBool("StartButtonPushed", true);
		}
	}
}
