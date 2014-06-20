using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaunchProjectileSingleTargetScript : IProjectile
{

    void FixedUpdate()
    {
        if (m_isLaunch)
        {
            Vector3 distance = m_limit - m_transform.position;

            if (distance.sqrMagnitude > m_minDistance)
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
