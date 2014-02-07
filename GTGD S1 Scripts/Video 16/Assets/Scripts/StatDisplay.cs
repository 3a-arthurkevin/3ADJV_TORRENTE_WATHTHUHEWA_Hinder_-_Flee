using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the player and allows
/// the player to see a box with their current health
/// to the lower right of the crosshair.
/// 
/// This script accesses the HealthAndDamage script.
/// 
/// This script accesses the PlayerEnergy script.
/// 
/// This script accesses the PlayerResource script.
/// </summary>


public class StatDisplay : MonoBehaviour {
	
	//Variables Start___________________________________
	
	//The healthbar texture is attached to this in the
	//inspector.
	
	public Texture healthTex;
	
	
	//The energybar texture is attached to this in the inspector.
	
	public Texture energyTex;
	
	
	//The resourcebar texture is attached to this in the inspector.
	
	public Texture resourceTex;
	
	
	//These are used in calculating and displaying the health.
	
	private float health;
	
	private int healthForDisplay;
	
	
	//Used in displaying energy.
	
	private float energy;
	
	private int energyForDisplay;
	
	
	//Used in displaying resource.
	
	private float resource;
	
	private int resourceForDisplay;
	
	
	
	//These are used in defining the StatDisplay box.
	
	private int boxWidth = 160;
	
	private int boxHeight = 85;
	
	private int labelHeight = 20;
	
	private int labelWidth = 35;
	
	private int padding = 10;
	
	private int gap = 120;
	
	private float healthBarLength;
	
	private int healthBarHeight = 15;
	
	private float energyBarLength;
	
	private int energyBarHeight = 15;
	
	private float resourceBarLength;
	
	private int resourceBarHeight = 15;
	
	private GUIStyle healthStyle = new GUIStyle();
	
	private GUIStyle energyStyle = new GUIStyle();
	
	private GUIStyle resourceStyle = new GUIStyle();
	
	private float commonLeft;
	
	private float commonTop;
	
	
	//Quick references.
	
	private HealthAndDamage HDScript;
	
	private PlayerEnergy energyScript;
	
	private PlayerResource resourceScript;
	
	
	//Variables End_____________________________________
	
	// Use this for initialization
	void Start () 
	{
		if(networkView.isMine == true)
		{
			//Access the HealthAndDamage, PlayerEnergy, and PlayerResource scripts.
			
			Transform triggerTransform = transform.FindChild("Trigger");
			
			HDScript = triggerTransform.GetComponent<HealthAndDamage>();
			
			
			energyScript = gameObject.GetComponent<PlayerEnergy>();
			
			
			resourceScript = gameObject.GetComponent<PlayerResource>();
			
			
			//Set the GUIStyle
			
			healthStyle.normal.textColor = Color.green;
			
			healthStyle.fontStyle = FontStyle.Bold;
			
			
			energyStyle.normal.textColor = Color.cyan;
			
			energyStyle.fontStyle = FontStyle.Bold;
			
			
			resourceStyle.normal.textColor = Color.magenta;
			
			resourceStyle.fontStyle = FontStyle.Bold;
		}
		
		else
		{
			enabled = false;	
		}
			
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Access the HealthAndDamage script continuously and retrieve the
		//player's current heatlh.
		
		health = HDScript.myHealth;
		
		
		//I also want to display health as numbers without any decimals.
		
		healthForDisplay = Mathf.CeilToInt(health);
		
		
		//Calculate how long the healthbar should be. The max length is 100
		//representing 100%.
		
		healthBarLength = (health /HDScript.maxHealth) * 100;
		
		
		
		//Access the PlayerEnergy script continuously and retrieve the player's
		//current energy.
		
		energy = energyScript.energy;
		
		
		//Round the energy value so there aren't any decimals.
		
		energyForDisplay = Mathf.CeilToInt(energy);
		
		
		//Calculate how long the energybar should be.
		
		energyBarLength = (energy/energyScript.baseEnergy) * 100;
		
		
		
		//Access the PlayerResource script continuously and retrieve the player's
		//current resource.
		
		resource = resourceScript.resource;
		
		
		//Round the resource value so there aren't any decimals.
		
		resourceForDisplay = Mathf.CeilToInt(resource);
		
		
		//Calculate how long the resourcebar should be.
		
		resourceBarLength = (resource/resourceScript.baseResource) * 100;
	}
	
	
	void OnGUI ()
	{
		commonLeft = Screen.width / 2 + 180;		
		
		commonTop = Screen.height / 2 + 50;
		
		
		//Draw a plain box behind the health bar.
		
		GUI.Box(new Rect(commonLeft, commonTop, boxWidth, boxHeight), "");
		
		
		
		//Draw a grey box behind the healthbar.
		
		GUI.Box(new Rect(commonLeft + padding, commonTop + padding, 100, healthBarHeight), "");
		
		
		//Draw the healthbar and make it's length be dependant on the player's current health.
		
		GUI.DrawTexture(new Rect(commonLeft + padding, commonTop + padding, healthBarLength
		                         ,healthBarHeight), healthTex);
		
		GUI.Label(new Rect(commonLeft + gap, commonTop + padding, labelWidth, labelHeight),
		          healthForDisplay.ToString(), healthStyle);
		
		
		
				
		
		
		//Draw a grey box behind the energybar.
		
		GUI.Box(new Rect(commonLeft + padding, commonTop + energyBarHeight + padding * 2, 100, energyBarHeight), "");
		
		
		//Draw the energybar and make it's length be dependant on the player's current energy.
		
		GUI.DrawTexture(new Rect(commonLeft + padding, commonTop + energyBarHeight + padding * 2, energyBarLength
		                         ,energyBarHeight), energyTex);
		
		GUI.Label(new Rect(commonLeft + gap, commonTop + energyBarHeight + padding * 2, labelWidth, labelHeight),
		          energyForDisplay.ToString(), energyStyle);
		
		
		
		
		//Draw a grey box behind the resourcebar.
		
		GUI.Box(new Rect(commonLeft + padding, commonTop + resourceBarHeight * 2 + padding * 3, 100, resourceBarHeight), "");
		
		
		//Draw the resourcebar and make it's length be dependant on the player's current resource.
		
		GUI.DrawTexture(new Rect(commonLeft + padding, commonTop + resourceBarHeight * 2 + padding * 3, resourceBarLength
		                         ,resourceBarHeight), resourceTex);
		
		GUI.Label(new Rect(commonLeft + gap, commonTop + resourceBarHeight * 2 + padding * 3, labelWidth, labelHeight),
		          resourceForDisplay.ToString(), resourceStyle);
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
}
