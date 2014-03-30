using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConfigLevelManager : MonoBehaviour
{

    private static List<List<Vector3>> m_spawnSurvivor;
    private static List<List<Vector3>> m_spawnZombie;
    private static List<List<Vector3>> m_movePointZombie;

    private static int m_currentSpawnSurvivor = 0;
    private static int m_currentSpawnLevelSuvivor = 0;

    public static void LoadLevel()
    {
        ConfigLevelManager.m_spawnSurvivor = new List<List<Vector3>>();
        ConfigLevelManager.m_spawnZombie = new List<List<Vector3>>();
        ConfigLevelManager.m_movePointZombie = new List<List<Vector3>>();

        switch (Application.loadedLevelName)
        {
            case "Level_1":
                ConfigLevelManager.LoadLevelOne();
                break;

            case "Level_2":
                ConfigLevelManager.LoadLevelTwo();
                break;
        }
    }

    static void LoadLevelOne()
    {
        //Load Spawn Survivor
        m_spawnSurvivor.Add(new List<Vector3>());
        m_spawnSurvivor[0].Add(new Vector3(10, 1, 70));
        m_spawnSurvivor[0].Add(new Vector3(30, 1, 70));

        //Load Spawn Zombie
        m_spawnZombie.Add(new List<Vector3>());
        m_spawnZombie[0].Add(new Vector3(10.5f, 1, 95));
        m_spawnZombie[0].Add(new Vector3(20, 1, 95));
        m_spawnZombie[0].Add(new Vector3(-1, 1, 52));
        m_spawnZombie[0].Add(new Vector3(30, 1, 52));

        m_spawnZombie.Add(new List<Vector3>());
        m_spawnZombie[1].Add(new Vector3(30, 1, -60));
        m_spawnZombie[1].Add(new Vector3(7, 1, -60));

        //Load Move point Zombie
        m_movePointZombie.Add(new List<Vector3>());
        m_movePointZombie[0].Add(new Vector3(5, 1, 92));
        m_movePointZombie[0].Add(new Vector3(21, 1, 92));
        m_movePointZombie[0].Add(new Vector3(40, 1, 92));
        m_movePointZombie[0].Add(new Vector3(9, 1, 62));
        m_movePointZombie[0].Add(new Vector3(28, 1, 63));
        m_movePointZombie[0].Add(new Vector3(40, 1, 72));
        m_movePointZombie[0].Add(new Vector3(40, 1, 53));
        m_movePointZombie[0].Add(new Vector3(25, 1, 53));
        m_movePointZombie[0].Add(new Vector3(28, 1, 53));
        m_movePointZombie[0].Add(new Vector3(13, 1, 53));
        m_movePointZombie[0].Add(new Vector3(0, 1, 53));
        m_movePointZombie[0].Add(new Vector3(0, 1, 75));

        m_movePointZombie.Add(new List<Vector3>());
        m_movePointZombie[1].Add(new Vector3(17, 1, -34));
        m_movePointZombie[1].Add(new Vector3(17, 1, -47));
        m_movePointZombie[1].Add(new Vector3(24, 1, -34));
        m_movePointZombie[1].Add(new Vector3(35, 1, -42));
        m_movePointZombie[1].Add(new Vector3(35, 1, -63));
        m_movePointZombie[1].Add(new Vector3(5, 1, -63));
    }

    static void LoadLevelTwo()
    {
    }

    public static Vector3 getNextSpawnSurvivor(out int level)
    {
        if (m_spawnSurvivor == null)
            ConfigLevelManager.LoadLevel();

        if (m_currentSpawnSurvivor >= m_spawnSurvivor[m_currentSpawnLevelSuvivor].Count)
        {
            ++m_currentSpawnLevelSuvivor;
            m_currentSpawnSurvivor = 0;

            if (m_currentSpawnLevelSuvivor >= m_spawnSurvivor.Count)
                m_currentSpawnLevelSuvivor = 0;
        }

        level = m_currentSpawnLevelSuvivor;
        return m_spawnSurvivor[m_currentSpawnLevelSuvivor][m_currentSpawnSurvivor++];
    }

    public static Vector3 getRandomSpawnZombie(int level)
    {
        if (m_spawnZombie == null)
            ConfigLevelManager.LoadLevel();

        if (level >= m_spawnZombie.Count || level < 0)
            return m_spawnZombie[0][0];
        
        else
            return m_spawnZombie[level][Random.Range(0, m_spawnZombie[level].Count)];
    }

    public static Vector3 getRandomMoveZombie(int level)
    {
        if (m_movePointZombie == null)
            ConfigLevelManager.LoadLevel();

        if (level >= m_movePointZombie.Count || level < 0)
            return m_movePointZombie[0][0];

        else
            return m_movePointZombie[level][Random.Range(0, m_movePointZombie[level].Count)];
    }
    
    /*
    static int m_currentSpawn = 0;
    static List<Vector3> m_spawn = null;

    public static List<List<Point>> getMovePointForFirtsLevel()
    {
        List<List<Point>> levelOne = new List<List<Point>>();
        List<Point> firstFloor = new List<Point>();
        Point point = new Point();

        point.Level = 1;

        point.Position = new Vector3(-10, 0, 0);
        firstFloor.Add(point.Clone());

        point.Position = new Vector3(0, 0, 0);
        firstFloor.Add(point.Clone());

        point.Position = new Vector3(10, 0, 0);
        firstFloor.Add(point.Clone());

        List<Point> secondFloor = new List<Point>();
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

    public static Vector3 getNextSpawn()
    {
        if (Application.loadedLevelName == "Level_1")
            return getNextSpawnForLevelOne();
        else if (Application.loadedLevelName == "Level_2")
            return getNextSpawnForLevelTwo();

        return Vector3.zero;
    }

    private static Vector3 getNextSpawnForLevelOne()
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

        m_spawn.Add(new Vector3(10, 1, 70));
        m_spawn.Add(new Vector3(30, 1, 70));
    }

    private static Vector3 getNextSpawnForLevelTwo()
    {
        if (m_spawn == null)
            fillSpawnLevelTwo();

        if (m_currentSpawn == m_spawn.Count)
            m_currentSpawn = 0;

        return m_spawn[m_currentSpawn++];
    }

    private static void fillSpawnLevelTwo()
    {
        m_spawn = new List<Vector3>();

        m_spawn.Add(new Vector3(10, 1, 70));
        m_spawn.Add(new Vector3(30, 1, 70));
    }
    */
}
