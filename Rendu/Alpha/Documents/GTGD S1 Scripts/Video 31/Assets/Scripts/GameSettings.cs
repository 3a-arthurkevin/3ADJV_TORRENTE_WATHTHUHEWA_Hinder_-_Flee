using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the GameManager.
/// 
/// This script is accessed by the following scripts for the 
/// water height value:
/// WaterBehaviour
/// WaterEffect
/// ServerRemovesConstructionBlock 
/// ServerRemovesAirBlock
/// RocketScript
///  
/// This script accesses the SetVolume script on the Main Camera.
/// 
/// This script accesses the SpawnScript to determine if the player
/// has spawned into the game.
/// 
/// This script accesses the MouseLook scripts on the player.
/// 
/// This script is accessed by the MultiplayerManagment script for turning
/// the settings window on.
/// 
/// This script is accessed by the CursorControl script.
/// </summary>

public class GameSettings : MonoBehaviour {
	
	//Variables Start_________________________________________________________
	
	public float waterHeight = 14;
	
	
	public bool showSettings = false;
	
	
	private GameObject spawnManager;
	
	private SpawnScript spawnScript;
	
	private GameObject cameraMain;
	
	private SetVolume volumeScript;
	
	private PlayerDatabase pDataScript;
	
	
	private Rect windowRect;
	
	public float volumeSlider;
	
	public float mouseSlider;

	
	public float volume;
	
	private float maxVolume = 1.0f;
		
	private float minVolume = 0.0f;	
	
	
	public float mouseSensitivity;
	
	private float maxMouseSensitivity = 25;
	
	private float minMouseSensitivity = 1;
	
	
	
	private float windowHeight = 150;
			
	private float windowWidth = 320;
	
	private float controlHeight = 25;
	
	private float labelWidth = 150;
	
		
	private GUIStyle plainStyle = new GUIStyle();

	
	//Variables End___________________________________________________________
	
	

	// Use this for initialization
	void Start () 
	{
		volume = PlayerPrefs.GetFloat("volume");
		
		if(volume == 0)
		{
			volume = 0.5f;
		}
		
		//Access the SetVolume script on the Mian Camera
		//and apply the volume.
		
		cameraMain = GameObject.Find("Main Camera");
		
		volumeScript = cameraMain.GetComponent<SetVolume>();
		
		volumeScript.volume = volume;
		
		volumeScript.setVolume = true;
		
		
		//Retrieve the mouse sensitivity value from PlayerPrefs.
		
		mouseSensitivity = PlayerPrefs.GetFloat("mouse sensitivity");
		
		if(mouseSensitivity == 0)
		{
			mouseSensitivity = 10;	
		}
				
		
		spawnManager = GameObject.Find("SpawnManager");
		
		spawnScript = spawnManager.GetComponent<SpawnScript>();
		
		pDataScript = transform.GetComponent<PlayerDatabase>();
		
		plainStyle.alignment = TextAnchor.MiddleLeft;
		
		plainStyle.normal.textColor = Color.white;
		
		plainStyle.fontStyle = FontStyle.Bold;

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonDown("Settings") && Screen.lockCursor == true)
		{
			//Set the slider values to the actual values.
			
			volumeSlider = volume;
			
			mouseSlider = mouseSensitivity;
			
			showSettings = true;
		}
		
		
		//Close the Settings window if the player presses the settings button again.
		
		if(Input.GetButtonDown("Settings") && Screen.lockCursor == false)
		{
			showSettings = false;	
		}
		
		
		//Close the Settings window if the player wants to access the Disconnect
		//window.
		
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			showSettings = false;	
		}
	}
	
	
	
	void SettingsWindow(int windowID)
	{	
		//The volume slider.
		
		GUILayout.BeginHorizontal();
		
		GUILayout.Label("Volume", plainStyle, GUILayout.Width(labelWidth));
		
		volumeSlider = GUILayout.HorizontalSlider(volumeSlider, minVolume, maxVolume);
		
		GUILayout.EndHorizontal();
		
		
		GUILayout.Space(15);
		
		
		//The mouse sensitivity slider.
		
		GUILayout.BeginHorizontal();
		
		GUILayout.Label("Mouse Sensitivity", plainStyle, GUILayout.Width(labelWidth));
		
		mouseSlider = GUILayout.HorizontalSlider(mouseSlider, minMouseSensitivity, maxMouseSensitivity);
		
		GUILayout.EndHorizontal();	
		

		
		GUILayout.Space(15);
		
		
		
		if(GUILayout.Button("Save Settings", GUILayout.Height(controlHeight)))
		{	
			//Save the volume setting to PlayerPrefs.
			
			volume = volumeSlider;
			
			PlayerPrefs.SetFloat("volume", volume);	
			
			
			//Access the volume script and apply the volume setting.
		
			volumeScript.volume = volume;
			
			volumeScript.setVolume = true;
			
			
			//Save the mouse sensitivity to PlayerPrefs.
			
			mouseSensitivity = mouseSlider;
			
			PlayerPrefs.SetFloat("mouse sensitivity", mouseSensitivity);
			

					
			//Only attempt to find the player and change the mouse
			//sensitivity of its MouseLook scripts if the player is
			//currently in the game.
			
			if(spawnScript.amIOnTheBlueTeam == true ||
				spawnScript.amIOnTheRedTeam == true &&
				spawnScript.iAmDestroyed == false)
			{	
				//Access the MouseLook scripts on the player (the player is
				//fouund by their Network player ID) and apply the mouse
				//sensitivity and tell the MouseLook script to apply those values.
				
				for(int i = 0; i < pDataScript.PlayerList.Count; i++)
				{
					if(int.Parse(Network.player.ToString()) == pDataScript.PlayerList[i].networkPlayer)
					{	
						
								
						string playerName = pDataScript.PlayerList[i].playerName;
						
						
						//Set the mouse sensitivity X. It is in the MouseLook script
						//attached to the main player prefab.
						
						GameObject playerObj = GameObject.Find(playerName);
						
						MouseLook mouseXScript = playerObj.GetComponent<MouseLook>();
						
						mouseXScript.sensitivityX = mouseSensitivity;
						
						
						//Set the mouse sensitivity Y. It is in the MouseLook script
						//attached to the CameraHead.
						
						Transform cameraHead = playerObj.transform.FindChild("CameraHead");
						
						MouseLook mouseYScript = cameraHead.GetComponent<MouseLook>();
						
						mouseYScript.sensitivityY = mouseSensitivity;
						
					}
				}
			}
			
			//Close the Settings window.
			
			showSettings = false;
		}
		
		if(GUILayout.Button("Cancel", GUILayout.Height(controlHeight)))
		{	
			//Return the slider values back to the actual values
			//and close the Settings window.
			
			volumeSlider = volume;
			
			mouseSlider = mouseSensitivity;
			
			showSettings = false;
		}
	}
	
	
	void OnGUI ()
	{	
		//Show the Settings window in the middle of the screen
		
		if(showSettings == true)
		{
			float windowLeft = Screen.width / 2 - windowWidth / 2;
			
			float windowTop = Screen.height / 2 - windowHeight / 2;
			
			
			windowRect = new Rect(windowLeft, windowTop, windowWidth, windowHeight);
			
			windowRect = GUILayout.Window(15, windowRect, SettingsWindow, "Settings");
		}
	}


}
