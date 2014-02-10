using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConfigLevelManager : MonoBehaviour
{

    static int m_currentSpawn = 0;
    static List<Vector3> m_spawn = null;

    public static List<List<PointScript>> getMovePointForFirtsLevel()
    {
        List<List<PointScript>> levelOne = new List<List<PointScript>>();
        List<PointScript> firstFloor = new List<PointScript>();
        PointScript point = ScriptableObject.CreateInstance<PointScript>();

        point.Level = 1;

        point.Position = new Vector3(-10, 0, 0);
        firstFloor.Add(point.Clone());

        point.Position = new Vector3(0, 0, 0);
        firstFloor.Add(point.Clone());

        point.Position = new Vector3(10, 0, 0);
        firstFloor.Add(point.Clone());

        List<PointScript> secondFloor = new List<PointScript>();
        point.Level = 2;

        point.Position = new Vector3(-10, 0, -35);
        secondFloor.Add(point.Clone());

        point.Position = new Vector3(0, 0, -35);
        secondFloor.Add(point.Clone());

        point.Position = new Vector3(10, 0, -35);
        secondFloor.Add(point.Clone());

        levelOne.Add(firstFloor);
        levelOne.Add(secondFloor);

        return levelOne;
    }

    public static Vector3 getNextSpawnForLevelOne()
    {
        if (m_spawn == null)
            fillSpawnLevelOne();
        
        if (m_currentSpawn == m_spawn.Count)
            m_currentSpawn = 0;

        return m_spawn[m_currentSpawn++];
    }

    private static void fillSpawnLevelOne()
    {
        m_spawn = new List<Vector3>();

        m_spawn.Add(new Vector3(0, 1, 0));
        m_spawn.Add(new Vector3(10, 1, 10));
    }
}
