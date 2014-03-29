using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PopZombiesManagerScript : MonoBehaviour {
    private List<List<Transform>> m_listZombies;
    
    [SerializeField]
    private float m_waitingTime = 60f;

    private float m_timer = 0;
    private bool m_manage = false;
    
    void Start()
    {
        if (Network.isClient)
            enabled = false;

        m_listZombies = new List<List<Transform>>();
    }

    void Update()
    {
        m_timer += Time.deltaTime;
    }

    public void init()
    {
        m_manage = true;
        
        StartCoroutine(managePop());
    }

    IEnumerator managePop()
    {
        while(m_manage)
        {

            yield return new WaitForSeconds(m_waitingTime);//Wait 60 seconde for re launch function
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
