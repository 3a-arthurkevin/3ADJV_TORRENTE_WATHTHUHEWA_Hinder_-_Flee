using UnityEngine;
using System.Collections;

public class MoveData
{
    private NavMeshPath m_path = null;
    private bool m_isMoved = false;
    private float m_speed = 2f;
    private Vector3 m_curCorner;
    private uint m_numCorner = 0;
    private Transform m_survivorPosition = null;
    private Rigidbody m_rigidBody = null;

    public NavMeshPath Path
    {
        get { return m_path; }
        set { m_path = value; }
    }

    public bool IsMoved
    {
        get { return m_isMoved; }
        set { m_isMoved = value; }
    }

    public float Speed
    {
        get { return m_speed; }
        set { m_speed = value; }
    }
    public uint NumCorner
    {
        get { return m_numCorner; }
        set { m_numCorner = value; }
    }
    public Transform Position
    {
        get { return m_survivorPosition; }
        set { m_survivorPosition = value; }
    }
    public Rigidbody RigidBody
    {
        get { return m_rigidBody; }
        set { m_rigidBody = value; }
    }
}