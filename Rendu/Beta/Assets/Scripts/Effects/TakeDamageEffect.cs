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
        
        else
            healthManager.LifePoint -= m_damage;
    }

    public void SetParam(Dictionary<string, string> param)
    {
        string parameter = "";
        
        if (param.TryGetValue("Damage", out parameter))
            m_damage = int.Parse(parameter);
    }
}
