using UnityEngine;
using System.Collections;

public class PointScript : MonoBehaviour {
    public enum PointType
    {
        SpawnSurvivor,
        SpawnZombie,
        MoveZombie
    }

    [SerializeField]
    public PointType pointType;

    [SerializeField]
    private Transform m_transform;

    void OnDrawGizmos()
    {
        if (m_transform == null)
            return;

        switch(pointType)
        {
            case PointType.SpawnZombie:
                Gizmos.color = Color.red;
                break;
            case PointType.SpawnSurvivor:
                Gizmos.color = Color.blue;
                break;

            case PointType.MoveZombie:
                Gizmos.color = Color.green;
                break;
        }

        Gizmos.DrawWireSphere(m_transform.position, 1);
    }
}
