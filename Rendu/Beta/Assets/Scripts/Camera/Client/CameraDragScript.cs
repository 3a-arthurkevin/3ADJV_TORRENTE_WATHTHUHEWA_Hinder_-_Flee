using UnityEngine;
using System.Collections;

public class CameraDragScript : MonoBehaviour
{
    [SerializeField]
    private float m_moveSpeed = 10f;

    [SerializeField]
    private CameraFollowMouseScript m_scriptFollow;

    [SerializeField]
    private CharacterController m_characterController;

    //Pour pouvoir désactiver le script et joueur avec la camera quand on fait des tests
    //A enlever apres
    void Start()
    {
        m_scriptFollow = gameObject.GetComponent<CameraFollowMouseScript>();
        m_characterController = gameObject.GetComponent<CharacterController>();
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

            if (mouvementY > 0)
            {
                m_characterController.Move(Vector3.back * mouvementY * Time.deltaTime * m_moveSpeed);
            }
            else if (mouvementY < 0)
            {
                 m_characterController.Move(Vector3.back * mouvementY * Time.deltaTime * m_moveSpeed);
            }

            if (mouvementX > 0)
            {
                 m_characterController.Move(Vector3.left * mouvementX * Time.deltaTime * m_moveSpeed);
            }
            else if (mouvementX < 0)
            {
                 m_characterController.Move(Vector3.left * mouvementX * Time.deltaTime * m_moveSpeed);
            }
        }
        else
            m_scriptFollow.enabled = true;
    }
}
