using UnityEngine;
using System.Collections;

//Script collé à une arme
//Permet d'attaquer

public class AttackManagerScript : MonoBehaviour 
{

    //Represente le GameObject arme du joueur qui attaque
    [SerializeField]
    private GameObject m_parentGameObject;

    //Info sur l'arme du joueur qui attaque --> obtenue grace à m_parentGameObject
    //On regarde dans l'inventaire ?
    [SerializeField]
    private WeaponInfo m_weaponEquiped;

    [SerializeField]
    private int m_idSkill = -1;

    [SerializeField]
    private bool intentToAttack = false;

    //Pour savoir si le joueur doit cibler un perso pour lancer son attaque
    [SerializeField]
    private bool aiming = false;

    [SerializeField]
    private bool clientHasAim = false;

    //Pour savoir si l'attaque doit être lancer (juste après avoir viser pour la skill)
    [SerializeField]
    private bool lunchAttackAnimation = false;

    [SerializeField]
    private bool coolDownOk = false; // --> à mettre sur la skill

    //Personnage attaquée
    [SerializeField]
    private GameObject attacked;

    //[SerializeField]
    //private NetworkView m_networkView;

	// Use this for initialization
	void Start () 
    {
        m_parentGameObject = gameObject;
        //m_weaponEquiped = m_parentGameObject.GetComponent<WeaponInfo>();
        //m_networkView = gameObject.GetComponent<NetworkView>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Network.isClient)
        {
            // -- 1 --
            //Si !intentToAttack && !aiming && !clientHasAim && !lunchAnimationAttack
            //Si le joueur fait un input
            //On vérifie si il s'agit de A, Z, E ou R (association touche skill) et envoie de m_idSkill et intentToAttack à true avec rpc au serveur

            // -- 3 --
            //Si intentToAttack && aiming && !clientHasAim && !lunchAnimationAttack
            //Envoie de l'input/position de la souris, aiming = false et clientHasAim = true au serveur avec rpc
        }
        if (Network.isServer)
        {
            // -- 2 --
            //Si intentToAttack && !aiming && !clientHasAim && !lunchAnimationAttack
            //chercher l'arme du joueur (dans l'inventaire ?)
            //Vérifier si le cooldown de la skill est ok
            //Si cool down ok
            //Regarder le type de l'attaque (auto/cible/aoe) --> aiming true ou false selon type
            //Si target ou AOE --> mettre aiming à true et envoyer en rpc aux client
            //Sinon si attaque pas besoin de viser, lunchAnimationAttack = true envoie RPC tout les joueurs

            //Sinon
            //Ne rien faire/bruit indiquant que attaque non réalisable
            //intentToAttack / aiming / clientHasAim / lunchAnimationAttack --> tout à false en rpc à tout le monde

            // -- 4 --
            //si intentToAttack && !aiming && clientHasAim && !lunchAnimationAttack
            // Si position de la sourir pour la target ou AOE est ok, lunchAnimationAttack à true

        }
        /*
         // -- 5 --
         si intentToAttack && !aiming && clientHasAim && lunchAnimationAttack
            Lancer la skill + animation
            if(Network.isServer)
            {
              verification de on TriggerStay/enter de l'amre dans un collider ennemi si oui attribution dégat/malus à la victime
            }
			intentToAttack / aiming / clientHasAim / lunchAnimationAttack --> tout à false 
         */
        if (!intentToAttack && !aiming)
        {
            m_idSkill = findInputSkill();
            if (m_idSkill >= 0)
            {
                //Recherche de l'arme dans l'inventaire (ou la stocker dans variable --> a voir)
                //weaponEquiped = inventoryManager.getWeaponEquiped()
                //aiming = weapon.get...... --> fonction qui retourne le vrai ou faux 
                    //selon type skill (AOE, direct, cible...)
                intentToAttack = true;
                //coolDownSkillOk = isCoolDownSkillOk(m_idSkill)
                if (intentToAttack && !aiming)
                {
                    //Lancement de l'attaque n'ayant pas besoin de ciblage
                    intentToAttack = false;
                }
            }
        }
        else if (intentToAttack && aiming)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                aiming = false;
                intentToAttack = false;
                if (doesAttackHasHit(m_idSkill))
                {
                    applyAttackEffect(m_idSkill);
                }
                else
                {
                    //Animation "Miss"
                }
            }
        }
	}

    //RPC
    private void playerInputAttack()
    {
        m_idSkill = findInputSkill();
    }

    //Pour savoir si le bouton appuyé est un bouton d'attaque
    private int findInputSkill()
    {
        //Id par défaut = -1
        int idAttack = -1;

        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.A) /* && coolDown skillA ok*/)
            {
                idAttack = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Z) /* && coolDown skillZ ok*/)
            {
                idAttack = 1;
            }
            else if (Input.GetKeyDown(KeyCode.E) /* && coolDown skillE ok*/)
            {
                idAttack = 2;
            }
            else if (Input.GetKeyDown(KeyCode.R) /* && coolDown skillR ok*/)
            {
                idAttack = 3;
            }
        }

        return idAttack;
    }

    //Pour savoir si un ennemi etait dans le collider de l'arme pendant l'attaque
    public bool doesAttackHasHit(int idSkill)
    {
        return m_weaponEquiped.getHasHit(idSkill);
    }

    //Application de l'effet de l'attaque
        //Différencie le zombie et le survivant et applique les dégats ou malus selon la cas
        //Reset du collider (à null) et du boolean hasHit (à false)
    public void applyAttackEffect(int idSkill)
    {
        Collider targetCollider = m_weaponEquiped.getTargetCollider();

        if (targetCollider.tag == "Zombie")
        {
            HealthManaTmpScript targetHealthManager = targetCollider.transform.GetComponent<HealthManaTmpScript>();
            //Mettre en parametre l'id skill utiliser de l'arme
            targetHealthManager.applyDamage(m_weaponEquiped.getDamage());

            /*if (targetHealthManager.getZeroLifePoint())
            {
                // --> détruire le zombie ou transformer de survivant
            }*/
        }
        else if (targetCollider.tag == "Survivant")
        {
            PlayerStatsManager targetStatsManager = targetCollider.transform.GetComponent<PlayerStatsManager>();
            //Mettre arme en parametre
            targetStatsManager.applySkillAlteration(idSkill);
        }

        m_weaponEquiped.resetAfterHit();
    }
}
