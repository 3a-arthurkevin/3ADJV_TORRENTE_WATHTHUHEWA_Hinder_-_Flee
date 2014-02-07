using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the player prefabs and it hides
/// the players own pointer so that it doesn't obstruct their camera.
/// </summary>


public class HideMyPointer : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		if(networkView.isMine == false)
		{
			enabled = false;	
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		Transform pointer = transform.FindChild("Pointer");
		
		pointer.renderer.enabled = false;
		
		enabled = false;
	}
}
