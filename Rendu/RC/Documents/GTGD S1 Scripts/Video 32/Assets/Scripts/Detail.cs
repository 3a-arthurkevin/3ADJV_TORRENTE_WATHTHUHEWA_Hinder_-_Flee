using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the terrain and its purpose is to deactivate
/// detail (grass etc.) so that resources aren't wasted on that.
/// </summary>


public class Detail : MonoBehaviour {

	
	// Update is called once per frame
	void Update () 
	{
		if(Network.peerType == NetworkPeerType.Server)
		{
			Terrain.activeTerrain.detailObjectDistance = 0;
			
			Terrain.activeTerrain.detailObjectDensity = 0;
			
			enabled = false;
		}
		
		if(Network.peerType == NetworkPeerType.Client)
		{
			enabled = false;	
		}
	}
}
