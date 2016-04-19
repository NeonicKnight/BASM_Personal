using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public AudioSource efxSource;
	public AudioSource musicSource;
	public static AudioManager instance = null;

	public float volume;

	public float lowPitchRange = .95f;
	public float highPitchRange = 1.05f;

	// Use this for initialization
	void Awake () {
	
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
		}

		//Keeps background song playing through different scenes.
		// DontDestroyOnLoad (gameObject);
	}

	public void PlaySingle (AudioClip clip){

		efxSource.clip = clip;
		efxSource.Play ();
	}

	public void RandomizeSfx (params AudioClip [] clips)
	{
		//Generate a random number between 0 and the length of our array of clips passed in
		int randomIndex = Random.Range (0, clips.Length);
		//Choose a random pitch to play back our clip at between our high and low pitch ranges.
		float randomPitch = Random.Range (lowPitchRange, highPitchRange);
		//Set the pitch of the audio source to the randomly chosen pitch
		efxSource.pitch = randomPitch;
		//Set the clip to the clip at our randomly chosen index
		efxSource.clip = clips [randomIndex];
		//Play the clip
		efxSource.Play ();

	}
	
	//To play sound just call 
	//AudioManager.instance.PlaySingle(the sound u want to play);

	//To stop a sound from playing use
	//AudioManager.instance.MusicSource.Stop();
}
