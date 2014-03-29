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

            //Si pc a une adresse public utilisé NAT sinon non
            m_useNat = Network.HavePublicAddress();

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
    {//Nouveau joueur se connect au serveur

        if (m_currentPlayer < m_maxPlayers)
        {
            if (m_gameLauched)
            {
                Debug.LogError("Game is already lauched");
            }
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
    {//Instancie toute les préfabs et supprime la liste m_beforeGame

        MoveManagerSurvivorScript moveManagerSurvivor = GetComponent<MoveManagerSurvivorScript>();

        for(int i = 0; i < m_players.Count; ++i)
        {
            NetworkPlayer player = m_players.ElementAt(i).Key;


            Transform transformPlayer = (Transform)Network.Instantiate(m_SurvivorPrefab, ConfigLevelManager.getNextSpawnSurvivor(), Quaternion.identity, int.Parse(player.ToString()));

            transformPlayer.name = "Survivor" + player.ToString();

            NetworkView playerNetworkView = transformPlayer.networkView;

            playerNetworkView.RPC("SetPlayer", RPCMode.AllBuffered, player);
            playerNetworkView.RPC("SetName", RPCMode.OthersBuffered, player, "Survivor" + player.ToString());

            moveManagerSurvivor.addPlayer(player, transformPlayer);
            
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
    {//Positionne la camera sur le survivor
        //Et fait les différente initialisation du client

        GameObject character = GameObject.Find("Survivor" + Network.player.ToString());
        
        Debug.LogError(Network.player.ToString());

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
    {//Reçois un message du client pour prévenir qu'il est prêt
        //La partie ne démarre seulement quand tous les clients sont prêt

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
