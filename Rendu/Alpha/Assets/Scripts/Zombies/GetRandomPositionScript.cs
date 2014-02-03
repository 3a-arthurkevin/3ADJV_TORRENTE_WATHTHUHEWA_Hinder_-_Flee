using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetRandomPositionScript : MonoBehaviour {

    [SerializeField]
    private static List<List<PointScript>> m_allPoint;

    

    public static PointScript getRandomPoint()
    {
        //Tire un point sur la map au hasard
        List<PointScript> tmp = m_allPoint[Random.Range(0, m_allPoint.Capacity)];

        return tmp[Random.Range(0, tmp.Capacity)];
    }

    public static PointScript getRandomPoint(int level)
    {//Retourne un point Random dans l'étage level
        return m_allPoint[level][Random.Range(0, m_allPoint[level].Capacity)];
    }

    private void loadPoint()
    {
        //Rempli la list m_allPoint de tous les points sur la Map
    }
}
