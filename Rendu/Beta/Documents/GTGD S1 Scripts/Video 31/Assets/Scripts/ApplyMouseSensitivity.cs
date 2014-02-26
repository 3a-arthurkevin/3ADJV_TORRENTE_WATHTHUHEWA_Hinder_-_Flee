using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the players and its purpose is
/// to retrieve the mouse sensitivity values in the GameSettings
/// script and apply them to the MouseLook scripts on this player.
/// This happens when the player spawns.
/// 
/// This script accesses the GameSettings script.
/// 
/// This script accesses the MouseLook scripts on the main player
/// prefab and on the CameraHead.
/// </summary>

public class ApplyMouseSensitivity : MonoBehaviour {
	

	// Use this for initialization
	void Start () 
	{
		if(networkView.isMine == true)
		{
			GameObject gManager = GameObject.Find("GameManager");
			
			GameSettings settingsScript = gManager.GetComponent<GameSettings>();
			
			
			//Set the mouse sensitivity X. It is in the MouseLook script
			//attached to the main player prefab.
			
			MouseLook mouseXSensitivity = transform.GetComponent<MouseLook>();
			
			mouseXSensitivity.sensitivityX = settingsScript.mouseSensitivity;
			
			
			//Set the mouse sensitivity Y. It is in the MouseLook script
			//attached to the CameraHead.
			
			Transform cameraHead = transform.FindChild("CameraHead");
			
			MouseLook mouseYsensitivity = cameraHead.GetComponent<MouseLook>();
			
			mouseYsensitivity.sensitivityY = settingsScript.mouseSensitivity;
		}
		
		else
		{
			enabled = false;
		}			
	}
}
