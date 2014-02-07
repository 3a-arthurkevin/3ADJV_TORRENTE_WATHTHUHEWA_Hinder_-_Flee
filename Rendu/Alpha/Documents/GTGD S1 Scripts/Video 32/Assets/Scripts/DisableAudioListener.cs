using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the MainCamera and disables
/// the server from receiving sound.
/// </summary>


public class DisableAudioListener : MonoBehaviour {

	
	// Update is called once per frame
	void Update () 
	{
		if(Network.peerType == NetworkPeerType.Server)
		{
			AudioListener.pause = true;
			
			enabled = false;
		}
		
		if(Network.peerType == NetworkPeerType.Client)
		{
			enabled = false;	
		}
	}
}
