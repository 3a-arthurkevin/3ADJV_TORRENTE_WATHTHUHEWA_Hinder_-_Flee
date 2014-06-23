using UnityEngine;
using System.Collections;

public class CoupDeCouteau : SingleTargetSkill
{
    protected override void init()
    {
        m_name = "Coup de couteau";
        m_description = "Donne un coup de couteau.";
        m_coolDownDuration = 12f;
        m_range = 10f;
        m_survivorEffect.Add(new TakeDamageEffect(10));
        m_zombieEffect.Add(new TakeDamageEffect(20));
    }
}
