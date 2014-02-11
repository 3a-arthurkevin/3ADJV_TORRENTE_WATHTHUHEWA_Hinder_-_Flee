using UnityEngine;
using System.Collections;

public class CameraFollowMouseScript : MonoBehaviour 
{
    [SerializeField]
    private GameObject m_parentGameObject;
    
    [SerializeField]
    private Transform m_transformCamera;

    [SerializeField]
    private Vector2 m_mousePosition;

    [SerializeField]
    private float m_activeZoneBegin = 0.90f;
    
    [SerializeField]
    private float m_activeZoneEnd = 0.96f;

    [SerializeField]
    private float m_moveSpeed = 3f;

    private int m_limitX;

    private int m_limitZ;


    void Start()
    {
        m_parentGameObject = gameObject;
        m_transformCamera = transform;
    }

	// Update is called once per frame
	void LateUpdate () 
    {
        m_mousePosition = Vector3.zero;
        m_mousePosition.Set(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height);

        if (m_parentGameObject.GetComponent<CameraLimitDeplacement>().enabled == true)
        {
            m_limitX = GetComponent<CameraLimitDeplacement>().blockMoveX(m_transformCamera);
            m_limitZ = GetComponent<CameraLimitDeplacement>().blockMoveY(m_transformCamera);
        }
        else
        {
            m_limitX = 0;
            m_limitZ = 0;
        }

        if ( (m_mousePosition.x <= 1 - m_activeZoneBegin) && (m_mousePosition.x >= 1 - m_activeZoneEnd) && (m_limitX >= 0) )
        {
            m_transformCamera.position -= Vector3.right * Time.deltaTime * m_moveSpeed;
        }
        else if ((m_mousePosition.x >= m_activeZoneBegin) && (m_mousePosition.x <= m_activeZoneEnd) && (m_limitX <= 0) )
        {
            m_transformCamera.position += Vector3.right * Time.deltaTime * m_moveSpeed;
        }


        if ((m_mousePosition.y <= 1 - m_activeZoneBegin) && (m_mousePosition.y >= 1 - m_activeZoneEnd) && (m_limitZ >= 0) )
        {
            m_transformCamera.position -= Vector3.forward * Time.deltaTime * m_moveSpeed;
        }
        else if ((m_mousePosition.y >= m_activeZoneBegin) && (m_mousePosition.y <= m_activeZoneEnd) && (m_limitZ <= 0))
        {
            m_transformCamera.position += Vector3.forward * Time.deltaTime * m_moveSpeed;
        }
	}
}