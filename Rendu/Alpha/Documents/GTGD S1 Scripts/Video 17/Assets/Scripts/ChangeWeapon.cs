using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This script is attached to each player character and allows the player
/// to change their currently selected weapon.
/// 
/// The FireBlaster script accesses this script to determine if it is the 
/// currently selected weapon.
/// 
/// The FireBlockEraser script accesses this script to determine if it is the
/// currently selected weapon.
/// </summary>

public class ChangeWeapon : MonoBehaviour {
	
	//Variables Start_________________________________________________________	
	
	//The names of the weapon types that will
	//be available to the player.
	
	public enum State
	{
		blaster,
		
		blockEraser
	}
	
	
	//selectedWeapon will store the currently selected
	//weapon.
	
	public State selectedWeapon = State.blaster;
	
	
	//Used for showing the currently selected weapon.
	
	private Rect weaponRect;
	
	private int weaponLeft = 310;
	
	private int weaponTop = 150;
	
	private int weaponHeight = 140;
	
	private int weaponWidth = 140;
	
	private int labelWidth = 128;
	
	private int labelHeight = 128;
	

	public Texture blasterIcon;

	public Texture blockEraserIcon;
	
	
	private GUIStyle weaponStyle = new GUIStyle();

	
	private int padding = 20;
	
	
	//Used in cycling though weapons for quick selection.
	
	private int selectedWeaponNumber = 0;
	
	
	//This list is used to store all the weapon states and will
	//be used for quick selection of a weapon.
	
	public List<ChangeWeapon.State> weaponList = new List<ChangeWeapon.State>();
	
	
	//Variables End___________________________________________________________
	
	
	
	// Use this for initialization
	void Start () 
	{
		if(networkView.isMine == true)
		{
			weaponStyle.normal.textColor = Color.white;
				
			weaponStyle.fontStyle = FontStyle.Bold;	
			
			weaponStyle.wordWrap = true;
			
			weaponStyle.alignment = TextAnchor.MiddleLeft;
			
			
			//Add each of the weapons to the weaponList. This list
			//will be used for quick selection of weapons.
			
			weaponList.Add(State.blaster);
			
			weaponList.Add(State.blockEraser);
		}
		
		else
		{
			enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		//The player can quickly change weapons using the Change Weapon button.
			
		if(Input.GetButtonDown("Change Weapon"))
		{	
			selectedWeaponNumber ++;
			
			//Remember that the list index starts from 0 but the count
			//starts from 1. So if the list had 4 items in it and
			//selectedWeaponNumber were 4 then selectedWeaponNumber must
			//be set to 0 because there is no element 4 in the list since it
			//starts from 0.
			
			if(selectedWeaponNumber == weaponList.Count)
			{
				selectedWeaponNumber = 0;	
			}
			
			selectedWeapon = weaponList[selectedWeaponNumber];
		}
	}
	
	
	
	//Contents of the window that will display the currently selected weapon.
	
	void selectedWeaponWindow(int windowID)
	{
		if(selectedWeapon == ChangeWeapon.State.blaster)
		{
			GUILayout.Label(blasterIcon, weaponStyle, GUILayout.Width(labelWidth), GUILayout.Height(labelHeight));
		}
		
		
		if(selectedWeapon == ChangeWeapon.State.blockEraser)
		{
			GUILayout.Label(blockEraserIcon, weaponStyle, GUILayout.Width(labelWidth), GUILayout.Height(labelHeight));	
		}
	}
	
	
	
	void OnGUI()
	{		
		//The window that shows the currently selected weapon.
		//I want the window to be placed below the crosshair and just
		//a bit to the right of it.
		
		weaponLeft = Screen.width / 2 - padding;
		
		weaponTop = Screen.height / 2 - Screen.height / 4;
		
		weaponRect = new Rect(Screen.width - weaponLeft, Screen.height - weaponTop, weaponWidth, weaponHeight);
		
		weaponRect = GUI.Window(9, weaponRect, selectedWeaponWindow, "Selected Weapon");
	}
}
