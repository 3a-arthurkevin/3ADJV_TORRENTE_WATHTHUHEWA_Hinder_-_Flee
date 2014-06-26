using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This script is attached to the BlockManger and it
/// only executes on the server when it is given the
/// unique name of a construction block to remove and a 
/// positive signal to destroy the block.
/// 
/// This script is accessed by the 
/// ConstructionBlockAsksToRemoveItself script.
/// 
/// This script accesses the GameSettings script to get the value
/// for water height.
/// </summary>

public class ServerRemovesConstructionBlock : MonoBehaviour {
	
	//Variables Start_________________________________________________________
	
	//The destroySignal is supplied by
	//the ConstructionBlockAsksToRemoveItself script.
	
	public bool destroySignal = false;
	
	
	//A list is used to capture all the names of blocks to be destroyed.
	
	public List<string> destroyedConstructionBlocks = new List<string>();
	
	
	//The explosion prefabs are attached to these.
	
	public GameObject explosion;
	
	public GameObject waterExplosion;
	
	
	private float waterHeight;
	
	//Variables End___________________________________________________________
	
	
	void Start ()
	{
		//Access GameSettings to get the water height.
		
		GameObject gManager = GameObject.Find("GameManager");
		
		GameSettings settingsScript = gManager.GetComponent<GameSettings>();
		
		waterHeight = settingsScript.waterHeight;	
	}
	
	
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
				//destroy each one. By using a list multiple blocks
				//can be destroyed at the same time and this will be
				//important for rockets which have a blast radius.
				
				foreach(string blockName in destroyedConstructionBlocks)
				{
					//The server instantiates an explosion at the location of the 
					//construction block across the network.
					
					networkView.RPC("InstantiateConstBlockExplosion", RPCMode.All, blockName);
					
					//Send the destroy RPC to all game instances and buffer it
					//so that the block is removed for future players that join. 
							
					networkView.RPC("DestroyConstructionBlock", RPCMode.AllBuffered, blockName);
				}	
			
				destroySignal = false;
												
				destroyedConstructionBlocks.Clear();
			}
		}
	}
	
	
	//Find the unique block by its name and then
	//destroy it across all game instances.
	
	[RPC]
	void DestroyConstructionBlock(string blockNme)
	{	
		GameObject go = GameObject.Find(blockNme);
		
	 	Destroy(go);

	}
	
	//Find the unique block by its name and then instantiate an
	//explosion at its location. The type of explosion depends on
	//the water height.
	
	[RPC]
	void InstantiateConstBlockExplosion (string blockNme)
	{
		GameObject go = GameObject.Find(blockNme);
		
		
		if(go.transform.position.y >= waterHeight)
		{
			Instantiate(explosion, go.transform.position, go.transform.rotation);		
		}
		
		else
		{
			Instantiate(waterExplosion, go.transform.position, go.transform.rotation);		
		}
	}
	
	
	
	
	
	
	
	
}
