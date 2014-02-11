using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveManagerSurvivorScript : MonoBehaviour
{

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
        private Transform m_survivorPosition = null;
        private Rigidbody m_rigidBody = null;

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
        public uint NumCorner
        {
            get { return m_numCorner; }
            set { m_numCorner = value; }
        }
        public Transform Position
        {
            get { return m_survivorPosition; }
            set { m_survivorPosition = value; }
        }
        public Rigidbody RigidBody
        {
            get { return m_rigidBody; }
            set { m_rigidBody = value; }
        }
    }

    private Dictionary<NetworkPlayer, MoveData> m_players = null;

    void Start()
    {
        if (Network.isClient)
            enabled = false;

        m_players = new Dictionary<NetworkPlayer, MoveData>();
    }

    public void addPlayer(NetworkPlayer player, Transform transformPlayer)
    {
        MoveData data = new MoveData();
        data.Speed = m_defaultSpeed;
        data.Position = transformPlayer;
        data.RigidBody = transformPlayer.rigidbody;

        m_players.Add(player, data);
    }

    public void setTarget(NetworkPlayer player, Transform target)
    {//Calcul du path
     //Commencement du déplacement des survivant

        MoveData data;
        m_players.TryGetValue(player, out data);

        if (data != null)
        {
            var path = getCalcPath(data.Position.position, target.position);

            if (path != null)
            {
                data.Path = path;
                data.NumCorner = 1;
            }
        }

    }

    void Update()
    {//Move each survivor

        MoveData data;

        foreach (var item in m_players)
        {
            //Manage move de chaque player
            data = item.Value;

            if (data.Path != null)
            {
                var direction = data.Path.corners[data.NumCorner] - data.Position.position;

                if (data.IsMoved)
                {
                    if (direction.sqrMagnitude < m_minDistance)
                    {
                        if (data.NumCorner + 1 > data.Path.corners.Length)
                        {
                            data.RigidBody.velocity = Vector3.zero;
                            data.Path = null;
                        }
                        else
                        {
                            ++data.NumCorner; 
                            data.Position.LookAt(data.Path.corners[data.NumCorner]);
                        }

                        data.IsMoved = false;
                        return;
                    }
                }
                else
                {
                    data.RigidBody.AddForce(direction.normalized * data.Speed, ForceMode.Impulse);
                    data.IsMoved = true;
                }
            }
        }
    }

    private NavMeshPath getCalcPath(Vector3 origin, Vector3 wantToGo)
    {
        var path = new NavMeshPath();
        NavMesh.CalculatePath(origin, wantToGo, -1, path);

        if (path.corners.Length > 1)
            return path;

        else
            return null;
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
