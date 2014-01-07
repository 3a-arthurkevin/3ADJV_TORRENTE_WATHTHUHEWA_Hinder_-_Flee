using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour 
{

    [SerializeField]
    private Transform _transform;

    [SerializeField]
    private float scrollSpeed = 5f;

    [SerializeField]
    public float zoomInLimit = 5f;

    [SerializeField]
    public float zoomOutLimit = 10f;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {     
        float scrollMove;
        Vector3 cameraPosition = _transform.position;
          
        scrollMove = Input.GetAxis("Mouse ScrollWheel")*scrollSpeed;

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

        _transform.position = cameraPosition;
	}
}
