using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryScript : MonoBehaviour
{
    [SerializeField]
    private NetworkView m_networkView;

    [SerializeField]
    private int m_quantityMaxForOneItem = 10;

    [SerializeField]
    private int m_nbSlotInventory = 6;
    
    private List<Slot> m_inventory;


	void Start () 
    {
        m_inventory = new List<Slot>();

        for (int i = 0; i < m_nbSlotInventory; i++)
        {
            m_inventory.Add(new Slot(m_quantityMaxForOneItem));
        }
	}
	
	// Update is called once per frame
    /*
	void Update () 
    {
        
	}
    */

    public Slot getItem(int index)
    {
        return m_inventory[index];
    }

    //Pour l'instant quantité 1 lorsque l'on trouve un objet
    public bool AddItem(int idItemToAdd/*, int quantityToAdd*/)
    {
        bool canAddItemToInventory = false;
        bool findItemInInventory = false;
        bool indexFreeExist = false;
        int indexToAdd = -1;

        int i = 0;
        while(i < m_nbSlotInventory && !findItemInInventory)
        {
            if (m_inventory[i].id == idItemToAdd)
            {
                m_inventory[i].quantity += 1;
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
            //m_inventory[indexToAdd].addItem(idItemToAdd, 1);
            m_networkView.RPC("addItemToInventoryForAll", RPCMode.All, idItemToAdd, indexToAdd);
            canAddItemToInventory = true;
        }
        else
        {
            Debug.LogError("Plus de place dans l'inventaire pour un nouvel Item");
            canAddItemToInventory = false;
        }
        return canAddItemToInventory;
    }

    [RPC]
    void addItemToInventoryForAll(int idItemToAdd/*, int quantityToAdd*/, int indexInventory)
    {
        m_inventory[indexInventory].addItem(idItemToAdd, 1);
    }

    public void throwItem(int indexItemToThrow)
    {
        m_inventory[indexItemToThrow].resetSlot();
    }
}
