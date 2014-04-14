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
    private bool m_hasClicked;

    [SerializeField]
    private NetworkView m_networkView;

    private Dictionary<NetworkPlayer, bool> m_survivorWhoWantToTakeStair;

    void Awake()
    {
        m_cursorMode = CursorMode.Auto;
        m_hotSpot = Vector2.zero;
        m_hasClicked = false;
        m_survivorWhoWantToTakeStair = new Dictionary<NetworkPlayer, bool>();
    }

    void OnMouseEnter()
    {
        //Debug.LogError("Cursor Stair");
        Cursor.SetCursor(m_cursor, m_hotSpot, m_cursorMode);
    }

    void OnMouseExit()
    {
        //Debug.LogError("Cursor normal");
        Cursor.SetCursor(null, m_hotSpot, m_cursorMode);
    }

    void OnMouseDown()
    {
        //Debug.LogError("click down");
        if (!m_hasClicked && Network.isClient)
        {
            m_hasClicked = true;
            m_networkView.RPC("hasClickedTrueForServer", RPCMode.Server, Network.player);
        }
    }

    void OnMouseUp()
    {
        //Debug.LogError("click Up");
        if (m_hasClicked && Network.isClient)
        {
            m_hasClicked = false;
            m_networkView.RPC("hasClickedFalseForServer", RPCMode.Server, Network.player);
        }
    }

    [RPC]
    void hasClickedTrueForServer(NetworkPlayer clientKey)
    {
        m_hasClicked = true;

        if (m_survivorWhoWantToTakeStair.ContainsKey(clientKey))
        {
            m_survivorWhoWantToTakeStair[clientKey] = true;
        }
        else
        {
            m_survivorWhoWantToTakeStair.Add(clientKey, true);
        }
    }

    [RPC]
    void hasClickedFalseForServer(NetworkPlayer clientKey)
    {
        m_hasClicked = false;

        if (m_survivorWhoWantToTakeStair.ContainsKey(clientKey))
        {
            m_survivorWhoWantToTakeStair[clientKey] = false;
        }
    }

    //Mise à jour du floor courant du survivor, Reset du path, update position survivor apres avoir pris escalier 
    void updateSurvivorPathAndCurrentFloorAndPostion(Collider survivor, int floorOut, NetworkPlayer clientNetworkPlayer)
    {
        survivor.gameObject.GetComponent<MoveManagerSurvivorScript>().tookStair(floorOut, m_stairOut.position, clientNetworkPlayer);
    }

    void OnTriggerEnter(Collider survivor)
    {
        if (Network.isServer)
        {
            NetworkPlayer tmpNetworkPlayer = survivor.gameObject.GetComponent<InputManagerMoveSurvivorScript>().getNetworkPlayer();
            if (!m_survivorWhoWantToTakeStair.ContainsKey(tmpNetworkPlayer))
            {
                m_survivorWhoWantToTakeStair.Add(tmpNetworkPlayer, true);
            }
            else
            {
                m_survivorWhoWantToTakeStair[tmpNetworkPlayer] = true;
            }

            updateSurvivorPathAndCurrentFloorAndPostion(survivor, m_floorOfStairOut, tmpNetworkPlayer);

            m_survivorWhoWantToTakeStair[tmpNetworkPlayer] = false;
        }
    }
}
