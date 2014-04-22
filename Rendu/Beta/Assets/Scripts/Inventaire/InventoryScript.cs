using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryScript : MonoBehaviour
{
    [SerializeField]
    private NetworkView m_networkView;

    [SerializeField]
    private Camera m_characterCamera;

    [SerializeField]
    private int m_quantityMaxForOneItem = 10;

    [SerializeField]
    private int m_nbSlotInventory = 6;

    private List<Slot> m_inventory;

    private bool m_wantToPickAnItem = false;

    void Start()
    {
        m_inventory = new List<Slot>();

        for (int i = 0; i < m_nbSlotInventory; i++)
        {
            m_inventory.Add(new Slot(m_quantityMaxForOneItem));
        }

        m_characterCamera = gameObject.GetComponent<InputManagerMoveSurvivorScript>().getCharacterCamera();
    }

    // Update is called once per frame
    /*
	void Update () 
    {
        
	}
    */

    //Le serveur check si le client a cliqué sur un Item lorsqu'il est dans 
    //la zone de détection de ce dernier pour pouvoir le ramasser
    //C'est la fonction AddItem qui effectue l'ajout ou le non ajout
    //selon l'espace dans l'inventaire du survivant
    //Destruction gameObject (si Item ramassé) dans la scene mais concervation de ses infos (id/quantité) dans inventaire survivant
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            if (Network.isClient)
            {  
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = m_characterCamera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 1000, 1 << LayerMask.NameToLayer("Item")))
                    {
                        if (!m_wantToPickAnItem)
                        {
                            m_wantToPickAnItem = true;
                            m_networkView.RPC("setBoolPickItemForServer", RPCMode.Server, true);
                        }
                    }
                }
            }
            if (Network.isServer)
            {
                if (m_wantToPickAnItem)
                {
                    if (AddItem(other.gameObject.GetComponent<InteractionsWithSurvivorScript>().getId()))
                    {
                        Debug.LogError("Objet ajouté à l'inventaire pour" + gameObject.name);
                        Network.Destroy(other.gameObject);
                    }
                    else
                    {
                        Debug.LogError("Impossibilité d'ajouter cette objet à l'inventaire de " + gameObject.name);
                    }
                    m_networkView.RPC("setBoolPickItemForServer", RPCMode.All, false);
                }
            }
        }
    }

    [RPC]
    private void setBoolPickItemForServer(bool value)
    {
        m_wantToPickAnItem = value;
    }

    [RPC]
    private void wantToPickItem(Vector3 itemPosition)
    {
        
    }

    public Slot getItem(int index)
    {
        return m_inventory[index];
    }

    //Pour l'instant quantité 1 lorsque l'on trouve un objet
    //Fonction qui recherche dans l'inventaire si l'Id de l'Item existe dans l'inventaire
    //Si Item existant dans l'inventaire --> ajout de la quantié au bon index dans l'inventaire
    //Si Item non trouvé --> recherche du 1ere index libre pour l'ajout
    //Sinon impossibilité d'ajouter (quand plus de place ou quantité max de l'Item atteint)
    public bool AddItem(int idItemToAdd/*, int quantityToAdd*/)
    {
        bool canAddItemToInventory = false;
        bool findItemInInventory = false;
        bool indexFreeExist = false;
        int indexToAdd = -1;

        int i = 0;
        while (i < m_nbSlotInventory && !findItemInInventory)
        {
            if (m_inventory[i].id == idItemToAdd)
            {
                indexToAdd = i;
                findItemInInventory = true;
            }
            else if (!indexFreeExist && m_inventory[i].id == -1)
            {
                indexFreeExist = true;
                indexToAdd = i;
            }
            i++;
        }

        if (findItemInInventory || indexFreeExist)
        {
            if (m_inventory[indexToAdd].checkQuantityBeforeAddItem(1))
            {
                m_networkView.RPC("addItemToInventoryForAll", RPCMode.All, idItemToAdd, indexToAdd);
                canAddItemToInventory = true;
            }
        }

        return canAddItemToInventory;
    }

    [RPC]
    void addItemToInventoryForAll(int idItemToAdd/*, int quantityToAdd*/, int indexInventory)
    {
        //m_inventory est une list de Slot --> donc voir le script Slot pour mieux comprendre
        m_inventory[indexInventory].addItem(idItemToAdd, 1);
    }

    public void throwItem(int indexItemToThrow)
    {
        m_inventory[indexItemToThrow].resetSlot();
    }
}
