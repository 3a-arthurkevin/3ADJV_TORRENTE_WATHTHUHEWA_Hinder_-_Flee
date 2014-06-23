using UnityEngine;
using System.Collections;

public class PointScript : MonoBehaviour {
    public enum PointType
    {
        SpawnSurvivor,
        SpawnZombie,
        MoveZombie,
        Item,
        Dealer
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

            case PointType.Item:
                Gizmos.color = Color.yellow;
                break;

            case PointType.Dealer:
                Gizmos.color = Color.white;
                break;
        }

        Gizmos.DrawWireSphere(m_transform.position, 1);
    }
}
