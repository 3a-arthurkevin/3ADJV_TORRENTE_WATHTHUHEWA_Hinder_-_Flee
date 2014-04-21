using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaunchProjectileSingleTargetScript : MonoBehaviour
{
    [SerializeField]
    private Transform m_transform;

    private NetworkPlayer m_launcher;
    public NetworkPlayer Launcher
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

    private Vector3 m_target = Vector3.zero;
    public Vector3 Target
    {
        get { return m_target; }
        set
        {
            m_target = value;
            m_isLaunch = true;
            m_target.y = 0;
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
        Debug.LogError("Start Projo");
        if (m_transform == null)
            m_transform = transform;
    }

    void FixedUpdate()
    {
        if (m_isLaunch)
        {
            Vector3 direction = m_target - m_transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.2f)
                m_transform.position += direction.normalized * Time.deltaTime * m_speed;

            else
            {
                Debug.LogError("Destroy Projo");
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.LogError("TriggerEnter");

        if (col.tag == "Zombie" || col.tag == "Survivor")
        {
            GameObject target = col.gameObject;
            m_applyEffect(target);
        }

        Destroy(gameObject);
    }
}
