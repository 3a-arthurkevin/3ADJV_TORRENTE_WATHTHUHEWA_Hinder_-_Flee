using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ISkill
{
    private Animation m_animation;
    public Animation Animation
    {
        get { return m_animation; }
        set { m_animation = value; }
    }

    private string m_name;
    public string Name
    {
        get { return m_name; }
        set { m_name = value; }
    }

    private string m_description;
    public string Description
    {
        get { return m_description; }
        set { m_description = value; }
    }

    private List<IEffect> m_survivorEffect;
    private List<IEffect> m_zombieEffect;

    public ISkill()
    {
        m_survivorEffect = new List<IEffect>();
        m_zombieEffect = new List<IEffect>();
    }

    public virtual void LaunchSkill(GameObject target)
    {
        if (target.tag == "Zombie")
            foreach (IEffect effect in m_zombieEffect)
                effect.Apply(target);

        else if (target.tag == "Survivor")
            foreach (IEffect effect in m_survivorEffect)
                effect.Apply(target);
        
        else
            Debug.Log("Skill not availiable for this target " + target.name);
    }

    public void addZombieEffect(IEffect effect)
    {
        m_zombieEffect.Add(effect);
    }

    public void addSurvivorEffect(IEffect effect)
    {
        m_survivorEffect.Add(effect);
    }
}
