using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the Player character and allows them to
/// do fake crouching. The script really just changes the player's Y scale
/// and changes their movement speed.
/// 
/// This script accesses the player's CharacterMotor script to change
/// the player's movement speeds and their jump height.
/// </summary>

public class Crouching : MonoBehaviour {
	
	//Variables Start___________________________________
	
	//Have a quick reference to the CharacterMotor script.
	
	private CharacterMotor motorScript;
	
	
	//Crouching and standing height values.
	
	private float crouchScale = 0.19f;
	
	private float standingScale = 0.4f;
	
	
	//Crouch and normal movement speed values.
	
	private float crouchSpeed = 2;
	
	private float normalSpeed = 5;
	
	
	//Crouch and normal jump height values.
	
	private float crouchBaseHeight = 0.5f;
	
	private float normalBaseHeight = 1;
	
	private float crouchExtraHeight = 0.5f;
	
	private float normalExtraHeight = 1;
	
	
	//Used in determining whether the player will be 
	//crouching or standing.
	
	public bool crouchEngaged = false;
	
	//Variables End_____________________________________
	
	// Use this for initialization
	void Start () 
	{
		if(networkView.isMine == true)
		{
			//Set the player's normal speed on spawning.
			
			motorScript = gameObject.GetComponent<CharacterMotor>();
			
			motorScript.movement.maxForwardSpeed = normalSpeed;
			
			motorScript.movement.maxBackwardsSpeed = normalSpeed;
			
			motorScript.movement.maxSidewaysSpeed = normalSpeed;
			
			//Set the player's normal jumping height.
			
			motorScript.jumping.baseHeight = normalBaseHeight;
			
			motorScript.jumping.extraHeight = normalExtraHeight;
		}
		
		else
		{
			enabled = false;	
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Engage crouching when the player presses left ctrl, and is not already crouching, 
		//and the cursor is not unlocked.
		
		if(Input.GetButtonDown("Crouch") && crouchEngaged == false && Screen.lockCursor == true)
		{
			crouchEngaged = true;
			
			
			//The camera is shifted down so that it remains inside the player before the player
			//scale is changed. This prevents the palyer from seeing parts of themselves when
			//crouching.
			
			GameObject mainCam = GameObject.Find("Main Camera");
			
			Vector3 cameraPos = new Vector3(transform.position.x, mainCam.transform.position.y - 0.14f, transform.position.z);
			
			mainCam.transform.position = cameraPos;
			
			
			//Change the player's Y scale.
			
			transform.localScale = new Vector3(transform.lossyScale.x, crouchScale, transform.lossyScale.z);
			
			
			//Access the CharacterMotor scripts and change the player's speed and jump height values.
			
			motorScript.movement.maxForwardSpeed = crouchSpeed;
			
			motorScript.movement.maxBackwardsSpeed = crouchSpeed;
			
			motorScript.movement.maxSidewaysSpeed = crouchSpeed;
			
			motorScript.jumping.baseHeight = crouchBaseHeight;
			
			motorScript.jumping.extraHeight = crouchExtraHeight;
			
		}
		
		
		//Disengage crouching when the player presses left ctrl, and is currently crouching
		//and the cursor is not unlocked.
		
		if(Input.GetButtonUp("Crouch") && crouchEngaged == true && Screen.lockCursor == true)
		{
			crouchEngaged = false;
			
			
			//Boost the player up a bit so that they can't fall through the floor when rescaling.
			
			transform.position = new Vector3(transform.position.x, transform.position.y + crouchScale, transform.position.z);
			
			
			//Rescale the player to their standing scale.
			
			transform.localScale = new Vector3(transform.lossyScale.x, standingScale, transform.lossyScale.z);
			
			
			//Access the CharacterMotor scripts and change the player's speed and jump height values.
			
			motorScript.movement.maxForwardSpeed = normalSpeed;
			
			motorScript.movement.maxBackwardsSpeed = normalSpeed;
			
			motorScript.movement.maxSidewaysSpeed = normalSpeed;
			
			motorScript.jumping.baseHeight = normalBaseHeight;
			
			motorScript.jumping.extraHeight = normalExtraHeight;
			
		}
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
}

