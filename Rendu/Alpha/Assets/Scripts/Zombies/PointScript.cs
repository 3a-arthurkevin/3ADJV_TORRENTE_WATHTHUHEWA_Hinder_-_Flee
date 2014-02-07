using UnityEngine;
using System.Collections;


public class PointScript : ScriptableObject
{
    private Vector3 m_position;
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

    public PointScript()
    {
        m_level = 0;
        m_position = Vector3.zero;
    }

    public PointScript(int level)
    {
        m_level = level;
    }

    public PointScript(Vector3 position)
    {
        m_position = position;
    }

    public PointScript(int level, Vector3 position)
    {
        m_level = level;
        m_position = position;
    }

    public PointScript Clone()
    {
        PointScript tmp = ScriptableObject.CreateInstance<PointScript>();
        tmp.m_level = m_level;
        tmp.m_position = m_position;

        return tmp;
    }
}
