using UnityEngine;
using System.Collections;

public class HealthManagerScript : MonoBehaviour {

    [SerializeField]
    private int m_playerMaxLifePoint;

    [SerializeField]
    private int m_currentLifePoint;

    public void addLifePoint(int lifePointToAdd)
    {
        m_currentLifePoint += lifePointToAdd;

        if (m_currentLifePoint > m_playerMaxLifePoint)
            m_currentLifePoint = m_playerMaxLifePoint;
    }

    public void removeLifePoint(int lifePointToRemove)
    {
        m_currentLifePoint -= lifePointToRemove;

        if (m_currentLifePoint < 0)
            m_currentLifePoint = 0;
    }

    public bool isDead()
    {
        return (m_currentLifePoint == 0);
    }
}
