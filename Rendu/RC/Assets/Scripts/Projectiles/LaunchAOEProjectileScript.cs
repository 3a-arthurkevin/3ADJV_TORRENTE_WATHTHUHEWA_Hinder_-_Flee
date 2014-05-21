using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaunchAOEProjectileScript : MonoBehaviour
{
    [SerializeField]
    private Transform m_transform;

    private NetworkViewID m_launcher;
    public NetworkViewID Launcher
    {
        get { return m_launcher; }
        set { m_launcher = value; }
    }

    private float m_speed;
    public float Speed
    {
        get { return m_speed; }
        set { m_speed = value; }
    }

    private float m_minDistance;
    public float MinDistance
    {
        get { return m_minDistance; }
        set { m_minDistance = value; }
    }

    private float m_duration = 0f;
    public float Duration
    {
        get { return m_duration; }
        set { m_duration = value; }
    }

    private bool m_isLaunch = false;
    public bool IsLaunch
    {
        get { return m_isLaunch; }
    }

    private bool m_goToTarget = false;
    public bool GoToTarget
    {
        get { return m_goToTarget; }
    }

    private Vector3 m_target = Vector3.zero;
    public Vector3 Target
    {
        get { return m_target; }
        set
        {
            m_target = value;
            m_goToTarget = true;
        }
    }

    public float m_timer;

    private float m_aoeSize;
    public float AoeSize
    {
        get { return m_aoeSize; }
        set { m_aoeSize = value; }
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
        if (m_transform)
            m_transform = transform;

        m_timer = 0;
    }

    void Update()
    {
        if (m_isLaunch)
        {
            m_timer += Time.deltaTime;

            if (m_timer >= m_duration)
                Destroy(gameObject);
        }
        else
        {
            m_transform.position += m_target * m_speed * Time.deltaTime;

            if ((m_transform.position - m_target).sqrMagnitude <= m_minDistance)
            {
                m_goToTarget = false;
                m_isLaunch = true;
                m_transform.localScale = new Vector3(m_aoeSize, 0.1f, m_aoeSize);
            }
        }
            
    }

    void OnTriggerEnter(Collider col)
    {
        if (m_isLaunch)
            if (col.networkView.viewID != m_launcher)
                m_applyEffect(col.gameObject);
    }
}