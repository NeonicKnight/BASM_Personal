using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public float expireTime = 5f;
	//public GameObject playerObject;
	public float MissleDam = 10f;

	//ATEMPTING TO MAKE PLAYER SEEKING MISSILES
	public float Velocity = 10f;
	public float RotationSpeed = 90f;

	private Transform mTransform;

	void Awake() {
		mTransform = transform;
	}

	// Use this for initialization
	void Start () {

		mTransform = transform;
		//Destroys the bullet after set time
		Destroy (gameObject, expireTime);
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject TargetPlayer = GameObject.Find ("Player");

		Vector3 targetDirection = TargetPlayer.transform.position - mTransform.position;

		Quaternion targetRotation = Quaternion.LookRotation (targetDirection);

		mTransform.rotation = Quaternion.RotateTowards (mTransform.rotation, targetRotation, RotationSpeed * Time.deltaTime);

		mTransform.position += mTransform.forward * Velocity * Time.deltaTime;
	}


	void OnTriggerEnter (Collider other){

		GameObject playerObject = GameObject.Find("Player");
		//Debug.Log(other.gameObject.name);

		if (other.gameObject.name == "Player") {

			//Debug.Log("player has been hit yo");


			//Deals damage to player
			playerObject.GetComponent<PlayerHealth> ().TakeDamage(MissleDam);

		}

		if (other.gameObject.tag == "Obstacle") {
			Destroy(this.gameObject);
		
		}
		
	}
}
