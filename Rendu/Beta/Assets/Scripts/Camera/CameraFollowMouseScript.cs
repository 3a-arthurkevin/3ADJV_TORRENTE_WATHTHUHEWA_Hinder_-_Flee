using UnityEngine;
using System.Collections;

public class CameraFollowMouseScript : MonoBehaviour
{
    [SerializeField]
    private Transform m_transformCamera;

    [SerializeField]
    private Vector2 m_mousePosition;

    [SerializeField]
    private float m_activeZoneBegin = 0.90f;

    [SerializeField]
    private float m_activeZoneEnd = 0.99f;

    [SerializeField]
    private float m_moveSpeed = 3f;

    [SerializeField]
    private CameraLimitDeplacement m_scriptLimit;

    void Start()
    {
        m_transformCamera = transform;
        m_scriptLimit = transform.GetComponent<CameraLimitDeplacement>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        m_mousePosition = Vector3.zero;
        m_mousePosition.Set(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);

        if ((m_mousePosition.x <= 1 - m_activeZoneBegin) && (m_mousePosition.x >= 1 - m_activeZoneEnd) && !(m_scriptLimit.blockMoveX(m_transformCamera) < 0))
        {
            m_transformCamera.position += Vector3.left * Time.deltaTime * m_moveSpeed;
        }
        else if ((m_mousePosition.x >= m_activeZoneBegin) && (m_mousePosition.x <= m_activeZoneEnd) && !(m_scriptLimit.blockMoveX(m_transformCamera) > 0))
        {
            m_transformCamera.position += Vector3.right * Time.deltaTime * m_moveSpeed;
        }


        if ((m_mousePosition.y <= 1 - m_activeZoneBegin) && (m_mousePosition.y >= 1 - m_activeZoneEnd) && !(m_scriptLimit.blockMoveY(m_transformCamera) < 0))
        {
            m_transformCamera.position += Vector3.back * Time.deltaTime * m_moveSpeed;
        }
        else if ((m_mousePosition.y >= m_activeZoneBegin) && (m_mousePosition.y <= m_activeZoneEnd) && !(m_scriptLimit.blockMoveY(m_transformCamera) > 0))
        {
            m_transformCamera.position += Vector3.forward * Time.deltaTime * m_moveSpeed;
        }
    }
}