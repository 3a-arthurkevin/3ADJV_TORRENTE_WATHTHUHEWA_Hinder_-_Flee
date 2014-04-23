using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SurvivorManagerForStairScript : MonoBehaviour
{
    [SerializeField]
    private Texture2D m_cursor;

    private Vector2 m_hotSpot;
    private CursorMode m_cursorMode;

    [SerializeField]
    private Transform m_stairOut;

    [SerializeField]
    private int m_floorOfStairOut;

    [SerializeField]
    private NetworkView m_networkView;

    private bool m_wantToTakeStair = false;

    void Awake()
    {
        m_cursorMode = CursorMode.Auto;
        m_hotSpot = Vector2.zero;
    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(m_cursor, m_hotSpot, m_cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, m_hotSpot, m_cursorMode);
    }

    //Mise à jour du floor courant du survivor, Reset du path, update position survivor apres avoir pris escalier 
    void updateSurvivorPathAndCurrentFloorAndPostion(Collider survivor, int floorOut, NetworkPlayer clientNetworkPlayer)
    {
        survivor.gameObject.GetComponent<MoveManagerSurvivorScript>().tookStair(floorOut, m_stairOut.position, clientNetworkPlayer);
    }

    void OnTriggerStay(Collider survivor)
    {
        if (survivor.gameObject.layer == LayerMask.NameToLayer("Survivor"))
        {
            if (Network.isClient)
            {
                if (Input.GetMouseButtonDown(0))
                {

                    Ray ray = survivor.gameObject.GetComponent<InputManagerMoveSurvivorScript>().getCharacterCamera().ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 1000, 1 << LayerMask.NameToLayer("StairSurvivorTeleport")))
                    {
                        if (!m_wantToTakeStair)
                        {
                            m_wantToTakeStair = true;
                            m_networkView.RPC("setBoolTakeStair", RPCMode.Server, true);
                        }
                    }
                }
            }
            if (Network.isServer)
            {
                if (m_wantToTakeStair)
                {
                    NetworkPlayer tmpNetworkPlayer = survivor.gameObject.GetComponent<InputManagerMoveSurvivorScript>().getNetworkPlayer();

                    updateSurvivorPathAndCurrentFloorAndPostion(survivor, m_floorOfStairOut, tmpNetworkPlayer);

                    m_networkView.RPC("setBoolTakeStair", RPCMode.All, false);
                }
            }
        }
    }

    [RPC]
    private void setBoolTakeStair(bool value)
    {
        m_wantToTakeStair = value;
    }
}
