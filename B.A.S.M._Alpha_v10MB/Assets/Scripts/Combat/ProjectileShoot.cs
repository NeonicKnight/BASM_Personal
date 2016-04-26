using UnityEngine;
using System.Collections;

public class ProjectileShoot : MonoBehaviour {

	//Our Granade or missle 
	public Rigidbody projectile;

	public Transform bulletSpawn;

	public float projectileForce = 500f;

	public float fireRate = .25f;

	private float nextFireTime;


	
	// Update is called once per frame
	void Update () {

		//When button press (right click)
		if (Input.GetButtonDown ("Fire2") && Time.time > nextFireTime) 
		{
			//bulletSpawn(location where it is being created
			Rigidbody cloneRb = Instantiate (projectile, bulletSpawn.position, Quaternion.identity) as Rigidbody;
			//Applys force
			cloneRb.AddForce(bulletSpawn.transform.forward * projectileForce);
			//Applies the fireRate
			nextFireTime = Time.time + fireRate;
		}
	}
}
