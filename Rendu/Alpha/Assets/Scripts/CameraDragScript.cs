using UnityEngine;
using System.Collections;

public class CameraDragScript : MonoBehaviour 
{
    //Transform de la camera
    [SerializeField]
    private Transform _transform;

    //Vitesse du déplacement de la caméra
    [SerializeField]
    public float _moveSpeed = 3f;

	// Use this for initialization
	void Start () 
    {

	}

	// Update is called once per frame
	void LateUpdate () 
    {
        //Si le clique droit de la souris est maintenu
        if (Input.GetKey(KeyCode.Mouse1))
        {
            var mouvementX = Input.GetAxis("Mouse X");
            var mouvementY = Input.GetAxis("Mouse Y");

            _transform.position -= Vector3.forward * mouvementY * Time.deltaTime * _moveSpeed;
            _transform.position -= Vector3.right * mouvementX * Time.deltaTime * _moveSpeed;
        }
	}
}
