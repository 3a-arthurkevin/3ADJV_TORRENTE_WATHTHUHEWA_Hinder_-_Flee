using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ISkill
{
    [SerializeField]
    private List<int> m_survivorEffectId;
    private List<IEffect> m_survivorEffect;

    [SerializeField]
    private List<int> m_zombieEffectId;
    private List<IEffect> m_zombieEffect;
}
