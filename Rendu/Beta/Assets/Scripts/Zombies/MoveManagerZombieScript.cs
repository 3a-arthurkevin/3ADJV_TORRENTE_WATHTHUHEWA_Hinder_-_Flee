using UnityEngine;
using System.Collections;

public class MoveManagerZombieScript : MonoBehaviour {
    [SerializeField]
    private NetworkView m_networkView;

    [SerializeField]
    private float m_defaultSpeed = 2f;

    [SerializeField]
    private float m_minDistance = 0.1f;

    private MoveData m_data;

    [SerializeField]
    private Transform m_survivor;
    private bool m_follow = false;

    void Start()
    {
        m_follow = false;
        m_survivor = null;

        if (m_networkView == null)
            m_networkView = networkView;

        m_data = new MoveData();
        m_data.Position = transform;
        m_data.Speed = m_defaultSpeed;
        m_data.IsInFloor = 0;
    }

    void FixedUpdate()
    {

        if (m_follow)
            genPath(m_data.Position.position, m_survivor.position);

        if (m_data.Path == null)
        {
            if (Network.isServer)
                m_networkView.RPC("setTarget", RPCMode.All, ConfigLevelManager.getRandomMoveZombie(m_data.IsInFloor));
            
            return;
        }

        Vector3 direction = m_data.Path.corners[m_data.NumCorner] - m_data.Position.position;
        direction.y = 0;

        if (direction.sqrMagnitude < m_minDistance)
        {
            if ((m_data.NumCorner + 1) >= m_data.Path.corners.Length)
            {   
                if (m_follow)
                    return;

                m_data.Path = null;

                if (Network.isServer)
                    m_networkView.RPC("setTarget", RPCMode.All, ConfigLevelManager.getRandomMoveZombie(m_data.IsInFloor));

                return;
            }
            else
            {
                Vector3 look = m_data.Path.corners[++m_data.NumCorner];
                look.y = m_data.Position.position.y;
                m_data.Position.LookAt(look);
            }
        }

        m_data.Position.position += direction.normalized * m_data.Speed * Time.deltaTime;
    }

    public void Follow(Transform target)
    {
        m_survivor = target;
        m_follow = true;
    }

    public void Unfollow()
    {
        m_survivor = null;
        m_follow = false;
    }

    [RPC]
    public void setTarget(Vector3 target)
    {
        genPath(m_data.Position.position, target);
    }

    public void genPath(Vector3 origin, Vector3 target)
    {
        m_data.Path = MoveUtilsScript.getCalcPath(origin, target);

        if (m_data.Path != null)
            m_data.NumCorner = 1;
        
        else
        {
            Debug.Log("Path not valid");
            m_data.NumCorner = 0;
        }
    }
}
