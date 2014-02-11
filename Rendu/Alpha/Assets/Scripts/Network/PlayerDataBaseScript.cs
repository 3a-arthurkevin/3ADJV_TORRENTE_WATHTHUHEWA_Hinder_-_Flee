using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//Script qui manage la liste des player/connexion joueur et serveur
//Script attaché à un emptyObject GameManager
//Script accessbile par le HealthManager Script ...

public class PlayerDataBaseScript : MonoBehaviour {

    //Stocke le Transform + le NetworkPlayer durant la game
    private Dictionary<NetworkPlayer, Transform> m_players;
    
    //Stocke les NetworkPlayer avant le lancement de la game
    private List<NetworkPlayer> m_beforeGamePlayer;

    [SerializeField]
    private bool m_isBuildingServer = true;

    [SerializeField]
    private int m_portNumber = 9090;
    
    [SerializeField]
    private string m_idAdress = "127.0.0.1";

    [SerializeField]
    private int m_maxPlayers = 2;
    private int m_currentPlayer = 0;

    [SerializeField]
    private Transform m_prefab;

    
    void Start() 
    {
        Application.runInBackground = true;

        if (m_isBuildingServer)
        {
            m_beforeGamePlayer = new List<NetworkPlayer>();
            Network.InitializeSecurity();
            var useNat = !Network.HavePublicAddress();
            Network.InitializeServer(m_maxPlayers, m_portNumber, useNat);
            Debug.LogError("Server start");
        }
        else
        {
            Network.Connect(m_idAdress, m_portNumber);
            Debug.LogError("Client connect");
        }
    }

    void OnPlayerConnected(NetworkPlayer newPlayer)
    {//Nouveau joueur se connect au serveur
        createNewPlayer(newPlayer);
    }

    //Enlever joueur de la liste quand il se déconnecte 
    void OnPlayerDisconnected(NetworkPlayer oldPlayer)
    {
        removePlayer(oldPlayer);
    }


    private void createNewPlayer(NetworkPlayer newPlayer)
    {
        m_beforeGamePlayer.Add(newPlayer);

        if (++m_currentPlayer == m_maxPlayers)
            initialiseGame();
    }

    private void initialiseGame()
    {//Instancie toute les préfabs et supprime la liste m_beforeGame

        m_players = new Dictionary<NetworkPlayer, Transform>(m_beforeGamePlayer.Count);
        var moveManagerSurvivor = GetComponent<MoveManagerSurvivorScript>();

        foreach(var player in m_beforeGamePlayer)
        {
            Transform transformPlayer = (Transform)Network.Instantiate(m_prefab, ConfigLevelManager.getNextSpawnForLevelOne(), Quaternion.identity, int.Parse(player.ToString()));
            
            NetworkView playerNetworkView = transformPlayer.networkView;

            playerNetworkView.RPC("SetPlayer", RPCMode.AllBuffered, player);

            moveManagerSurvivor.addPlayer(player, transformPlayer);

            m_players.Add(player, transformPlayer);
            ++m_currentPlayer;
        }

        m_beforeGamePlayer = null;
    }

    private void removePlayer(NetworkPlayer oldPlayer)
    {
        int idPlayer = int.Parse(oldPlayer.ToString());

        for (int i = 0; i < m_players.Count; ++i)
        {
            var item = m_players.ElementAt(i);

            if (int.Parse(item.Key.ToString()) == idPlayer)
            {
                Destroy(item.Value.gameObject);
                m_players.Remove(item.Key);
                --m_currentPlayer;
                
                return;
            }
        }
    }

    public Transform getTransformPlayer(NetworkPlayer player)
    {
        Transform transformPlayer = null;
        m_players.TryGetValue(player, out transformPlayer);
        return transformPlayer;
    }
}
