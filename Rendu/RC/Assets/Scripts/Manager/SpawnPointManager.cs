using UnityEngine;
using System.Collections;

public class SpawnPointManager : MonoBehaviour {
    [SerializeField]
    public int m_floor;
    
    [SerializeField]
    public Transform[] m_spawnSurvivor;

    [SerializeField]
    public Transform[] m_spawnZombie;
}
