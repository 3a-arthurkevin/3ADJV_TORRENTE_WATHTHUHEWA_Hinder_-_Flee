using UnityEngine;
using System.Collections;

public class AttackScript : MonoBehaviour {

    //Represente le joueur qui attaque
    private GameObject m_parentGameObject;

    //L'arme du joueur qui attaque --> obtenue grace à m_parentGameObject
    private WeaponScript m_weapon;


	// Use this for initialization
	void Start () 
    {
        m_parentGameObject = transform.parent.gameObject;
        //Affectation de l'arme comme ca provisorement, le temps de faire l'inventaire/armeEquipé Scirpt
        m_weapon = m_parentGameObject.GetComponent<WeaponScript>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        /*
        if (changement weapon) 
            update de l'arme attaque --> m_weapon = m_parentGameObject.GetComponent<WeaponScript>();
         
         */

        int idSkill = pushButtonAttack();
        if (idSkill >= 0)
        {
            if (doesAttackHasHit(idSkill))
            {
                applyAttackEffect(idSkill);
            }
        }
	}


    //
    // Fonction ci dessous utilisé dans le Update()
    //

    //Pour savoir si le bouton appuyé est un bouton d'attaque
    // Est ce que unity est en QWERTY ????? -__-'
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
        return m_weapon.getHasHit(idSkill);
    }

    //Application de l'effet de l'attaque
        //Différencie le zombie et le survivant et applique les dégat ou malus selon la cas
        //Reset du collider (à null) et du boolean hasHit (à false)
    public void applyAttackEffect(int idSkill)
    {
        Collider targetCollider = m_weapon.getTargetCollider();

        if (targetCollider.tag == "zombie")
        {
            HealthManaTmpScript targetHealthManager = targetCollider.transform.GetComponent<HealthManaTmpScript>();
            targetHealthManager.applyDamage(m_weapon.getDamage());
        }
        else if (targetCollider.tag == "survivor")
        {
            PlayerStatsManager targetStatsManager = targetCollider.transform.GetComponent<PlayerStatsManager>();
            targetStatsManager.applySkillAlteration(idSkill);
        }

        m_weapon.resetAfterHit();
    }
}
