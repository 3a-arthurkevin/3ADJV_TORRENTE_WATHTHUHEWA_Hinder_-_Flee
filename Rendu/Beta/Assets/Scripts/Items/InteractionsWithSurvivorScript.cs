using UnityEngine;
using System.Collections;

public class InteractionsWithSurvivorScript : MonoBehaviour
{

    /*
        Script attaché aux gameObject Items
    */

    [SerializeField]
    private int m_idItem = 0;
    /*
    [SerializeField]
    private string m_description = "Item test";
    
    [SerializeField]
    private int m_quantity = 1;

    [SerializeField]
    private MeshRenderer m_itemMeshRenderer;

    [SerializeField]
    private MeshFilter m_ItemMeshFilter;
    */
    [SerializeField]
    private NetworkView m_networkView;
    
    private bool m_wantToPickItem = false;
    

    public int getId()
    {
        return m_idItem;
    }
    /*
    public int getQuantity()
    {
        return m_quantity;
    }
    */

    [RPC]
    void setBoolWantToPickItem(bool click)
    {
        m_wantToPickItem = click;
    }

    //Le serveur check si le client a cliqué sur un Item lorsqu'il est dans 
    //la zone de détection de ce dernier pour pouvoir le ramasser
    //C'est la fonction AddItem qui effectue l'ajout ou le non ajout
    //selon l'espace dans l'inventaire du survivant
    //Destruction gameObject (si Item ramassé) dans la scene mais concervation de ses infos (id/quantité) dans inventaire survivant
    void OnTriggerStay(Collider survivor)
    {
        if (survivor.gameObject.layer == LayerMask.NameToLayer("Survivor"))
        {
            if (Network.isClient)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = survivor.gameObject.GetComponent<InputManagerMoveSurvivorScript>().getCharacterCamera().ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 1000, 1 << LayerMask.NameToLayer("Item")))
                    {
                        if (!m_wantToPickItem)
                        {
                            m_wantToPickItem = true;
                            m_networkView.RPC("setBoolWantToPickItem", RPCMode.Server, true);
                        }
                    }
                }
            }
            if (Network.isServer)
            {
                if (m_wantToPickItem)
                {
                    if (survivor.gameObject.GetComponent<InventoryScript>().AddItem(m_idItem))
                    {
                        Debug.LogError("Objet ajouté à l'inventaire pour" + gameObject.name);
                        m_wantToPickItem = false;
                        Network.Destroy(this.gameObject);
                    }
                    else
                    {
                        Debug.LogError("Impossibilité d'ajouter cette objet à l'inventaire de " + gameObject.name);
                        m_networkView.RPC("setBoolWantToPickItem", RPCMode.All, false);
                    }
                }
            }
        }
    }

    /*
    void OnTriggerEnter(Collider survivor)
    {
        if (Network.isServer && !m_destroy)
        {
            if (survivor.gameObject.GetComponent<InventoryScript>().AddItem(m_idItem))
            {
                Debug.Log("Trying to pick an item");
                m_networkView.RPC("selfDestroy", RPCMode.All);
                m_destroy = true;
            }
            else
            {  
                Debug.LogError("Plus de place dans l'inventaire"); 
            }
        }
    } 
    
    [RPC]
    void selfDestroy()
    {
        Debug.LogError("Destruction de l'objet");
        Destroy(this.gameObject);
    }
    */
}
