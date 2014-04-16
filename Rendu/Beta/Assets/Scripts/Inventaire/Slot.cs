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
    public Slot()
    {
        this.m_idItem = -1;
        this.m_quantity = 0;
    }

    //Constructeur 2
    public Slot(int pId, int pQuantite)
    {
        this.m_idItem = pId;
        this.m_quantity = pQuantite;
    }

    //Propriété Id
    public int id
    {
        get { return m_idItem; }
        set { m_idItem = value; }
    }

    //Pour pouvoir modifier facilement la quantité Max d'un object 
    //Pas de propriété mais getter et setter --> paramètre quantityMax est SerializedField de InventoryScript
    public void setQuantityToAdd(int value, int quantityMax)
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

    //Set un slot (utilisé quand l'inventaire ne comporte pas l'item que l'on veut ajouter)
    public void addItem(int id, int quantity)
    {
        this.m_idItem = id;
        this.m_quantity = quantity;
    }

    //Quand on jette un objet de l'inventaire
    public void resetSlot()
    {
        this.m_idItem = -1;
        this.m_quantity = 0;
    }
}
