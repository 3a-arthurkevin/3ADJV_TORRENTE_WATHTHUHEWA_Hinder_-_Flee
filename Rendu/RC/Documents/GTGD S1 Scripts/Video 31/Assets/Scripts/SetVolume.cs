using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the main camera and its purpose
/// is to set the volume of the audio listener.
/// 
/// This script is accessed by the GameSettings script.
/// </summary>


public class SetVolume : MonoBehaviour {
	
	//Variables Start_________________________________________________________
	
	public bool setVolume;
	
	public float volume;
	
	//Variables End___________________________________________________________


	
	// Update is called once per frame
	void Update () 
	{
		if(setVolume == true)
		{
			AudioListener.volume = volume;
			
			setVolume = false;
		}
	}
}
