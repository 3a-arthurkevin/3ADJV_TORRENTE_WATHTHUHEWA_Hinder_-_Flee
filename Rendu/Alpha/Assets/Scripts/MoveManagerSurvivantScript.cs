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

    private Transform m_previousWantToGo;

    private NavMeshPath m_path;

    void FixedUpdate()
    {
        if (m_wantToGo != null)
        {
            // Si la position de wantToGo est différente de celle du précédent tour de boucle
            // On recalcule le path
            if(m_wantToGo != m_previousWantToGo)
                NavMesh.CalculatePath(m_characterPosition.position, m_wantToGo.position, -1, m_path);

            
        }
    }
}
