using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaunchAOEProjectileScript : MonoBehaviour
{
    [SerializeField]
    private Transform m_transform;

    private NetworkPlayer m_launcher;
    public NetworkPlayer Launcher
    {
        get { return m_launcher; }
        set { m_launcher = value; }
    }

    private List<IEffect> m_effectZombie;
    public List<IEffect> EffectZombie
    {
        set { m_effectZombie = value; }
    }

    private List<IEffect> m_effectSurvivor;
    public List<IEffect> EffectSurvivor
    {
        set { m_effectSurvivor = value; }
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

    private Vector3 m_target = Vector3.zero;
    public Vector3 Target
    {
        get { return m_target; }
        set
        {
            m_target = value;
            m_isLaunch = true;
        }
    }

    void Start()
    {
        if (m_transform)
            m_transform = transform;
    }

    void Update()
    {
        if (m_isLaunch)
        {
            m_duration -= Time.deltaTime;
            if (m_duration <= 0f)
                Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {//Applique les effets à la cible rencontrer si Survivor ou Zombie
        if (m_isLaunch)
        {
            if (col.tag == "Zombie")
                foreach (IEffect effect in m_effectZombie)
                    effect.Apply(col.gameObject);

            else if (col.tag == "Survivor")
                foreach (IEffect effect in m_effectSurvivor)
                    effect.Apply(col.gameObject);

            else
                Debug.Log("Collide with wall");
        }
    }
}