using UnityEngine;
using System.Collections;

public class TeleportZombieToEndStairScript : MonoBehaviour
{
    [SerializeField]
    private Transform m_stairOut;

    void OnTriggerEnter(Collider zombie)
    {
        zombie.transform.position = m_stairOut.position;        
    }
}