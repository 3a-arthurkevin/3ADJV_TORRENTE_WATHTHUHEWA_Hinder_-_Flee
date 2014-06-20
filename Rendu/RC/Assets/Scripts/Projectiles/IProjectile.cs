using UnityEngine;
using System.Collections;

public abstract class IProjectile : MonoBehaviour {

    [SerializeField]
    protected Transform m_transform;

    protected NetworkViewID m_launcher;
    public NetworkViewID Launcher
    {
        get { return m_launcher; }
        set { m_launcher = value; }
    }

    protected float m_minDistance = 0.1f;
    public float MinDistance
    {
        get { return m_minDistance; }
        set { m_minDistance = value; }
    }

    protected float m_speed = 2f;
    public float Speed
    {
        get { return m_speed; }
        set { m_speed = value; }
    }

    protected bool m_isLaunch = false;
    public bool IsLaunch
    {
        get { return m_isLaunch; }
    }

    protected Vector3 m_direction = Vector3.zero;
    public Vector3 Direction
    {
        get { return m_direction; }
        set
        {
            m_direction = (value - m_transform.position).normalized;
            m_direction.y = 0;
        }
    }

    protected Vector3 m_limit = Vector3.zero;
    public float Limit
    {
        set
        {
            m_limit = (m_direction * value) + m_transform.position;
            m_isLaunch = true;
        }
    }

    public delegate void ApplySkillEffect(GameObject target);
    protected ApplySkillEffect m_applyEffect;
    public ApplySkillEffect ApplyEffect
    {
        get { return m_applyEffect; }
        set { m_applyEffect = value; }
    }

    virtual public void Start()
    {
        if (!m_transform)
            m_transform = transform;
    }
}
