﻿using UnityEngine;
using System.Collections;

public class CameraResetOnCharacterScript : MonoBehaviour
{

    [SerializeField]
    private Transform m_transformCharacter;

    [SerializeField]
    private Transform m_transformCamera;

    [SerializeField]
    private Vector3 m_cameraPosition;

    [SerializeField]
    private CameraZoomScript m_scriptZoom;

    [SerializeField]
    private CameraFollowMouseScript m_scriptFollow;

    [SerializeField]
    private float m_positionResetX = 0f;

    [SerializeField]
    private float m_positionResetY = 7f;

    [SerializeField]
    private float m_positionResetZ = -7f;

    void Start()
    {
        m_transformCamera = transform;
        m_scriptZoom = transform.GetComponent<CameraZoomScript>();
        m_scriptFollow = transform.GetComponent<CameraFollowMouseScript>();
        resetCamera();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetButtonDown("ResetCamera"))
        {
            m_scriptFollow.enabled = false;
        }
        else if (Input.GetButton("ResetCamera"))
        {
            resetCamera();
        }
        else if (Input.GetButtonUp("ResetCamera"))
        {
            m_scriptFollow.enabled = true;
        }
    }

    public void resetCamera()
    {
        m_cameraPosition.x = m_transformCharacter.position.x + m_positionResetX;
        m_cameraPosition.y = m_transformCharacter.position.y + m_positionResetY;
        m_cameraPosition.z = m_transformCharacter.position.z + m_positionResetZ;

        m_transformCamera.position = m_cameraPosition;

        m_scriptZoom.resetNbScroll();
    }

    public void setSurvivorTranform(Transform survivorTransform)
    {
        m_transformCharacter = survivorTransform;
    }
}
