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
	
	// Update is called once per frame
	void LateUpdate () {

	    if(Input.GetKeyUp(KeyCode.Space))
        {
            _cameraPosition = _transformCamera.position;

            _cameraPosition.x = _transformCharacter.position.x;
            _cameraPosition.y = _transformCharacter.position.y+7;
            _cameraPosition.z = _transformCharacter.position.z-7;
            
            _transformCamera.position = _cameraPosition;

            _scriptZoom.resetNbScroll();
        }
	}
}
