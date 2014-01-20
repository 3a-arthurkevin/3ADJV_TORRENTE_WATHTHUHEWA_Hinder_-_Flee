using UnityEngine;
using System.Collections;

public class InputManagerMoveSurvivantScript : MonoBehaviour
{
    [SerializeField]
    private Camera m_characterCamera;

    [SerializeField]
    private Transform m_wantToGo;
	
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = m_characterCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100000, ~LayerMask.NameToLayer("Ground")))
                m_wantToGo.position = hit.point;

        }
	}
}
