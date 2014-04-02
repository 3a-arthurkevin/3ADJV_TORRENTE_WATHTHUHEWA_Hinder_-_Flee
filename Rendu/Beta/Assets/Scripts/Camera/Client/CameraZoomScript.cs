using UnityEngine;
using System.Collections;

public class CameraZoomScript : MonoBehaviour
{
    [SerializeField]
    private Transform m_transformCamera;

    [SerializeField]
    private float m_scrollSpeed = 15f;

    [SerializeField]
    private int m_scrollLimitMin = 0;

    [SerializeField]
    private int m_scrollLimitMax = 15;

    [SerializeField]
    private float m_nbScroll = 7f;

    [SerializeField]
    private float m_nbScrollDefault = 7f;

    [SerializeField]
    private Vector3 m_cameraPosition;

    [SerializeField]
    private CameraLimitDeplacementScript m_scriptLimit;

    void Start()
    {
        m_transformCamera = transform;
        m_scriptLimit = transform.GetComponent<CameraLimitDeplacementScript>();
    }

    void LateUpdate()
    {
        float mouvement = Input.GetAxis("Mouse ScrollWheel");
        mouvement = Mathf.Clamp(mouvement, -1, 1);

        if (mouvement > 0 && !(m_scriptLimit.blockMoveY(m_transformCamera) > 0))
        {
            if (m_nbScroll <= m_scrollLimitMax)
            {
                m_transformCamera.position += m_transformCamera.rotation * Vector3.forward * Time.deltaTime * m_scrollSpeed * mouvement;
                m_nbScroll += mouvement;
            }
        }
        if (mouvement < 0)
        {
            if (m_nbScroll >= m_scrollLimitMin && !(m_scriptLimit.blockMoveY(m_transformCamera) < 0))
            {
                m_transformCamera.position += m_transformCamera.rotation * Vector3.forward * Time.deltaTime * m_scrollSpeed * mouvement;
                m_nbScroll += mouvement;
            }
        }
    }

    //Fonction utilisé dans le script CameraResetOnCharacter
    public void resetNbScroll()
    {
        m_nbScroll = m_nbScrollDefault;
    }
}
