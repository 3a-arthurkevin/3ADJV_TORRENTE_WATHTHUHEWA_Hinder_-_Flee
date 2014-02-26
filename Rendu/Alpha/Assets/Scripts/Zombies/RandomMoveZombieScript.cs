using UnityEngine;
using System.Collections;

public class RandomMoveZombieScript : MonoBehaviour {

    [SerializeField]
    private Transform m_zombie;

    [SerializeField]
    private Vector3 m_direction;

    [SerializeField]
    private MoveManagerZombieScript m_manager;

    private NavMeshPath m_path;
    private bool m_directionChange;
    private Vector3 m_curCorner;
    private uint m_numCorner;

    [SerializeField]
    private float m_minDistance;

    [SerializeField]
    private float m_speed;

    public Vector3 Direction
    {
        get
        {
            return m_direction;
        }
        set
        {
            m_direction = value;
            m_directionChange = true;
        }
    }

	void Awake()
	{
        m_path = new NavMeshPath();
        changeDirection();
	}

    void OnEnable()
    {
        changeDirection();
    }

    void changeDirection()
    {
        m_direction = GetRandomPositionScript.getRandomPoint(m_manager.AtIsFloor).Position;
        m_directionChange = true;
    }

	void FixedUpdate()
	{
        if (m_directionChange)
        {

            NavMesh.CalculatePath(m_zombie.position, m_direction, -1, m_path);

            if (m_path.corners.Length > 1)
            {
                m_curCorner = m_path.corners[1];
                m_numCorner = 1;
            }
            else
                changeDirection();

            m_directionChange = false;
        }

        var direction = m_curCorner - m_zombie.position;
        direction.Set(direction.x, 0, direction.z);

        if (direction.sqrMagnitude < m_minDistance)
        {
            if (m_numCorner + 1 > m_path.corners.Length)
            {
                changeDirection();
                m_path.ClearCorners();
            }
            else
                m_curCorner = m_path.corners[m_numCorner++];
            
            return;
        }

        m_zombie.position += direction.normalized * m_speed * Time.deltaTime;
	}
}
