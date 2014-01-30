using UnityEngine;
using System.Collections;

public class DoorOpenAndCloseScript : MonoBehaviour
{

    [SerializeField]
    Transform m_doorTransform;

    [SerializeField]
    float m_doorSpeed = 1;

    [SerializeField]
    private float m_limitOpening;

    [SerializeField]
    int m_compteur;

    [SerializeField]
    bool m_isOpening = false;

    [SerializeField]
    Vector3 m_doorInitialPosition;

    // Use this for initialization
    void Start()
    {
        m_doorInitialPosition = m_doorTransform.position;
        m_compteur = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isOpening)
        {
            if ((m_doorTransform.position - m_doorInitialPosition).y < m_limitOpening)
            {
                m_doorTransform.position += Vector3.up * m_doorSpeed * Time.deltaTime;
            }
            else
            {

            }
        }
        else
        {
            if ((m_doorTransform.position - m_doorInitialPosition).y >= 0f)
            {
                m_doorTransform.position += Vector3.down * m_doorSpeed * Time.deltaTime;
            }
            else
            {

            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        m_compteur++;
        if (m_compteur == 1)
        {
            m_isOpening = true;

        }
    }

    void OnTriggerExit(Collider other)
    {
        m_compteur--;
        if (m_compteur == 0)
        {
            m_isOpening = false;

        }
    }
}