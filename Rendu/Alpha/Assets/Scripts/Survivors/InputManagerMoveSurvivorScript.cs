using UnityEngine;
using System.Collections;

public class InputManagerMoveSurvivorScript : MonoBehaviour
{
    [SerializeField]
    private Camera m_characterCamera;

    [SerializeField]
    private MoveManagerSurvivorScript m_moveSurvivor;

    [SerializeField]
    private Transform m_target;

	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = m_characterCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, 1 << LayerMask.NameToLayer("Ground")))
            {
                Debug.Log(hit.point);
                m_target.position = hit.point;
                m_moveSurvivor.Target = m_target;
            }
        }
	}
}
