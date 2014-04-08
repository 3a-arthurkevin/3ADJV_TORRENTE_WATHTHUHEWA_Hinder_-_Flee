using UnityEngine;
using System.Collections;

public class BottomScreen : MonoBehaviour {

    void OnGUI()
    {
        /*
         En hauteur => ecran coupé en 5
         En largeur => ecran coupé en 10
         
         */
        if (Network.isClient)
        {
            // Make a background box
            /*
            int width = Screen.width;
            int heigth = Screen.height;

            int widthLittleBlock = width * (1 / 5);
            int heightLittleBLock = heigth * (1 / 6);

            int widthBigBlock = width * (2 / 5);
            int heightBigBLock = heigth * (5 / 6);

            //Map (1/5)
            GUI.Box(new Rect(Screen.width - widthLittleBlock, Screen.height - heightBigBLock, widthLittleBlock, heightBigBLock), "Map");

            //Skill (2/5)
            GUI.Box(new Rect(Screen.width - widthBigBlock*2, Screen.height - heightLittleBLock*2, widthBigBlock, heightLittleBLock), "Skills");

            //Item (2/5)
            GUI.Box(new Rect(Screen.width - widthBigBlock * 2, Screen.height - heightLittleBLock, widthBigBlock, heightLittleBLock), "Objets");

            //Arme (1/5)
            GUI.Box(new Rect(Screen.width - widthLittleBlock * 2, Screen.height - heightLittleBLock, widthLittleBlock, heightLittleBLock), "Arme");

            //Equipement (1/5)
            GUI.Box(new Rect(100, Screen.height - 100, 100, 100), "Equipement");

            //Stat perso
            GUI.Box(new Rect(0, Screen.height - 100, 100, 100), "Stats");


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
}
