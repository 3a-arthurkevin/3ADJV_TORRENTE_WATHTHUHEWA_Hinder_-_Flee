using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TakeDamageEffect : IEffect
{
    private int m_damage = 10;
    public int Damage
    {
        get { return m_damage; }
        set { Damage = value; }
    }

    public TakeDamageEffect() : base()
    {
        m_damage = 10;
    }

    public TakeDamageEffect(int damage) : base()
    {
        m_damage = damage;
    }

    public void Apply(GameObject target)
    {
        HealthManagerScript healthManager = target.GetComponent<HealthManagerScript>();

        if (healthManager == null)
            Debug.LogError("Target haven't health Manager");
        
        else
            healthManager.LifePoint -= m_damage;
    }

    public void SetParam(Dictionary<string, string> param)
    {
        string parameter = "";
        
        if (param.TryGetValue("Damage", out parameter))
            m_damage = int.Parse(parameter);
    }

    public void SetParam(string name, string value)
    {
        if (name == "Damage")
            m_damage = int.Parse(value);
    }
}
