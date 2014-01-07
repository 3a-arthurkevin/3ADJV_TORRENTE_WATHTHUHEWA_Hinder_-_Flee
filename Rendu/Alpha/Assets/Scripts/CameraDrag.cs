using UnityEngine;
using System.Collections;

public class CameraDrag : MonoBehaviour 
{

    [SerializeField]
    private Transform _transform;

    [SerializeField]
    private float deadZone = 0.5f;

    [SerializeField]
    public float moveSpeed = 0.6f;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Vector2 mousePosition = Vector3.zero;
            Vector3 cameraPosition = _transform.position;

            mousePosition.Set(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);

            if (mousePosition.x < deadZone)
            {
                cameraPosition.x += Time.deltaTime * moveSpeed * cameraPosition.y;
            }
            else if (mousePosition.x > deadZone)
            {
                cameraPosition.x -= Time.deltaTime * moveSpeed * cameraPosition.y;
            }

            if (mousePosition.y < deadZone)
            {
                cameraPosition.z += Time.deltaTime * moveSpeed * cameraPosition.y;
            }
            else if (mousePosition.y > deadZone)
            {
                cameraPosition.z -= Time.deltaTime * moveSpeed * cameraPosition.y;
            }

            _transform.position = cameraPosition;
        }
	}
}
