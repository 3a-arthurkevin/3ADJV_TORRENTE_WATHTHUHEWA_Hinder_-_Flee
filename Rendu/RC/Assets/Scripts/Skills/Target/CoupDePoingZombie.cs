using UnityEngine;
using System.Collections;

public class CoupDePoingZombie : SingleTargetSkill
{

    protected override void init()
    {
        m_name = "Coup de Poing";
        m_description = "Donne un coup de poing.";
        m_coolDownDuration = 5f;
        m_range = 10f;

        m_survivorEffect.Add(new TakeDamageEffect(10));
    }
}
