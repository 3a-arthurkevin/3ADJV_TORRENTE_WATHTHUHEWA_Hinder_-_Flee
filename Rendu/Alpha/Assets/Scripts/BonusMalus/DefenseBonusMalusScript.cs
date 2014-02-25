using UnityEngine;
using System.Collections;

public class DefenseBonusMalusScript
{
    //Je fais de la merde ici
    private float m_alterDefenseValue;

    public DefenseBonusMalusScript(float value)
    {
        m_alterDefenseValue = value;
    }

    public float getAlterValue()
    {
        return m_alterDefenseValue;
    }
}