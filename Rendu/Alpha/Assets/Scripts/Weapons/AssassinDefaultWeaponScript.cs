using UnityEngine;
using System.Collections;

public class AssassinDefaultWeaponScript : MonoBehaviour
{
    [SerializeField]
    int m_attack = 5; //valeur choisi au hasard pour tester

    [SerializeField]
    ISkillScript m_defaultSkill;
}
