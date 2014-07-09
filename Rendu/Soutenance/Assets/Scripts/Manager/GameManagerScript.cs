using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    private PlayerDataBaseScript m_playerDatabase;

    [SerializeField]
    public PopZombiesManagerScript m_popZombieManager;

    private Dictionary<NetworkViewID, int> m_playerScore;

    private Dictionary<NetworkViewID, bool> m_playerAreDead;

    private bool m_lastPlayerAlive = false;
    private float m_timerEndGame = 0f;

    [SerializeField]
    private float m_timeOfEndGame = 60f;

    void Start()
    {
        Debug.LogError("START GAME MANAGER");
        m_playerAreDead = new Dictionary<NetworkViewID, bool>();
        m_playerScore = new Dictionary<NetworkViewID, int>();

        if (m_playerDatabase == null)
            m_playerDatabase = GetComponent<PlayerDataBaseScript>();

        if (m_popZombieManager == null)
            m_popZombieManager = GetComponent<PopZombiesManagerScript>();
    }

    public void initGame()
    {
        Debug.LogError("INIT");

        if (m_playerAreDead == null)
            Debug.LogError("INITNULL");

        Dictionary<NetworkPlayer, Transform> players = m_playerDatabase.Players;

        foreach (KeyValuePair<NetworkPlayer, Transform> pair in players)
        {
            NetworkViewID nid = pair.Value.networkView.viewID;
            m_playerScore.Add(nid, 0);
            m_playerAreDead.Add(nid, false);
        }
    }

    void Update()
    {
        if(m_lastPlayerAlive)
        {
            m_timerEndGame += Time.deltaTime;

            if (m_timerEndGame >= m_timeOfEndGame)
                gameFinish();
        }
    }

    public void survivorDied(NetworkViewID survivor)
    {

        if (m_playerAreDead == null)
        {
            Debug.LogError("NULL");
            return;
        }

        if(!m_playerAreDead.ContainsKey(survivor))
        {
            Debug.LogError("Survivor not in m_playerAreDead");
            return;
        }

        m_playerAreDead[survivor] = true;

        int deadPlayer = m_playerAreDead.Where<KeyValuePair<NetworkViewID, bool>>(item => item.Value == true).Count();

        if ( deadPlayer == m_playerAreDead.Count - 1)
        {// One player stay Alive
            m_lastPlayerAlive = true;
        }
    }

    void gameFinish()
    {
        Debug.LogError("Game is finish");
    }

    void OnGUI()
    {
        if(Network.isClient && m_lastPlayerAlive)
        {//Display time for endGame

        }
    }
}
