using UnityEngine;
using System.Collections;

public class InputManagerMoveSurvivorScript : MonoBehaviour
{
    [SerializeField]
    private NetworkPlayer m_owner;

    [SerializeField]
    private Camera m_characterCamera;

    [SerializeField]
    private MoveManagerSurvivorScript m_moveSurvivor;

    [SerializeField]
    private Transform m_target;

    [SerializeField]
    private NetworkView m_networkView = null;

	void Update ()
    {
        if (Network.isClient && m_owner == Network.player)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = m_characterCamera.ScreenPointToRay(Input.mousePosition);
                
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000, 1 << LayerMask.NameToLayer("Ground")))
                    m_networkView.RPC("setTarget", RPCMode.Server, Network.player, hit.point);
            }
        }
	}

    [RPC]
    void setTarget(NetworkPlayer player, Vector3 targetPosition)
    {
        if (Network.isServer)
        {
            m_target.position = targetPosition;
            m_moveSurvivor.setTarget(player, m_target);
        }
    }

    [RPC]
    public void SetPlayer(NetworkPlayer owner)
    {
        m_owner = owner;
    }
}
