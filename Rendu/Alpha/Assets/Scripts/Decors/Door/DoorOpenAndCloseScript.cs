using UnityEngine;
using System.Collections;

public class DoorOpenAndCloseScript : MonoBehaviour
{
    [SerializeField]
    Animator m_controller;

    void OnTriggerEnter(Collider other)
    {
        m_controller.SetInteger("numberOfCharacter", m_controller.GetInteger("numberOfCharacter") + 1);
        //Debug.Log(other.tag);
    }

    void OnTriggerExit(Collider other)
    {
        m_controller.SetInteger("numberOfCharacter", m_controller.GetInteger("numberOfCharacter") - 1);
    }
}