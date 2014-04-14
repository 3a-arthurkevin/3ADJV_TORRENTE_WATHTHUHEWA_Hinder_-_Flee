using UnityEngine;
using System.Collections;

public class MoveCameraScript : MonoBehaviour {

    [SerializeField]
    private Transform m_cameraTransform;

    [SerializeField]
    private float m_speed = 5;

    private Vector3 m_oldPosition;
    private bool m_wheelClicked = false;
	
	void Update ()
    {
        if (Input.GetButtonDown("MoveCamera"))
        {

            m_oldPosition = Input.mousePosition;
            m_oldPosition.x /= Screen.width;
            m_oldPosition.y /= Screen.height;

            m_wheelClicked = true;
        }
        else if (Input.GetButtonUp("MoveCamera"))
            m_wheelClicked = false;
	}

    void LateUpdate()
    {
        if(m_wheelClicked)
        {
            Vector3 mouse = Input.mousePosition;

            mouse.x /= Screen.width;
            mouse.y /= Screen.height;

            m_cameraTransform.Translate((m_oldPosition - mouse) * m_speed);

            m_oldPosition = mouse;
        }
    }

}
