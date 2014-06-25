using UnityEngine;
using System.Collections;

public class InteractionsWithSurvivorScript : MonoBehaviour
{
    public enum ItemId
    {
        PiegeLoup,
        Potion,
        Autre
    }

    /*
        Script attaché aux gameObject Items
    */

    [SerializeField]
    private ItemId m_idItem;

    [SerializeField]
    private string m_name;
    
    [SerializeField]
    private string m_description;

    [SerializeField]
    private int m_quantity;

    [SerializeField]
    private float m_range;
    /*
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
                InventoryItemScript inventaire = survivor.gameObject.GetComponent<InventoryItemScript>();
                int slotPosition = inventaire.itemExistInInventory((int) m_idItem);
                bool itemPicked = true;

                if(slotPosition >= 0)
                {
                    if (inventaire.getItem(slotPosition).checkQuantityBeforeAddItem((int)m_idItem))
                        inventaire.addItemQuantityNetwork(slotPosition, m_quantity);
                    else
                        itemPicked = false;
                }
                else
                {
                    slotPosition = inventaire.freeIndexExistInInventory();
                    if (slotPosition >= 0)
                        inventaire.addItemNetwork(slotPosition, (int)m_idItem, m_name, m_description, m_range, m_quantity);
                }

                if(itemPicked)
                {
                    m_destroy = true;
                    Network.Destroy(this.gameObject);
                }
            }
        }
    }

    [RPC]
    void selfDestroy()
    {
        Destroy(this.gameObject);
    }

}
