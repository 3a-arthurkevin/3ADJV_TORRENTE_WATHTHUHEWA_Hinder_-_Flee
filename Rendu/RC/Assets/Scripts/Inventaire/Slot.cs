using UnityEngine;
using System.Collections;

public class Slot
{
    private int m_idItem;
    private int m_quantity;
    private int m_maxQuantity;

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

    //Propriété Quantity
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

    //Propriété maxQuantity
    public int maxQuantity
    {
        get { return this.m_maxQuantity; }
        set { this.m_maxQuantity = value; }
    }

    //Vérification de l'espace dans le slot (utilisé avant d'ajouter un item)
    public bool checkQuantityBeforeAddItem(int quantity)
    {
        bool check = true;
        if (m_quantity >= m_maxQuantity)
        {
            check = false;
        }
        return check;
    }

    //Set un slot (utilisé pour l'ajout d'un Item existant et inexistant dans l'inventaire )
    public void addItem(int id, int quantity)
    {
        this.m_idItem = id;
        if (m_quantity + quantity <= m_maxQuantity)
        {
            this.m_quantity += quantity;
        }
        else if (m_quantity < m_maxQuantity && m_quantity + quantity >= m_maxQuantity)
        {
            this.m_quantity = m_maxQuantity;
        }
        else
        {
            //Debug.LogError("L'inventaire ne peut contenir l'objet " + id + " que " + m_maxQuantity + " fois");
        }
    }

    //Quand le joueur jette un objet de l'inventaire
    public void resetSlot()
    {
        this.m_idItem = -1;
        this.m_quantity = 0;
    }
}
