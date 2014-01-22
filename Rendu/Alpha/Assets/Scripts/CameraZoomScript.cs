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
    private float _nbScroll = 7f;

    [SerializeField]
    private float _nbScrollDefault = 7f;

    [SerializeField]
    private Vector3 _cameraPosition;
	
	// Update is called once per frame
	void LateUpdate () 
    {
        var mouvement = Input.GetAxis("Mouse ScrollWheel");
        mouvement = Mathf.Clamp(mouvement, -1, 1);

        if (mouvement > 0)
        {
            if ((_nbScroll >= _scrollLimitMin) && (_nbScroll < _scrollLimiteMax))
            {
                _transform.position += _transform.rotation * Vector3.forward * Time.deltaTime * _scrollSpeed * mouvement;
                _nbScroll += mouvement;
            }     
        }
        if (mouvement < 0)
        {
            if ((_nbScroll > _scrollLimitMin) && (_nbScroll <= _scrollLimiteMax))
            {
                _transform.position += _transform.rotation * Vector3.forward * Time.deltaTime * _scrollSpeed * mouvement;
                _nbScroll += mouvement;
            }
        }
    }

    //Fonction utilisé dans le script CameraResetOnCharacter
    public void resetNbScroll()
    {
        _nbScroll = _nbScrollDefault;
    }
}
