using UnityEngine;
using System.Collections;

public class ChangeCursorStairScript : MonoBehaviour {
    
    [SerializeField]
    private Texture2D m_cursor;

    private Vector2 m_hotSpot;
    private CursorMode m_cursorMode;

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
}
