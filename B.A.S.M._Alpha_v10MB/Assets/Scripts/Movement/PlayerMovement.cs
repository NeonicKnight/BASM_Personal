using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	//List of needed variables
	//Instance of this class
	public static PlayerMovement shipMovement;
	//Not sure what this is for
	public Transform bankingShip;
	public float bankSpeed;
	//These past few relate to the PlayerMovementArea rotating to view the next point
	private Quaternion worldRotation;

	//This bool is used to disable player control in between stages
	public bool activateControls = false;
	//How fast the player returns to the CenterPlayer position
	public float speed = 10f;
	//This position is the center of the box/frame
	//After the stage ends, the player moves to this position
	public Transform CenterPlayer;

	//Collider controls
	//The player's input in a direction is disabled when the player hits the collider for that direction
	//If true the direction is locked, if false the player is free to move about the cabin
	//Triggers are used with the player's frame so only the player may collide with them
	public bool leftLock = false;
	public bool rightLock = false;
	public bool topLock = false;
	public bool bottomLock = false;

	//Increases the player's Y axis
	//Player moves up
	public void Ascend()
	{
		//If the direction IS NOT locked, then the player moves
		if (topLock == false) {
			transform.Translate (0, .5f, 0);
		}

	}

	//Decreases the X axis of the player
	//Player moves to the left
	public void BankLeft()
	{
		//If the direction IS NOT locked, then the player moves
		if (leftLock == false) {
			transform.Translate (-.5f, 0, 0);
		}

	}

	//Increases the player's X axis
	//Player moves to the right
	public void BankRight()
	{
		//If the direction IS NOT locked, then the player moves
		if (rightLock == false) {
			transform.Translate (.5f, 0, 0);
		}

	}

	//Decreases the player's Y axis
	//Player moves down
	public void Dive()
	{
		//If the direction IS NOT locked, then the player moves
		if (bottomLock == false) {
			transform.Translate (0, -.5f, 0);
		}

	}

	void FixedUpdate()
	{

		//This line involves the whole thing's rotation to its target point
		// normalize ship rotation
		this.GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;

		//if statement checks to see if the controls are on
		//controls are activated when the stage starts
		if (activateControls == true) {
			//Checks for input key
			//Also inlcudes XBox Controller support attaching left thumb to character movement

			//First block is for up or down
			if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S) || Input.GetAxis("LeftThumbY") > 0) {
				//calls function for intended direction
				this.Dive ();
			} else if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W) || Input.GetAxis("LeftThumbY") < 0) {
				this.Ascend ();
			}
			//second block is for left and right
			if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A) || Input.GetAxis("LeftThumbX") < 0) {
				this.BankLeft ();
			} else if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D) || Input.GetAxis("LeftThumbX") > 0) {
				this.BankRight ();
			}
		}
			

		//If player controls are NOT active then the player object is to stay in CenterPlayer's position
		else if (activateControls == false) {

			//This determines how quickly the player's object is sent to CenterPlayer
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, CenterPlayer.transform.position, step);

		}

		//Update checks to see is "escape" has been pressed
		//If so, the game quits
		if (Input.GetKey("escape"))
			Application.Quit();
	}
		
	//This rotates the PlayerMovementArea to face the next point in the array
	void RotateShip(float speed, Vector3 targetDirection)
	{
		Vector3 newDirection = Vector3.RotateTowards(bankingShip.forward, targetDirection, Time.fixedDeltaTime * speed, 0);
		bankingShip.rotation = Quaternion.LookRotation(newDirection);
	}

	void Start()
	{
		//Instance created
		shipMovement = this;
	}

	//These Triggers check for collision and pass what the player collided with as "col"
	void OnTriggerEnter (Collider col)
	{

		//This list checks to see if the player hit one of the frame walls
		//If a frame wall has been hit, the that direction is locked by its respective bool
		if (col.gameObject.name == "PlayerLeftWall") 
		{
			leftLock = true;
		}

		if (col.gameObject.name == "PlayerRightWall") 
		{
			rightLock = true;
		}

		if (col.gameObject.name == "PlayerRoof") 
		{
			topLock = true;
		}

		if (col.gameObject.name == "PlayerFloor") 
		{
			bottomLock = true;
		}
	}

	//Once the player leaves the collision with a wall, its respective direction is unlocked
	void OnTriggerExit (Collider col)
	{

		if (col.gameObject.name == "PlayerLeftWall") 
		{
			leftLock = false;
		}

		if (col.gameObject.name == "PlayerRightWall") 
		{
			rightLock = false;
		}

		if (col.gameObject.name == "PlayerRoof") 
		{
			topLock = false;
		}

		if (col.gameObject.name == "PlayerFloor") 
		{
			bottomLock = false;
		}
	}
}
