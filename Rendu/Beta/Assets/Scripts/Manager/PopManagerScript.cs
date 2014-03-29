using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PopManagerScript : MonoBehaviour {
    private List<List<Transform>> m_listZombies;

    void Start()
    {
        m_listZombies = new List<List<Transform>>();
    }
}
