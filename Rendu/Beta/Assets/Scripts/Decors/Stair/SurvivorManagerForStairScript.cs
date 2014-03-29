using UnityEngine;
using System.Collections;

public class SurvivorManagerForStairScript : MonoBehaviour
{
    [SerializeField]
    private Texture2D m_cursor;

    private Vector2 m_hotSpot;
    private CursorMode m_cursorMode;

    [SerializeField]
    private Transform m_stairOut;
    private bool m_hasClicked;

    [SerializeField]
    private NetworkView m_networkView;

    void Awake()
    {
        m_cursorMode = CursorMode.Auto;
        m_hotSpot = Vector2.zero;
        m_hasClicked = false;
    }

    void OnMouseEnter()
    {
         Cursor.SetCursor(m_cursor, m_hotSpot, m_cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, m_hotSpot, m_cursorMode);
    }

    void OnMouseDown()
    {
        m_hasClicked = true;
    }

    void OnMouseUp()
    {
        m_hasClicked = false;
    }

    [RPC]
    void setClickedStairForServer()
    {
        m_hasClicked = true;
    }

    [RPC]
    void setClickedStairForAll()
    {
        m_hasClicked = false;
    }

    void OnTriggerStay(Collider survivor)
    {
        if (m_hasClicked && Network.isClient)
        {
            //envoyer en RPC m_hasClicked
            m_networkView.RPC("setClickedStairForServer", RPCMode.Server);
            survivor.gameObject.transform.position = m_stairOut.position + Vector3.up;
            InputManagerMoveSurvivorScript inputManager = survivor.GetComponent<InputManagerMoveSurvivorScript>();
            if (inputManager != null)
            {
                inputManager.getCharacterCamera().GetComponent<CameraResetOnCharacterScript>().resetCamera();
            }
        }

        if (m_hasClicked && Network.isServer)
        {   
            //TelePporter survivor
            survivor.gameObject.transform.position = m_stairOut.position + Vector3.up;
            //Envoyé en RPC la nouvelle position du client, et remettre son m_hasClicked à false
            m_networkView.RPC("setClickedStairForAll", RPCMode.All);
        }
    }
    /*
    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.Serialize(ref m_hasClicked);
        }
        else
        {
            stream.Serialize(ref m_hasClicked);
        }
    }
    */
}
