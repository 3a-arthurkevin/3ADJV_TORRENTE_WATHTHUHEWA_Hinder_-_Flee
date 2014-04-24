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


    /* GUI PART */
    private int m_nbCelluleX = 10;
    private int m_nbCelluleY = 10;
    private float m_screenWidth = Screen.width;
    private float m_screenHeight = Screen.height;
    private float m_largeurCellule;
    private float m_hauteurCellule;
    private Rect layoutBottom;
    private Rect boxItem;

    void Start()
    {
        m_inventory = new List<Slot>();

        for (int i = 0; i < m_nbSlotInventory; i++)
        {
            m_inventory.Add(new Slot(m_quantityMaxForOneItem));
        }

        //GUI PArt
        m_screenWidth = Screen.width;
        m_screenHeight = Screen.height;
        m_largeurCellule = m_screenWidth / m_nbCelluleX;
        m_hauteurCellule = m_screenHeight / m_nbCelluleY;
        layoutBottom = new Rect(0, m_screenHeight - m_hauteurCellule * 2, m_screenWidth, m_hauteurCellule * 2);
        boxItem = new Rect(layoutBottom.x + layoutBottom.width * 0.4f, layoutBottom.y + layoutBottom.height * 0.5f, layoutBottom.width * 0.4f, layoutBottom.height * 0.5f);
    }


    void OnGUI()
    {
        if (Network.player == gameObject.GetComponent<InputManagerMoveSurvivorScript>().getNetworkPlayer())
        {
            GUILayout.BeginArea(boxItem, new GUIStyle("Box"));
            GUILayout.BeginHorizontal();

            foreach (Slot slot in m_inventory)
            {
                GUILayout.BeginVertical();
                if (slot.id == -1)
                    GUILayout.Label("Vide");
                else
                    GUILayout.Label("id : " + slot.id.ToString() + "\n " + "quantité : " + slot.quantity);

                GUILayout.EndVertical();
            }

            GUILayout.EndHorizontal();
            GUILayout.EndArea();
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
