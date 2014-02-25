using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetRandomPositionScript : MonoBehaviour {

    [SerializeField]
    private static List<List<PointScript>> m_allPoint = null;

    public static PointScript getRandomPoint()
    {
        if (m_allPoint == null)
            loadPoint();
        
        List<PointScript> tmp = m_allPoint[Random.Range(0, m_allPoint.Count)];

        return tmp[Random.Range(0, tmp.Count)];
    }

    public static PointScript getRandomPoint(int level)
    {//Retourne un point Random dans l'étage level

        if (m_allPoint == null)
            loadPoint();

        return m_allPoint[level][Random.Range(0, m_allPoint[level].Count)];
    }

    private static void loadPoint()
    {
        //Rempli la list m_allPoint de tous les points sur la Map
        m_allPoint = ConfigLevelManager.getMovePointForFirtsLevel();
    }
}

