using UnityEngine;
using System.Collections;

public class CameraZoomScript : MonoBehaviour 
{
    //Transform de la caméra
    [SerializeField]
    private Transform _transform;

    //Vitesse de zoom/dézoom
    [SerializeField]
    private float _scrollSpeed = 3f;

    //Hauteur caméra min (zoom max)
    [SerializeField]
    public float _zoomInLimit = 5f;

    //Hauteur de caméra max (dézoom max)
    [SerializeField]
    public float _zoomOutLimit = 10f;

    [SerializeField]
    Vector3 _cameraPosition;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void LateUpdate () 
    {
        var mouvement = Input.GetAxis("Mouse ScrollWheel");

        if ((_transform.position.y >= _zoomInLimit) && (_transform.position.y <= _zoomOutLimit))
            _transform.position += /*_transform.rotation * */Vector3.down * mouvement * Time.deltaTime * _scrollSpeed;
        else
        {
            _cameraPosition = _transform.position;

            if (_cameraPosition.y < _zoomInLimit)
            {
                _cameraPosition.y = _zoomInLimit;
            }
            else
            {
                _cameraPosition.y = _zoomOutLimit;
            }

            _transform.position = _cameraPosition;
        }
    }
}
