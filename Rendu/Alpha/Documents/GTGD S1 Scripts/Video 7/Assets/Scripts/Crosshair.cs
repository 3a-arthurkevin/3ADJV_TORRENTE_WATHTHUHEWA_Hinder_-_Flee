using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the player and causes 
/// a crosshair to be drawn in the center of the screen.
/// </summary>


public class Crosshair : MonoBehaviour {
	
	//Variables Start___________________________________
	
	public Texture crosshairTex;
	
	private float crosshairDimension = 256;
	
	private float halfCrosshairWidth = 128;
	
	
	//Variables End_____________________________________

	// Use this for initialization
	void Start () 
	{
		if(networkView.isMine == false)
		{
			enabled = false;	
		}
	}
	
	
	void OnGUI ()
	{
		//Display the crosshair in the center of the screen while the cursor is locked.
		
		if(Screen.lockCursor == true)
		{
			GUI.DrawTexture(new Rect(Screen.width / 2 - halfCrosshairWidth,
			                         Screen.height / 2 - halfCrosshairWidth,
			                         crosshairDimension, crosshairDimension), crosshairTex);
		}	
	}
}
