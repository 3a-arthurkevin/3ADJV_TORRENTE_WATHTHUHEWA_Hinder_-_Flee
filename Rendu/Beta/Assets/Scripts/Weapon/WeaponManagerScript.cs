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
        {
            try
            {
                m_skills[i] = SkillFactory.getSkill(m_skillId[i]);
            }
            catch (System.ArgumentException)
            {
                Debug.Log("Skill : " + i.ToString() + " not found");
            }
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Skill1"))
            m_skills[0].StartSkill();

        if (Input.GetButtonDown("Skill2"))
            m_skills[1].StartSkill();

        if (Input.GetButtonDown("Skill3"))
            m_skills[2].StartSkill();

        if (Input.GetButtonDown("Skill4"))
            m_skills[3].StartSkill();


    }
}
