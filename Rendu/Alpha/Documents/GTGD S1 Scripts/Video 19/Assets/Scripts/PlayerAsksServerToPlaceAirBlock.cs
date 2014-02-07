using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to each player and it
/// allows them to place Air Blocks. This
/// script determines the position where an air
/// block is to be placed and then asks the server to build
/// a block at that location.
/// 
/// This script accesses the ServerCreatesAirBlock script
/// in the BlockManager.
/// 
/// This script accesses the PlayerResource script.
/// 
/// This script accesses the ChangeBuildOption script.
/// </summary>

public class PlayerAsksServerToPlaceAirBlock : MonoBehaviour {
	
	//Variables Start_________________________________________________________	
	
		
	//Used for the virtual grid.
	
	private float gridWidth = 0.6f;
	
	private float posY;
	
	private float range = 1.2f;
	
	private float maxRange = 24f;
	
	public float cost = 10f;

	
	//cached items.
	
	private Transform myTransform;
	
	private Transform myCameraHeadTransform;
	
	private PlayerResource resourceScript;
	
	private ChangeBuildOption buildOptionScript;
	
	
	//The tempBlock is dropped so that construction
	//looks immediate to the player dispite the lag.
	//That way they will be able to do construction 
	//quickly even in a laggy network. The temp
	//block gets rid of itself after a few moments.
	
	public GameObject tempBlock;
	
	
	//Variables End___________________________________________________________
	
	
	// Use this for initialization
	void Start () 
	{
		if(networkView.isMine == true)
		{	
			myTransform = transform;

			myCameraHeadTransform = myTransform.FindChild("CameraHead");
						
			resourceScript = gameObject.GetComponent<PlayerResource>();
						
			buildOptionScript = myTransform.GetComponent<ChangeBuildOption>();
		}
		
		else
		{
			enabled = false;	
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Screen.lockCursor == true && Input.GetButtonDown("Place Block") && resourceScript.resource >= cost 
		   && buildOptionScript.buildOption == ChangeBuildOption.State.airBlock)
		{
			//Our aim is to place this block at some point infront of the player.
			//This depends on the float range which is altered by turning the 
			//mousewheel (see below).
				
			Vector3 position = myCameraHeadTransform.TransformPoint(0, 0, range);
			
			
			//This calculation places the block within a virtual grid that matches
			//up with the other block types
			
			position /= gridWidth;
							
			position = new Vector3(Mathf.Round(position.x), Mathf.Round(position.y), Mathf.Round(position.z));
			
			
			//Take into account whether the player is looking up or down.
			//This will help determine whether the placement of the new construction
			//block should tend upwards or downwards.
			
			if(myCameraHeadTransform.eulerAngles.x >= 90)
			{
				//Debug.Log(myCameraHeadTransform.eulerAngles.x.ToString());
				
				posY = position.y + 0.5f;	
			}
			
			if(myCameraHeadTransform.eulerAngles.x < 90)
			{
				//Debug.Log(myCameraHeadTransform.eulerAngles.x.ToString());
				
				posY = position.y - 0.5f;	 
			}
			
			position = new Vector3(position.x, posY, position.z);
										
			position *= gridWidth;
			
			//We should check that nothing already occupies that position before placing the
			//block.
			
			Vector3 checkPos = new Vector3(position.x, position.y, position.z);
			
							
			if(!Physics.Linecast(myCameraHeadTransform.position, checkPos) &&
			   	Vector3.Distance(transform.position, position) > gridWidth &&
			   	resourceScript.resource >= cost)
			{
				//Finally send an RPC to the server only telling it the position the 
				//construction block should be placed at and its rotation.
				
				networkView.RPC("AskServerToDropAirBlock", RPCMode.Server, position, Quaternion.identity);
				
				
				//Place the temp block so that construction looks instantaneous to 
				//the player despite the lag.
				
				DropTempAirBlock(position, Quaternion.identity);
				
				
				
				resourceScript.resource = resourceScript.resource - cost;
				
			}
		}
		
		//When the player moves their mousewheel the range at which the block
		//will be placed changes.
		
		if(buildOptionScript.buildOption ==  ChangeBuildOption.State.airBlock)
		{
			if(Input.GetAxis("Mouse ScrollWheel") > 0)
			{
				range = range + gridWidth;
			}
			
			if(Input.GetAxis("Mouse ScrollWheel") < 0)
			{
				range = range - gridWidth;	
			}
			
			if(range < gridWidth * 2)
			{
				range = gridWidth * 2;
			}
			
			if(range > maxRange)
			{
				range = maxRange;	
			}
		}
	}
	
	
	[RPC]
	void AskServerToDropAirBlock (Vector3 pos, Quaternion rot)
	{	
		//Find the BlockManager gameObject and assign the position and rotation values
		//to its script ServerCreatesConstructionBlock.
		
		GameObject go = GameObject.Find("BlockManager");
		
		ServerCreatesAirBlock script = go.GetComponent<ServerCreatesAirBlock>();
		
		script.dropPosition = pos;
		
		script.dropRotation = rot;
		
		script.dropSignal = true;
	}
	
	
	
	void DropTempAirBlock (Vector3 pos, Quaternion rot)
	{
		Instantiate(tempBlock, pos, rot);	
	}
}
