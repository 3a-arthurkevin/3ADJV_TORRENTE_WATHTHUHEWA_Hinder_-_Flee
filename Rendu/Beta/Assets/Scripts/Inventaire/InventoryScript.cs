using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryScript : MonoBehaviour
{
    /*
    [SerializeField]
    int m_quantityMaxForOneItem = 10;
    */

    [SerializeField]
    int m_nbSlotInventory = 10;
    
    List<Slot> m_inventory;

    int m_indexOfFirstSlotAvailable = 0;

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
        bool findItemInInventory = false;

        for(int i = 0; i < m_indexOfFirstSlotAvailable; i++)
        {
            if (m_inventory[i].id == idItemToAdd)
            {
                m_inventory[i].setQuantityToAdd(1/*quantityToAdd*/, /*m_quantityMaxForOneItem*/ 10);
                findItemInInventory = true;
            }
            
        }

        if (!findItemInInventory && m_indexOfFirstSlotAvailable < 10)
        {
            m_inventory[m_indexOfFirstSlotAvailable].addItem(idItemToAdd, 1);
            //PROBLEME ICI --> Revoir la gestion des slot lors de l'ajout et suppression
            //List.ElementAt() ne fonctionne pas --> pourquoi ?
            m_indexOfFirstSlotAvailable++;
        }
        else
        {
            Debug.LogError("Plus de place dans l'inventaire pour une nouvel Item");
        }
    }

    public void throwItem(int indexItemToThrow)
    {
        m_inventory[indexItemToThrow].resetSlot();
        m_indexOfFirstSlotAvailable = indexItemToThrow;
    }
}
