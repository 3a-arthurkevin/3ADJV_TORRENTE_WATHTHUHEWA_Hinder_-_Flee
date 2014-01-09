using UnityEngine;
using System.Collections;

public class CameraDrag : MonoBehaviour 
{
    //Transform de la camera
    [SerializeField]
    private Transform _transform;

    //Vitesse du déplacement de la caméra
    [SerializeField]
    public float moveSpeed = 0.6f;

    //Position de la souris lorsque l'on fait clique droit (coordonnées X et Y par rapport à l'écran)
    [SerializeField]
    public float initialMousePositionX;

    [SerializeField]
    public float initialMousePositionY;

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void LateUpdate () 
    {
        //Si on fait clique droit avec la souris
        //Enregistrement de la position de la souris à cet instant t précis
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            initialMousePositionX = Input.mousePosition.x/Screen.width;
            initialMousePositionY = Input.mousePosition.y/Screen.height;
        }
        //Si le clique droit de la souris est maintenu
        if (Input.GetKey(KeyCode.Mouse1))
        {
            //On récupère la position de la souris et celle de la caméra
            //Pour le mouseLocation on le set à position 0 --> (0; 0)
            Vector2 mouseLocation = Vector3.zero;
            Vector3 cameraPosition = _transform.position;

            //Puis on set les valeurs X et Y de la position souris avec la fonction Set --> fonction de type void
            //Position de la souris s'adapte à la taille de l'écran (valeur de X et Y comprisent entre 0 et 1)
            mouseLocation.Set(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height);

            //On regarde si la position de la souris est plus à gauche ou plus à droit
            //sur l'axe des X par rapport à la position initiale (celle enregistré lors du clique)
            //Et on adapte le déplacement sur l'axe x de la camera
            if (mouseLocation.x < initialMousePositionX)
            {
                cameraPosition.x += Time.deltaTime * moveSpeed * cameraPosition.y;
            }
            else if (mouseLocation.x > initialMousePositionX)
            {
                cameraPosition.x -= Time.deltaTime * moveSpeed * cameraPosition.y;
            }

            //On regarde si la position de la souris est plus en bas ou plus en haut
            //sur l'axe des Y par rapport à la position initiale (celle enregistré lors du clique)
            //Et on adapte le déplacement sur l'axe z de la camera
            if (mouseLocation.y < initialMousePositionY)
            {
                cameraPosition.z += Time.deltaTime * moveSpeed * cameraPosition.y;
            }
            else if (mouseLocation.y > initialMousePositionY)
            {
                cameraPosition.z -= Time.deltaTime * moveSpeed * cameraPosition.y;
            }

            //On affecte le déplacement au transform de la camera
            _transform.position = cameraPosition;
        }
	}
}
