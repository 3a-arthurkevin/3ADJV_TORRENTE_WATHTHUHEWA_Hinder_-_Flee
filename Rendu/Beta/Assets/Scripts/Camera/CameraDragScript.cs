using UnityEngine;
using System.Collections;

public class CameraDragScript : MonoBehaviour
{
    [SerializeField]
    private Transform m_transformCamera;

    [SerializeField]
    private float m_moveSpeed = 10f;

    [SerializeField]
    private CameraFollowMouseScript m_scriptFollow;

    [SerializeField]
    private CameraLimitDeplacement m_scriptLimit;

    //Pour pouvoir désactiver le script et joueur avec la camera quand on fait des tests
    //A enlever apres
    void Start()
    {
        m_transformCamera = transform;
        m_scriptFollow = transform.GetComponent<CameraFollowMouseScript>();
        m_scriptLimit = transform.GetComponent<CameraLimitDeplacement>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Si le clique droit de la souris est maintenu
        if (Input.GetKey(KeyCode.Mouse1))
        {
            m_scriptFollow.enabled = false;

            float mouvementX = Input.GetAxis("Mouse X");
            float mouvementY = Input.GetAxis("Mouse Y");

            if (!(m_scriptLimit.blockMoveY(m_transformCamera) < 0) && (mouvementY > 0))
            {
                m_transformCamera.position -= Vector3.forward * mouvementY * Time.deltaTime * m_moveSpeed;
            }
            else if (!(m_scriptLimit.blockMoveY(m_transformCamera) > 0) && (mouvementY < 0))
            {
                m_transformCamera.position -= Vector3.forward * mouvementY * Time.deltaTime * m_moveSpeed;
            }

            if (!(m_scriptLimit.blockMoveX(m_transformCamera) < 0) && (mouvementX > 0))
            {
                m_transformCamera.position -= Vector3.right * mouvementX * Time.deltaTime * m_moveSpeed;
            }
            else if (!(m_scriptLimit.blockMoveX(m_transformCamera) > 0) && (mouvementX < 0))
            {
                m_transformCamera.position -= Vector3.right * mouvementX * Time.deltaTime * m_moveSpeed;
            }
        }
        else
            m_scriptFollow.enabled = true;
    }
}
