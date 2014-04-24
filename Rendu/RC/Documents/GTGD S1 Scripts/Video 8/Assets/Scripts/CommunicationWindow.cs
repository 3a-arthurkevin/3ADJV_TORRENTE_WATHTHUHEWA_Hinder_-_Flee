using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the GameManager.
/// 
/// This script accesses the SpawnScript to check if the
/// player has joined a team.
/// 
/// This script is accessed by the PlayerName script.
/// 
/// This script is accessed by the CursorControl script.
/// </summary>


public class CommunicationWindow : MonoBehaviour {
	
	//Variables Start___________________________________
	
	//This is supplied by the PlayerName script.
	
	public string playerName;
	
	
	//These are used in sending a message.
	
	private string messageToSend;
	
	private string communication;
	
	private bool showTextBox = false;
	
	private bool sendMessage = false;
	
	public bool unlockCursor = false;
	
	
	//These are used to define the communication window.
	
	private Rect windowRect;
	
	private int windowLeft = 10;
	
	private int windowTop;
	
	private int windowWidth = 300;
	
	private int windowHeight = 140;
	
	private int padding = 20;
	
	private int textFieldHeight = 30;
	
	private Vector2 scrollPosition;
	
	private GUIStyle myStyle = new GUIStyle();
	
	
	//Quick references
	
	private GameObject spawnManager;
	
	private SpawnScript spawnScript;
	
	
	//Used in informing all the other players that a new
	//player has joined.
	
	public bool tellEveryoneIJoined = true;
	
	
	//Variables End_____________________________________	
	
	
	void Awake ()
	{
		//Allow my pressing the Return key to be recognised
		//when using the textfield.
		
		Input.eatKeyPressOnTextFieldFocus = false;
		
		
		messageToSend = "";
		
		
		myStyle.normal.textColor = Color.white;
		
		myStyle.wordWrap = true;
		
	}
	
	
	
	// Use this for initialization
	void Start () 
	{
		spawnManager = GameObject.Find("SpawnManager");
		
		spawnScript = spawnManager.GetComponent<SpawnScript>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Network.peerType != NetworkPeerType.Disconnected)
		{
			//If the player presses the T key then set showTextBox to 
			//true. This will bring up the textfield that will allow
			//the player to type in messages.
			
			if(Input.GetButtonDown("Communication") && showTextBox == false)
			{
				showTextBox = true;	
			}
			
			
			//If the player presses Return and the textfield is visible then
			//set sendMessage to true.
			
			if(Input.GetButtonDown("Send Message") && showTextBox == true)
			{
				sendMessage = true;	
			}
		}
		
		//When the player joins for the first time tell everyone that they have
		//just joined and only send an RPC if it is a client and the playerName is
		//not an empty string.
		
		if(Network.isClient && tellEveryoneIJoined == true && playerName != "")
		{
			networkView.RPC("TellEveryonePlayerJoined", RPCMode.All, playerName);
			
			tellEveryoneIJoined = false;
		}
	}
	
	
	void CommLogWindow (int windowID)
	{
		//Begin a scroll view so that as the label increases with
		//length the scroll bar will appear and allow the player
		//to view past messages.
		
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, 
		                                           GUILayout.Width(windowWidth - padding),
		                                           GUILayout.Height(windowHeight - padding - 5));
		
		GUILayout.Label(communication, myStyle);
		
		GUILayout.EndScrollView();
		
	}
	
	
	void OnGUI ()
	{
		if(Network.peerType != NetworkPeerType.Disconnected)
		{
			windowTop = Screen.height - windowHeight - textFieldHeight;
			
			windowRect = new Rect(windowLeft, windowTop, windowWidth, windowHeight);
			
			
			//Don't display the communication log until the player has joined a team,
			//or it is the server, and don't allow them to send messages either.
			
			if(spawnScript.amIOnTheRedTeam == true || spawnScript.amIOnTheBlueTeam == true ||
			   Network.isServer == true)
			{
				windowRect = GUI.Window(5, windowRect, CommLogWindow, "Communication Log");
				
				GUILayout.BeginArea(new Rect(windowLeft, windowTop + windowHeight, windowWidth,
				                             windowHeight));
				
				
				//If showTextBox is true then show the textfield that will allow players to
				//type messages.
				
				if(showTextBox == true)
				{
					unlockCursor = true;
					
					Screen.lockCursor = false;
					
					
					//Give the textfield a name so that it can be found with
					//the GUI.FocusControl method.
					
					GUI.SetNextControlName("MyTextField");
					
					messageToSend = GUILayout.TextField(messageToSend, GUILayout.Width(windowWidth));
					
					
					//Give focus to the textfield so the user can immediately start typing without
					//having to click their mouse cursor on the textfield.
					
					GUI.FocusControl("MyTextField");
					
					
					if(sendMessage == true)
					{
						//Only send a message if the textfield isn't empty.
						//If the textfield is empty and the user presses return
						//then that means that they don't want to send a message
						//and the textfield should no longer display.
						
						if(messageToSend != "")
						{
							if(Network.isClient == true)
							{
								networkView.RPC("SendMessageToEveryone", RPCMode.All, messageToSend, playerName);
							}
							
							if(Network.isServer == true)
							{
								networkView.RPC("SendMessageToEveryone", RPCMode.All, messageToSend, "Server");
							}
						}
						
						//Set sendMessage to false so that the message doesn't continue
						//to send after having pressed Return.
						
						sendMessage = false;
						
						
						//Set showTextBox to false so that the textfield is hidden.
						
						showTextBox = false;
						
						unlockCursor = false;
						
						
						//Reset messageToSend so that the user doesn't
						//have to manually delete the text when they
						//bring up the textfield again.
						
						messageToSend = "";
						
					}
				}
				                             
				GUILayout.EndArea();
			}
				
		}
		
	}
	
	
	[RPC]
	void SendMessageToEveryone (string message, string pName)
	{
		//This string is displayed by the label in the 
		//CommLogWindow.
		
		communication = pName + " : " + message + "\n" + "\n" + communication;
	}
	
	
	[RPC]
	void TellEveryonePlayerJoined (string pName)
	{
		communication = pName + " has joined the game." + "\n" + "\n" + communication;	
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
}
