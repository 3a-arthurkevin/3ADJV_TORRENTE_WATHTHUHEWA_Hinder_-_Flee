using UnityEngine;
using System.Collections;
using System;

public class SpawnPointManager : MonoBehaviour, IComparable<SpawnPointManager>
{
    [SerializeField]
    public int m_floor;

    [SerializeField]
    public float ratioPopZombie = 0.4f;
    
    [SerializeField]
    public Transform[] m_spawnSurvivor;

    [SerializeField]
    public Transform[] m_spawnZombie;

    int IComparable<SpawnPointManager>.CompareTo(SpawnPointManager other)
    {
        if (m_floor < other.m_floor)
            return -1;
        
        else if (m_floor > other.m_floor)
            return 1;

        return 0;
    }
}
