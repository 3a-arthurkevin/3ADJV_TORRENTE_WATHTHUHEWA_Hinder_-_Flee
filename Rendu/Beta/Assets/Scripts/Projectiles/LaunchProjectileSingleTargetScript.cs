using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaunchProjectileSingleTargetScript : MonoBehaviour
{
    [SerializeField]
    private Transform m_transform;

    [SerializeField]
    private NetworkView m_networkView;

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
        }
    }

    void Start()
    {
        Debug.LogError("Start Projo");
        if (m_transform)
            m_transform = transform;
    }

    void Update()
    {
        if (m_isLaunch)
        {
            Vector3 direction = m_transform.position - m_target;

            if (direction.sqrMagnitude > 0.2f)
                m_transform.position += direction.normalized * Time.deltaTime * m_speed;

            else
                if (Network.isServer)
                    Network.Destroy(gameObject);            
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (Network.isServer)
            if (col.tag == "Zombie" || col.tag == "Survivor")
                m_networkView.RPC("ApplyEffect", RPCMode.All, col.networkView.viewID);
            
            else
                Network.Destroy(gameObject);
    }

    [RPC]
    void ApplyEffect(NetworkViewID idTarget)
    {
        NetworkView networkView = NetworkView.Find(idTarget);

        if (!networkView)
        {
            Debug.Log("Target not found");
            return;
        }

        GameObject target = networkView.gameObject;

        if (target.tag == "Zombie")
            foreach (IEffect effect in m_effectZombie)
                effect.Apply(target);

        else if (target.tag == "Survivor")
            foreach (IEffect effect in m_effectSurvivor)
                effect.Apply(target);

        if (Network.isServer)
            Network.Destroy(gameObject);
    }
}
