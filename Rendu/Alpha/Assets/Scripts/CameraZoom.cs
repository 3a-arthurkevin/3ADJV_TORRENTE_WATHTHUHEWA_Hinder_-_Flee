using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour 
{
    //Transform de la caméra
    [SerializeField]
    private Transform _transform;

    //Vitesse de zoom/dézoom
    [SerializeField]
    private float scrollSpeed = 5f;

    //Hauteur caméra min (zoom max)
    [SerializeField]
    public float zoomInLimit = 5f;

    //Hauteur de caméra max (dézoom max)
    [SerializeField]
    public float zoomOutLimit = 10f;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {   
        //On déplare la variable qui contiendra le mouvement caméra de la molette souris
        //et on récupère la position de la camera
        float scrollMove;
        Vector3 cameraPosition = _transform.position;
        
        //Affecte la valeur emise par l'utilisation de la molette (valeur allant de -1 à 1) 
        scrollMove = Input.GetAxis("Mouse ScrollWheel")*scrollSpeed;

        //Application du mouvement sur l'axe y de la caméra
        //Scolling si on est entre les limites de hauteur autorisé
        //Si on a dépassé une limite de hauteur on se remet à la limite
        if ((cameraPosition.y - scrollMove >= zoomInLimit) && (cameraPosition.y - scrollMove <= zoomOutLimit))
        {
            cameraPosition.y -= scrollMove;
        }
        if(cameraPosition.y < zoomInLimit) 
        {
            cameraPosition.y = zoomInLimit;
        }
        if (cameraPosition.y > zoomOutLimit) 
        {
            cameraPosition.y = zoomOutLimit;
        }

        //Application du déplacement sur le transform de la caméra
        _transform.position = cameraPosition;
	}
}
