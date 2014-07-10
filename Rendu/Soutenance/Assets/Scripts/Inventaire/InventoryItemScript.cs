using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryItemScript : MonoBehaviour
{
    [SerializeField]
    private NetworkView m_networkView;

    [SerializeField]
    private NetworkPlayer m_owner;

    [SerializeField]
    private GameObject m_player;

    [SerializeField]
    private Camera m_playerCamera;
    
    [SerializeField]
    private GameObject m_rangeObject;
    
    [SerializeField]
    private Texture2D m_viseurCursor;

    [SerializeField]
    private int m_quantityMaxForOneItem = 10;

    [SerializeField]
    private int m_nbSlotInventory = 6;

    private List<Slot> m_inventory;

    private int m_slotToUse;

    private bool m_isAiming;

    /* GUI PART */
    //private int m_nbCelluleX = 10;
    private int m_nbCelluleY = 10;
    private float m_screenWidth = Screen.width;
    private float m_screenHeight = Screen.height;
    //private float m_largeurCellule;
    private float m_hauteurCellule;
    private Rect layoutBottom;
    private Rect boxItem;

    void Start()
    {
        m_owner = m_player.GetComponent<InputManagerMoveSurvivorScript>().getNetworkPlayer();
        m_playerCamera = m_player.GetComponent<InputManagerMoveSurvivorScript>().getCharacterCamera();
        m_rangeObject.GetComponent<MeshRenderer>().enabled = false;

        m_inventory = new List<Slot>();

        for (int i = 0; i < m_nbSlotInventory; i++)
        {
            m_inventory.Add(new Slot(m_quantityMaxForOneItem));
        }

        m_isAiming = false;
        m_slotToUse = -1;

        //GUI Part
        m_screenWidth = Screen.width;
        m_screenHeight = Screen.height;
        //m_largeurCellule = m_screenWidth / m_nbCelluleX;
        m_hauteurCellule = m_screenHeight / m_nbCelluleY;
        layoutBottom = new Rect(0, m_screenHeight - m_hauteurCellule * 2, m_screenWidth, m_hauteurCellule * 2);
        boxItem = new Rect(layoutBottom.x + layoutBottom.width * 0.4f, layoutBottom.y + layoutBottom.height * 0.5f, layoutBottom.width * 0.4f, layoutBottom.height * 0.5f);
    }


    void OnGUI()
    {
        if (m_owner == Network.player && Network.isClient)
        {
            GUILayout.BeginArea(boxItem, new GUIStyle("Box"));
            GUILayout.BeginHorizontal();

            foreach (Slot slot in m_inventory)
            {
                GUILayout.BeginVertical();
                if (slot.Id == -1)
                    GUILayout.Label("Vide");
                else
                    GUILayout.Label(slot.Name + "\n " + "qté : " + slot.Quantity);

                GUILayout.EndVertical();
            }

            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }


    // Update is called once per frame
    
	void Update () 
    {
        if (Network.player == m_owner && Network.isClient)
        {
            if (!m_isAiming)
                checkInputForUseItem();
            else
                checkInputToPutItem();
        }
	}

    //_________________________________________________

    public void checkInputForUseItem()
    {
        //si pas de mode clique
        /*sinon
            * attente clique
            *  si clique, récupérer position hit
            *  voir si distance respecte range
            *  avec des if, instanciate le bon préfab
        */
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_slotToUse = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_slotToUse = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            m_slotToUse = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            m_slotToUse = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            m_slotToUse = 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            m_slotToUse = 5;
        }

        if (m_slotToUse >= 0 && m_slotToUse < m_nbSlotInventory)
        {
            m_networkView.RPC("checkItemToUse", RPCMode.Server, m_slotToUse, m_owner);
        }
    }

    [RPC]
    public void checkItemToUse(int slotPosition, NetworkPlayer owner)
    {
        m_slotToUse = slotPosition;

        if (m_inventory[slotPosition].Id >= 0 && m_inventory[slotPosition].Quantity > 0)
        {
            if (m_inventory[slotPosition].Range > 0)
                m_networkView.RPC("setAimingTrue", RPCMode.All, owner, (m_inventory[m_slotToUse].Range));
            else
            {
                m_networkView.RPC("directUseItem", RPCMode.Others, m_slotToUse, owner);
            }
        }
        else
            m_networkView.RPC("stopItemUse", RPCMode.All, m_slotToUse, owner);
    }

    [RPC]
    public void setAimingTrue(NetworkPlayer owner, float range)
    {
        m_isAiming = true;

        if (m_owner == owner)
        {
            Cursor.SetCursor(m_viseurCursor, Vector2.zero, CursorMode.Auto);
            m_rangeObject.GetComponent<MeshRenderer>().enabled = true;
            m_rangeObject.transform.localScale = new Vector3(range * 2, 0.05f, range * 2);
        }
    }

    [RPC]
    public void stopItemUse(int slotPosition, NetworkPlayer owner)
    {
        if (m_owner == owner && m_isAiming == true)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            m_rangeObject.transform.localScale = new Vector3(1, 1, 1);
            m_rangeObject.GetComponent<MeshRenderer>().enabled = false;
        }

        m_inventory[slotPosition].Quantity -= 1;
        if (m_inventory[slotPosition].Quantity <= 0)
            resetSlot(slotPosition);

        m_slotToUse = -1;
        m_isAiming = false;
    }

    public void checkInputToPutItem()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetButtonDown("LaunchSkill"))
            {
                Ray ray = m_playerCamera.ScreenPointToRay(new Vector3(Input.mousePosition.x + (m_viseurCursor.texelSize.x / 2), Input.mousePosition.y - (m_viseurCursor.texelSize.y / 2), 0));
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100f, 1 << LayerMask.NameToLayer("Ground")))
                    m_networkView.RPC("checkCanPutItem", RPCMode.Server, hit.point, m_owner);
            }
            else if(!(Input.GetButtonDown("MoveCharacter") || Input.GetKeyDown(KeyCode.Space)))
            {
                m_networkView.RPC("stopItemUse", RPCMode.All, m_slotToUse, m_owner);
            }
        }
    }

    [RPC]
    public void checkCanPutItem(Vector3 hitPosition, NetworkPlayer owner)
    {
        if (Mathf.Abs(Vector3.Distance(m_player.transform.position, hitPosition)) <= m_inventory[m_slotToUse].Range)
        {
            useItem(m_slotToUse, hitPosition, owner);
        }
    }

    public void useItem(int slotPosition, Vector3 hitPosition, NetworkPlayer owner)
    {
        GameObject itemPrefab = ItemFactoryScript.getItemById(m_inventory[slotPosition].Id);

        if (itemPrefab == null)
            return;

        Network.Instantiate(itemPrefab, hitPosition, m_player.transform.localRotation, 0);

        m_networkView.RPC("stopItemUse", RPCMode.All, slotPosition, owner);
    }

    [RPC]
    public void directUseItem(int slotPosition, NetworkPlayer owner)
    {
        //GameObject itemPrefab = ItemFactoryScript.getItemById(m_inventory[slotPosition].Id);

        //if (itemPrefab == null)
        //    return;

       // Network.Instantiate(itemPrefab, Vector3.zero, m_player.transform.localRotation, 0);

        
        m_networkView.RPC("stopItemUse", RPCMode.All, slotPosition, m_owner);
    }


    //________________________________________
    
    //Retourne un Slot de l'inventaire à une position précise
    public Slot getItem(int index)
    {
        return m_inventory[index];
    }

    //Vérification si l'id objet est présent dans un des slot de l'inventaire
    public int itemExistInInventory(int idItemToAdd)
    {
        int indexToAdd = -1;

        int i = 0;
        while (i < m_nbSlotInventory && indexToAdd == -1)
        {
            if (m_inventory[i].Id == idItemToAdd)
                indexToAdd = i;
            i++;
        }

        return indexToAdd;
    }

    //Vérification si un slot est vide (pour accueillir un objet)
    public int freeIndexExistInInventory()
    {
        int indexToAdd = -1;

        int i = 0;
        while (i < m_nbSlotInventory && indexToAdd == -1)
        {
            if (m_inventory[i].Id == -1)
            {
                indexToAdd = i;
            }
            i++;
        }

        return indexToAdd;
    }

    //Fonction d'ajout d'un objet (avec sa fonction RPC)
    public void addItemNetwork(int slotPosition, int idItemToAdd, string name, string description, float range, int quantityToAdd)
    {
        m_networkView.RPC("addItem", RPCMode.All, slotPosition, idItemToAdd, name, description, range, quantityToAdd);
    }

    [RPC]
    void addItem(int slotPosition, int idItemToAdd, string name, string description, float range, int quantityToAdd)
    {
        m_inventory[slotPosition].addNewItem(idItemToAdd, name, description, range, quantityToAdd);
    }

    //Fonction d'ajout d'une quantité à un objet (avec sa fonction RPC) qui existe déja dans l'inventaire
    public void addItemQuantityNetwork(int slotPosition, int quantityToAdd)
    {
        m_networkView.RPC("addItemQuantity", RPCMode.All, slotPosition, quantityToAdd);
    }


    [RPC]
    void addItemQuantity(int slotPosition, int quantityToAdd)
    {
        m_inventory[slotPosition].addItemQuantity(quantityToAdd);
    }

    public void resetSlot(int slotPosition)
    {
        m_inventory[slotPosition].resetSlot();
    }
}
