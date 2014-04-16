using UnityEngine;
using System.Collections;

public class InteractionsWithSurvivorScript : MonoBehaviour {

    [SerializeField]
    private int m_idItem = 0;

    [SerializeField]
    private string m_description = "Item test";

    [SerializeField]
    private int m_quantity = 1;

    [SerializeField]
    private MeshRenderer m_itemMeshRenderer;

    [SerializeField]
    private MeshFilter m_ItemMeshFilter;

    [SerializeField]
    private NetworkView m_networkView;

    private bool m_hasClicked;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnMouseDown()
    {
        if (!m_hasClicked && Network.isClient)
        {
            m_hasClicked = true;
            m_networkView.RPC("hasClickedTrueForServer", RPCMode.Server, Network.player);
        }
    }

    void OnMouseUp()
    {
        if (m_hasClicked && Network.isClient)
        {
            m_hasClicked = false;
            m_networkView.RPC("hasClickedFalseForServer", RPCMode.Server, Network.player);
        }
    }

    [RPC]
    void hasClickedTrueForServer(NetworkPlayer clientKey)
    {
        m_hasClicked = true;
    }

    [RPC]
    void hasClickedFalseForServer(NetworkPlayer clientKey)
    {
        m_hasClicked = false;
    }

    void OnTriggerStay(Collider survivor)
    {
        if (Network.isServer)
        {
            if (m_hasClicked)
            {
                
            }
        }
    }
}
