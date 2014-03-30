using UnityEngine;
using System.Collections;

public class SurvivorManagerForStairScript : MonoBehaviour
{
    [SerializeField]
    private Texture2D m_cursor;

    private Vector2 m_hotSpot;
    private CursorMode m_cursorMode;

    [SerializeField]
    private Transform m_stairOut;

    [SerializeField]
    private int m_floorOfStairOut;

    [SerializeField]
    private bool m_hasClicked;

    [SerializeField]
    private NetworkView m_networkView;

    void Awake()
    {
        m_cursorMode = CursorMode.Auto;
        m_hotSpot = Vector2.zero;
        m_hasClicked = false;
    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(m_cursor, m_hotSpot, m_cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, m_hotSpot, m_cursorMode);
    }

    void OnMouseDown()
    {
        m_hasClicked = true;
    }

    void OnMouseUp()
    {
        m_hasClicked = false;
    }

    [RPC]
    void setClickedStairFromClientToServer(NetworkPlayer player)
    {
        m_hasClicked = true;
        //GameObject.Find("GameManager").GetComponent<MoveManagerSurvivorScript>().resetPathAfterStair(player);
        //Update de l'étage courant du player
        //GameObject.Find("GameManager").GetComponent<MoveManagerSurvivorScript>().getPlayerMoveData(player).IsInFloor = m_floorOfStairOut;
    }

    [RPC]
    void setClickedStairForAll()
    {
        m_hasClicked = false;
    }

    void setSurvivorPositionAfterStair(Collider survivor)
    {
        //Vector3.up pour que le survivant soit au dessus du plane (sinon survivor coupé en 2 par le plane)
        survivor.gameObject.transform.position = m_stairOut.position + Vector3.up;
    }

    void OnTriggerStay(Collider survivor)
    {
        if (m_hasClicked && Network.isClient)
        {
            //envoyer en RPC m_hasClicked et reset du path
            m_networkView.RPC("setClickedStairFromClientToServer", RPCMode.Server, Network.player);

            //Teleportation du survivant exectuer par le joueur (sera fait coté serveur aussi)
            setSurvivorPositionAfterStair(survivor);

            //Le serveur ne s'occupe pas des cameras donc code exectuer que chez le client (pour sa camera)
            //Récupération de la camera pour la reset sur le joueur apres avoir pris l'escalier
            //Et reset de la limte de la camera au nouvel étage courrant du joueur
            InputManagerMoveSurvivorScript inputManager = survivor.GetComponent<InputManagerMoveSurvivorScript>();
            if (inputManager != null)
            {
                inputManager.getCharacterCamera().GetComponent<CameraResetOnCharacterScript>().resetCamera();

                string gameObjectName = "Floor";
                //gameObjectName += GameObject.Find("GameManager").GetComponent<MoveManagerSurvivorScript>().getPlayerMoveData(Network.player).IsInFloor.ToString();
                inputManager.getCharacterCamera().GetComponent<CameraLimitDeplacementScript>().setPlaneLimit(GameObject.Find(gameObjectName).transform.FindChild("CamBorder").transform);
            }
        }

        if (m_hasClicked && Network.isServer)
        {
            //Teleporter survivor
            setSurvivorPositionAfterStair(survivor);
            //Envoyé en RPC la nouvelle position du client, et remettre son m_hasClicked à false et path à null
            m_networkView.RPC("setClickedStairForAll", RPCMode.All);
        }
    }
}
