using UnityEngine;
using System.Collections;

public class Slot 
{
    private int m_idItem;
    private int m_quantity;
    private int m_maxQuantity;

    public int maxQuantity
    {
        get { return this.m_maxQuantity; }
        set { this.m_maxQuantity = value; }
    }

    //Constructeur 1
    public Slot(int pQuantiteMax)
    {
        this.m_idItem = -1;
        this.m_quantity = 0;
        this.m_maxQuantity = pQuantiteMax;
    }

    //Constructeur 2
    public Slot(int pId, int pQuantite, int pQuantityMax)
    {
        this.m_idItem = pId;
        this.m_quantity = pQuantite;
        this.m_maxQuantity = pQuantityMax;
    }

    //Propriété Id
    public int id
    {
        get { return this.m_idItem; }
        set { this.m_idItem = value; }
    }

    public int quantity
    {
        get { return this.m_quantity; }
        set 
        { 
            this.m_idItem = value;

            if (this.m_idItem > m_maxQuantity)
            {
                this.m_idItem = m_maxQuantity;
            }
        }
    }

    //Set un slot
    public void addItem(int id, int quantity)
    {
        this.m_idItem = id;
        this.m_quantity += quantity;
    }

    //Quand on jette un objet de l'inventaire
    public void resetSlot()
    {
        this.m_idItem = -1;
        this.m_quantity = 0;
    }
}
