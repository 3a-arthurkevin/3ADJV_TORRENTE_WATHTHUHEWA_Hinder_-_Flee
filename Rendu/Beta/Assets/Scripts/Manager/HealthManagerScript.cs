using UnityEngine;
using System.Collections;

public class HealthManagerScript : MonoBehaviour {

    [SerializeField]
    private int m_maxLifePoint;

    private int m_currentLifePoint;

    public int LifePoint
    {
        get { return m_currentLifePoint; }
        set
        {
            m_currentLifePoint += value;

            if (m_currentLifePoint <= 0)
                Died();
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

    void Died()
    {
        
    }

    public bool isDead()
    {
        return (m_currentLifePoint == 0);
    }
}
