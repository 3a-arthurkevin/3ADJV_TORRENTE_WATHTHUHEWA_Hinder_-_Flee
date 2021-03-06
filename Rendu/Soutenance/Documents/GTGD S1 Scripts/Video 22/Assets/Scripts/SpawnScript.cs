using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the SpawnManager and it allows
/// the player to spawn into the multiplayer game.
/// 
/// This script accesses the PlayerDatabase script to supply the team
/// that the player belongs to.
/// 
/// This script is accessed by the FireBlaster script in determining
/// which team the player is on.
/// 
/// This script is accessed by the HealthAndDamage script.
/// 
/// This script is accessed by AssignHealth script to see if firstSpawn
/// is true.
/// 
/// This script is accessed by the ReloadLevelScript.
/// 
/// This script accesses the PlayerStats script to make use of the Stats
/// function in allowing the player to choose their stats.
/// </summary>


public class SpawnScript : MonoBehaviour {
	
	//Variables Start___________________________________
	
	//Used to determine if the palyer needs to spawn into
	//the game.
	
	private bool justConnectedToServer = false;
	
	
	//Used to determine which team the player is on.
	
	public bool amIOnTheRedTeam = false;
	
	public bool amIOnTheBlueTeam = false;
	
	
	//Used to define the JoinTeamWindow.
	
	private Rect joinTeamRect;
	
	private string joinTeamWindowTitle = "Team Selection";
	
	private int joinTeamWindowWidth = 330;
	
	private int joinTeamWindowHeight = 370;
	
	private int joinTeamLeftIndent;
	
	private int joinTeamTopIndent;
	
	private int buttonHeight = 40;
	
	
	//The Player prefabs are connected to these in the 
	//inspector
	
	public Transform redTeamPlayer;
	
	public Transform blueTeamPlayer;
	
	private int redTeamGroup = 0;
	
	private int blueTeamGroup = 1;
	
	
	//Used to capture spawn points.
	
	private GameObject[] redSpawnPoints;
	
	private GameObject[] blueSpawnPoints;
	
	
	//Used in determining whether the player is destroyed.
	
	public bool iAmDestroyed = false;
	
	
	//Used in determining if the player has spawned for the
	//first time.
	
	public bool firstSpawn = false;
	
	
	//This is used in allowing the player to select a team again
	//if the match has restarted.
	
	public bool matchRestart = false;

	//Variables End_____________________________________
	
	
	
	void OnConnectedToServer ()
	{
		justConnectedToServer = true;	
	}
	
	
	
	void JoinTeamWindow (int windowID)
	{	
		//Run the Stats function from the PlayerStats script.
		
		GameObject gameManager = GameObject.Find("GameManager");
		
		PlayerStats statScript = gameManager.GetComponent<PlayerStats>();
		
		statScript.Stats();
		
		
		//Only show these two buttons when the player has just connected to
		//the server or if the match has restarted. They allow the player
		//choose a team and spawn into the game.
		
		
		if(justConnectedToServer == true || matchRestart == true)
		{
			//If the player clicks on the Join Red Team button then
			//assign them to the red team and spawn them into the game.
			
			if(GUILayout.Button("Join Red Team", GUILayout.Height(buttonHeight)))
			{
				amIOnTheRedTeam = true;
				
				justConnectedToServer = false;
				
				matchRestart = false;
				
				SpawnRedTeamPlayer();
				
				firstSpawn = true;
			}
			
			
			//If the player clicks on the Join Blue Team button then
			//assign them to the blue team and spawn them into the game.
			
			if(GUILayout.Button("Join Blue Team", GUILayout.Height(buttonHeight)))
			{
				amIOnTheBlueTeam = true;
				
				justConnectedToServer = false;
				
				matchRestart = false;
				
				SpawnBlueTeamPlayer();
				
				firstSpawn = true;
			}
		}
		
		//Allow the player to respawn if they were destroyed.
		
		if(iAmDestroyed == true)
		{
			if(GUILayout.Button("Respawn", GUILayout.Height(buttonHeight * 2)))
			{
				if(amIOnTheRedTeam == true)
				{
					SpawnRedTeamPlayer();	
				}
				
				if(amIOnTheBlueTeam == true)
				{
					SpawnBlueTeamPlayer();	
				}
				
				iAmDestroyed = false;
			}
		}
		
	}
	
	
	void OnGUI()
	{
		//If the player has just connected to the server then draw the 
		//Join Team window.
		
		if(justConnectedToServer == true || iAmDestroyed == true || matchRestart == true
		   && Network.isClient)
		{	
			Screen.lockCursor = false;
			
			joinTeamLeftIndent = Screen.width / 2 - joinTeamWindowWidth / 2;
			
			joinTeamTopIndent = Screen.height / 2 - joinTeamWindowHeight / 2;
			
			joinTeamRect = new Rect(joinTeamLeftIndent, joinTeamTopIndent,
			                        joinTeamWindowWidth, joinTeamWindowHeight);
			
			joinTeamRect = GUILayout.Window(0, joinTeamRect, JoinTeamWindow,
			                                joinTeamWindowTitle);
		}
	}
	
	
	void SpawnRedTeamPlayer ()
	{
		//Find all red spawn points and place a reference to them in the array
		//redSpawnPoints.
		
		redSpawnPoints = GameObject.FindGameObjectsWithTag("SpawnRedTeam");
		
		
		//Randomly select one of those spawn points.
		
		GameObject randomRedSpawn = redSpawnPoints[Random.Range(0, redSpawnPoints.Length)];
		
		
		//Instantiate the player at the randomly selected spawn point.
		
		Network.Instantiate(redTeamPlayer, randomRedSpawn.transform.position,
		                    randomRedSpawn.transform.rotation, redTeamGroup);
		
		
		//Access the PlayerDatabase and supply it with the team that this player
		//has joined.
		
		GameObject gameManager = GameObject.Find("GameManager");
		
		PlayerDatabase dataScript = gameManager.GetComponent<PlayerDatabase>();
		
		dataScript.joinedTeam = true;
		
		dataScript.playerTeam = "red";
	}
	
	
	
	void SpawnBlueTeamPlayer ()
	{
		//Find all blue spawn points and place a reference to them in the array
		//blueSpawnPoints.
		
		blueSpawnPoints = GameObject.FindGameObjectsWithTag("SpawnBlueTeam");
		
		
		//Randomly select one of those spawn points.
		
		GameObject randomBlueSpawn = blueSpawnPoints[Random.Range(0, blueSpawnPoints.Length)];
		
		
		//Instantiate the player at the randomly selected spawn point.
		
		Network.Instantiate(blueTeamPlayer, randomBlueSpawn.transform.position,
		                    randomBlueSpawn.transform.rotation, blueTeamGroup);
		
		
		//Access the PlayerDatabase and supply it with the team that this player
		//has joined.
		
		GameObject gameManager = GameObject.Find("GameManager");
		
		PlayerDatabase dataScript = gameManager.GetComponent<PlayerDatabase>();
		
		dataScript.joinedTeam = true;
		
		dataScript.playerTeam = "blue";
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
}
