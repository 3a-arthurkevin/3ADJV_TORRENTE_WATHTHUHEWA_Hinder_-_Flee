using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the WindZone and is used
/// to disable the WindZone if the peerType is a server.
/// </summary>


public class WindScript : MonoBehaviour {

	// Update is called once per frame
	void Update () 
	{
		if(Network.peerType == NetworkPeerType.Server)
		{
			gameObject.active = false;
			
			enabled = false;
		}
		
		if(Network.peerType == NetworkPeerType.Client)
		{
			enabled = false;	
		}
	}

}
