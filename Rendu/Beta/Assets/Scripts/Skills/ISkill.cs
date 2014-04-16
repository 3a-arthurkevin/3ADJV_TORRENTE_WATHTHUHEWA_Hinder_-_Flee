using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ISkill
{
    protected Animation m_animation;
    public Animation Animation
    {
        get { return m_animation; }
        set { m_animation = value; }
    }

    protected WeaponManagerScript m_weaponManager;
    public WeaponManagerScript WeaponManager
    {
        get { return m_weaponManager; }
        set { m_weaponManager = value; }
    }

    protected string m_name;
    public string Name
    {
        get { return m_name; }
        set { m_name = value; }
    }

    protected string m_description;
    public string Description
    {
        get { return m_description; }
        set { m_description = value; }
    }

    protected float m_coolDownDuration;
    public float CoolDownDuration
    {
        get { return m_coolDownDuration; }
        set { m_coolDownDuration = value; }
    }

    protected float m_coolDown;
    public float CoolDown
    {
        get { return m_coolDown; }
        set { m_coolDown = value; }
    }

    protected float m_maxRange;
    public float Range
    {
        get { return m_maxRange; }
        set { m_maxRange = value; }
    }

    protected List<IEffect> m_survivorEffect;
    protected List<IEffect> m_zombieEffect;

    public ISkill()
    {
        m_survivorEffect = new List<IEffect>();
        m_zombieEffect = new List<IEffect>();
    }


    abstract public void StartSkill();
    abstract public void StopSkill();
    abstract public bool CheckLaunch(Vector3 hit, string targetName);

    public virtual void LaunchSkill(Vector3 hit, string targetName)
    {
        GameObject target = GameObject.Find(targetName);

        if (target.tag.Substring(0, 6) == "Zombie")
            foreach (IEffect effect in m_zombieEffect)
                effect.Apply(target);

        else if (target.tag.Substring(0, 8) == "Survivor")
            foreach (IEffect effect in m_survivorEffect)
                effect.Apply(target);
        
        else
            Debug.Log("Skill not availiable for this target " + target.name);

        StopSkill();
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
