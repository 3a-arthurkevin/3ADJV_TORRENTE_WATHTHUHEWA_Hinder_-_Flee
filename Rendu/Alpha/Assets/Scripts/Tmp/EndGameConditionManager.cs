using UnityEngine;
using System.Collections;

public class EndGameConditionManager : MonoBehaviour
{

    private bool m_endGame = false;
    bool m_lastSurvivorCase = false;
    bool m_lastSurvivorFunctionActivated = false;
    float m_gameDuration = 8 * 60; //8 min * 60 sec

	// Use this for initialization
	void Start () 
    {
        //Pour arreter la partie au bout du time up
        Invoke("EndGameWhenTimeUp", m_gameDuration);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (m_lastSurvivorCase && !m_lastSurvivorFunctionActivated)
        {
            Invoke("EndGame", 60);
        }
	}

    void EndGameWhenTimeUp()
    {
        if (!m_endGame && !m_lastSurvivorCase)
        {
            Application.LoadLevel("EndGameScene");
        }
    }

    void EndGame()
    {
        Application.LoadLevel("EndGameScene");
    }

    /*
     
    Fonction qui regarde dans la liste des joueur le nombre de joueur avec le tag Survivant à chaque mort d'un survivant
        --> si == 0 alors fin de partie
        --> si == 1 alors lancement phase 1 min de survie
        --> si > 0 alors rien
     
     */
}
