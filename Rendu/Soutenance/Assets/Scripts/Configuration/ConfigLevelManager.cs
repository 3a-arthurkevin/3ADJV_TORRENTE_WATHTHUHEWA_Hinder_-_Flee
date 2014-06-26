using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ConfigLevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnManager;
    
    [SerializeField]
    private GameObject movePointManager;

    private SpawnPointManager[] m_spawnManagerScript;
    private MovePointManagerScript[] m_movePointManagerScript;

    private int m_currentSpawnSurvivor = 0;
    private int m_currentSpawnLevelSuvivor = 0;

    void Awake()
    {
        m_spawnManagerScript = spawnManager.GetComponents<SpawnPointManager>();
        m_movePointManagerScript = movePointManager.GetComponents<MovePointManagerScript>();

        Array.Sort(m_spawnManagerScript);
        Array.Sort(m_movePointManagerScript);
    }

    public Vector3 getNextSpawnSurvivor(out int level)
    {
        if (m_currentSpawnSurvivor >= m_spawnManagerScript[m_currentSpawnLevelSuvivor].m_spawnSurvivor.Length)
        {
            ++m_currentSpawnLevelSuvivor;
            m_currentSpawnSurvivor = 0;

            if (m_currentSpawnLevelSuvivor >= m_spawnManagerScript.Length)
                m_currentSpawnLevelSuvivor = 0;
        }

        level = m_currentSpawnLevelSuvivor;
        return m_spawnManagerScript[m_currentSpawnLevelSuvivor].m_spawnSurvivor[m_currentSpawnSurvivor++].position;
    }

    public Vector3 getRandomSpawnZombie(int level)
    {
        if (level >= m_spawnManagerScript.Length || level < 0)
            return m_spawnManagerScript[0].m_spawnZombie[0].position;
        
        else
            return m_spawnManagerScript[level].m_spawnZombie[UnityEngine.Random.Range(0, m_spawnManagerScript[level].m_spawnZombie.Length-1)].position;
    }

    public Vector3 getRandomMoveZombie(int level)
    {
        if (level >= m_movePointManagerScript.Length || level < 0)
            return m_movePointManagerScript[0].movePoint[0].position;

        else
            return m_movePointManagerScript[level].movePoint[UnityEngine.Random.Range(0, m_movePointManagerScript[level].movePoint.Length-1)].position;
    }

    public int getNbLevel()
    {
        try
        {
            return m_spawnManagerScript.Length;
        }
        catch (System.Exception)
        {
            Debug.LogError("Out of bounds getNbLevel");
            return -1;
        }
    }

    public float getPopZombieRatio(int level)
    {
        try
        {
            return m_spawnManagerScript[level].ratioPopZombie;
        }
        catch (System.Exception)
        {
            Debug.Log("Out of bounds getPopZombieRatio : " + level.ToString());
            return 0;
        }
    }
}
