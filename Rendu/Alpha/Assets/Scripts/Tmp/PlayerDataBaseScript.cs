using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Script qui manage la liste des player/connexion joueur et serveur
//Script attaché à un emptyObject GameManager
//Script accessbile par le HealthManager Script ...

public class PlayerDataBaseScript : MonoBehaviour {

    public List<PlayerDateClassScript> m_playerList = new List<PlayerDateClassScript>();

    //utilisé pour ajouter joueur
    public NetworkPlayer m_networkPlayer;

    //utilisé pour mettre à jour la list des joueurs avec le nom du joueur
    public bool m_nameSet = false;
    public string m_playerName;

    //utilisé pour mettre à jour la classe du joueur
    public bool m_playerClassChoice = false;
    public int m_playerClass;

    [SerializeField]
    private bool m_isBuildingServer = true;

    [SerializeField]
    private int portNumber = 9090;

    void Start() 
    {
        Application.runInBackground = true;

        if (m_isBuildingServer)
        {
            Network.InitializeSecurity();
            Network.InitializeServer(1, portNumber, true);
            Debug.Log("Serveur Démarré !!!");
        }
        else
        {
            Network.Connect("127.0.0.1", portNumber);
            Debug.Log("1 joueur connecté !!!");
        }
    }

    void Update() 
    {
        //Affect la classe du joueur
        if (m_playerClassChoice)
        {
            networkView.RPC("EditPlayerListWithClass", RPCMode.AllBuffered, Network.player, m_playerClass);
            m_playerClassChoice = false;
        }
    }


    //Pour ajouter un joueur à la list
    void OnPlayerConnected(NetworkPlayer netPlayer)
    {
        //utilise la fonction "AddPlayerToList" pour tout les players 
        //--> allBuffered (buffer au cas ou quelqu'un en plus arrive en cours)
        // netPlayer --> parametre de la fonction
        networkView.RPC("AddPlayerToList", RPCMode.AllBuffered, netPlayer);
        if (m_playerList.Count == 2)
        {
            //Code pour démarrer la partie
        }
    }

    //Enlever joueur de la liste quand il se déconnecte 
    void OnPlayerDisconnected(NetworkPlayer netPlayer)
    {
        networkView.RPC("RemovePlayerFromList", RPCMode.AllBuffered, netPlayer);
    }


    //Ajouter joueur à la liste et lui donner un id réseau
    [RPC]
    void AddPlayerToList(NetworkPlayer nPlayer)
    {
        PlayerDateClassScript player = new PlayerDateClassScript();

        player.networkPlayer = int.Parse(nPlayer.ToString());

        m_playerList.Add(player);
    }

    //Trouve l'id du joueur et l'enleve de la list
    [RPC]
    void RemovePlayerFromList(NetworkPlayer nPlayer)
    {
        int i = 0;
        bool find = false;
        int listSize = m_playerList.Count;

        while (i < listSize && !find)
        {
            if (m_playerList[i].networkPlayer == int.Parse(nPlayer.ToString()))
            {
                find = true;
                m_playerList.RemoveAt(i);
            }
        }
    }

    //Trouve le joueur dans la list et affecte sa classe (Barbar ou assasin)
    [RPC]
    void EditPlayerListWithClass(NetworkPlayer nPlayer, int playerClass)
    {
        int i = 0;
        bool find = false;
        int listSize = m_playerList.Count;

        while (i < listSize && !find)
        {
            if (m_playerList[i].networkPlayer == int.Parse(nPlayer.ToString()))
            {
                m_playerList[i].playerClass = playerClass;
            }
        }
    }
}
