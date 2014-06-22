using UnityEngine;
using System.Collections;

public class HealthManagerScript : MonoBehaviour
{
    [SerializeField]
    private NetworkView m_networkView;

    [SerializeField]
    private int m_maxLifePoint;

    private int m_currentLifePoint;

    void Start()
    {
        m_currentLifePoint = m_maxLifePoint;

        if (m_networkView == null)
            m_networkView = networkView;
    }

    public int LifePoint
    {
        get { return m_currentLifePoint; }
        set
        {
            m_currentLifePoint = value;

            if (m_currentLifePoint <= 0)
                Died();
        }
    }

    [RPC]
    public void AddLifePoint(int hp)
    {
        if (Network.isServer)
        {
            m_networkView.RPC("AddLifePoint", RPCMode.Others, hp);
            LifePoint += hp;
        }
        else
            LifePoint = hp;
    }

    void Died()
    {
        Debug.LogError("Zombie died");

        if (Network.isServer)
            Network.Destroy(gameObject);
    }

    public bool isDead()
    {
        return (m_currentLifePoint == 0);
    }
}
