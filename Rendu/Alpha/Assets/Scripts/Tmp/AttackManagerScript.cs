using UnityEngine;
using System.Collections;

public class AttackManagerScript : MonoBehaviour 
{

    //Represente l'arme du joueur qui attaque
    [SerializeField]
    private GameObject m_parentGameObject;

    //Info sur l'arme du joueur qui attaque --> obtenue grace à m_parentGameObject
    [SerializeField]
    private WeaponInfo m_weaponInfo;

    [SerializeField]
    private int m_idSkill = -1;

	// Use this for initialization
	void Start () 
    {
        m_parentGameObject = transform.gameObject;
        m_weaponInfo = m_parentGameObject.GetComponent<WeaponInfo>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        m_idSkill = pushButtonAttack();
        if (m_idSkill >= 0)
        {
            Debug.Log(m_weaponInfo.getDamage());
            if (doesAttackHasHit(m_idSkill))
            {
                applyAttackEffect(m_idSkill);
            }
        }
	}


    //
    // Fonction ci dessous utilisé dans le Update()
    //

    //Pour savoir si le bouton appuyé est un bouton d'attaque
    private int pushButtonAttack()
    {
        int idAttack = -1;

        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.A) /* && coolDown ok*/)
            {
                idAttack = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Z) /* && coolDown ok*/)
            {
                idAttack = 1;
            }
            else if (Input.GetKeyDown(KeyCode.E) /* && coolDown ok*/)
            {
                idAttack = 2;
            }
            else if (Input.GetKeyDown(KeyCode.R) /* && coolDown ok*/)
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
        //Différencie le zombie et le survivant et applique les dégat ou malus selon la cas
        //Reset du collider (à null) et du boolean hasHit (à false)
    public void applyAttackEffect(int idSkill)
    {
        Collider targetCollider = m_weaponInfo.getTargetCollider();

        if (targetCollider.tag == "Zombie")
        {
            HealthManaTmpScript targetHealthManager = targetCollider.transform.GetComponent<HealthManaTmpScript>();
            targetHealthManager.applyDamage(m_weaponInfo.getDamage());
        }
        else if (targetCollider.tag == "Survivant")
        {
            PlayerStatsManager targetStatsManager = targetCollider.transform.GetComponent<PlayerStatsManager>();
            targetStatsManager.applySkillAlteration(idSkill);
        }

        m_weaponInfo.resetAfterHit();
    }
}
