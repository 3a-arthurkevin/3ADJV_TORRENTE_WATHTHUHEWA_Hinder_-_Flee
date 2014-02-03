using UnityEngine;
using System.Collections;

public class PointScript : MonoBehaviour {

    [SerializeField]
    private Vector3 m_position;

    [SerializeField]
    private int m_level;

    public int Level
    {
        get
        {
            return m_level;
        }
        set
        {
            m_level = value;
        }
    }

    public Vector3 Position
    {
        get
        {
            return m_position;
        }
        set
        {
            m_position = value;
        }
    }
}
