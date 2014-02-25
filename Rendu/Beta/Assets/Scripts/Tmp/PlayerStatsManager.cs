using UnityEngine;
using System.Collections;

public class PlayerStatsManager : MonoBehaviour
{
    //Object sur lequel le script est attaché --> le survivant ou zombie
    [SerializeField]
    private GameObject m_parentGameObject;

    //Toutes les stats du joueurs (dont celle pouvant etre modifié par bonus malus)

    [SerializeField]
    private float m_defense = 1f;

    /*[SerializeField]
    private float m_attack = 1f;

    [SerializeField]
    private float m_playerSpeed = 2.5f;

    [SerializeField]
    private float m_coolDownSpeed = 1f;*/


    // Use this for initialization
    void Start()
    {
        m_parentGameObject = gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Fonction qui applique l'alteration sur un survivant (appelé dans l'AttackManager)
    //Détarminiation de l'alteration à appliqué selon la Skill avec idSkill
    //Vu qu'on a un interface Skill, on regarde son type pour savoir quelle stat modifié
    public void applySkillAlteration(int idSkill)
    {
        WeaponInfo weapon = m_parentGameObject.GetComponent<WeaponInfo>();

        ISkillScript skillUsed = weapon.getSkill(idSkill);

        if (skillUsed == null)
        {
            return;
        }

        float alteration = skillUsed.UseSkill();

        if(skillUsed is DefenseBonusMalusScript)
        {
            m_defense *=  (1-alteration); 
        }
        /*if(skillUsed is  Autre Classe Malus Script ) ....... */
    }
}
