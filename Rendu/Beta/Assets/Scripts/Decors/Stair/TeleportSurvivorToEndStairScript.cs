using UnityEngine;
using System.Collections;

public class TeleportSurvivorToEndStairScript : MonoBehaviour {

    [SerializeField]
    private Transform m_stairOut;
    private bool m_hasClicked;

    void Awake()
    {
        m_hasClicked = false;
    }

    void OnMouseDown()
    {
        Debug.Log("MouseDown");
        m_hasClicked = true;
    }

    void OnMouseUp()
    {
        Debug.Log("MouseUp");
        m_hasClicked = false;
    }

    //Fonction booléen avec pointeur en sortie
    //Recherche (parmi les GameObjects) l'etage sur lequelle se trouve l'escalier de sortie (utiliser par le suvivant)
    bool findCurrentFloor(out GameObject floor)
    {
        bool findFloor = false;
        int nameLength = 0;

        //Logiquement l'escalier de sortie est forcement enfant de quelque chose (au moins Level)
        floor = m_stairOut.transform.parent.gameObject;
        //Limite de tour de boucle pour trouver l'etage courrant
        int limit = 0; 

        while (limit<10)
        {
            Debug.LogError(floor.name);
            nameLength = floor.name.Length;
            if (nameLength >= 5)
            {
                if (Equals(floor.name.Substring(0, 5), "Floor"))
                {
                    return true;
                }
            }
            if (floor.transform.parent)
            {
                floor = floor.transform.parent.gameObject;
            }
            else
            {
                floor = null;
                Debug.LogError("404 No floor found");
                return false;
            }
            limit++;
        }
        floor = null;
        return findFloor;
    }

    void OnTriggerStay(Collider survivor)
    {
        if (m_hasClicked)
        {
            //TP survivor lors qu'il prend l'escalier
            //Mise à jour des limites de la camera du joueur
            survivor.transform.position = m_stairOut.position;
            Camera.current.GetComponent<CameraResetOnCharacterScript>().resetCamera();
            
            GameObject floor = null;
            Transform camBorder = null;

            if (findCurrentFloor(out floor))
            {   
                camBorder = floor.transform.FindChild("camBorder").gameObject.transform;
                Camera.current.GetComponent<CameraLimitDeplacement>().setPlaneLimit(camBorder);
            }
            else
            {
                Debug.LogError("Error 404 : No floor found");
            }
        }
    }
}
