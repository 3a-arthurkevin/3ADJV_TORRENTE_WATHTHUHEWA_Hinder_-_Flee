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
            m_networkView.RPC("hasClickedTrueForServer", RPCMode.Server);
        }
    }

    void OnMouseUp()
    {
        if (m_hasClicked && Network.isClient)
        {
            m_hasClicked = false;
            m_networkView.RPC("hasClickedFalseForServer", RPCMode.Server);
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
                if (survivor.gameObject.GetComponent<InventoryScript>().AddItem(m_idItem))
                {
                    Debug.LogError("Objet ajouté à l'inventaire pour" + survivor.gameObject.name);
                    m_networkView.RPC("selfDestroy", RPCMode.All);
                }
                else
                {
                    Debug.LogError("Impossibilité d'ajouter cette objet à l'inventaire de " + survivor.gameObject.name);
                }
            }
        }
    }

    [RPC]
    void selfDestroy()
    {
        Debug.LogError("Destruction de l'objet");
        Destroy(this.gameObject);
    }
}
