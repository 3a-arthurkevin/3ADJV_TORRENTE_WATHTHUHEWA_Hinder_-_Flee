using UnityEngine;
using System.Collections;

public class CameraFollowMouseScript : MonoBehaviour 
{
    //Transform de la caméra
    [SerializeField]
    private Transform _transform;

    [SerializeField]
    Vector2 _mousePosition;

    [SerializeField]
    Vector3 _cameraPosition;

    //Début de la zone permettant le déplacement souris
    [SerializeField]
    public float _activeZoneBegin = 0.90f;
    
    //Fin de la zone permettant le déplacement souris
    [SerializeField]
    public float _activeZoneEnd = 0.96f;

    //Vitesse de déplacement de la caméra
    [SerializeField]
    public float _moveSpeed = 1f;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void LateUpdate () 
    {
        //On récupère la position de la souris et celle de la caméra
        //Pour le mouseLocation on le set à position 0 --> (0; 0)
        _mousePosition = Vector3.zero;
        _cameraPosition = _transform.position;

        //Puis on set les valeurs X et Y de la position souris avec la fonction Set --> fonction de type void
        //Position de la souris s'adapte à la taille de l'écran (valeur de X et Y comprisent entre 0 et 1)
        _mousePosition.Set(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height);

        //On vérifie sur la souris se trouve dans la zone de l'écran qui permet le déplacement du personnage
        //Selon le cas on applique le sens de déplacement qu'il convient
        // --> verificarsur sur l'axe X (si on est du coté gauche ou droit de l'écran)
        if ( (_mousePosition.x <= 1 - _activeZoneBegin) && (_mousePosition.x >= 1 - _activeZoneEnd) )
        {
            _cameraPosition.x -= Time.deltaTime * _moveSpeed * _cameraPosition.y;
        }
        else if ((_mousePosition.x >= _activeZoneBegin) && (_mousePosition.x <= _activeZoneEnd))
        {
            _cameraPosition.x += Time.deltaTime * _moveSpeed * _cameraPosition.y;
        }
        // --> verificarsur sur l'axe Y (si on est en bas ou en haut de l'écran)
        if ( (_mousePosition.y <= 1 - _activeZoneBegin) && (_mousePosition.y >= 1 - _activeZoneEnd) )
        {
            _cameraPosition.z -= Time.deltaTime * _moveSpeed * _cameraPosition.y;
        }
        else if ( (_mousePosition.y >= _activeZoneBegin) && (_mousePosition.y <= _activeZoneEnd) )
        {
            _cameraPosition.z += Time.deltaTime * _moveSpeed * _cameraPosition.y;
        }

        //Application du déplacement au transform de la camera
        _transform.position = _cameraPosition;
	}
}