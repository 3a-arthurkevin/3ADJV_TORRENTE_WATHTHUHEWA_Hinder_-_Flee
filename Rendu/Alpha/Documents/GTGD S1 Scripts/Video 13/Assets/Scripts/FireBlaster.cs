using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the player and allows
/// them to fire the Blaster projectile.
/// 
/// This script accesses the SpawnScript.
/// 
/// This script accesses the BlasterScript of a newly
/// instantiated blaster projectile.
/// 
/// This script accesses the PlayerEnergy script.
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
	
	
	//Used in determining which team the player is on.
	
	private bool iAmOnTheRedTeam = false;
	
	private bool iAmOnTheBlueTeam = false;
	
	
	//Used in affecting the player's energy.
	
	private PlayerEnergy energyScript;
		
	private float energyCost = 10;
	
	//Variables End_____________________________________
	

	// Use this for initialization
	void Start () 
	{
		if(networkView.isMine == true)
		{
		
			myTransform = transform;
			
			cameraHeadTransform = myTransform.FindChild("CameraHead");
			
			
			//Find the SpawnManager and access the SpawnScript to
			//find out which team the player is on.
			
			GameObject spawnManager = GameObject.Find("SpawnManager");
			
			SpawnScript spawnScript = spawnManager.GetComponent<SpawnScript>();
			
			if(spawnScript.amIOnTheRedTeam == true)
			{
				iAmOnTheRedTeam = true;	
			}
			
			if(spawnScript.amIOnTheBlueTeam == true)
			{
				iAmOnTheBlueTeam = true;	
			}
			
			
			//Access the PlayerEnergy script.
			
			energyScript = myTransform.GetComponent<PlayerEnergy>();
		}
		
		else
		{
			enabled = false;
		}	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButton("Fire Weapon") && Time.time > nextFire && Screen.lockCursor == true &&
		   energyScript.energy >= energyCost)
		{	
			nextFire = Time.time + fireRate;
			
			//Drain the player's energy.
			
			energyScript.energy = energyScript.energy - energyCost;			
			
			
			//The launch position of the projectile will be just in front
			//of the CameraHead.
			
			launchPosition = cameraHeadTransform.TransformPoint(0, 0, 0.2f);	
			
			
			//Create the blaster projectile across the network 
			//at the launchPosition and tilt its angle
			//so that its horizontal using the angle eulerAngles.x + 90.
			//Also make it team specific.
			
			if(iAmOnTheRedTeam == true)
			{
				networkView.RPC("SpawnProjectile", RPCMode.All,launchPosition,
			                Quaternion.Euler(cameraHeadTransform.eulerAngles.x + 90,
			                                                    myTransform.eulerAngles.y, 0),
				                myTransform.name, "red");
			}
			
			
			if(iAmOnTheBlueTeam == true)
			{
				networkView.RPC("SpawnProjectile", RPCMode.All,launchPosition,
			                Quaternion.Euler(cameraHeadTransform.eulerAngles.x + 90,
			                                                    myTransform.eulerAngles.y, 0),
				                myTransform.name, "blue");
			}
			
		}
	}
	
	
	[RPC]
	void SpawnProjectile (Vector3 position, Quaternion rotation, 
	                      string originatorName, string team)
	{
		//Access the BlasterScript of the newly instantiated blaster
		//projectile and supply the player's name and team.
		
		GameObject go = Instantiate(blaster, position, rotation) as GameObject;
		
		BlasterScript bScript = go.GetComponent<BlasterScript>();
		
		bScript.myOriginator = originatorName;
		
		bScript.team = team;
	}
}












