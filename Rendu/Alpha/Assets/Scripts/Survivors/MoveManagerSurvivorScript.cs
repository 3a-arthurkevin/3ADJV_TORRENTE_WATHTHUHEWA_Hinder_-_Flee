using UnityEngine;
using System.Collections;

public class MoveManagerSurvivorScript : MonoBehaviour {
    [SerializeField]
    private Rigidbody m_rigidBodyPlayer;
    
    [SerializeField]
    private Camera m_characterCamera;

    [SerializeField]
    private Transform m_character;

    [SerializeField]
    private Transform m_target;

    private NavMeshPath m_path;
    private Vector3 m_curCorner;
    private uint m_numCorner;

    [SerializeField]
    private float m_minDistance = 2;

    [SerializeField]
    private float m_speed = 2;

    public Transform Target
    {
        get
        {
            return m_target;
        }
        set
        {
            m_target = value;
            reCalcPath();
        }
    }

    void Awake()
    {
        m_target = null;
    }

    void FixedUpdate()
    {
        if (m_target != null)
        {//Déplacement jusqu'au coint final
            
            var direction = m_curCorner - m_character.position;
            direction.y = 0;

            if (direction.sqrMagnitude < m_minDistance)
            {
                if (m_numCorner + 1 > m_path.corners.Length)
                {
                    m_rigidBodyPlayer.velocity = Vector3.zero;
                    m_target = null;
                    m_path.ClearCorners();
                }
                else
                {
                    m_curCorner = m_path.corners[m_numCorner++];
                    m_character.LookAt(m_curCorner);
                }
                return;
            }

            m_rigidBodyPlayer.velocity = direction.normalized * m_speed;
            //m_character.position += m_character.forward * m_speed * Time.deltaTime;
        }
    }

    private void reCalcPath()
    {
        m_path = new NavMeshPath();
        NavMesh.CalculatePath(m_character.position, m_target.position, -1, m_path);

        if (m_path.corners.Length > 1)
        {
            m_curCorner = m_path.corners[1];
            m_numCorner = 1;
            m_character.LookAt(m_curCorner);
        }
        else
        {
            Debug.Log(m_path.status);
            m_target = null;
        }
    }

    public void teleport(Vector3 position)
    {
        m_target = null;
        transform.position = position;
        m_characterCamera.GetComponent<CameraResetOnCharacterScript>().resetCamera();
    }
}
