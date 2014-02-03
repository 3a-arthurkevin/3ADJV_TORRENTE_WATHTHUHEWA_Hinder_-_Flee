using UnityEngine;
using System.Collections;

public class PointScript : MonoBehaviour {

    [SerializeField]
    private Transform m_position;

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

    public Transform Position
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
