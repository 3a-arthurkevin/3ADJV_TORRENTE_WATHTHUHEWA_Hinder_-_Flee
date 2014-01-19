using UnityEngine;
using System.Collections;

public class CameraZoomScript : MonoBehaviour 
{
    [SerializeField]
    private Transform _transform;

    [SerializeField]
    private float _scrollSpeed = 20f;

    [SerializeField]
    private int _scrollLimitMin = 0;

    [SerializeField]
    private int _scrollLimiteMax = 20;

    [SerializeField]
    private int _nbScroll = 10;

    [SerializeField]
    private int _nbScrollDefault = 10;

    [SerializeField]
    private Vector3 _cameraPosition;
	
	// Update is called once per frame
	void LateUpdate () 
    {
        var mouvement = Input.GetAxis("Mouse ScrollWheel");

        if (mouvement > 0)
        {
            if ((_nbScroll >= _scrollLimitMin) && (_nbScroll < _scrollLimiteMax))
            {
                _transform.position += _transform.rotation * Vector3.forward * mouvement * Time.deltaTime * _scrollSpeed;
                _nbScroll++;
            }
        }
        if (mouvement < 0)
        {
            if ((_nbScroll > _scrollLimitMin) && (_nbScroll <= _scrollLimiteMax))
            {
                _transform.position += _transform.rotation * Vector3.forward * mouvement * Time.deltaTime * _scrollSpeed;
                _nbScroll--;
            }
        }
    }

    public void resetNbScroll()
    {
        _nbScroll = _nbScrollDefault;
    }
}
