using UnityEngine;
using System.Collections;

//Affect le nom du joueur dans le jeu

//Accède au PlayerDatabaseScript et indique d'ajouter le nom dans la liste des joueur su le bon joueur

public class PlayerNameScript : MonoBehaviour {

    // Variable début

    //public string PlayerName;

    // Variable fin


    void Awake()
    { 
        //Quand le joueur apparait dans le jeu, prend le nom du joueur dans playerPrefs and 
        //vérifie si le nom n'est pas deja utilisé par d'autre joueur

        //isMine fonction à voir dans une des autres vidéo précédente du type
        /*
        if (networkView.isMine == true)
        {
            PlayerName = PlayerPrefs.GetString("playerName");


        }
        */
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
