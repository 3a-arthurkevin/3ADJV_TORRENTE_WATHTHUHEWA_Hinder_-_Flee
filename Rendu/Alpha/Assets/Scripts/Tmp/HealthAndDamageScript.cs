using UnityEngine;
using System.Collections;

//Script attaché à l'objet déclanchant l'evenement sur le joueur
//Manage les pv à traver le réseau

//Script a acces à la PlayerDataBase pour avoir acces à la liste des joueurs

//Accède au attaque script (qui attaque et l'arme)

public class HealthAndDamageScript : MonoBehaviour {

    // Variable debut

    private GameObject parentGameObject;

    //Utilisé pour savoir sur quel Joueur/ordianateur les dommage doit etre appliqué
    public string myAttacker;
    public bool iWasJustAttacked;

    //Variable pour savoir quelle player à taper et les degats à appliquer
    public bool hitByWeapon = false;
    private int defaultWeaponDamage = 10;

    //autre arme degat
    // ....
    //.....

    //Pour evite que le joueur prennent des degat une fois mort
    private bool destroyed = false;

    //Pour manager la santé du joueur
    public int currentHeath = 200;
    public int maxHealth = 200;

    // Variable fin


	// Use this for initialization
	void Start () 
    {
        //L'objet déclancheur est utilisé pour taper mais 
        //c'est la parent qui a besoin d'etre détruit si les pb sont en dessous de 0.
        parentGameObject = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (iWasJustAttacked == true)
        {
            GameObject gameManager = GameObject.Find("GameManager");

            PlayerDataBaseScript dataScript = gameManager.GetComponent<PlayerDataBaseScript>();

            //Verification dans la list des joueurs pour savoir qui a attaqué, pris les dégats

            int i = 0;
            bool find = false;
            int listSize = dataScript.PlayerList.Count;

            while (i < listSize && !find)
            {
                if (myAttacker == dataScript.PlayerList[i].playerName)
                {
                    if (int.Parse(Network.player.ToString()) == dataScript.PlayerList[i].networkPlayer)
                    {
                        //Verification de ce qui a touché le joueur et application des dégats
                        if (hitByWeapon == true && destroyed == false)
                        {
                            currentHeath = currentHeath - defaultWeaponDamage;

                            //envoie sur le réseau les dégats pour tout les monde
                            networkView.RPC("UpdateMyAttacker", RPCMode.Others, myAttacker);

                            //envoie du RPC de la modif de point de vie
                            networkView.RPC("UpdateCurrentHealth", RPCMode.Others, currentHeath);
                        }
                    }
                }
            }
            iWasJustAttacked = false;
        }

        //Chaque joueur s'occupe d'updater sur le réseau leur état (mort, zombie)
        if (currentHeath <= 0 && networkView.isMine == true)
        { 
            //Enlever les RPC du joueur
            //Si ce n'est pas fait, il restera un fantome de ce joueur dans le jeu
            networkView.RPC("DestroySelf", RPCMode.All);
        }

        //Cas ou regen du perso
        //if(drinkPotion) ....
        //  fonction pv+

        if (currentHeath > maxHealth)
        {
            currentHeath = maxHealth;
        }
	}

    [RPC]
    void UpdateMyAttacker(string attacker)
    {
        myAttacker = attacker;
    }

    [RPC]
    void UpdateCurrentHealth(int health)
    {
        currentHeath = health;
    }

    [RPC]
    void DestroySelf()
    {
        Destroy(parentGameObject);
    }
}
