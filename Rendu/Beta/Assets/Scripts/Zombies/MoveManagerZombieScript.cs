using UnityEngine;
using System.Collections;

public class MoveManagerZombieScript : MonoBehaviour {
    [SerializeField]
    private NetworkView m_networkView;

    [SerializeField]
    private int m_atIsFloor;

    [SerializeField]
    private float m_defaultSpeed = 2f;

    [SerializeField]
    private float m_minDistance = 0.1f;

    private MoveData m_data;

    //Client
    [SerializeField]
    private Vector3 m_target = Vector3.zero;

    //Server
    [SerializeField]
    private Transform m_survivor;
    private bool m_follow = false;

    public int AtIsFloor
    {
        get
        {
            return m_atIsFloor;
        }
        set
        {
            m_atIsFloor = value;
        }
    }

    void Start()
    {
        m_follow = false;
        m_survivor = null;

        if (m_networkView == null)
            m_networkView = networkView;

        m_data = new MoveData();
        m_data.Position = transform;
        m_data.Speed = m_defaultSpeed;
        
        m_target = Vector3.zero;
    }

    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo message)
    {
        if (stream.isWriting)
        {
            stream.Serialize(ref m_target);
        }
        else
        {
            stream.Serialize(ref m_target);
        }
    }

    void FixedUpdate()
    {
        /*
        if (Network.isClient)
        {//Client side
            Vector3 direction = m_target - m_data.Position.position;

            if (direction.sqrMagnitude >= m_minDistance)
                m_data.Position.position += direction.normalized * m_data.Speed * Time.deltaTime;
        }
        else
        {//Server side
            Vector3 direction = Vector3.zero;

            if (m_follow)
            {
                m_data.Path = MoveUtilsScript.getCalcPath(m_data.Position.position, m_survivor.position);

                if (m_data.Path == null)
                {
                    m_data.NumCorner = 0;
                    m_target = Vector3.zero;
                }
                else
                {
                    m_data.NumCorner = 1;
                    direction = m_data.Path.corners[1] - m_data.Position.position;
                    m_data.Position.LookAt(m_data.Path.corners[1]);
                    m_target = m_data.Path.corners[1];
                }
            }
            else
            {
                if (m_data.Path == null)
                    getRandomPath();
                
                else
                {
                    direction = m_data.Path.corners[m_data.NumCorner] - m_data.Position.position;

                    if (direction.sqrMagnitude < m_minDistance)
                    {
                        if ((m_data.NumCorner + 1) >= m_data.Path.corners.Length)
                            m_data.Path = null;

                        else
                        {
                            Vector3 look = m_data.Path.corners[++m_data.NumCorner];
                            look.y = m_data.Position.position.y;
                            m_data.Position.LookAt(look);
                        }

                        direction = Vector3.zero;
                    }
                }
            }

            direction.y = 0;
            m_data.Position.position += direction.normalized * m_data.Speed * Time.deltaTime;
        }*/
    }

    private void getRandomPath()
    {
        /*
        if (Network.isServer)
        {
            Vector3 target = ConfigLevelManager.getRandomMoveZombie(m_atIsFloor);
            m_data.Path = MoveUtilsScript.getCalcPath(m_data.Position.position, target);

            if (m_data.Path != null)
            {
                m_data.NumCorner = 1;
                m_target = m_data.Path.corners[1];
                
                Vector3 look = m_data.Path.corners[1];
                look.y = m_data.Position.position.y;
                m_data.Position.LookAt(look);
            }
        }*/
    }

    public void Follow(Transform target)
    {
        m_survivor = target;
    }

    public void Unfollow()
    {
        m_survivor = null;
        m_follow = false;
    }

    [RPC]
    public void setTarget(Vector3 target)
    {
        m_target = target;
    }

    public void updatePath(Vector3 origin, Vector3 target)
    {
        m_data.Path = MoveUtilsScript.getCalcPath(origin, target);

        if (m_data.Path != null)
            m_data.NumCorner = 1;
        
        else
        {
            Debug.Log("Path not valid");
            m_data.NumCorner = 0;
            m_target = Vector3.zero;
        }
    }
}
