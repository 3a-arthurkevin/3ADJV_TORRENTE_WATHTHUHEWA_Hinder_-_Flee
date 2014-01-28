using UnityEngine;
using System.Collections;

public class CameraFollowMouseScript : MonoBehaviour 
{
    [SerializeField]
    private Transform _transform;

    [SerializeField]
    private Vector2 _mousePosition;

    [SerializeField]
    private float _activeZoneBegin = 0.90f;
    
    [SerializeField]
    private float _activeZoneEnd = 0.96f;

    [SerializeField]
    private float _moveSpeed = 3f;

	
	// Update is called once per frame
	void LateUpdate () 
    {
        _mousePosition = Vector3.zero;
        _mousePosition.Set(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height);

        if ( (_mousePosition.x <= 1 - _activeZoneBegin) && (_mousePosition.x >= 1 - _activeZoneEnd) )
        {
            _transform.position -= Vector3.right * Time.deltaTime * _moveSpeed;
        }
        else if ((_mousePosition.x >= _activeZoneBegin) && (_mousePosition.x <= _activeZoneEnd))
        {
            _transform.position += Vector3.right * Time.deltaTime * _moveSpeed;
        }

        if ( (_mousePosition.y <= 1 - _activeZoneBegin) && (_mousePosition.y >= 1 - _activeZoneEnd) )
        {
            _transform.position -= Vector3.forward * Time.deltaTime * _moveSpeed;
        }
        else if ( (_mousePosition.y >= _activeZoneBegin) && (_mousePosition.y <= _activeZoneEnd) )
        {
            _transform.position += Vector3.forward * Time.deltaTime * _moveSpeed;
        }
	}
}