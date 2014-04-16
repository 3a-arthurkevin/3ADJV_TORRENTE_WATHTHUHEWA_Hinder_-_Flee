using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryScript : MonoBehaviour
{
    [SerializeField]
    int m_quantityMaxForOneItem = 10;

    [SerializeField]
    int m_nbSlotInventory = 6;
    
    List<Slot> m_inventory;


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
    public void AddItem(int idItemToAdd/*, int quantityToAdd*/)
    {
        bool findItemInInventory = false;
        bool indexFreeExist = false;
        int indexFree = -1;

        int i = 0;
        while(i < m_nbSlotInventory && !findItemInInventory)
        {
            if (m_inventory[i].id == idItemToAdd)
            {
                m_inventory[i].quantity += 1;
                findItemInInventory = true;
            }
            else if (!indexFreeExist && m_inventory[i].id == -1)
            {
                indexFreeExist = true;
                indexFree = i;
            }
            i++;
        }

        if (!findItemInInventory && indexFreeExist)
        {
            m_inventory[indexFree].addItem(idItemToAdd, 1);
        }
        else
        {
            Debug.LogError("Plus de place dans l'inventaire pour un nouvel Item");
        }
    }

    public void throwItem(int indexItemToThrow)
    {
        m_inventory[indexItemToThrow].resetSlot();
    }
}
