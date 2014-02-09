using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the player and allows them to
/// fire a ray that destroys blocks.
///
/// This script accesses the ConstructionBlockAsksToRemoveItself 
/// script and this is done by sending an RPC to the server.
/// 
/// This script accesses the AirBlockAsksToRemoveItself 
/// script and this is done by sending an RPC to the server.
/// 
/// This script accesses the PlayerResource script.
/// 
/// This script accesses the ChangeWeapon script.
/// </summary>

public class FireBlockEraser : MonoBehaviour {
	
	//Variables Start_________________________________________________________
	
	private float fireRate = 0.5f;
	
	private float nextFire = 0.0f;
	
	
	//Quick references.
	
	private Transform myTransform;
	
	private Transform cameraHeadTransform;
	
	private ChangeWeapon weaponScript;
	

	private Vector3 lineOrigin = new Vector3();
	
	private Vector3 hitPosition = new Vector3();
	
		
	//These variables are used for the rayacast.
	
	private RaycastHit hit;
	
	private float range = 5;
	
	
	//These are used to colour the beam.
	
	private Color c1 = Color.magenta;
	
    private Color c2 = Color.magenta;
	
	
	//Our beam will be a LineRenderer
	
	private LineRenderer lineRenderer;
	
	
	//For deducting resource.
	
	private PlayerResource resourceScript;
	
	private float cost = 3;
	
	
	//Variables End___________________________________________________________
	
	
	// Use this for initialization
	void Start () 
	{
		if(networkView.isMine == true)
		{	
			//Store a reference to these objects since we will need to reference
			//them frequently.
			
			myTransform = transform.parent;
			
			cameraHeadTransform = myTransform.FindChild("CameraHead");
			
			resourceScript = myTransform.GetComponent<PlayerResource>();
			
			weaponScript = myTransform.GetComponent<ChangeWeapon>();
			
			
			//Attaching a LineRenderer component which will give a visual
			//effect for the block eraser. Only the player will be able to see it.
									
			lineRenderer = gameObject.AddComponent<LineRenderer>();
			
	        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
			
	        lineRenderer.SetColors(c1, c2);
			
	        lineRenderer.SetWidth(0.04f, 0.01f);
			
	        lineRenderer.SetVertexCount(2); //Start and end position.
		}
		
		else
		{
			enabled = false;	
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Screen.lockCursor == true &&
		   weaponScript.selectedWeapon == ChangeWeapon.State.blockEraser)
		{
			//A ray is cast ahead continuously and it checks the tag of any object hit. 
			//If it is a block then a line is rendered from the player's
			//position to the point where the ray hits the block.
			
			if(Physics.Raycast(cameraHeadTransform.position, cameraHeadTransform.forward, out hit, range))
			{	
				if(hit.transform.tag == "ConstructionBlock" ||
				   hit.transform.tag == "AirBlock")
				{	
					//The start and end positions for the beam
					
					lineOrigin = cameraHeadTransform.TransformPoint(0.3f, 0, 0);
					
					hitPosition = hit.point;
					
					
					//There is a block infront of the player so draw
					//the beam.
					
					lineRenderer.enabled = true;
					      
		        	lineRenderer.SetPosition(0, lineOrigin);
					
					lineRenderer.SetPosition(1, hitPosition);	
					
					
					//If the player presses the fire button then a message is sent to the server with the
					//name of the block. The server will find the block and tell it that it has been hit.
					//Block are only removed by the server and hits on the block are only registered on the
					//server, that is why we need such an RPC.
					
					if(Input.GetButton("Fire Weapon") && Time.time > nextFire && resourceScript.resource >= cost)
					{
						nextFire = Time.time + fireRate;
						
						resourceScript.resource = resourceScript.resource - cost;
						
						
						if(hit.transform.tag == "ConstructionBlock")
						{
							//Send an RPC to the server and inform the block on the
							//server that it has been hit and needs to be removed.
							
							networkView.RPC("TellServerConstructionBlockIsHit", RPCMode.Server, hit.transform.name);
						}
						
						
						if(hit.transform.tag == "AirBlock")
						{
							//Send an RPC to the server and inform the block on the
							//server that it has been hit and needs to be removed.
							
							networkView.RPC("TellServerAirBlockIsHit", RPCMode.Server, hit.transform.name);
						}
					}
				}
				
				else
				{	
					//If the tag of the object hit is not a block then disable the line renderer.
					
					lineRenderer.enabled = false;	
				}
			}
			
			else
			{
				//If the ray doesn't hit anything then disable the line renderer.
				
				lineRenderer.enabled = false;	
			}
		}
		
		else
		{
			//If the cursor is not locked then disable the line renderer.
			
			lineRenderer.enabled = false;
		}
	}
	
	
	[RPC]
	void TellServerConstructionBlockIsHit (string blockName)
	{
		GameObject block = GameObject.Find(blockName);
		
		ConstructionBlockAsksToRemoveItself script = block.transform.GetComponent<ConstructionBlockAsksToRemoveItself>();
				
		script.iAmHit = true;
	}
	
	
	[RPC]
	void TellServerAirBlockIsHit (string blockName)
	{
		GameObject block = GameObject.Find(blockName);
		
		AirBlockAsksToRemoveItself script = block.transform.GetComponent<AirBlockAsksToRemoveItself>();
				
		script.iAmHit = true;
	}
}