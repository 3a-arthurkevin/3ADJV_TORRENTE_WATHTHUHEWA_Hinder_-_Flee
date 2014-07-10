using UnityEngine;
using System.Collections;

public class GUITutorialScript : MonoBehaviour 
{
    [SerializeField]
    Camera m_camera;

    private float m_screenWidth;

    private float m_screenHeight;

    private Rect boxButtonLeft;
    private Rect boxButtonRight;

    private Rect boxButtonReturn;

    private int idImage = 0;

    private int idMax = 3;

	// Use this for initialization
	void Start () 
    {
        m_screenWidth = Screen.width;
        m_screenHeight = Screen.height;

        boxButtonLeft = new Rect(0, 0, 80, 20);
        boxButtonRight = new Rect(m_screenWidth-80, 0, 80, 20);
        boxButtonReturn = new Rect(m_screenWidth/2, 0, 110, 20);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (idImage > 0)
        {
            if (GUI.Button(boxButtonLeft, "Précédent"))
            {
                Vector3 vec = new Vector3(0, 0, -100);
                idImage--;
                m_camera.transform.position += vec ;
            }
        }
        if (idImage < idMax)
        {
            if (GUI.Button(boxButtonRight, "Suivant"))
            {
                Vector3 vec = new Vector3(0, 0, 100);
                idImage++;
                m_camera.transform.position += vec;
            }
        }

        if (GUI.Button(boxButtonReturn, "Retour au lobby"))
        {
            Application.LoadLevel(0);
        }
    }
}
