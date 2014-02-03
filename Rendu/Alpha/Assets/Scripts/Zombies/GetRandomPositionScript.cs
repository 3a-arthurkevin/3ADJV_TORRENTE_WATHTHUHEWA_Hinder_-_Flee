using UnityEngine;
using System.Collections;

/*
 * Retourne un des points dans l'étage correspondant à l'étage du zombie
 */

public class GetRandomPositionScript : MonoBehaviour {

    [SerializeField]
    private static PointScript[] m_allPoint;

    public static Transform getRandomPoint()
    {
        
        return m_allPoint[Random.Range(0, m_allPoint.Length)].Position;
    }
}
