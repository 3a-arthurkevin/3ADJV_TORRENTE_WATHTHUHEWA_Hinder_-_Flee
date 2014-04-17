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

    void OnTriggerStay(Collider survivor)
    {
        
        if (m_hasClicked)
        {//TP survivor
            survivor.GetComponent<MoveManagerSurvivorScript>().teleport(m_stairOut.position);
        }
    }
}
