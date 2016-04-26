using UnityEngine;
using System.Collections;

public class MichaelsMouseLook : MonoBehaviour {

	//Instance
	public static MichaelsMouseLook mouseLook;

	//float for sensitivity of mouse
	//for mouse, 450f
		//public float mouseSensitivity = 450.0f;

	//for xbox controller 
		public float mouseSensitivity = 200.0f;


		private float rotY = 0.0f; // rotation around the up/y axis
		private float rotX = 0.0f; // rotation around the right/x axis

	//Im trying to restrict the players view during flight
		//public bool FreeLook = false;

		void Start ()
		{
			//collects rotation as angle and stores it
			Vector3 rot = transform.localRotation.eulerAngles;
			rotY = rot.y;
			rotX = rot.x;

		}

		void Update ()
		{

			//gets mouse current input
			float mouseX = Input.GetAxis ("Mouse X");
			float mouseY = -Input.GetAxis ("Mouse Y");

		//XBox Controller input attaches right thumb to camera controll
		mouseX = Input.GetAxis ("RightThumbX");
		mouseY = Input.GetAxis ("RightThumbY");
				
			//moves current camera view to mouse input by the product of time and sensitivity
			rotY += mouseX * mouseSensitivity * Time.deltaTime;
			rotX += mouseY * mouseSensitivity * Time.deltaTime;
					
			//Applies the rotation solved above
			Quaternion localRotation = Quaternion.Euler (rotX, rotY, 0.0f);
			transform.rotation = localRotation;

		}
	}