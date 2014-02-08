using UnityEngine;
using System.Collections;

public class WeaponInfo : MonoBehaviour {

    //Dommage infligé au zombie --> pas de pris en compte des dommage des skill pour l'instant
    [SerializeField]
    int m_damage;

    //Les skills affecté à l'arme selon la touche
    [SerializeField]
    ISkillScript m_SkillA;

    [SerializeField]
    ISkillScript m_SkillZ;

    [SerializeField]
    ISkillScript m_SkillE;

    [SerializeField]
    ISkillScript m_SkillR;

	public int getDamage()
    {
        return m_damage;
    }

    private bool m_hasHit = false;

    //Collider de l'entité qui rentre dans le collider l'objet auquel le script est attaché --> ici l'arme
    private Collider m_targetCollider;


    //Pour savoir qui quelquechose est dans le collider de l'arme
    //--> le paramètre collider de la fonction represente le collider de l'entité entrant dans le collider de l'arme
    void onTriggerStay(Collider collider)
    {
        //Faudra mettre des tag sur les entités pour les reconnaitres
        //J'ai vue les tag mais les noms sont en francais --> on les met en anglais ????
        if (collider.tag == "survivor" || collider.tag == "zombie")
        {
            m_hasHit = true;
            m_targetCollider = collider;
        }
    }


    //
    // Les fonctions ci dessous sont appelé depuis le attackManager
    //


    //Pour récupérer hasHit
    public bool getHasHit(int idSkill)
    {
        return m_hasHit;
    }

    //Pour récupérer le skill à appliquer --> si cible survivant
    public ISkillScript getSkill(int idSkill)
    {
        if (idSkill == 0)
            return m_SkillA;
        else if (idSkill == 1)
            return m_SkillZ;
        else if (idSkill == 2)
            return m_SkillE;
        else if (idSkill == 3)
            return m_SkillR;

        return null;
    }

    //Récupération du collider de la cible --> pour pouvoir remonter au GameObject cible
    public Collider getTargetCollider()
    {
        return m_targetCollider;
    }

    //Reset apres l'application de l'attack 
    public void resetAfterHit()
    {
        m_hasHit = false;
        m_targetCollider = null;
    }
}
