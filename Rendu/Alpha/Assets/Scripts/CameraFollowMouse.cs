using UnityEngine;
using System.Collections;

public class CameraFollowMouse : MonoBehaviour 
{
    //Transform de la caméra
    [SerializeField]
    private Transform _transform;

    //Début de la zone permettant le déplacement souris
    [SerializeField]
    public float activeZoneBegin = 0.90f;
    
    //Fin de la zone permettant le déplacement souris
    [SerializeField]
    public float activeZoneEnd = 0.96f;

    //Vitesse de déplacement de la caméra
    [SerializeField]
    public float moveSpeed = 1f;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void LateUpdate () 
    {
        //On récupère la position de la souris et celle de la caméra
        //Pour le mouseLocation on le set à position 0 --> (0; 0)
        Vector2 mousePosition = Vector3.zero;
        Vector3 cameraPosition = _transform.position;

        //Puis on set les valeurs X et Y de la position souris avec la fonction Set --> fonction de type void
        //Position de la souris s'adapte à la taille de l'écran (valeur de X et Y comprisent entre 0 et 1)
        mousePosition.Set(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height);

        //On vérifie sur la souris se trouve dans la zone de l'écran qui permet le déplacement du personnage
        //Selon le cas on applique le sens de déplacement qu'il convient
        // --> verificarsur sur l'axe X (si on est du coté gauche ou droit de l'écran)
        if ( (mousePosition.x <= 1 - activeZoneBegin) && (mousePosition.x >= 1 - activeZoneEnd) )
        {
            cameraPosition.x -= Time.deltaTime * moveSpeed * cameraPosition.y;
        }
        else if ((mousePosition.x >= activeZoneBegin) && (mousePosition.x <= activeZoneEnd))
        {
            cameraPosition.x += Time.deltaTime * moveSpeed * cameraPosition.y;
        }
        // --> verificarsur sur l'axe Y (si on est en bas ou en haut de l'écran)
        if ( (mousePosition.y <= 1 - activeZoneBegin) && (mousePosition.y >= 1 - activeZoneEnd) )
        {
            cameraPosition.z -= Time.deltaTime * moveSpeed * cameraPosition.y;
        }
        else if ( (mousePosition.y >= activeZoneBegin) && (mousePosition.y <= activeZoneEnd) )
        {
            cameraPosition.z += Time.deltaTime * moveSpeed * cameraPosition.y;
        }

        //Application du déplacement au transform de la camera
        _transform.position = cameraPosition;
	}
}