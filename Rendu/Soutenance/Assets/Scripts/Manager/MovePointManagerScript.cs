using UnityEngine;
using System.Collections;
using System;

public class MovePointManagerScript : MonoBehaviour, IComparable<MovePointManagerScript>
{
    [SerializeField]
    public int m_floor;

    [SerializeField]
    public Transform[] movePoint;

    int IComparable<MovePointManagerScript>.CompareTo(MovePointManagerScript other)
    {
        if (m_floor < other.m_floor)
            return -1;

        else if (m_floor > other.m_floor)
            return 1;

        return 0;
    }
}
