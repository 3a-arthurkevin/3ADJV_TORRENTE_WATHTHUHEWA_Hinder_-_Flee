using UnityEngine;
using System.Collections;

public class EntailleDeCheville : AOESkill
{

    protected override void init()
    {
        m_name = "Entaille de cheville";
        m_coolDownDuration = 12f;
        m_range = 10f;
        m_duration = 5f;
        m_aoeRange = 2f;
        m_description = "Place une marre de couteux au sol. La cible marchant dessus se voit ralentit pendant 5 seconde";

        SlowMoveEffect moveEffect = new SlowMoveEffect();
        moveEffect.SpeedReduce = 0.5f;
        moveEffect.TimeToReduce = 5f;

        m_zombieEffect.Add(moveEffect);

        moveEffect = new SlowMoveEffect();
        moveEffect.SpeedReduce = 1f;
        moveEffect.TimeToReduce = 5f;

        m_survivorEffect.Add(moveEffect);
    }
}
