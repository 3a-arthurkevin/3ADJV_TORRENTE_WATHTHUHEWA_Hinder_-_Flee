using UnityEngine;
using System.Collections;

public class TeleportSurvivorToEndStairScript : MonoBehaviour {

    [SerializeField]
    private Transform m_stairOut;
    private bool m_hasClicked;

    void Awake()
    {
        m_hasClicked = false;
    }

    void OnMouseDown()
    {
        m_hasClicked = true;
        Debug.Log("Click mouse");
    }

    void OnMouseUp()
    {
        m_hasClicked = false;
    }

    void OnTriggerStay(Collider survivor)
    {
        if (m_hasClicked)
        {//TP survivor
            survivor.transform.position = m_stairOut.position;
        }
    }
}
