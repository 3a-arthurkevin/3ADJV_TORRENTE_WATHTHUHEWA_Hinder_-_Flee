using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaunchProjectileSingleTargetScript : IProjectile
{
    private bool m_alreadyHit = false;
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
        if (col.tag == "Wall")
        {
            Destroy(gameObject);
            return;
        }

        if (col.networkView.viewID != m_launcher && !m_alreadyHit)
        {
            if (Network.isServer)
            {
                m_applyEffect(col.gameObject);
                m_alreadyHit = true;
            }

            Destroy(gameObject);
        }
    }
}
