using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PopZombiesManagerScript : MonoBehaviour {
    [SerializeField]
    private Transform m_prefabZombie;

    [SerializeField]
    private GameObject ConfigLevelManager;
    private ConfigLevelManager m_configLevelManagerScript;
    
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

        m_configLevelManagerScript = ConfigLevelManager.GetComponent<ConfigLevelManager>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        m_timer += Time.deltaTime;
    }

    public void init()
    {
        int nbLevel = m_configLevelManagerScript.getNbLevel();

        for(int i = 0; i < nbLevel; ++i)
            m_listZombies.Add(new List<Transform>());

        enabled = true;
        m_manage = true;
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
                int nbZombieVoulu = (int)(m_timer * m_configLevelManagerScript.getPopZombieRatio(i));
                
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
            tempZombie = (Transform)Network.Instantiate(m_prefabZombie, m_configLevelManagerScript.getRandomSpawnZombie(level), Quaternion.identity, 0);

            MoveManagerZombieScript move = tempZombie.GetComponent<MoveManagerZombieScript>();
            move.Data.IsInFloor = level;
            move.ConfigLevelManager = m_configLevelManagerScript;

            m_listZombies[level].Add(tempZombie);
            tempZombie = null;

            yield return new WaitForSeconds(m_waveTime);
        }
    }

    public void zombieDied(int level, Transform died)
    {
        Debug.LogError("is server ? " + Network.isServer);
        Debug.LogError("level : " + level);
        Debug.LogError("m_list : " + m_listZombies);

        if (level < m_listZombies.Count)
        {
            m_listZombies[level].Remove(died);
        }
        else
            Debug.LogError("Out of bound listZombie array in zombieDied function");
    }

    public void killAll()
    {
        foreach(List<Transform> list in m_listZombies)
        {
            foreach(Transform t in list)
                Network.Destroy(t.gameObject);

            list.Clear();
        }

        m_listZombies.Clear();
    }

    void OnGUI()
    {
        if(Network.isServer)
        {//Draw Stats of game (nbZombie pop, ...)

        }
    }
}
