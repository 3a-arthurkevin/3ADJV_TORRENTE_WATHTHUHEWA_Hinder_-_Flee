using UnityEngine;
using System.Collections;

public class DefaultAssassinSkill : MonoBehaviour, ISkillScript
{
    /*[SerializeField]
    int m_damage = 10;

    [SerializeField]
    int m_coolDownDuration = 10; 
        //Durée des cool down en secondes

    [SerializeField]
    int m_typeOfAttack = 0;
        // typeOfAttack (à définir)
            // 0 --> attaque CàC
            // 1 --> attaque AOE
            // 2 --> .....
            // 3 --> .....  
     */

    [SerializeField]
    int m_sizeOfCollider; 
        //taille du collider de la zone d'attaque lors de l'utilisation du skill

    //[SerializeField]
    //Script BonusMalus
        //Script malus associé à la skill



    void ISkillScript.UseSkill()
    {
        //code déclanchant les effets
        //Fonction se déclanche quand on a un onTriggerEnter de l'attaque --> surement dans le characterAttackManager ???

        bool targetIsHuman = false;

        if (targetIsHuman)
        {
            //declanche malus sur joueur
        }
        else
        {
            //declanche degat sur zombie
        }
    }
}
