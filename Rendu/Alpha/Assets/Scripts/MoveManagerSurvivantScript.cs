﻿using UnityEngine;
using System.Collections;

public class MoveManagerSurvivantScript : MonoBehaviour {

    [SerializeField]
    private Rigidbody m_characterRigidbody;

    [SerializeField]
    private Transform m_character;

    [SerializeField]
    private Transform m_target;

    private NavMeshPath m_path;
    private bool m_targetChange;
    private Vector3 m_curCorner;

    [SerializeField]
    private float minDistance;

    [SerializeField]
    private float velocity;

    public Transform Target
    {
        get
        {
            return m_target;
        }
        set
        {
            m_target = value;
            m_targetChange = true;
        }
    }

    void Start()
    {
        m_target = null;
        m_path = new NavMeshPath();
    }

    void FixedUpdate()
    {
        if (m_targetChange)
        {

            NavMesh.CalculatePath(m_character.position, m_target.position, 0, m_path);

            if (m_path.corners.Length > 1)
                m_curCorner = m_path.corners[1];

            else
                m_target = null;

            m_targetChange = false;
        }

        if (m_target != null)
        {
            var direction = m_curCorner - m_character.position;
            direction = new Vector3(direction.x, 0, direction.z);

            if (direction.sqrMagnitude < minDistance)
            {
                
            }

            rigidbody.velocity = direction.normalized * velocity;
        }
    }
}
