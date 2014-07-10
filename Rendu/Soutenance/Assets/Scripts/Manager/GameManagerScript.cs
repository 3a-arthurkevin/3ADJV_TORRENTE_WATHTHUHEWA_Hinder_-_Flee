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
    private bool m_allPlayerLoose = false;
    private bool m_gameFinish = false;

    [SerializeField]
    private float m_timeOfEndGame = 60f;

    /* GUI PART */
    private Rect m_timerPosition = new Rect(Screen.width / 2, 20, 100, 25);
    private Rect m_finishMessagePosition = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 250, 25);
    private Rect m_ButtonQuit = new Rect(Screen.width / 2 - 50, Screen.height / 2 + 50, 80, 25);
    private string m_gameFinishMessage = "";

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

        if(m_lastPlayerAlive)
        {
            m_allPlayerLoose = true;
            gameFinish();
        }
        else
        {
            m_playerAreDead[survivor] = true;

            int deadPlayer = m_playerAreDead.Where<KeyValuePair<NetworkViewID, bool>>(item => item.Value == true).Count();

            if (deadPlayer == m_playerAreDead.Count - 1)
            {// One player stay Alive
                m_networkView.RPC("setLastAlive", RPCMode.OthersBuffered, true);
                m_lastPlayerAlive = true;
            }
        }
    }

    [RPC]
    void mutateSurvivor(NetworkViewID id)
    {
        NetworkView mutatingSurvivorNetworkView = NetworkView.Find(id);

        if (!mutatingSurvivorNetworkView)
        {
            Debug.LogError("Mutating Survivor not found");
            return;
        }

        GameObject mutatingSurvivor = mutatingSurvivorNetworkView.gameObject;
    }

    [RPC]
    void setLastAlive(bool lastAlive)
    {
        m_lastPlayerAlive = lastAlive;
    }

    void gameFinish()
    {
        if (Network.isClient)
            return;

        if(m_allPlayerLoose)
            m_gameFinishMessage = "Aucun vainqueur pour cette partie !";
        else
        {
            NetworkViewID idWinner = m_playerAreDead.Where<KeyValuePair<NetworkViewID, bool>>(item => item.Value == false).First().Key;
            string idPlayer = m_playerDatabase.Players.First<KeyValuePair<NetworkPlayer, Transform>>(item => item.Value.networkView.viewID == idWinner).Key.ToString();
            m_gameFinishMessage = "Joueur " + idPlayer + " a Gagnée la partie !!!";
        }

        m_networkView.RPC("setGameFinish", RPCMode.AllBuffered, m_gameFinishMessage, true);

        m_popZombieManager.stopManage();
        Dictionary<NetworkPlayer, Transform> players = m_playerDatabase.Players;

        foreach(KeyValuePair<NetworkPlayer, Transform> pair in players)
            m_networkView.RPC("setActiveObject", RPCMode.All, pair.Value.networkView.viewID, false);

        m_lastPlayerAlive = false;
    }

    [RPC]
    void setGameFinish(string message, bool gameFinish)
    {
        m_gameFinishMessage = message;
        m_gameFinish = gameFinish;
    }

    [RPC]
    void setActiveObject(NetworkViewID id, bool active)
    {
        NetworkView.Find(id).gameObject.SetActive(active);
    }

    void OnGUI()
    {
        if (m_gameFinish)
        {
            GUI.Label(m_finishMessagePosition, m_gameFinishMessage);

            if (GUI.Button(m_ButtonQuit, "Quiter le jeu"))
                Application.Quit();
        }

        if(Network.isClient)
        {//Display time for endGame

            if (m_lastPlayerAlive)
                GUI.Label(m_timerPosition, (m_timeOfEndGame - m_timerEndGame).ToString("F2"));
        }
    }
}
