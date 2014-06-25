using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerDataBaseScript : MonoBehaviour {

    [SerializeField]
    private ConfigLevelManager m_configLevelManager;

    private Dictionary<NetworkPlayer, Transform> m_players;
    private Dictionary<NetworkPlayer, bool> m_playerReady;
    private List<NetworkPlayer> m_playerRemoved;

    [SerializeField]
    private bool m_buildServer = true;

    [SerializeField]
    private int m_portNumber = 9090;
    
    [SerializeField]
    private string m_idAdress = "127.0.0.1";

    private bool m_useNat = false;

    [SerializeField]
    private int m_maxPlayers = 2;
    private int m_currentPlayer = 0;
    private bool m_gameLauched = false;

    [SerializeField]
    private Transform m_SurvivorPrefab;
    
    [SerializeField]
    private Transform m_CharacterCameraPrefab;

    [SerializeField]
    private Transform m_serverCamera;

    [SerializeField]
    private NetworkView m_networkView;

    
    void Start() 
    {
        Application.runInBackground = true;

        if (m_buildServer)
        {
            m_players = new Dictionary<NetworkPlayer, Transform>();
            m_playerReady = new Dictionary<NetworkPlayer, bool>();
            m_playerRemoved = new List<NetworkPlayer>();

            Network.InitializeSecurity();

            m_useNat = !Network.HavePublicAddress();

            NetworkConnectionError err = Network.InitializeServer(m_maxPlayers, m_portNumber, m_useNat);
            
            if (err != NetworkConnectionError.NoError)
                Debug.LogError(err.ToString());
            else
                Debug.LogError("Server start");
        }
        else
        {
            
            NetworkConnectionError err = Network.Connect(m_idAdress, m_portNumber);

            if (err != NetworkConnectionError.NoError)
                Debug.LogError(err.ToString());
            else
                Debug.LogError("Client Connect");
        }
    }

    void OnPlayerConnected(NetworkPlayer newPlayer)
    {
        if (m_currentPlayer < m_maxPlayers)
        {
            if (m_gameLauched)
                Debug.LogError("Game is already lauched");

            else
            {
                m_players.Add(newPlayer, null);
                m_playerReady.Add(newPlayer, false);

                if (m_players.Count == m_maxPlayers)
                    initialiseGame();
            }
        }
        else
            Debug.LogError("Server Full");
    }

    void OnPlayerDisconnected(NetworkPlayer oldPlayer)
    {
        if (removePlayer(oldPlayer))
        {
            Debug.LogError(oldPlayer.ToString() + " Has remove at game");
            
            m_playerRemoved.Add(oldPlayer);
        }
        else
            Debug.LogError("Player not found");
        
        pauseGame();
    }

    private void resetGame()
    {
    }

    private void pauseGame()
    {
        /*for (int i = 0; i < m_players.Count; ++i)
        {
            
        }*/
    }

    private void initialiseGame()
    {
        if (Network.isClient)
            return;

        int level = 0;
        for(int i = 0; i < m_players.Count; ++i)
        {
            NetworkPlayer player = m_players.ElementAt(i).Key;

            Transform transformPlayer = (Transform)Network.Instantiate(m_SurvivorPrefab, m_configLevelManager.getNextSpawnSurvivor(out level), Quaternion.identity, int.Parse(player.ToString()));

            transformPlayer.GetComponent<MoveManagerSurvivorScript>().MoveData.IsInFloor = level;
            transformPlayer.name = "Survivor" + player.ToString();

            NetworkView playerNetworkView = transformPlayer.networkView;

            playerNetworkView.RPC("SetPlayer", RPCMode.AllBuffered, player);
            playerNetworkView.RPC("SetName", RPCMode.AllBuffered, player, "Survivor" + player.ToString());
            
            m_players[player] = transformPlayer;
        }

        m_networkView.RPC("InitClient", RPCMode.OthersBuffered);
    }

    private bool removePlayer(NetworkPlayer oldPlayer)
    {
        return m_players.Remove(oldPlayer) && m_playerReady.Remove(oldPlayer);
    }

    public Transform getTransformPlayer(NetworkPlayer player)
    {
        Transform transformPlayer = null;
        m_players.TryGetValue(player, out transformPlayer);
        return transformPlayer;
    }

    [RPC]
    public void InitClient()
    {
        GameObject character = GameObject.Find("Survivor" + Network.player.ToString());
        
        if (character == null)
            Debug.LogError("Character non trouvé");

        Transform camera = (Transform)Instantiate(m_CharacterCameraPrefab);

        if (camera == null)
            Debug.LogError("Error instanciate Camera");

        camera.name = "CharacterCamera";

        ConfigCharacterCameraScript configCameraScript = camera.GetComponent<ConfigCharacterCameraScript>();

        if (configCameraScript == null)
            Debug.LogError("Config non trouvé");

        configCameraScript.ConfigCameraAndSurvivor(camera, character.transform);

        camera.camera.enabled = false;

        m_networkView.RPC("clientIsInit", RPCMode.Server, Network.player);
    }

    [RPC]
    void clientIsInit(NetworkPlayer player)
    {
        if (Network.isServer)
        {
            bool playerInit = false;
            
            if (m_playerReady.TryGetValue(player, out playerInit))
            {
                if (playerInit)
                {
                    Debug.LogError("Player already init");
                }
                else
                {
                    m_playerReady[player] = true;
                }
            }
            else
            {
                Debug.LogError("Player not found for initialisation");
            }

            //Tous à true ?
            if (m_playerReady.All<KeyValuePair<NetworkPlayer, bool>>(item => item.Value == true))
            {//Oui
                m_networkView.RPC("LaunchGame", RPCMode.OthersBuffered);
                GetComponent<PopZombiesManagerScript>().init();
                Instantiate(m_serverCamera, Vector3.zero + Vector3.up * 20, Quaternion.identity);
            }
        }
    }

    [RPC]
    void LaunchGame()
    {
        GameObject cam = GameObject.Find("CharacterCamera");

        if (cam == null)
        {
            Debug.LogError("Failed find camera");
            return;
        }

        cam.camera.enabled = true;
        Debug.LogError("Game launched");
    }
}
