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

    void OnDrawGizmos()
    {
        //Si remplissage des champs correctement (normalement avec le transform de l'escalier se sorti et son n° d'étage)
        if (m_stairOut != null && m_floorOfStairOut != -1)
        {
             Debug.DrawLine(this.transform.position, m_stairOut.transform.position, Color.blue);
        }
    }

    void Awake()
    {
        m_cursorMode = CursorMode.Auto;
        m_hotSpot = Vector2.zero;
        m_hasClicked = false;
    }

    void OnMouseEnter()
    {
        //Debug.LogError("Cursor Stair");
        Cursor.SetCursor(m_cursor, m_hotSpot, m_cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, m_hotSpot, m_cursorMode);
    }

    void OnMouseDown()
    {
        if (!m_hasClicked && Network.isClient)
        {
            m_hasClicked = true;
            m_networkView.RPC("hasClickedTrueForServer", RPCMode.Server, Network.player);
        }
    }

    void OnMouseUp()
    {
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
    }

    [RPC]
    void hasClickedFalseForServer(NetworkPlayer clientKey)
    {
        m_hasClicked = false;
    }

    //Mise à jour du floor courant du survivor, Reset du path, update position survivor apres avoir pris escalier 
    void updateSurvivorPathAndCurrentFloorAndPostion(Collider survivor, int floorOut, NetworkPlayer clientNetworkPlayer)
    {
        survivor.gameObject.GetComponent<MoveManagerSurvivorScript>().tookStair(floorOut, m_stairOut.position, clientNetworkPlayer);
    }

    void OnTriggerStay(Collider survivor)
    {
        if (Network.isServer && m_hasClicked == true)
        {
            NetworkPlayer tmpNetworkPlayer = survivor.gameObject.GetComponent<InputManagerMoveSurvivorScript>().getNetworkPlayer();
            updateSurvivorPathAndCurrentFloorAndPostion(survivor, m_floorOfStairOut, tmpNetworkPlayer);
            
            m_hasClicked = false;
        }
    }
}