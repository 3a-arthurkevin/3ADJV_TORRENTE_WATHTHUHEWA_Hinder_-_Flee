using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaunchAOEProjectileScript : IProjectile
{
    private float m_timer;

    private List<Transform> m_alreadyHit;

    private float m_duration = 0f;
    public float Duration
    {
        get { return m_duration; }
        set { m_duration = value; }
    }

    private float m_aoeSize;
    public float AoeSize
    {
        get { return m_aoeSize; }
        set { m_aoeSize = value; }
    }

    protected Vector3 m_target = Vector3.zero;
    public Vector3 Target
    {
        get { return m_target; }
        set
        {
            m_target = value;
            m_isLaunch = true;
        }
    }
    

    private bool m_arrived = false;

    override public void Start()
    {
        if (m_transform)
            m_transform = transform;

        m_timer = 0;
        m_alreadyHit = new List<Transform>();
    }

    void FixedUpdate()
    {
        if (m_isLaunch && !m_arrived)
        {
            Vector3 distance = m_target - m_transform.position;

            if (distance.sqrMagnitude > m_minDistance)
                m_transform.position += distance.normalized * Time.deltaTime * m_speed;

            else
            {
                m_transform.localScale = new Vector3(m_aoeSize, 0.1f, m_aoeSize);
                m_arrived = true;
            }
        }
    }

    void Update()
    {
        if (m_arrived)
        {
            m_timer += Time.deltaTime;

            if (m_timer >= m_duration)
                Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {

        if (m_arrived && !m_alreadyHit.Contains(col.transform))
            if (col.networkView != null && col.networkView.viewID != m_launcher)
            {
                m_applyEffect(col.gameObject);
                m_alreadyHit.Add(col.transform);
            }
    }
}