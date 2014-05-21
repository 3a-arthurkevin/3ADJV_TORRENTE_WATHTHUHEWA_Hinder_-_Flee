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

    protected float m_range;
    public float Range
    {
        get { return m_range; }
        set { m_range = value; }
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
    abstract public bool CheckLaunch(Vector3 hit);
    
    public virtual void ApplyEffect(GameObject target)
    {
        if (target.tag == "Zombie")
            foreach (IEffect effect in m_zombieEffect)
                effect.Apply(target);

        else if (target.tag == "Survivor")
            foreach (IEffect effect in m_survivorEffect)
                effect.Apply(target);
    }
    
    public virtual void setParameters(Dictionary<string, string> parameterList)
    {
        foreach(KeyValuePair<string, string> parameter in parameterList)
            setParameter(parameter.Key, parameter.Value);
    }

    public virtual void setParameter(string key, string value)
    {
        if (key == "Name")
            m_name = value;

        else if (key == "Description")
            m_description = value;

        else if (key == "CoolDownDuration")
        {
            try
            {
                m_coolDownDuration = float.Parse(value);
            }
            catch(System.FormatException)
            {
                Debug.Log("Error number format for CoolDownDuration parameter");
            }
        }
        else if (key == "Range")
        {
            try
            {
                m_range = float.Parse(value);
            }
            catch (System.FormatException)
            {
                Debug.Log("Error number format for Range parameter");
            }
        }
        else if (key == "")
        {

        }
    }

    public virtual void LaunchSkill(Vector3 hit)
    {
        m_coolDown = m_coolDownDuration;
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
