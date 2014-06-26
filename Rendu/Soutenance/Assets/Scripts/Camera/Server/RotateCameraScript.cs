using UnityEngine;
using System.Collections;

public class RotateCameraScript : MonoBehaviour {

    [SerializeField]
    private Transform m_cameraTransform;

    [SerializeField]
    private float m_speedRotate = 5.0f;


    private float m_x;
    private float m_y;
    private bool m_rightClicked = false;
	
	void Start ()
    {
        m_y = m_cameraTransform.eulerAngles.y;
        m_x = m_cameraTransform.eulerAngles.x;
	}

    void Update()
    {
        if (Input.GetButtonDown("RotateCameraServer"))
            m_rightClicked = true;

        else if (Input.GetButtonUp("RotateCameraServer"))
            m_rightClicked = false;
    }
	
	
	void LateUpdate ()
    {
	    if(m_rightClicked)
        {
            m_y += Input.GetAxis("Mouse X") * m_speedRotate;
            m_x += -Input.GetAxis("Mouse Y") * m_speedRotate;

            Quaternion rotation = Quaternion.Euler(new Vector3(m_x, m_y, 0));

            m_cameraTransform.rotation = rotation;
        }
	}
}
