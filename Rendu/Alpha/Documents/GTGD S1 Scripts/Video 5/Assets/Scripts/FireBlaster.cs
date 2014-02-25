using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the player and allows
/// them to fire the Blaster projectile.
/// </summary>

public class FireBlaster : MonoBehaviour {
	
	//Variables Start___________________________________
	
	//The blaster projectile is attached to this in the 
	//inspector
	
	public GameObject blaster;
	
	
	//Quick references.
	
	private Transform myTransform;
	
	private Transform cameraHeadTransform;
	
	
	//The position at which the projectile should be
	//instantiated.
	
	private Vector3 launchPosition = new Vector3();
	
	
	//Used to control the rate of fire.
	
	private float fireRate = 0.2f;
	
	private float nextFire = 0;
	
	//Variables End_____________________________________
	

	// Use this for initialization
	void Start () 
	{
		if(networkView.isMine == true)
		{
		
			myTransform = transform;
			
			cameraHeadTransform = myTransform.FindChild("CameraHead");
		}
		
		else
		{
			enabled = false;
		}	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButton("Fire Weapon") && Time.time > nextFire && Screen.lockCursor == true)
		{	
			nextFire = Time.time + fireRate;
			
			//The launch position of the projectile will be just in front
			//of the CameraHead.
			
			launchPosition = cameraHeadTransform.TransformPoint(0, 0, 0.2f);	
			
			
			//Create the blaster projectile across the network 
			//at the launchPosition and tilt its angle
			//so that its horizontal using the angle eulerAngles.x + 90.
			
			networkView.RPC("SpawnProjectile", RPCMode.All,launchPosition,
			                Quaternion.Euler(cameraHeadTransform.eulerAngles.x + 90,
			                                                    myTransform.eulerAngles.y, 0));
		}
	}
	
	
	[RPC]
	void SpawnProjectile (Vector3 position, Quaternion rotation)
	{
		Instantiate(blaster, position, rotation);	
	}
}












