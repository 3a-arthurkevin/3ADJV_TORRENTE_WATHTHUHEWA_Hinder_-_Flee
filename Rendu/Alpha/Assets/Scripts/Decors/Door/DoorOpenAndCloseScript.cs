using UnityEngine;
using System.Collections;

public class DoorOpenAndCloseScript : MonoBehaviour
{
    [SerializeField]
    Animator _controller;

    void OnTriggerEnter(Collider other)
    {
        _controller.SetInteger("numberOfCharacter", _controller.GetInteger("numberOfCharacter") + 1);
    }

    void OnTriggerExit(Collider other)
    {
        _controller.SetInteger("numberOfCharacter", _controller.GetInteger("numberOfCharacter") - 1);
    }
}