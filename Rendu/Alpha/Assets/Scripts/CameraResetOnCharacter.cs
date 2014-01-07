using UnityEngine;
using System.Collections;

public class CameraResetOnCharacter : MonoBehaviour {

    [SerializeField]
    private Transform _transformCharacter;

    [SerializeField]
    private Transform _transformCamera;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	    if(Input.GetKeyUp(KeyCode.Space))
        {
            Vector3 cameraPosition = _transformCamera.position;

            cameraPosition.x = _transformCharacter.position.x;
            cameraPosition.z = _transformCharacter.position.z;

            _transformCamera.position = cameraPosition;
        }
	}
}
