using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveManagerZombieScript : MonoBehaviour {

    [SerializeField]
    private NetworkView m_networkView;

    [SerializeField]
    private float m_minDistance = 2f;

    [SerializeField]
    private float m_defaultSpeed = 2f;

    private Dictionary<string, MoveData> m_zombies;

    void Start()
    {
        if (m_networkView == null)
            m_networkView = networkView;

        m_zombies = new Dictionary<string, MoveData>();
    }

    void FixedUpdate()
    {
        MoveData data;
        foreach(KeyValuePair<string, MoveData> item in m_zombies)
        {
            data = item.Value;

            if (data.Path != null)
            {
                Vector3 direction = data.Path.corners[data.NumCorner] - data.Position.position;
                direction.y = 0;

                if (direction.sqrMagnitude < m_minDistance)
                {
                    if ((data.NumCorner + 1) >= data.Path.corners.Length)
                        data.Path = null;

                    else
                        data.Position.LookAt(data.Path.corners[++data.NumCorner]);

                    data.IsMoved = false;
                }
                else
                    data.Position.position += direction.normalized * data.Speed * Time.deltaTime;
            }
        }
    }
}
