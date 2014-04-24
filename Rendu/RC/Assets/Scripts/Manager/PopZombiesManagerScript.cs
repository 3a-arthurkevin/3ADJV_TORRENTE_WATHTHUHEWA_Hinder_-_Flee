using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PopZombiesManagerScript : MonoBehaviour {
    [SerializeField]
    private Transform m_prefabZombie;
    
    private List<List<Transform>> m_listZombies;
    
    [SerializeField]
    private float m_respawnTime = 30f;

    [SerializeField]
    private float m_waveTime = 1f;

    private float m_timer = 0;
    private bool m_manage = false;
    
    void Awake()
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
        int nbLevel = ConfigLevelManager.getNbLevel();

        for(int i = 0; i < nbLevel; ++i)
            m_listZombies.Add(new List<Transform>());

        enabled = true;
        m_timer = 0;
        StartCoroutine(managePop());
    }

    IEnumerator managePop()
    {
        int nbLevel = m_listZombies.Count;

        while(m_manage)
        {
            for (int i = 0; i < nbLevel; ++i)
            {
                int nbCurZombie = m_listZombies[i].Count;
                int nbZombieVoulu = (int)(m_timer * ConfigLevelManager.getPopZombieRatio(i));
                
                StartCoroutine(zombieLauncher(i, nbZombieVoulu - nbCurZombie));
            }
            
            yield return new WaitForSeconds(m_respawnTime);
        }
    }

    IEnumerator zombieLauncher(int level, int nbZombie)
    {
        Transform tempZombie = null;

        for (int i = 0; i < nbZombie; ++i)
        {
            tempZombie = (Transform)Network.Instantiate(m_prefabZombie, ConfigLevelManager.getRandomSpawnZombie(level), Quaternion.identity, 0);
            
            tempZombie.GetComponent<MoveManagerZombieScript>().Data.IsInFloor = level;
            m_listZombies[level].Add(tempZombie);
            tempZombie = null;

            yield return new WaitForSeconds(m_waveTime);
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
