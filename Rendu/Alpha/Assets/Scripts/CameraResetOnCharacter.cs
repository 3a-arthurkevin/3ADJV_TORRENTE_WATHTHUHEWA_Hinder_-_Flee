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
	void LateUpdate () {

        //Si on a appuiyé sur la barre espace (juste appuiyé, pas de maintient de la touche !)
	    if(Input.GetKeyUp(KeyCode.Space))
        {
            //On récupère la position de la camera à ce moment la
            Vector3 cameraPosition = _transformCamera.position;

            //On applique le mouvement sur l'axe x et z
            //de facon à ce qu'elle se retrouve au dessus du personnage associé (grace au tranform du personnage)
            cameraPosition.x = _transformCharacter.position.x;
            cameraPosition.z = _transformCharacter.position.z;
            
            //On applique le déplacement sur le transform de la caméra
            _transformCamera.position = cameraPosition;
        }
	}
}
