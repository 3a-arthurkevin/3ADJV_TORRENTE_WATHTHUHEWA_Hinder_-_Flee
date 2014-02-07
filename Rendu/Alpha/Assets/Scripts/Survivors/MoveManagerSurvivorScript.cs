using UnityEngine;
using System.Collections;

public class MoveManagerSurvivorScript : MonoBehaviour {
    [SerializeField]
    private Camera m_characterCamera;
    
    [SerializeField]
    private Rigidbody m_rigidBodySurvivor;

    [SerializeField]
    private Transform m_character;

    [SerializeField]
    private Transform m_target;

    private NavMeshPath m_path;
    private bool m_targetChange;
    private Vector3 m_curCorner;
    private uint m_numCorner;

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

    void Awake()
    {
        m_target = null;
    }

    void FixedUpdate()
    {
        if (m_targetChange)
        {//Recalcul du chemin

            m_path = new NavMeshPath();
            NavMesh.CalculatePath(m_character.position, m_target.position, -1, m_path);

            if (m_path.corners.Length > 1)
            {
                m_curCorner = m_path.corners[1];
                m_numCorner = 1;
            }
            else
            {
                Debug.Log(m_path.status);
                m_target = null;
            }

            m_targetChange = false;
        }

        if (m_target != null)
        {//Déplacement jusqu'au coint final

            var direction = m_curCorner - m_character.position;
            direction.Set(direction.x, 0, direction.z);

            if (direction.sqrMagnitude < minDistance)
            {
                if (m_numCorner+1 > m_path.corners.Length)
                {
                    m_target = null;
                    m_path.ClearCorners();
                }
                else
                    m_curCorner = m_path.corners[m_numCorner++];

                return;
            }

            m_rigidBodySurvivor.velocity = direction.normalized * velocity;
        }
    }

    public void teleport(Vector3 position)
    {
        m_target = null;
        transform.position = position;
        m_characterCamera.GetComponent<CameraResetOnCharacterScript>().resetCamera();
    }
}
