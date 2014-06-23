using UnityEngine;
using System.Collections;

public class PiegeDeCouteau : AOESkill
{

    protected override void init()
    {
        m_name = "Piege de couteau";
        m_coolDownDuration = 12f;
        m_range = 10f;
        m_duration = 5f;
        m_aoeRange = 2f;
        m_description = "Lance un couteau sur le sol. Inflige des dégâts lorsqu'un adversaire marche dessus dans une rayon de " + m_aoeRange + "m";

        m_survivorEffect.Add(new TakeDamageEffect(10));
        m_zombieEffect.Add(new TakeDamageEffect(20));
    }
}
