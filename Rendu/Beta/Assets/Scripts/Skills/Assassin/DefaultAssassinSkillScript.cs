using UnityEngine;
using System.Collections;

public class DefaultAssassinSkill : MonoBehaviour, ISkillScript
{
    /*[SerializeField]
    int m_damage = 10;

    [SerializeField]
    int m_coolDownDuration = 10; */
        //Durée des cool down en secondes
    /*
     * --> pour mettre des collider différents selon le type d'attaque
    [SerializeField]
    int m_typeOfAttack = 0;
        // typeOfAttack (à définir)
            // 0 --> attaque CàC
            // 1 --> attaque AOE
            // 2 --> .....
            // 3 --> .....  
     */

    //Instanciation
    [SerializeField]
    private DefenseBonusMalusScript malusDefense = new DefenseBonusMalusScript(-0.10f);

    //Utilisé dans PlayerStatsManager
    public float UseSkill()
    {
        return malusDefense.getAlterValue();
    }
}