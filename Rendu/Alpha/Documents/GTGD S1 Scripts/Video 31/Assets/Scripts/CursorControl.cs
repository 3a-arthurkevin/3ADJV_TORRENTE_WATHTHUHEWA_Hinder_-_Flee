using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the player and it controls
/// whether the cursor is locked or unlocked.
/// 
/// This script accesses the Multiplayer script.
/// 
/// This script accesses the CommunicationWindow script.
/// 
/// This script accesses the ScoreTable script.
/// 
/// This script accesses the GameSettings script.
/// </summary>


public class CursorControl : MonoBehaviour {
	
	//Variables Start___________________________________
	
	private GameObject multiplayerManager;
	
	private MultiplayerScript multiScript;
	
	
	private GameObject gameManager;
	
	private CommunicationWindow commScript;
	
	private ScoreTable scoreScript;
	
	private GameSettings settingsScript;
	
	//Variables End_____________________________________
	
	// Use this for initialization
	void Start () 
	{
		if(networkView.isMine == true)
		{
			multiplayerManager = GameObject.Find("MultiplayerManager");	
			
			multiScript = multiplayerManager.GetComponent<MultiplayerScript>();
			
			
			gameManager = GameObject.Find("GameManager");
			
			commScript = gameManager.GetComponent<CommunicationWindow>();
			
			scoreScript = gameManager.GetComponent<ScoreTable>();
			
			settingsScript = gameManager.GetComponent<GameSettings>();
		}
		
		else
		{
			enabled = false;	
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(multiScript.showDisconnectWindow == false &&
		   commScript.unlockCursor == false &&
		   scoreScript.blueTeamHasWon == false &&
		   scoreScript.redTeamHasWon == false &&
		   settingsScript.showSettings == false)
		{
			Screen.lockCursor = true;	
		}
		
		if(multiScript.showDisconnectWindow == true ||
		   commScript.unlockCursor == true ||
		   scoreScript.blueTeamHasWon == true ||
		   scoreScript.redTeamHasWon == true ||
		   settingsScript.showSettings == true)
		{
			Screen.lockCursor = false;	
		}
	}
}
