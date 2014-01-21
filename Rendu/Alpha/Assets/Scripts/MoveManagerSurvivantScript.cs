using UnityEngine;
using System.Collections;

public class MoveManagerSurvivantScript : MonoBehaviour
{
    [SerializeField]
    private Transform m_characterPosition;

    [SerializeField]
    private Transform m_wantToGo;

    [SerializeField]
    private float m_speed = 5f;

    private NavMeshPath m_path;
    private Vector3 m_curCorner;
    private uint m_numCorner;

    private Vector3 m_oldPosition;

    void Start()
    {
        m_oldPosition = m_characterPosition.position;
        m_wantToGo.position = m_characterPosition.position;
    }

    void FixedUpdate()
    {
        if (m_wantToGo.position != m_oldPosition)
        {
            var tmpPath = new NavMeshPath();

            if(NavMesh.CalculatePath(m_characterPosition.position, m_wantToGo.position, -1, tmpPath))
            {//Calcul du nouveau déplacement

                if (tmpPath.corners.Length > 1)
                {
                    m_path = tmpPath;
                    m_curCorner = m_path.corners[1];
                    m_numCorner = 1;
                }
                else
                    m_path = null;
            }
            m_oldPosition = m_wantToGo.position;
        }

        if (m_path == null)
            return;

        if (m_curCorner == m_characterPosition.position)
        {//Changement de coin de déplacement
            // Si on a atteint le dernier coin on set m_path a null
            //Sinon on target le prochain coin

            if (m_path.corners.Length < ++m_numCorner)
                m_path = null;

            else
                m_curCorner = m_path.corners[m_numCorner];
        }

        if (m_path == null)
            return;

        Debug.Log(m_curCorner);
        m_characterPosition.position += m_curCorner.normalized * m_speed * Time.deltaTime;

    }
}
