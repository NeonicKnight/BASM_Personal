using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour,IDamageable{
	//Enemies starting health
	public int startingHealth = 3;
	public GameObject hitParticles;

	//keeps track of enemies health
	public int CurrentHealth;

	//Animator to play animation
	public Animator anim;

	public GameObject lightningTrail;
	public GameObject smoke;
	public GameObject enemyObject;

	public AudioSource deadSound;

	// Use this for initialization
	void Start () 
	{
		

		anim = GetComponent<Animator>();
		CurrentHealth = startingHealth;

		deadSound = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	public void Damage (int damage, Vector3 hitPoint) 
	{
		//Plays hurt animation
		anim.Play ("Hurt", -1, 0f);
		//Instantiate (hitParticles, hitPoint, Quaternion.identity);
		//Substracts health 
		CurrentHealth -= damage;

		//if enemy is dead play the dead animation
		if (CurrentHealth <= 0) 
		{
			Defeated ();

		}
	
	}

	//When enemy life is 0 or less
	void Defeated()
	{
		//plays death animation
		 anim.Play ("Death", -1, 0f);
		//starts countdown till it dissapears
		StartCoroutine(deathTimer());

		//-------------Play dead Sound
		//deadSound.Play();

		//Plays particle effects on death of Android
		Instantiate (lightningTrail, enemyObject.transform.position, enemyObject.transform.rotation);
		Instantiate (smoke, enemyObject.transform.position, enemyObject.transform.rotation);

	}
	//countdown till it deletes enemy gameobject
	IEnumerator deathTimer(){
		//Debug.Log ("Waitfor seconds");
		yield return new WaitForSeconds(1.5f);

		//Add points to score and counter in ScorePoints.cs on 
		GameObject scoreDisplay = GameObject.Find ("ScoreDisplay");
		ScorePoints addScore = scoreDisplay.GetComponent<ScorePoints> ();

		addScore.AddAndroidScore ();
		//Need to determine if android or drone and add apropriate score

		gameObject.SetActive (false);
	}
}

