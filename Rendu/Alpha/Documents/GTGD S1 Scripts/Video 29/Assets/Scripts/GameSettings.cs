using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the GameManager.
/// 
/// This script is accessed by the following scripts for the water
/// height value:
/// WaterBehaviour
/// WaterEffect
/// ServerRemovesConstructionBlock
/// ServerRemovesAirBlock
/// RocketScript
/// </summary>

public class GameSettings : MonoBehaviour {
	
	//Variables Start_____________________________________
	
	public float waterHeight = 14;
	
	//Variables End________________________________________

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
