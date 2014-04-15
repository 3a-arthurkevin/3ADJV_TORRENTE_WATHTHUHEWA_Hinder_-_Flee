using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryScript : MonoBehaviour
{
    [SerializeField]
    int m_quantityMaxForOneItem = 10;

    [SerializeField]
    int m_nbSlotInventory = 10;
    
    List<Slot> m_inventory;

	// Use this for initialization
	void Start () 
    {
        m_inventory = new List<Slot>();

        for (int i = 0; i < m_nbSlotInventory; i++)
        {
            m_inventory.Add(new Slot());
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public Slot getItem(int index)
    {
        return m_inventory[index];
    }

    public void AddItem(int idItemToAdd/*, int quantityToAdd*/)
    {
        foreach (Slot slotInventory in m_inventory)
        {
            if (slotInventory.id == idItemToAdd)
            {
                slotInventory.setQuantity(1/*quantityToAdd*/, m_quantityMaxForOneItem);
            }
        }
    }

    public void throwItem(int idItemToThrow)
    {
        m_inventory[idItemToThrow].resetSlot(); 
    }
}
