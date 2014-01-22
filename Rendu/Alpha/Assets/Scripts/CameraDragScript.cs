using UnityEngine;
using System.Collections;

public class CameraDragScript : MonoBehaviour 
{
    [SerializeField]
    private Transform _transform;

    [SerializeField]
    private float _moveSpeed = 3f;

    [SerializeField]
    private CameraFollowMouseScript _scriptZoom;


	// Update is called once per frame
	void LateUpdate () 
    {
        //Si le clique droit de la souris est maintenu
        if (Input.GetKey(KeyCode.Mouse1))
        {
            _scriptZoom.enabled = false;

            var mouvementX = Input.GetAxis("Mouse X");
            var mouvementY = Input.GetAxis("Mouse Y");

            //_transform.position -= Vector3.forward * mouvementY * Time.deltaTime * _moveSpeed;
            //_transform.position -= Vector3.right * mouvementX * Time.deltaTime * _moveSpeed;

            _transform.position -= _transform.rotation * Vector3.forward * mouvementY * Time.deltaTime * _moveSpeed;
            _transform.position -= _transform.rotation * Vector3.right * mouvementX * Time.deltaTime * _moveSpeed;
        }
        else
            _scriptZoom.enabled = true;
	}
}
