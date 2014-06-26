using UnityEngine;
using System.Collections;

public class LancerDeCouteaux : SingleTargetSkill
{

    protected override void init()
    {
        m_name = "Lancer de couteau";
        m_description = "Lance un couteau";
        m_coolDownDuration = 5f;
        m_range = 5f;

        m_zombieEffect.Add(new TakeDamageEffect(25));
    }
}
