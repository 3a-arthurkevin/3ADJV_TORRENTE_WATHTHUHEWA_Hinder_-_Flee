using UnityEngine;
using System.Collections;

public class WeaponManagerScript : MonoBehaviour
{
    [SerializeField]
    private int[] m_skillId = new int[4];
    private ISkill[] m_skills = new ISkill[4];

    void Start()
    {
        for (int i = 0; i < 4; ++i)
            m_skills[i] = SkillFactory.getSkill(m_skillId[i]);
    }

    void Update()
    {
    }
}
