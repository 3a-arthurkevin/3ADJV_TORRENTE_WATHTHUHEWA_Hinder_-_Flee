using UnityEngine;
using System.Collections;

public class InputManagerMoveSurvivorScript : MonoBehaviour
{
    [SerializeField]
    private NetworkPlayer m_owner;

    [SerializeField]
    private Camera m_characterCamera;

    [SerializeField]
    private Transform m_target = null;

    [SerializeField]
    private NetworkView m_networkView = null;

    [SerializeField]
    private Transform m_prefabsTarget = null;

    void Start()
    {
        if (m_target == null)
            m_target = (Transform)Instantiate(m_prefabsTarget, Vector3.zero, Quaternion.identity);
    }

	void Update ()
    {
        if (Network.isClient && m_owner == Network.player)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = m_characterCamera.ScreenPointToRay(Input.mousePosition);
                
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000, 1 << LayerMask.NameToLayer("Ground")))
                    m_networkView.RPC("setTarget", RPCMode.Server, Network.player, hit.point);

            }
        }
	}

    [RPC]
    void setTarget(NetworkPlayer player, Vector3 targetPosition)
    {
        if (Network.isServer)
        {
            m_target.position = targetPosition;

            GameObject gameManager = GameObject.Find("GameManager");

            if (gameManager == null)
            {
                Debug.LogError("gameManager Failed");
                return;
            }

            MoveManagerSurvivorScript moveManagerSurvivor = gameManager.GetComponent<MoveManagerSurvivorScript>();
            moveManagerSurvivor.setTarget(player, m_target);
        }
    }

    [RPC]
    public void SetPlayer(NetworkPlayer owner)
    {
        m_owner = owner;
    }

    public void setCameraTransform(Camera cameraObject)
    {
        m_characterCamera = cameraObject;
    }
}
