using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaunchProjectileSingleTargetScript : MonoBehaviour
{
    [SerializeField]
    private Transform m_transform;

    private NetworkViewID m_launcher;
    public NetworkViewID Launcher
    {
        get { return m_launcher; }
        set { m_launcher = value; }
    }

    private float m_speed = 2f;
    public float Speed
    {
        get { return m_speed; }
        set { m_speed = value; }
    }

    private bool m_isLaunch = false;
    public bool IsLaunch
    {
        get { return m_isLaunch; }
    }

    private Vector3 m_direction = Vector3.zero;
    public Vector3 Direction
    {
        get { return m_direction; }
        set
        {
            m_direction = (value - m_transform.position).normalized;
            m_direction.y = 0;
        }
    }

    private Vector3 m_limit = Vector3.zero;
    public float Limit
    {
        set
        {
            m_limit = (m_direction * value) + m_transform.position;
            m_isLaunch = true;
        }
    }

    public delegate void ApplySkillEffect(GameObject target);
    private ApplySkillEffect m_applyEffect;
    public ApplySkillEffect ApplyEffect
    {
        get { return m_applyEffect; }
        set { m_applyEffect = value; }
    }

    void Start()
    {
        if (m_transform == null)
            m_transform = transform;
    }

    void FixedUpdate()
    {
        if (m_isLaunch)
        {
            Vector3 distance = m_limit - m_transform.position;

            if (distance.sqrMagnitude > 0.1f)
                m_transform.position += distance.normalized * Time.deltaTime * m_speed;

            else
                Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.networkView.viewID != m_launcher)
        {
            if(Network.isServer)
                m_applyEffect(col.gameObject);
        }
        else
            Destroy(gameObject);
    }
}
