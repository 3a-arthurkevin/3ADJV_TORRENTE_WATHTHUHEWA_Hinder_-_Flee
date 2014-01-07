using UnityEngine;
using System.Collections;

public class CameraFollowMouse : MonoBehaviour 
{

    [SerializeField]
    private Transform _transform;

    [SerializeField]
    public float activeZoneBegin = 0.95f;

    [SerializeField]
    public float activeZoneEnd = 0.99f;

    [SerializeField]
    public float moveSpeed = 1f;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        Vector2 mousePosition = Vector3.zero;
        Vector3 cameraPosition = _transform.position;

        mousePosition.Set(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);

        if ( (mousePosition.x <= 1 - activeZoneBegin) && (mousePosition.x >= 1 - activeZoneEnd) )
        {
            cameraPosition.x += Time.deltaTime * moveSpeed * cameraPosition.y;
        }
        else if ((mousePosition.x >= activeZoneBegin) && (mousePosition.x <= activeZoneEnd))
        {
            cameraPosition.x -= Time.deltaTime * moveSpeed * cameraPosition.y;
        }

        if ( (mousePosition.y <= 1 - activeZoneBegin) && (mousePosition.y >= 1 - activeZoneEnd) )
        {
            cameraPosition.z += Time.deltaTime * moveSpeed * cameraPosition.y;
        }
        else if ( (mousePosition.y >= activeZoneBegin) && (mousePosition.y <= activeZoneEnd) )
        {
            cameraPosition.z -= Time.deltaTime * moveSpeed * cameraPosition.y;
        }

        _transform.position = cameraPosition;
	}
}