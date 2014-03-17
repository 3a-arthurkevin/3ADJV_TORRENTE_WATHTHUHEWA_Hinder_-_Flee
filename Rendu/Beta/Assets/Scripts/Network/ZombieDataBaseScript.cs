using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZombieDataBaseScript : MonoBehaviour {

    private Dictionary<string, Transform> m_zombie;

    [SerializeField]
    private Transform m_zombiePrefab;

    void Start()
    {
    }
}
