using UnityEngine;
using System.Collections;

public class MoveManagerZombieScript : MonoBehaviour {
    [SerializeField]
    private ConfigLevelManager m_configLevel;
    public ConfigLevelManager ConfigLevelManager
    {
        get { return m_configLevel; }
        set { m_configLevel = value; }
    }
    
    [SerializeField]
    private NetworkView m_networkView;

    [SerializeField]
    private float m_defaultSpeed = 2f;

    [SerializeField]
    private float m_minDistance = 0.1f;

    private MoveData m_data;

    public MoveData Data
    {
        get { return m_data; }
        set { m_data = value; }
    }

    [SerializeField]
    private Transform m_survivor;
    public Transform Surivor
    {
        get { return m_survivor; }
    }

    private bool m_follow = false;
    public bool IsFollowing
    {
        get { return m_follow; }
    }

    void Awake()
    {
        m_follow = false;
        m_survivor = null;

        if (m_networkView == null)
            m_networkView = networkView;

        m_data = new MoveData();
        m_data.Position = transform;
        m_data.Speed = m_defaultSpeed;
        m_data.CharacterController = GetComponent<CharacterController>();
    }

    void Update()
    {

        if (m_follow)
            genPath(m_data.Position.position, m_survivor.position);

        if (m_data.Path == null)
        {
            if (Network.isServer)
                m_networkView.RPC("setTarget", RPCMode.All, m_configLevel.getRandomMoveZombie(m_data.IsInFloor));
            
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
                    m_networkView.RPC("setTarget", RPCMode.All, m_configLevel.getRandomMoveZombie(m_data.IsInFloor));

                return;
            }
            else
            {
                Vector3 look = m_data.Path.corners[++m_data.NumCorner];
                look.y = m_data.Position.position.y;
                m_data.Position.LookAt(look);
            }
        }

        m_data.CharacterController.Move(direction.normalized * m_data.Speed * Time.deltaTime);
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

    [RPC]
    void SetName(string newName)
    {
        name = newName;
    }

    public float getDefaultSpeed()
    {
        return m_defaultSpeed;
    }
}
