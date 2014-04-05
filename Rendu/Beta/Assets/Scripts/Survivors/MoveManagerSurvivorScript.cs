using UnityEngine;
using System.Collections;

public class MoveManagerSurvivorScript : MonoBehaviour
{
    [SerializeField]
    private NetworkView m_networkView;

    [SerializeField]
    private float m_defaultSpeed = 2f;

    [SerializeField]
    private float m_defaultMinDistance = 0.1f;

    private MoveData m_data;

    public MoveData MoveData
    {
        get { return m_data; }
        set { m_data = value; }
    }

	void Awake ()
    {
        if (m_networkView == null)
            m_networkView = networkView;

        m_data = new MoveData();
        m_data.Speed = m_defaultSpeed;
        m_data.Position = transform;
        m_data.CharacterController = GetComponent<CharacterController>();
	}
	
	void Update()
    {
        if (m_data.Path != null)
        {
            Vector3 direction = m_data.Path.corners[m_data.NumCorner] - m_data.Position.position;
            direction.y = 0;

            if (direction.sqrMagnitude < m_defaultMinDistance)
            {
                if ((m_data.NumCorner + 1) >= m_data.Path.corners.Length)
                    m_data.Path = null;

                else
                {
                    Vector3 look = m_data.Path.corners[++m_data.NumCorner];
                    look.y = m_data.Position.position.y;
                    m_data.Position.LookAt(look);
                }
            }
            else
            {
                m_data.CharacterController.Move(direction.normalized * m_data.Speed * Time.deltaTime);
                //m_data.Position.position += direction.normalized * m_data.Speed * Time.deltaTime;
            }
        }
	}

    public void setTarget(Vector3 target)
    {
        if (Network.isServer)
        {
            if (pathAvailiable(target))
                m_networkView.RPC("GoTo", RPCMode.All, target);
        }
    }

    [RPC]
    public void GoTo(Vector3 target)
    {
        genPath(m_data.Position.position, target);
    }

    void genPath(Vector3 origin, Vector3 target)
    {
        m_data.Path = MoveUtilsScript.getCalcPath(origin, target);

        if (m_data.Path != null)
            m_data.NumCorner = 1;

        else
        {
            Debug.Log("Path not valid");
            m_data.NumCorner = 0;
        }
    }

    bool pathAvailiable(Vector3 wantToGo)
    {
        NavMeshPath path = MoveUtilsScript.getCalcPath(m_data.Position.position, wantToGo);

        if (path == null)
            return false;
        
        else
            return true;
    }


    //Lorsque le joueur prend des escaliers --> appelé depuis le script SurvivorManagerForStairScript
    [RPC]
    void SurvivorTookStair(int floorOut, Vector3 stairOut, NetworkPlayer clientNetworkPlayer)
    {
        //Reset du path et update du floor courant et update de la position
        this.m_data.Path = null;
        this.m_data.IsInFloor = floorOut;
        this.m_data.Position.position = stairOut + Vector3.up;

        if (Network.isClient && Network.player == clientNetworkPlayer)
        {
            InputManagerMoveSurvivorScript inputManager = gameObject.transform.GetComponent<InputManagerMoveSurvivorScript>();
            
            string gameObjectName = "Floor";
            gameObjectName += m_data.IsInFloor;

            inputManager.getCharacterCamera().GetComponent<CameraLimitDeplacementScript>().setPlaneLimit(GameObject.Find(gameObjectName).transform.FindChild("CamBorder").transform);

            inputManager.getCharacterCamera().GetComponent<CameraResetOnCharacterScript>().resetCamera();
        }
    }

    public void tookStair(int floorOut, Vector3 stairOut, NetworkPlayer clientNetworkPlayer)
    {
        if (Network.isServer)
        {
            m_networkView.RPC("SurvivorTookStair", RPCMode.All, floorOut, stairOut, clientNetworkPlayer);
        }
    }
}
