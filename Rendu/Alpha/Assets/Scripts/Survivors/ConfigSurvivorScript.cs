using UnityEngine;
using System.Collections;

public class ConfigSurvivorScript : MonoBehaviour {

    [SerializeField]
    private Transform m_characterTranform;

    [RPC]
    void SetName(NetworkPlayer player, string name)
    {
        m_characterTranform.name = name;
    }
}
