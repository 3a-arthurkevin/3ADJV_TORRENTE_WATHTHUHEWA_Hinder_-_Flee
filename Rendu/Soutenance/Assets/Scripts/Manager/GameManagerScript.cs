using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        m_playerAreDead = new Dictionary<NetworkViewID, bool>();
        m_playerScore = new Dictionary<NetworkViewID, int>();

        if (m_playerDatabase == null)
            m_playerDatabase = GetComponent<PlayerDataBaseScript>();

        if (m_popZombieManager == null)
            m_popZombieManager = GetComponent<PopZombiesManagerScript>();
    }

    public void initGame()
    {
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

    void lunchTimer()
    {
 
    }

    void gameFinish()
    {

    }

    void OnGUI()
    {
        if(Network.isClient && m_lastPlayerAlive)
        {//Display time for endGame

        }
    }
}
