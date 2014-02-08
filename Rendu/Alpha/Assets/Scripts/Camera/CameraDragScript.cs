﻿using UnityEngine;
using System.Collections;

public class CameraDragScript : MonoBehaviour 
{
    [SerializeField]
    private GameObject m_parentGameObject;
    
    [SerializeField]
    private Transform m_transformCamera;

    [SerializeField]
    private float m_moveSpeed = 3f;

    [SerializeField]
    private CameraFollowMouseScript m_scriptZoom;

    private int m_limitX;

    private int m_limitZ;

    //Pour pouvoir désactiver le script et joueur avec la camera quand on fait des tests
    //A enlever apres
    void Start()
    {
        m_parentGameObject = transform.gameObject;
    }

	// Update is called once per frame
	void LateUpdate () 
    {
        //Si le clique droit de la souris est maintenu
        if (Input.GetKey(KeyCode.Mouse1))
        {
            m_scriptZoom.enabled = false;

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

            var mouvementX = Input.GetAxis("Mouse X");
            var mouvementY = Input.GetAxis("Mouse Y");

            if (m_limitX < 0 && mouvementX > 0)
                mouvementX = 0;
            else if (m_limitX > 0 && mouvementX < 0)
                mouvementX = 0;


            if (m_limitZ < 0 && mouvementY > 0)
                mouvementY = 0;
            else if (m_limitZ > 0 && mouvementY < 0)
                mouvementY = 0;

            m_transformCamera.position -= Vector3.forward * mouvementY * Time.deltaTime * m_moveSpeed;
            m_transformCamera.position -= Vector3.right * mouvementX * Time.deltaTime * m_moveSpeed;
        }
        else
            m_scriptZoom.enabled = true;
	}
}
