using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This script is attached to the BlockManger and it
/// only executes on the server when it is given the
/// unique name of an air block to remove and positive
/// signal to destroy the block.
/// 
/// This script is accessed by the AirBlockAsksToRemoveItself
/// script which supplies the name of the block that is to
/// be removed.
/// </summary>

public class ServerRemovesAirBlock : MonoBehaviour {
	
	//Variables Start_________________________________________________________
	
	//The destroySignal is supplied by
	//the AirBlockAsksToRemoveItself script.

	public bool destroySignal = false;
	
	
	//A list is used to capture all the names of blocks to be destroyed.
	
	public List<string> destroyedAirBlocks = new List<string>();
	
	//Variables End___________________________________________________________
	
	
	
	// Update is called once per frame
	void Update () 
	{
		//Only run this script when actually connected,
		//otherwise this script may try to send an RPC
		//when there is no network connection.
		
		if(Network.peerType != NetworkPeerType.Disconnected)
		{
			if(destroySignal == true)
			{	
				//Go through the list of blocks to be destroyed and
				//destroy each one.
				
				foreach(string blockName in destroyedAirBlocks)
				{
					//Send the destroy RPC to all game instances and buffer it
					//so that the block is removed for future players that join. 
							
					networkView.RPC("DestroyAirBlock", RPCMode.AllBuffered, blockName);
				}
				
				//Turn off the destroySignal otherwise the server will
				//continue to instantiate block at the loction provided.
				
				destroySignal = false;
				
				destroyedAirBlocks.Clear();
			}
		}
	}
	
	
	//Find the unique block by its name and then
	//destroy it across all game instances.
	
	[RPC]
	void DestroyAirBlock(string blockNme)
	{	
		GameObject go = GameObject.Find(blockNme);
		
	 	Destroy(go);

	}
}
