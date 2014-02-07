using UnityEngine;
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

    void Start ()
    {
        resetCamera();
    }
	
	// Update is called once per frame
	void LateUpdate () 
    {
        if (Input.GetKeyUp(KeyCode.Space))
            resetCamera();
	}

    public void resetCamera()
    {
        m_cameraPosition.x = m_transformCharacter.position.x;
        m_cameraPosition.y = m_transformCharacter.position.y + 7;
        m_cameraPosition.z = m_transformCharacter.position.z - 7;

        m_transformCamera.position = m_cameraPosition;

        m_scriptZoom.resetNbScroll();
    }
}
