using UnityEngine;
using System.Collections;

public class PiegeLoupItemScript //: AfterAimingItemUseScript 
{
    [SerializeField]
    protected Texture2D m_viseurCursor;
    protected Vector2 m_hotSpot = Vector2.zero;
/*
    protected override void init()
    {
        m_name = "Piège à loup";
        m_description = "Piège qui ralentit toute personne tombant dedans";
        m_range = 5f;
        m_scaleX = 2f;
        m_scaleZ = 2f;
    }*/
}
