using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveManagerSurvivorScript : MonoBehaviour
{
    [SerializeField]
    private NetworkView m_networkView;

    [SerializeField]
    private float m_minDistance = 0.1f;

    [SerializeField]
    private float m_defaultSpeed = 2f;

    private Dictionary<NetworkPlayer, MoveData> m_players = null;

    void Start()
    {
        
        if (m_networkView == null)
            m_networkView = networkView;

        m_players = new Dictionary<NetworkPlayer, MoveData>();
    }

    public void addPlayer(NetworkPlayer player, Transform transformPlayer)
    {
        if (Network.isServer)
        {
            MoveData data = new MoveData();
            data.Speed = m_defaultSpeed;
            data.Position = transformPlayer;
            data.RigidBody = transformPlayer.rigidbody;

            m_networkView.RPC("addPlayerInClient", RPCMode.Others, player, transformPlayer.name);
            m_players.Add(player, data);
        }
    }

    [RPC]
    public void addPlayerInClient(NetworkPlayer player, string NameOfPlayerGameObject)
    {
        if (Network.isClient)
        {
            MoveData data = new MoveData();
            GameObject gameObjectOfPlayer = GameObject.Find(NameOfPlayerGameObject);

            if (gameObjectOfPlayer == null)
            {
                Debug.LogError("Player" + player.ToString() + " not found in Scene");
                return;
            }
            else
                Debug.LogError("Player " + player.ToString() + " Added");

            data.Position = gameObjectOfPlayer.transform;
            data.RigidBody = data.Position.rigidbody;
            data.Speed = m_defaultSpeed;

            m_players.Add(player, data);
        }
    }

    public void setTarget(NetworkPlayer player, Transform target)
    {//Calcul du path
     //Commencement du déplacement des survivant
        if (Network.isServer)
        {
            MoveData data = null;

            if (m_players.TryGetValue(player, out data) && data != null)
            {
                NavMeshPath path = MoveUtilsScript.getCalcPath(data.Position.position, target.position);

                if (path != null)
                {
                    data.Path = path;
                    data.NumCorner = 1;
                    //Envoyé la mise à jour du NavMeshPath à tous les clients
                    m_networkView.RPC("UpdatePathClient", RPCMode.OthersBuffered, player, target.position);
                }
                else
                {
                    data.Path = null;
                    data.NumCorner = 0;
                }

                data.IsMoved = false;
            }
        }
    }

    [RPC]
    public void UpdatePathClient(NetworkPlayer player, Vector3 targetPosition)
    {
        if (Network.isClient)
        {
            MoveData data = null;

            if (m_players.TryGetValue(player, out data) && data != null)
            {
                NavMeshPath path = MoveUtilsScript.getCalcPath(data.Position.position, targetPosition);

                if (path != null)
                {
                    data.Path = path;
                    data.NumCorner = 1;
                }
                else
                {
                    data.Path = null;
                    data.NumCorner = 0;
                    Debug.LogError("Failed calculated path");
                }

                data.IsMoved = false;
            }
        }
    }

    void FixedUpdate()
    {//Move each survivor
        MoveData data;

        foreach (KeyValuePair<NetworkPlayer, MoveData> item in m_players)
        {
            data = item.Value;

            if (data.Path != null)
            {
                Vector3 direction = data.Path.corners[data.NumCorner] - data.Position.position;
                direction.y = 0;

                if (Network.isClient)
                    Debug.Log(direction.sqrMagnitude.ToString("F6") + " - " + m_minDistance);

                if (direction.sqrMagnitude < m_minDistance)
                {
                    if ((data.NumCorner + 1) >= data.Path.corners.Length)
                        data.Path = null;

                    else
                    {
                        Vector3 look = data.Path.corners[++data.NumCorner];
                        look.y = data.Position.position.y;
                        data.Position.LookAt(look);
                    }

                    data.IsMoved = false;
                }
                else
                    data.Position.position += direction.normalized * data.Speed * Time.deltaTime;
            }
        }
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
