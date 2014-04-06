using UnityEngine;
using System.Collections;

public class CameraZoomScript : MonoBehaviour
{
    [SerializeField]
    private Transform m_cameraTransform;

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
    private CharacterController m_characterController;

    void Start()
    {
        m_cameraTransform = gameObject.transform;
        m_characterController = gameObject.GetComponent<CharacterController>();
    }

    void LateUpdate()
    {
        float mouvement = Input.GetAxis("Mouse ScrollWheel");
        mouvement = Mathf.Clamp(mouvement, -1, 1);

        if (mouvement > 0)
        {
            if (m_nbScroll <= m_scrollLimitMax)
            {
                m_characterController.Move(m_cameraTransform.rotation * Vector3.forward * Time.deltaTime * m_scrollSpeed * mouvement);
                m_nbScroll += mouvement;
            }
        }
        if (mouvement < 0)
        {
            if (m_nbScroll >= m_scrollLimitMin)
            {
                m_characterController.Move(m_cameraTransform.rotation * Vector3.forward * Time.deltaTime * m_scrollSpeed * mouvement);
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
