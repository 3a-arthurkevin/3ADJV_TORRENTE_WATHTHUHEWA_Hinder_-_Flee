using UnityEngine;
using System.Collections;

public class CameraZoomScript : MonoBehaviour 
{
    [SerializeField]
    private Transform _transform;

    [SerializeField]
    private float _scrollSpeed = 15f;

    [SerializeField]
    private int _scrollLimitMin = 0;

    [SerializeField]
    private int _scrollLimiteMax = 15;

    [SerializeField]
    private int _nbScroll = 7;

    [SerializeField]
    private int _nbScrollDefault = 7;

    /*[SerializeField]
    private float _limitYMin;

    [SerializeField]
    private float _limitYMax;

    [SerializeField]
    private float _limitZMin;

    [SerializeField]
    private float _limitZMax;

    [SerializeField]
    private Vector3 _cameraPosition;*/
	
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

    //Fonction utilisé dans le script CameraResetOnCharacter
    public void resetNbScroll()
    {
        _nbScroll = _nbScrollDefault;
    }
}
