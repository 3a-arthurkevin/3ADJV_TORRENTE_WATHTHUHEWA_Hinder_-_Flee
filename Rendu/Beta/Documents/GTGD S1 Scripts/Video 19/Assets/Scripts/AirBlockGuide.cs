using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the player and it causes the
/// AirBlockGuide GameObject to appear wherever the 
/// player can place an AirBlock.
/// 
/// This script accesses the ChangeBuildOption script.
/// </summary>

public class AirBlockGuide : MonoBehaviour {
	
	//Variables Start_________________________________________________________	
	
		
	//Used for the virtual grid.
	
	private float gridWidth = 0.6f;
	
	private float posY;
	
	private float range = 1.2f;
	
	private float maxRange = 24f;

	
	//cached items.
	
	private Transform myTransform;
	
	private Transform myCameraHeadTransform;
	
	private GameObject myself;
	
	private GameObject airGuideBlock;
	
	private ChangeBuildOption buildOptionScript;	
	
	//Variables End___________________________________________________________
	
	
	// Use this for initialization
	void Start () 
	{
		if(networkView.isMine == true)
		{	
			myTransform = transform;
		
			myCameraHeadTransform = myTransform.FindChild("CameraHead");	
			
			airGuideBlock = GameObject.Find("AirBlockGuide");
			
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
		if(Screen.lockCursor == true && buildOptionScript.buildOption == ChangeBuildOption.State.airBlock)
		{
			Vector3 position = myCameraHeadTransform.TransformPoint(0, 0, range);
				
				
			position /= gridWidth;
							
			position = new Vector3(Mathf.Round(position.x), Mathf.Round(position.y), Mathf.Round(position.z));
			
			if(myCameraHeadTransform.eulerAngles.x >= 90)
			{			
				posY = position.y + 0.5f;	
			}
			
			if(myCameraHeadTransform.eulerAngles.x < 90)
			{
				posY = position.y - 0.5f;	 
			}
			
			position = new Vector3(position.x, posY, position.z);
										
			position *= gridWidth;
			
			
			Vector3 checkPos = new Vector3(position.x, position.y, position.z);
			
			
			
			if(!Physics.Linecast(myCameraHeadTransform.position, checkPos) &&
			   	Vector3.Distance(transform.position, position) > gridWidth)
			{
				airGuideBlock.transform.position = position;
						
				airGuideBlock.transform.rotation = Quaternion.identity;
			
				airGuideBlock.renderer.enabled = true;
			}	
			
			else
			{
				airGuideBlock.renderer.enabled = false;	
			}
		}
	
		else
		{
			airGuideBlock.renderer.enabled = false;	
		}
		
		
		//Change the range if the player uses the scroll wheel.	
	
		if(buildOptionScript.buildOption == ChangeBuildOption.State.airBlock)
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
}
