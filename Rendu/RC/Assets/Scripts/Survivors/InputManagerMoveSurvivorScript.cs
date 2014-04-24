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

    private MoveManagerSurvivorScript m_moveManager;

    void Start()
    {
        if (m_target == null)
            m_target = (Transform)Instantiate(m_prefabsTarget, Vector3.zero, Quaternion.identity);

        if (m_moveManager == null)
            m_moveManager = GetComponent<MoveManagerSurvivorScript>();
    }

	void Update ()
    {
        if (Network.isClient && m_owner == Network.player)
        {
            if (Input.GetButtonDown("MoveCharacter"))
            {
                Ray ray = m_characterCamera.ScreenPointToRay(Input.mousePosition);
                
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000, 1 << LayerMask.NameToLayer("Ground")))
                    m_networkView.RPC("wantToGo", RPCMode.Server, Network.player, hit.point);

            }
        }
	}

    [RPC]
    void wantToGo(NetworkPlayer player, Vector3 targetPosition)
    {
        if (Network.isServer)
        {
            m_target.position = targetPosition;

            m_moveManager.setTarget(targetPosition);
        }
    }

    [RPC]
    public void SetPlayer(NetworkPlayer owner)
    {
        m_owner = owner;
        WeaponManagerScript weapon = GetComponentInChildren<WeaponManagerScript>();
        weapon.SetPlayer(owner);
    }

    public void setCameraTransform(Camera cameraObject)
    {
        m_characterCamera = cameraObject;
        WeaponManagerScript weapon = GetComponentInChildren<WeaponManagerScript>();
        weapon.SetCamera(cameraObject);
    }

    public Camera getCharacterCamera()
    {
        return this.m_characterCamera;
    }

    public NetworkPlayer getNetworkPlayer()
    {
        return this.m_owner;
    }
}
