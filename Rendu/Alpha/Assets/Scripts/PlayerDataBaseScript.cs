using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Script qui manage la liste des player
//Script attaché à n emptyObject GameManager
//Script accessbile par le playernameScript / Health,Damage Script ...

public class PlayerDataBaseScript : MonoBehaviour {

	// Variable début

    public List<PlayerDateClassScript> PlayerList = new List<PlayerDateClassScript>();

    //utilisé pour ajouter joueur
    public NetworkPlayer networkPlayer;

    //utilisé pour mettre à jour la list des joueurs avec le nom du joueur
    public bool nameSet = false;
    public string playerName;

    //utilisé pour mettre à jour la classe du joueur
    public bool playerClassChoice = false;
    public int playerClass;

    // Variable fin

    void Start() { }

    void Update() 
    {
        //Affect le nom du joueur
        if (nameSet)
        {
            networkView.RPC("EditPlayerListWithName", RPCMode.AllBuffered, Network.player, playerName);
            nameSet = false;
        }

        //Affect la classe du joueur
        if (playerClassChoice)
        {
            networkView.RPC("EditPlayerListWithClass", RPCMode.AllBuffered, Network.player, playerClass);
            playerClassChoice = false;
        }
    }



    //Pour ajouter un joueur à la list
    void OnPlayerConnected(NetworkPlayer netPlayer)
    {
        //utilise la fonction "AddPlayerToList" pour tout les players 
        //--> allBuffered (buffer au cas ou quelqu'un en plus arrive en cours)
        // netPlayer --> parametre de la fonction
        networkView.RPC("AddPlayerToList", RPCMode.AllBuffered, netPlayer);
    }

    //Enlver joueur de la liste quans il se déconnecte 
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

        PlayerList.Add(player);
    }

    //Trouve l'id du joueur et l'enleve de la list
    [RPC]
    void RemovePlayerFromList(NetworkPlayer nPlayer)
    {
        int i = 0;
        bool find = false;
        int listSize = PlayerList.Count;

        while (i < listSize && !find)
        {
            if (PlayerList[i].networkPlayer == int.Parse(nPlayer.ToString()))
            {
                find = true;
                PlayerList.RemoveAt(i);
            }
        }
    }


    //Trouve le joueur dans la list et affecte son nom
    [RPC]
    void EditPlayerListWithName(NetworkPlayer nPlayer, string name)
    {
        int i = 0;
        bool find = false;
        int listSize = PlayerList.Count;

        while (i < listSize && !find)
        {
            if (PlayerList[i].networkPlayer == int.Parse(nPlayer.ToString()))
            {
                PlayerList[i].playerName = name;
            }
        }
    }

    //Trouve le joueur dans la list et affecte sa classe (Barbar ou assasin)
    [RPC]
    void EditPlayerListWithClass(NetworkPlayer nPlayer, int playerClass)
    {
        int i = 0;
        bool find = false;
        int listSize = PlayerList.Count;

        while (i < listSize && !find)
        {
            if (PlayerList[i].networkPlayer == int.Parse(nPlayer.ToString()))
            {
                PlayerList[i].playerClass = playerClass;
            }
        }
    }
}
