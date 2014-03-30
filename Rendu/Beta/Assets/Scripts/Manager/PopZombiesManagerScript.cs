using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PopZombiesManagerScript : MonoBehaviour {
    [SerializeField]
    private Transform m_prefabZombie;
    
    private List<List<Transform>> m_listZombies;

    
    [SerializeField]
    private float m_waitingTime = 30f;

    private float m_timer = 0;
    private bool m_manage = false;
    
    void Start()
    {
        if (Network.isClient)
            enabled = false;

        m_listZombies = new List<List<Transform>>();
        enabled = false;
    }

    void Update()
    {
        m_timer += Time.deltaTime;
    }

    public void init()
    {
        m_manage = true;

        enabled = true;
        StartCoroutine(managePop());
    }

    IEnumerator managePop()
    {
        while(m_manage)
        {

            yield return new WaitForSeconds(m_waitingTime);
        }
    }

    public void zombieDied(int level, Transform died)
    {
        if (level < m_listZombies.Count)
        {
            m_listZombies[level].Remove(died);
            Network.Destroy(died.gameObject);
        }
        else
            Debug.LogError("Out of bound listZombie array in zombieDied function");
    }
}
