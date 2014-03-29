using UnityEngine;
using System.Collections;

public class ChangeMoveTypeZombieScript : MonoBehaviour {
    [SerializeField]
    private MoveManagerZombieScript m_zombie;

    void Start()
    {
        if (m_zombie == null)
        {
            Debug.LogError("MoveManagerZombieScript not found");
            enabled = false;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        m_zombie.Follow(col.transform);
    }

    void OnTriggerExit(Collider col)
    {
        m_zombie.Unfollow();
    }
}
