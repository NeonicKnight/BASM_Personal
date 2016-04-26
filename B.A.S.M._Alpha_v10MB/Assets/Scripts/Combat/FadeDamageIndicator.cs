using UnityEngine;
using System.Collections;

public class FadeDamageIndicator : MonoBehaviour {
	//set the maximum alpha for the dam indi
	public float MaxAlpha = 0.28f;
	//how quickly the screen flashes red
	public float AlphaChangeRate = 0.12f;
	//bool to start flash
	public bool DoTheFade = false;
	//brings the image alpha back to 0 after it flashes red up to MaxAlpha
	private bool LowerFade = false;

	//called from PlayerHealth.cs
	public void FadeRed () {
		//sets the bool to true so update can run
		DoTheFade = true;

	}

	void Update() {
		//only runs if bool is true
		if (DoTheFade == true) {
			//reference to UI image
			GameObject DamScreen = GameObject.Find ("DamageIndicator");
			//First brings the 0 alpha to the max alpha, by the rate variable over time
			if (DamScreen.GetComponent<CanvasGroup> ().alpha < MaxAlpha && LowerFade == false) {
				DamScreen.GetComponent<CanvasGroup> ().alpha += (Time.deltaTime * AlphaChangeRate);
				}
				//Once the max alpha has been hit, bring the alpha back to 0
			if (DamScreen.GetComponent<CanvasGroup> ().alpha >= MaxAlpha) {
				//this bool ensures that the alpha will now be brought down
				LowerFade = true;
			}
			//Brings the alpha back down from its max
			if (DamScreen.GetComponent<CanvasGroup> ().alpha > 0 && LowerFade == true) {
				DamScreen.GetComponent<CanvasGroup> ().alpha -= (Time.deltaTime * AlphaChangeRate);
				}
			//ONCE the alpha has been brought down to 0, bools are reset to ensure that code stops running here
			if (DamScreen.GetComponent<CanvasGroup> ().alpha <= 0) {
				LowerFade = false;
				DoTheFade = false;

				//also in this reset list
				//reference back to PlayerHealth, to reset the color of the health bar
				GameObject player = GameObject.Find ("Player");
				PlayerHealth playerHP = player.GetComponent<PlayerHealth> ();
				//calls function to set health bar back to green now that damage indication is over
				playerHP.NormalColor ();
			}
		}
	}

	public void PlayerDead() {
		GameObject DamScreen = GameObject.Find ("DamageIndicator");

		DamScreen.GetComponent<CanvasGroup> ().alpha += (Time.deltaTime * AlphaChangeRate);

	}
		
}
