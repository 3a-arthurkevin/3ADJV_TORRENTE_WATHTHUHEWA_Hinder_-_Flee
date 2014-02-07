using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the GameManager and its
/// purpose is to display the Combat Log.
/// 
/// The HealthAndDamage script accesses this script.
/// </summary>


public class CombatWindow : MonoBehaviour {
	
	//Variables Start___________________________________
	
	//These variables are affected by the HealthAndDamage
	//script.
	
	public string attackerName;
	
	public string destroyedName;
	
	public bool addNewEntry = false;
	
	
	//The string displayed in the combat log.
	
	private string combatLog;
	
	
	//The size limit for the combat log.
	
	private int characterLimit = 10000;
	
	
	//Used in defining the combat log window.
	
	public Rect windowRect;
	
	private int windowLeft = 10;
	
	private int windowTop = 150;
	
	private int windowWidth = 300;
	
	private int windowHeight = 150;
	
	private GUIStyle myStyle = new GUIStyle();
	
	
	//Used in scrolling the combat log entries.
	
	private float nextScrollTime = 0;
	
	private float scrollRate = 12;
	
	
	//Variables End_____________________________________

	// Use this for initialization
	void Start () 
	{
		myStyle.fontStyle = FontStyle.Bold;
		
		myStyle.fontSize = 11;
		
		myStyle.normal.textColor = Color.green;
		
		myStyle.wordWrap = true;
	}
	
	
	void CombatWindowFunction (int windowID)
	{
		GUILayout.Label(combatLog, myStyle);	
	}
	
	
	void OnGUI ()
	{
		//Run this code for both the server and the client.
		
		if(Network.peerType != NetworkPeerType.Disconnected)
		{
			windowTop = Screen.height - 350;
			
			windowRect = new Rect(windowLeft, windowTop, windowWidth, windowHeight);
			
			
			//If a player has been destroyed then addNewEntry would have been
			//set to true by the HealthAndDamage script.
			
			if(addNewEntry == true)
			{
				//The string combatLog continues to expand while its length is less
				//than the character limit. The latest attacker and destroyed names are
				//concatenated and the previous lines are pushed down a line.
				
				if(combatLog.Length < characterLimit)
				{
					combatLog = attackerName + " >>> " + destroyedName + "\n" + combatLog;	
					
					
					//A time for when the contents in the combat log should shift down a line.
					
					nextScrollTime = Time.time + scrollRate;
					
					
					addNewEntry = false;
				}
				
				
				//Reset combatLog to stop it from getting too large.
				
				if(combatLog.Length > characterLimit)
				{
					combatLog = attackerName + " >>> " + destroyedName;	 	
				}	
			}
			
			
			windowRect = GUI.Window(4, windowRect, CombatWindowFunction, "Combat Log");
			
			
			//Creates the effect of the combat log scrolling down every few seconds if 
			//no combat destruction has happened recently.
			
			if(Time.time > nextScrollTime && addNewEntry == false)
			{
				combatLog = "\n" + combatLog;
				
				nextScrollTime = Time.time + scrollRate;
			}
		}
		
	}

	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
}
