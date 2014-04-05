using UnityEngine;
using System.Collections;

public class HealthManagerScript : MonoBehaviour {

    [SerializeField]
    private int m_maxLifePoint;

    [SerializeField]
    private int m_currentLifePoint;

    public int LifePoint
    {
        get { return m_currentLifePoint; }
        set
        {
            m_currentLifePoint = Mathf.Clamp(m_currentLifePoint + value, 0, m_maxLifePoint);
        }
    }

    /*public void addLifePoint(int lifePointToAdd)
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
    }*/

    public bool isDead()
    {
        return (m_currentLifePoint == 0);
    }
}
