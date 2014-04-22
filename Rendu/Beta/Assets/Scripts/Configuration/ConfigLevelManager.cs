using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ConfigLevelManager
{
    private static bool m_isLoad = false;
    public static bool IsLoad
    {
        get { return m_isLoad; }
    }

    private static List<List<Vector3>> m_spawnSurvivor;
    private static List<List<Vector3>> m_spawnZombie;
    private static List<List<Vector3>> m_movePointZombie;
    private static List<float> m_popZombieRatio;

    private static int m_currentSpawnSurvivor = 0;
    private static int m_currentSpawnLevelSuvivor = 0;

    public static void LoadLevel()
    {
        ConfigLevelManager.m_spawnSurvivor = new List<List<Vector3>>();
        ConfigLevelManager.m_spawnZombie = new List<List<Vector3>>();
        ConfigLevelManager.m_movePointZombie = new List<List<Vector3>>();
        ConfigLevelManager.m_popZombieRatio = new List<float>();

        switch (Application.loadedLevelName)
        {
            case "Level_1":
                ConfigLevelManager.LoadLevelOne();
                break;

            case "Level_2":
                ConfigLevelManager.LoadLevelTwo();
                break;
        }

        ConfigLevelManager.m_isLoad = true;
    }

    static void LoadLevelOne()
    {
        //Pop zombie ratio
        m_popZombieRatio.Add(0.4f);
        m_popZombieRatio.Add(0.2f);

        /*Foreach Empty GameObject in Pop gameObject in Floor gameObject
         * get world position and add into List of Vector3
         */
        GameObject root = GameObject.Find("Level_1");
        
        if (root == null)
        {
            Debug.LogError("Level_1 not found");
            return;
        }

        Transform rootTransform = root.transform;
        int nbChild = rootTransform.childCount;

        for (int i = 0; i < nbChild; ++i)
        {
            //Transform floor = rootTransform.GetChild(i);
            Transform floor = rootTransform.Find("Floor" + i.ToString());

            if (floor != null && floor.name.Substring(0, 5) == "Floor")
            {
                Debug.LogError(floor.name);

                //Pop point
                Transform pop = floor.Find("Pop");
                m_spawnSurvivor.Add(new List<Vector3>());
                m_spawnZombie.Add(new List<Vector3>());

                if (pop == null)
                    Debug.LogError("In Floor : " + i + ", no Pop gameobject found");

                else
                {
                    Transform popSurvivor = pop.Find("Survivor");
                    
                    if (popSurvivor == null)
                        Debug.LogError("Survivor GameObject not found in Pop GameObject");
                    else
                    {
                        int nbPoint = popSurvivor.childCount;
                        for (int point = 0; point < nbPoint; ++point)
                            m_spawnSurvivor[i].Add(popSurvivor.GetChild(point).transform.position);
                    }

                    Transform popZombie = pop.Find("Zombie");

                    if (popZombie == null)
                        Debug.LogError("Zombie GameObject not found in Pop GameObject");
                    else
                    {
                        int nbPoint = popSurvivor.childCount;
                        for (int point = 0; point < nbPoint; ++point)
                            m_spawnZombie[i].Add(popZombie.GetChild(point).transform.position);
                    }
                }

                Transform movePoint = floor.Find("MovePoint");
                m_movePointZombie.Add(new List<Vector3>());

                if (movePoint == null)
                    Debug.LogError("In Floor : " + i + ", no MovePoint gameobject found");
                else
                {
                    Transform movePointZombie = movePoint.Find("Zombie");

                    if (movePointZombie == null)
                        Debug.LogError("Zombie GameObject not found in MovePoint GameObject");
                    else
                    {
                        int nbPoint = movePointZombie.childCount;
                        for (int point = 0; point < nbPoint; ++point)
                            m_movePointZombie[i].Add(movePointZombie.GetChild(point).transform.position);
                    }
                }
            }
        }
    }

    static void LoadLevelTwo()
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

        //Pop zombie ratio
        m_popZombieRatio.Add(0.4f);
        m_popZombieRatio.Add(0.2f);
    }

    public static Vector3 getNextSpawnSurvivor(out int level)
    {
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
        if (level >= m_spawnZombie.Count || level < 0)
            return m_spawnZombie[0][0];
        
        else
            return m_spawnZombie[level][Random.Range(0, m_spawnZombie[level].Count)];
    }

    public static Vector3 getRandomMoveZombie(int level)
    {
        if (level >= m_movePointZombie.Count || level < 0)
            return m_movePointZombie[0][0];

        else
            return m_movePointZombie[level][Random.Range(0, m_movePointZombie[level].Count)];
    }

    public static int getNbLevel()
    {
        try
        {
            return m_spawnZombie.Count;
        }
        catch (System.Exception)
        {
            Debug.LogError("Out of bounds getNbLevel");
            return -1;
        }
    }

    public static float getPopZombieRatio(int level)
    {
        try
        {
            return m_popZombieRatio[level];
        }
        catch (System.Exception)
        {
            Debug.Log("Out of bounds getPopZombieRatio : " + level.ToString());
            return 0;
        }
    }
}
