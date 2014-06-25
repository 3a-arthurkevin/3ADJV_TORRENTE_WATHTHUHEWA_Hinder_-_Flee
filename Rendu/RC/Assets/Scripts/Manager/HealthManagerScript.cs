using UnityEngine;
using System.Collections;

public class HealthManagerScript : MonoBehaviour
{

    public enum CharacterType
    {
        Zombie,
        Survivor
    }

    [SerializeField]
    private CharacterType m_characterType;
    public CharacterType IsType
    {
        get { return m_characterType; }
    }

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
            m_currentLifePoint = Mathf.Clamp(value, 0, m_maxLifePoint);

            if (m_currentLifePoint <= 0)
                Died();
        }
    }

    [RPC]
    public void AddLifePoint(int hp)
    {
        if (Network.isServer)
            m_networkView.RPC("AddLifePoint", RPCMode.Others, hp);

        m_currentLifePoint = Mathf.Clamp(m_currentLifePoint + hp, 0, m_maxLifePoint);

        if (m_currentLifePoint <= 0)
            Died();
    }

    [RPC]
    public void RemoveLifePoint(int hp)
    {
        if (Network.isServer)
            m_networkView.RPC("RemoveLifePoint", RPCMode.Others, hp);

        m_currentLifePoint = Mathf.Clamp(m_currentLifePoint - hp, 0, m_maxLifePoint);

        if (m_currentLifePoint <= 0)
            Died();
    }

    void Died()
    {
        if (m_characterType == CharacterType.Survivor)
            Debug.LogError("Survivor died");

        else if (m_characterType == CharacterType.Zombie)
            Debug.LogError("Zombie Died");

        if (Network.isServer)
            Network.Destroy(gameObject);
    }

    public bool isDead()
    {
        return (m_currentLifePoint == 0);
    }
}
