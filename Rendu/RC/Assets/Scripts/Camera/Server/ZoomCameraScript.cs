using UnityEngine;
using System.Collections;

public class ZoomCameraScript : MonoBehaviour {

    [SerializeField]
    private Transform m_cameraTransform;

    [SerializeField]
    private float m_speed = 0.1f;
    private float m_minSpeed = 0.01f;
    private float m_maxSpeed = 0.5f;
    private float m_step = 1f;
	
    void Update()
    {

        if (Input.GetKey(KeyCode.Plus) || Input.GetKey(KeyCode.KeypadPlus))
            m_speed = Mathf.Clamp(m_speed + m_step * Time.deltaTime, m_minSpeed, m_maxSpeed);

        else if (Input.GetKey(KeyCode.Minus) || Input.GetKey(KeyCode.KeypadMinus))
            m_speed = Mathf.Clamp(m_speed - m_step * Time.deltaTime, m_minSpeed, m_maxSpeed);
    }

    void LateUpdate ()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            m_cameraTransform.Translate(Vector3.forward * m_speed, Space.Self);

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            m_cameraTransform.Translate(Vector3.back * m_speed, Space.Self);
	}
}
