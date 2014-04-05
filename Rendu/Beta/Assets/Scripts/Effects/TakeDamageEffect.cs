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

    public void Apply(GameObject target)
    {
        HealthManagerScript healthManager = target.GetComponent<HealthManagerScript>();

        if (healthManager == null)
            Debug.LogError("Target haven't health Manager");

        healthManager.LifePoint -= m_damage;
    }

    public void SetParam(Dictionary<string, string> param)
    {
        string damage = "";
        if (param.TryGetValue("Damage", out damage))
            m_damage = int.Parse(damage);
    }
}
