using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the Trigger GameObject on the player and
/// it manages the health of the player across the network and applies
/// damage to the player across the network.
/// 
/// This script accesses the PlayerDatabase script to check the PlayerList.
/// 
/// This script accesses the SpawnScript to inform the player that they've
/// been destroyed.
/// 
/// This script accesses the CombatWindow script to tell to add a new entry.
/// 
/// This script accesses the PlayerScore script to inform that it needs to 
/// increment the players score.
/// 
/// This script is accessed by the BlasterScript.
/// 
/// This script is accessed by the StatDisplay script.
/// 
/// This script is accessed by the PlayerLabel script.
/// 
/// This script is accessed by the FallDamage script.
/// 
/// </summary>

public class HealthAndDamage : MonoBehaviour {
	
	//Variables Start___________________________________
	
	private GameObject parentObject;
	
	
	//Used in figuring out on who's computer the damage should be applied.
	
	public string myAttacker;
	
	public bool iWasJustAttacked;
	
	
	//These variables are used in figuring out what the player has been hit
	//by and how much damage to apply.
	
	public bool hitByBlaster = false;
	
	private float blasterDamage = 30;
	
	
	public bool takingFallDamage = false;
	
	public float fallDamage;
	
	
	//This is used to prevent the player from getting hit while they are
	//undergoing destruction.
	
	private bool destroyed = false;
	
	
	//These variables are used in managing the player's health.
	
	public float myHealth = 100;
	
	public float maxHealth = 100;
	
	private float healthRegenRate = 1.3f;
	
	public float previousHealth = 100;
	
		
	//Variables End_____________________________________
	
	
	// Use this for initialization
	void Start () 
	{
		//The trigger GameObject is used in hit detection but it is
		//the parent that needs to be destroyed if the player's health
		//falls below 0.
		
		parentObject = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//If the player is hit by an opposing team projectile, 
		//then that projectile will have set iWasJustAttacked to true.
		
		if(iWasJustAttacked == true)
		{
			GameObject gameManager = GameObject.Find("GameManager");
			
			PlayerDatabase dataScript = gameManager.GetComponent<PlayerDatabase>();
			
			
			//Sift through the player list and only carry out hit detection if the 
			//attacking player is the one running this game instance.
			
			for(int i = 0; i < dataScript.PlayerList.Count; i++)
			{
				if(myAttacker == dataScript.PlayerList[i].playerName)
				{
					if(int.Parse(Network.player.ToString()) == dataScript.PlayerList[i].networkPlayer)
					{
						//Check what the player was hit by and apply damage.
						
						if(hitByBlaster == true && destroyed == false)
						{
							myHealth = myHealth - blasterDamage;	
							
							//Send out an RPC so that this player's attacker is 
							//updated across the network. This way the attacker
							//can receive a score for destroying the enemy player.
							
							networkView.RPC("UpdateMyCurrentAttackerEverywhere",
							                RPCMode.Others, myAttacker);
							
							
							//Send out an RPC so that this player's health is reduced
							//across the network.
							
							networkView.RPC("UpdateMyCurrentHealthEverywhere",
							                RPCMode.Others, myHealth);
						
							hitByBlaster = false;
						}
						
						
						//This part of the script applies fall damage to the player character. myAttacker is set to
						//an empty string because from the FallDamage script I set myAttacker to this player's ownself
						//and I don't want the combat log showing this player attacking themself as that would look odd.
						
						if(takingFallDamage == true && destroyed == false)
						{
							myHealth = myHealth - fallDamage;
							
							myAttacker = "";
							
							networkView.RPC("UpdateMyCurrentAttackerEverywhere", RPCMode.Others, myAttacker);
							
							networkView.RPC("UpdateMyCurrentHealthEverywhere", RPCMode.Others, myHealth);
							
							takingFallDamage = false;
						}
						
						//Once the player's health drops below 0
						//destryoed is set to true and the attacker is
						//awarded a score point. destroyed remains true
						//so that the attacking player can't get free points
						//by shooting at this player while they are undergoing
						//their destruction.
						
						if(myHealth <= 0 && destroyed == false)
						{
							myHealth = 0;
							
							destroyed = true;
							
							
							//The attacking player should receive a score, though if there
							//is no attacking player (e.g. when a player destroys themself by 
							//falling) then ignore scoring.
							
							if(myAttacker != "")
							{
								GameObject attacker = GameObject.Find(myAttacker);
								
								PlayerScore scoreScript = attacker.GetComponent<PlayerScore>();
								
								scoreScript.iDestroyedAnEnemy = true;
								
								scoreScript.enemiesDestroyedInOneHit++;
							}
						}
					}
				}
			}
			
			iWasJustAttacked = false;
			
		}
		
		
		//Each player is responsible for destroying themselves.
		
		if(myHealth <= 0 && networkView.isMine == true)
		{	
			//Access the SpawnScript and set the iAmDestroyed bool to 
			//true so that this player can respawn.
			
			GameObject spawnManager = GameObject.Find("SpawnManager");
			
			SpawnScript spawnScript = spawnManager.GetComponent<SpawnScript>();
			
			spawnScript.iAmDestroyed = true;
			
			
			
			//Remove this player's RPCs. If we didn't do this a
			//ghost of this player would remain in the game which
			//would be very confusing to players just connecting
			
			Network.RemoveRPCs(Network.player);
			
			
			//Update the CombatWindow across the network.
			
			networkView.RPC("TellEveryoneWhoDestroyedWho", RPCMode.All, myAttacker, parentObject.name);
			
			
			//Send out an RPC to destroy our player across the network.
			
			networkView.RPC("DestroySelf", RPCMode.All);
		}
		
		
		//If the player's health is different from their previous health then
		//update the health record across the network and buffer it.
		
		if(myHealth > 0 && networkView.isMine == true)
		{
			if(myHealth != previousHealth)
			{
				networkView.RPC("UpdateMyHealthRecordEverywhere", RPCMode.AllBuffered, myHealth);	
			}
		}
		
		
		//Regen the player's health if it is below the max health.
		
		if(myHealth < maxHealth)
		{
			myHealth = myHealth + healthRegenRate * Time.deltaTime;
		}
		
		
		//If the player's health exceeds the max health while regenerating
		//then set it back to the max health.
		
		if(myHealth > maxHealth)
		{
			myHealth = maxHealth;	
		}
	
	}
	
	
	[RPC]
	void UpdateMyCurrentAttackerEverywhere (string attacker)
	{
		myAttacker = attacker;	
	}
	
	
	[RPC]
	void UpdateMyCurrentHealthEverywhere (float health)
	{
		myHealth = health;	
	}
	
	
	[RPC]
	void DestroySelf ()
	{
		Destroy(parentObject);	
	}
	
	
	[RPC]
	void UpdateMyHealthRecordEverywhere (float health)
	{
		previousHealth = health;	
	}
	
	
	[RPC]
	void TellEveryoneWhoDestroyedWho (string attacker, string destroyed)
	{
		//Access the CombatWindow script in the GameManager and
		//tell it who destroyed who so that it appears in the combat
		//window.
		
		GameObject gameManager = GameObject.Find("GameManager");
		
		CombatWindow combatScript = gameManager.GetComponent<CombatWindow>();
		
		combatScript.attackerName = attacker;
		
		combatScript.destroyedName = destroyed;
		
		combatScript.addNewEntry = true;
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
}
