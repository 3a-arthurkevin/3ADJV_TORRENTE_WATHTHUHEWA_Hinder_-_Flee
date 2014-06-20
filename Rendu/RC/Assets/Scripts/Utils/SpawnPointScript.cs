using UnityEngine;
using System.Collections;

public class SpawnPointScript : MonoBehaviour {
    public enum SpawnType
    {
        Survivor,
        Zombie
    }
    [SerializeField]
    public SpawnType spawType;

    [SerializeField]
    private Transform m_transform;

    void OnDrawGizmos()
    {
        if (spawType == SpawnType.Zombie)
            Gizmos.color = Color.red;
        
        else if (spawType == SpawnType.Survivor)
            Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(m_transform.position, 2);
    }
}
