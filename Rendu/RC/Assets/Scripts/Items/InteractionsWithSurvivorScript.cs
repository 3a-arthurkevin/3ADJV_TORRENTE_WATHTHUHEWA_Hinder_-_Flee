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

    private bool m_hasClicked;

    private bool m_destroy = false;

    //Quand clique enfoncé sur un gameobject Item, boolean mis à true pour client seulement et serveur
    void OnMouseDown()
    {
        if (!m_hasClicked && Network.isClient)
        {
            m_hasClicked = true;
            m_networkView.RPC("hasClickedForServer", RPCMode.Server, true);
        }
    }

    //Quand clique relaché sur un gameobject Item, boolean mis à false (état par défaut) pour client et serveur
    void OnMouseUp()
    {
        if (m_hasClicked && Network.isClient)
        {
            m_hasClicked = false;
            m_networkView.RPC("hasClickedForServer", RPCMode.Server, false);
        }
    }

    [RPC]
    void hasClickedForServer(bool click)
    {
        m_hasClicked = click;
    }

    //Le serveur check si le client a cliqué sur un Item lorsqu'il est dans 
    //la zone de détection de ce dernier pour pouvoir le ramasser
    //C'est la fonction AddItem qui effectue l'ajout ou le non ajout
    //selon l'espace dans l'inventaire du survivant
    //Destruction gameObject (si Item ramassé) dans la scene mais concervation de ses infos (id/quantité) dans inventaire survivant
    void OnTriggerStay(Collider survivor)
    {
        if (Network.isServer)
        {
            if (m_hasClicked && !m_destroy)
            {
                if (survivor.gameObject.GetComponent<InventoryScript>().AddItem(m_idItem))
                {
                    Debug.LogError("Objet ajouté à l'inventaire pour" + survivor.gameObject.name);
                    Network.Destroy(this.gameObject);
                    m_destroy = true;
                }
                else
                {
                    Debug.LogError("Impossibilité d'ajouter cette objet à l'inventaire de " + survivor.gameObject.name);
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
    */
    [RPC]
    void selfDestroy()
    {
        Debug.LogError("Destruction de l'objet");
        Destroy(this.gameObject);
    }

}
