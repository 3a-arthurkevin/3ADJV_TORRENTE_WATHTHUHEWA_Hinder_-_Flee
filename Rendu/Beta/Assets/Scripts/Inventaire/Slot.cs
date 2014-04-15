using UnityEngine;
using System.Collections;

public class Slot 
{
    private int m_idItem;
    private int m_quantity;

    public Slot()
    {
        this.m_idItem = -1;
        this.m_quantity = 0;
    }

    public Slot(int pId, int pQuantite)
    {
        this.m_idItem = pId;
        this.m_quantity = pQuantite;
    }

    public int id
    {
        get { return m_idItem; }
        set { m_idItem = value; }
    }

    public void setQuantity(int value, int quantityMax)
    {
        this.m_quantity += value;

        if (this.m_quantity > quantityMax)
        {
            this.m_quantity = quantityMax;
        }
    }

    public int getQuantity()
    {
        return this.m_quantity;
    }

    public void addItem(int id, int quantity)
    {
        this.m_idItem = id;
        this.m_quantity = quantity;
    }

    public void resetSlot()
    {
        this.m_idItem = -1;
        this.m_quantity = 0;
    }
}
