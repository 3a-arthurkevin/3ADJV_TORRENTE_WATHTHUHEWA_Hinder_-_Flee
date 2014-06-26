using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the player and it governs their energy.
/// 
/// This script is accessed by the StatDisplay script.
/// 
/// This script is accessed by the FireBlaster script.
/// 
/// This script is accessed by the Blink script.
/// </summary>

public class PlayerEnergy : MonoBehaviour {
	
	//Variables Start___________________________________
	
	public float energy;
	
	public float baseEnergy = 100;
	
	private float rechargeRate = 20;	
	
	//Variables End_____________________________________	

	// Use this for initialization
	void Start () 
	{
		if(networkView.isMine == true)
		{
			energy = baseEnergy;	
		}
		
		else
		{
			enabled = false;	
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		//If the player's energy falls below their base energy
		//then start recharging it.
		
		if(energy < baseEnergy)
		{
			energy = energy + rechargeRate * Time.deltaTime;	
		}
		
		
		//Prevent the player's energy from exceeding the baseEnergy or falling below 0.
		
		if(energy > baseEnergy)
		{
			energy = baseEnergy;	
		}
		
		if(energy < 0)
		{
			energy = 0;	
		}
	}
	
	
	
	
	
	
	
	
	
	
	
}
