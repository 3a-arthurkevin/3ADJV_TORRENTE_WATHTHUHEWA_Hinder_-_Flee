using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the BlockManager and
/// it is only executed on the server because only the server will
/// ever receive a positive dropSignal from the 
/// PlayerAsksServerToPlaceAirBlock script.
/// </summary>

public class ServerCreatesAirBlock : MonoBehaviour {
	
	//Variables Start_________________________________________________________	
	
	//The air block prefab is attached to
	//this in the inspector.
	
	public GameObject airBlock;
	
	
	//These variables are set by the PlayerAsksServerToPlaceAirBlock
	//script.
	
	public Vector3 dropPosition;
	
	public Quaternion dropRotation;
	
	public bool dropSignal = false;
	
	
	//blockCounter is used to give each block instantiated a unique name.
	
	public int blockCounter = 0;
	
	public string blockName;
	
	private GameObject block;
	
	
	//Variables End___________________________________________________________
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Only the server may instantiate air blocks.
		
		if(Network.isServer)
		{
			if(dropSignal == true)
			{	
				//Using blockCounter assign a unique name
				//to each block created.
				
				blockCounter++;
				
				blockName = "Air Block " + blockCounter.ToString();
				
				
				//Send an RPC to all game instances, and buffer it, with
				//the position and rotation to create the new step
				//block at.
				
				networkView.RPC("CreateAirBlock", RPCMode.AllBuffered, dropPosition, dropRotation, blockName);
				
				
				//Turn the dropSignal off so that the 
				//server doesn't continue to create blocks 
				//in the same place.
				
				dropSignal = false;
			}
		}
	}
	
	
	[RPC]
	void CreateAirBlock (Vector3 pos, Quaternion rot, string blockNme)
	{
		block = (GameObject) Instantiate(airBlock, pos, rot);
		
		//Assign the unique name to the created block
		//across all game instances.
		
		block.name = blockNme;
		
		
		//This bit of code adds the air cube created to the GroupAirBlock
		//so that the hierarchy window remains tidy.
		
		GameObject airCubeGroup = GameObject.Find("GroupAirBlock");
		
		block.transform.parent = airCubeGroup.transform;
	}
}
