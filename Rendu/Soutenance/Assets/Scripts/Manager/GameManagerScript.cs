using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    private PlayerDataBaseScript m_playerDatabase;

    private Dictionary<NetworkPlayer, int> m_playerScore;

    private Dictionary<NetworkPlayer, bool> m_playerAreDead;

    private bool m_lastPlayerAlive = false;
    private float m_timerEndGame = 0f;

    [SerializeField]
    private float m_timeOfEndGame = 60f;

    void Start()
    {
        m_playerAreDead = new Dictionary<NetworkPlayer, bool>();
        m_playerScore = new Dictionary<NetworkPlayer, int>();

        if (m_playerDatabase == null)
            m_playerDatabase = GetComponent<PlayerDataBaseScript>();
    }

    public void initGame()
    {
        Dictionary<NetworkPlayer, Transform> players = m_playerDatabase.Players;

        foreach(KeyValuePair<NetworkPlayer, Transform> pair in players)
        {
            m_playerAreDead.Add(pair.Key, false);
            m_playerScore.Add(pair.Key, 0);
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
