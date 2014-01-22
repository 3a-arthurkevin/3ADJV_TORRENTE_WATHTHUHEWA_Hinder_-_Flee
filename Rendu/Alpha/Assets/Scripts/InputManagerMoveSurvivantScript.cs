using UnityEngine;
using System.Collections;

public class InputManagerMoveSurvivantScript : MonoBehaviour
{
    [SerializeField]
    private Camera m_characterCamera;

    [SerializeField]
    private MoveManagerSurvivantScript m_moveSurvivant;

    [SerializeField]
    private Transform m_target;

    [SerializeField]
    private string m_nameLayerToMove;

    private int m_bitMaskLayerToMove;

    void Start()
    {
        m_bitMaskLayerToMove = 1 << LayerMask.NameToLayer(m_nameLayerToMove);
    }

	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = m_characterCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, m_bitMaskLayerToMove))
            {
                m_target.position = hit.point;
                m_moveSurvivant.Target = m_target;
            }
        }
	}
}
