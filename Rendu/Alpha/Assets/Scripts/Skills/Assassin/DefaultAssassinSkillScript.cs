using UnityEngine;
using System.Collections;

public class DefaultAssassinSkill : MonoBehaviour, ISkillScript
{
    [SerializeField]
    int m_damage = 10;

    [SerializeField]
    int m_coolDownDuration = 10; 
        //Durée des cool down en secondes
    /*
    [SerializeField]
    int m_typeOfAttack = 0;
        // typeOfAttack (à définir)
            // 0 --> attaque CàC
            // 1 --> attaque AOE
            // 2 --> .....
            // 3 --> .....  
     */

    //
    //A ce niveau la je pense que je fais un peu de la merde dans l'archi du code (pour les armes/skill/malusBonus)
    //

    //Instanciation
    [SerializeField]
    private DefenseBonusMalusScript malusDefense = new DefenseBonusMalusScript(-0.10f);

    //Utilisé dans PlayerStatsManager
    float ISkillScript.UseSkill()
    {
        return malusDefense.getAlterValue();
    }
}