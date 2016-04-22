using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;



public class PlayerHealth : MonoBehaviour {

	//Players lives
	public float health = 100f;
	//Amount of time from player dying to level reset
	public float resetAfterDeathTime = 5f;

	//Plays sound of death
	public AudioClip deathClip;

	//drag the healthbar from the UI into here
	public Slider HealthBar;
	//Drag the "fill" under "fill area" of the health bar into here
	//this will be used to change the color of the health bar
	public GameObject FillColor;

	//private PlayerMovement playerMovement;
	//private SceneFadeInOut sceneFadeInOut;

	//Reference to playerSighting - to use when moving camera to next stage
	//private LastPlayerSighting lastPlayerSighting;

	//timer to count time till level reset
	private float timer;

	//Boolean to see if player is dead or not
	private bool playerDead;


	// Update is called once per frame
	void Awake () {
	

		//sceneFadeInOut = GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<SceneFadeInOut>();
		//lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
	}

	void Update () {

		if (health <= 0f) 
		{
			if (!playerDead) {

				PlayerDying ();
			} else {

				PlayerDead ();

			}


		}

	}


	void PlayerDying () {

		playerDead = true;
		//AudioSource.PlayClipAtPoint (deathClip, transform.position);
	}

	void PlayerDead () {
		//Stops Player Movement **** need to work on this
		//playerMovement.enabled = false;
		//lastPlayerSighting.position = lastPlayerSighting.resetPosition;

		//Check what makes the audio stop completely
		AudioManager.instance.musicSource.Pause();

		//NEED TO CHECK IF AUDIO IS OFF, THEN PLAY
		//if (!AudioManager.instance.PlaySingle (deathClip)) {
			//AudioManager.instance.PlaySingle (deathClip);
		//}

		FlightPath.cameraMovement.speed = 0f;

		PlayerMovement.shipMovement.activateControls = false;

		GameObject DamScreen = GameObject.Find ("DamageIndicator");
		FadeDamageIndicator DamFade = DamScreen.GetComponent<FadeDamageIndicator> ();

		DamFade.PlayerDead ();

		LevelReset ();


	}

	void LevelReset () {

		GameObject sceneOrgan = GameObject.Find ("SceneOrcastrator");
		SceneOrganizer sceneScript = sceneOrgan.GetComponent<SceneOrganizer> ();

		timer += Time.deltaTime;


		if (timer >= resetAfterDeathTime) {

			//Application.Quit();

			//Calls the restart script from the SceneOrganizer
			sceneScript.RestartLevel ();

			//SceneManager.LoadScene ("Level_1_FullEnvironment");
			//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			//Application.LoadLevel(0);
			//Reset Scene
			//sceneFadeInOut.EndScene();
		}

	}

	public void TakeDamage (float amount)
	{
		//reference to the screen damage indicator
		GameObject DamScreen = GameObject.Find ("DamageIndicator");
		FadeDamageIndicator DamFade = DamScreen.GetComponent<FadeDamageIndicator> ();
		//changes the color of the healthbar to red when hit
		FillColor.GetComponent<Image> ().color = new Color (255f, 0f, 0f);
		//call the damage indicator to flash red
		DamFade.FadeRed ();

		//Reduces players health
		health -= amount;

		//healthbar reflects the numerical value of health
		HealthBar.value = health;
	
	}

	//This is called from FadeDamageIndicator class
	//sets the health bar color back to green after the screen has flashed red
	public void NormalColor () {
		//sets back to color green
		FillColor.GetComponent<Image> ().color = new Color (0f, 255f, 0f);

	}
}
