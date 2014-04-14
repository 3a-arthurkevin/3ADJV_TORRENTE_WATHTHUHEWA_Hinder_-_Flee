using UnityEngine;
using System.Collections;

public class GUIClientScript : MonoBehaviour
{
    private int m_nbCelluleX = 10;

    private int m_nbCelluleY = 10;

    private float m_largeurCellule;

    private float m_hauteurCellule;

    private float m_screenWidth = Screen.width;

    private float m_screenHeight = Screen.height;

    private bool m_display = false;

    //Toutes les box de l'interface
    //_____________________

    Rect layoutTopLeft;

    Rect boxHideUnHideLayoutTopLeft;
    Rect boxImagePerso;
    Rect boxStatsPerso;
    Rect boxInventaire;


    Rect layoutBottom;

    Rect boxEtatPerso;
    Rect boxArme;
    Rect boxEquipement;
    Rect boxSkill;
    Rect boxItem;
    Rect boxMap;


    //_____________________

    void Start()
    {
        /*
        Decoupage de l'ecran client en tableau pour placer la GUI
          
        En hauteur => ecran coupé en m_nbCelluleX
        En largeur => ecran coupé en m_nbCelluleY
      
     
        Changer les positions et taille layout (TopLeft et Bottom) pour avoir un répércutement sur la taille de toutes les box
        */

        m_screenWidth = Screen.width;
        m_screenHeight = Screen.height;

        m_largeurCellule = m_screenWidth / m_nbCelluleX;
        m_hauteurCellule = m_screenHeight / m_nbCelluleY;


        layoutTopLeft = new Rect(0, 0, m_largeurCellule * 2, m_hauteurCellule * 3);

        boxHideUnHideLayoutTopLeft = new Rect(layoutTopLeft.x + layoutTopLeft.width * 0.33f, layoutTopLeft.y, layoutTopLeft.width * 0.33f, 20);
        boxImagePerso = new Rect(layoutTopLeft.x, layoutTopLeft.y, layoutTopLeft.width * 0.5f, layoutTopLeft.height * 0.5f);
        boxStatsPerso = new Rect(layoutTopLeft.x, layoutTopLeft.y + layoutTopLeft.height * 0.5f, layoutTopLeft.width * 0.5f, layoutTopLeft.height * 0.5f);
        boxInventaire = new Rect(layoutTopLeft.x + layoutTopLeft.width * 0.5f, layoutTopLeft.y, layoutTopLeft.width * 0.5f, layoutTopLeft.height);


        layoutBottom = new Rect(0, m_screenHeight - m_hauteurCellule * 2, m_screenWidth, m_hauteurCellule * 2);

        boxEtatPerso = new Rect(layoutBottom.x, layoutBottom.y, layoutBottom.width * 0.2f, layoutBottom.height);
        boxArme = new Rect(layoutBottom.x + layoutBottom.width * 0.2f, layoutBottom.y, layoutBottom.width * 0.2f, layoutBottom.height * 0.5f);
        boxEquipement = new Rect(layoutBottom.x + layoutBottom.width * 0.2f, layoutBottom.y + layoutBottom.height * 0.5f, layoutBottom.width * 0.2f, layoutBottom.height * 0.5f);
        boxSkill = new Rect(layoutBottom.x + layoutBottom.width * 0.4f, layoutBottom.y, layoutBottom.width * 0.4f, layoutBottom.height * 0.5f);
        boxItem = new Rect(layoutBottom.x + layoutBottom.width * 0.4f, layoutBottom.y + layoutBottom.height * 0.5f, layoutBottom.width * 0.4f, layoutBottom.height * 0.5f);
        boxMap = new Rect(layoutBottom.x + layoutBottom.width * 0.8f, layoutBottom.y, layoutBottom.width * 0.2f, layoutBottom.height);
    }
	
	
	void OnGUI()
    {
		if(Network.isClient)
		{
			if (m_display)
			{
				GUI.Box(boxImagePerso, "ImagePerso");
				GUI.Box(boxStatsPerso, "StatsPerso");
				GUI.Box(boxInventaire, "Inventaire");

				if (GUI.Button(boxHideUnHideLayoutTopLeft, "Hide"))
				{
					hideGUITopLeft();
				}
			}
			else
			{
				if (GUI.Button(boxHideUnHideLayoutTopLeft, "Unhide"))
				{
					unhideGUITopLeft();
				}
			}

			GUI.Box(boxEtatPerso, "Etat Perso");
			GUI.Box(boxArme, "Arme");
			GUI.Box(boxEquipement, "Equipement");
			GUI.Box(boxSkill, "Skills");
			GUI.Box(boxItem, "Items");
			GUI.Box(boxMap, "Map");

			/*
			// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
			if (GUI.Button(new Rect(20, 40, 80, 20), "Level 1"))
			{
				Application.LoadLevel(1);
			}

			// Make the second button.
			if (GUI.Button(new Rect(20, 70, 80, 20), "Level 2"))
			{
				Application.LoadLevel(2);
			}
				* */
		}
    }
	
	
    void hideGUITopLeft()
    {
        boxHideUnHideLayoutTopLeft.Set(layoutTopLeft.x + layoutTopLeft.width * 0.33f, layoutTopLeft.y, layoutTopLeft.width * 0.33f, 20);
        m_display = false;
    }

    void unhideGUITopLeft()
    {
        boxHideUnHideLayoutTopLeft.Set(layoutTopLeft.x + layoutTopLeft.width * 0.33f, layoutTopLeft.y + layoutTopLeft.height, layoutTopLeft.width * 0.33f, 20);
        m_display = true;
    }
}
