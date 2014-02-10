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

    [SerializeField]
    private NetworkView m_networkView;

	void Update ()
    {
        if (Network.isClient)
        {
            if (Input.GetMouseButtonDown(0))
            {
                guiText.text = "Sending";

                m_networkView.RPC("setTarget", RPCMode.Server, Network.player, Input.mousePosition);
                /*var ray = m_characterCamera.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000, 1 << LayerMask.NameToLayer("Ground")))
                {
                    m_target.position = hit.point;
                    m_moveSurvivor.Target = m_target;
                }*/
            }
        }
	}

    [RPC]
    void setTarget(NetworkPlayer player, Vector3 mousePosition)
    {
        guiText.text = "player : " + player.ipAddress + ", mousePos : " + mousePosition.ToString("F6");
    }
}
