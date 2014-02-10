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
    [SerializeField]
    private WeaponInfo m_weaponInfo;

    [SerializeField]
    private int m_idSkill = -1;

    [SerializeField]
    private NetworkView m_networkView;

	// Use this for initialization
	void Start () 
    {
        m_parentGameObject = gameObject;
        m_weaponInfo = m_parentGameObject.GetComponent<WeaponInfo>();
        m_networkView = gameObject.GetComponent<NetworkView>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Network.isClient)
        {
            m_idSkill = pushButtonAttack();
            if (m_idSkill >= 0)
            {
                if (doesAttackHasHit(m_idSkill))
                {
                    applyAttackEffect(m_idSkill);
                }
            }
        }
	}


    //Pour savoir si le bouton appuyé est un bouton d'attaque
    private int pushButtonAttack()
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
        return m_weaponInfo.getHasHit(idSkill);
    }

    //Application de l'effet de l'attaque
        //Différencie le zombie et le survivant et applique les dégats ou malus selon la cas
        //Reset du collider (à null) et du boolean hasHit (à false)
    public void applyAttackEffect(int idSkill)
    {
        Collider targetCollider = m_weaponInfo.getTargetCollider();

        if (targetCollider.tag == "Zombie")
        {
            HealthManaTmpScript targetHealthManager = targetCollider.transform.GetComponent<HealthManaTmpScript>();
            targetHealthManager.applyDamage(m_weaponInfo.getDamage());

            /*if (targetHealthManager.getZeroLifePoint())
            {
                // --> détruire le zombie ou transformer de survivant
            }*/
        }
        else if (targetCollider.tag == "Survivant")
        {
            PlayerStatsManager targetStatsManager = targetCollider.transform.GetComponent<PlayerStatsManager>();
            targetStatsManager.applySkillAlteration(idSkill);
        }

        m_weaponInfo.resetAfterHit();
    }
}
