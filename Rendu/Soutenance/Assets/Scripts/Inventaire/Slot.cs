using UnityEngine;
using System.Collections;

public class Slot
{
    private int m_idItem;
    private string m_name;
    private string m_description;
    private float m_range;
    private int m_quantity;
    private int m_maxQuantity;

    //Constructeur (sert lors de l'init de l'inventaire)
    public Slot(int pQuantiteMax)
    {
        m_idItem = -1;
        m_name = "-";
        m_description = "-";
        m_quantity = 0;
        m_maxQuantity = pQuantiteMax;
        m_range = -1;
    }

    //Propriété Id
    public int Id
    {
        get { return m_idItem; }
        set { m_idItem = value; }
    }

    public string Name
    {
        get { return m_name; }
        set { m_name = value; }
    }

    public string Description
    {
        get { return m_description; }
        set { m_description = value; }
    }

    //Propriété Quantity
    public int Quantity
    {
        get { return m_quantity; }
        set
        {
            m_quantity = value;

            if (m_idItem > m_maxQuantity)
            {
                m_idItem = m_maxQuantity;
            }
        }
    }

    //Propriété maxQuantity
    public int MaxQuantity
    {
        get { return m_maxQuantity; }
        set { m_maxQuantity = value; }
    }

    //Property Range
    public float Range
    {
        get { return m_range; }
        set { m_range = value; }
    }

    //Vérification de l'espace dans le slot (utilisé avant d'ajouter un item)
    public bool checkQuantityBeforeAddItem(int quantity)
    {
        bool check = true;
        if (m_quantity + quantity > m_maxQuantity)
        {
            check = false;
        }
        return check;
    }

    public void addQuantity(int quantityToAdd)
    {
        m_quantity += quantityToAdd;
        if (m_quantity + quantityToAdd > m_maxQuantity)
            m_quantity = m_maxQuantity;
    }

    //Set un slot (utilisé pour l'ajout d'un Item existant et inexistant dans l'inventaire )
    public void addNewItem(int id, string name, string description, float range, int quantity)
    {
        m_idItem = id;
        m_name = name;
        m_description = description;
        m_range = range;
        addQuantity(quantity);
    }

    public void addItemQuantity(int quantity)
    {
        addQuantity(quantity);
    }

    //Quand le joueur jette un objet de l'inventaire
    public void resetSlot()
    {
        m_idItem = -1;
        m_name = "-";
        m_description = "-";
        m_quantity = 0;
        m_range = -1;
    }
}
