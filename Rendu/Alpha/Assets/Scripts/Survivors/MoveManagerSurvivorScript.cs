using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveManagerSurvivorScript : MonoBehaviour {

    [SerializeField]
    private float m_minDistance = 2;

    [SerializeField]
    private float m_defaultSpeed = 2;

    class MoveData
    {
        private NavMeshPath m_path = null;
        private bool m_isMoved = false;
        private float m_speed = 2;
        private Vector3 m_curCorner;
        private uint m_numCorner = 0;

        public NavMeshPath Path
        {
            get { return m_path; }
            set { m_path = value; }
        }

        public bool IsMoved
        {
            get { return m_isMoved; }
            set { m_isMoved = value; }
        }

        public float Speed
        {
            get { return m_speed; }
            set { m_speed = value; }
        }
    }

    private Dictionary<NetworkPlayer, MoveData> m_players = null;

    public void setTarget(NetworkPlayer player, Transform target)
    {//Calcul du path
     //Commencement du déplacement des survivant
       var GameManager = GameObject.Find("GameManager");
       PlayerDataBaseScript dataScript = GameManager.GetComponent<PlayerDataBaseScript>();

       Transform transformPlayer = dataScript.getTransformPlayer(player);

       transformPlayer.rigidbody.velocity = Vector3.zero;
        
    }

    void Start()
    {
        if (Network.isClient)
            enabled = false;
    }

    void FixedUpdate()
    {
        /*
        if (m_target != null)
        {//Déplacement jusqu'au coint final

            var direction = m_curCorner - m_character.position;

            if (m_isMoved)
            {
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

                    m_isMoved = false;
                    return;
                }
            }
            else
            {
                
                m_rigidBodyPlayer.AddForce(direction.normalized * m_speed, ForceMode.Impulse);
                m_isMoved = true;
            }
        }*/
    }

    private NavMeshPath getCalcPath()
    {
        /*
        var path = new NavMeshPath();
        NavMesh.CalculatePath(m_character.position, m_target.position, -1, path);

        if (path.corners.Length > 1)
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

        return path;
        */

        return new NavMeshPath();
    }

    public void teleport(Vector3 position)
    {
        /*
        m_target = null;
        transform.position = position;
        m_characterCamera.GetComponent<CameraResetOnCharacterScript>().resetCamera();
        */
    }
}
