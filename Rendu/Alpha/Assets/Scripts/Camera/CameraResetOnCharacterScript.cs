using UnityEngine;
using System.Collections;

public class CameraResetOnCharacterScript : MonoBehaviour
{

    [SerializeField]
    private Transform _transformCharacter;

    [SerializeField]
    private Transform _transformCamera;

    [SerializeField]
    private Vector3 _cameraPosition;

    [SerializeField]
    private CameraZoomScript _scriptZoom;

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

    void resetCamera()
    {
        _cameraPosition.x = _transformCharacter.position.x;
        _cameraPosition.y = _transformCharacter.position.y + 7;
        _cameraPosition.z = _transformCharacter.position.z - 7;

        _transformCamera.position = _cameraPosition;

        _scriptZoom.resetNbScroll();
    }
}
