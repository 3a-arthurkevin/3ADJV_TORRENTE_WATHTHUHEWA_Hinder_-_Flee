using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    private NetworkView m_networkView;

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

    /* GUI PART */
    private Rect m_timerPosition = new Rect(Screen.width / 2, 20, 100, 25);

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

    public void survivorDied(NetworkViewID survivor)
    {
        if(!m_playerAreDead.ContainsKey(survivor))
        {
            Debug.LogError("Survivor not in m_playerAreDead");
            return;
        }

        m_playerAreDead[survivor] = true;

        int deadPlayer = m_playerAreDead.Where<KeyValuePair<NetworkViewID, bool>>(item => item.Value == true).Count();

        if ( deadPlayer == m_playerAreDead.Count - 1)
        {// One player stay Alive
            m_networkView.RPC("setLastAlive", RPCMode.OthersBuffered, true);
            m_lastPlayerAlive = true;
        }
    }

    [RPC]
    void setLastAlive(bool lastAlive)
    {
        m_lastPlayerAlive = lastAlive;
    }

    void gameFinish()
    {
        Debug.LogError("Game is finish");
        m_lastPlayerAlive = false;
    }

    void OnGUI()
    {
        if(Network.isClient && m_lastPlayerAlive)
        {//Display time for endGame
            GUI.Label(m_timerPosition, (m_timeOfEndGame - m_timerEndGame).ToString("F2"));
        }
    }
}
