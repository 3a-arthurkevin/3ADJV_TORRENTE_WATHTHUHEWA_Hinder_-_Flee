using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to each air block and
/// is used to tell the ServerRemovesAirBlock script
/// (attached to the BlockManager) this block's name and that
/// it is to be destroyed if hit by a projectile from either team.
/// The bool iAmHit is set to true by any projectile hitting this
/// block.
/// 
/// This script accesses the ServerRemovesAirBlock script to signal
/// if this block is to be removed.
/// 
/// This script is accessed by the FireBlockEraser script which
/// tells this block that it has been hit. That script sends an
/// RPC to the server.
/// </summary>

public class AirBlockAsksToRemoveItself : MonoBehaviour {
	
	//Variables Start_________________________________________________________
	
	public bool iAmHit = false;
	
	//Variables End___________________________________________________________
	
	
	
	// Update is called once per frame
	void Update () 
	{
		//If this step block has been hit
		//then run the DestructionRoutine function.
		
		if(Network.isServer)
		{		
			if(iAmHit == true)
			{
				DeleteAirBlock(gameObject.name);
			}
			
			iAmHit = false;
		}
	}
	
	
	//This function supplies the ServerRemovesAirBlock script
	//with the unique name of this step block and a positive
	//signal that it is to be destroyed.
	
	void DeleteAirBlock(string blockNme)
	{
		GameObject go = GameObject.Find("BlockManager");
		
		ServerRemovesAirBlock script = go.GetComponent<ServerRemovesAirBlock>();
			
		script.destroyedAirBlocks.Add(blockNme);
		
		script.destroySignal = true;
	}
}
