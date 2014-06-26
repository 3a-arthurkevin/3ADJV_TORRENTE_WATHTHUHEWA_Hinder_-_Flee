using UnityEngine;
using System.Collections;

public class StopEffectForItemScript : MonoBehaviour 
{
    [SerializeField]
    NetworkView m_networkView;

    [SerializeField]
    float m_effectDuration = 3f;

    StopMoveEffect m_stopEffect;

    void Start()
    {
        m_stopEffect = new StopMoveEffect(m_effectDuration);
    }

    
    void OnTriggerEnter(Collider other)
    {
        if (Network.isServer)
        {
            GameObject character = other.gameObject;

            if (character.tag.Equals("Survivor") || character.tag.Equals("Zombie"))
            {
                m_networkView.RPC("applyEffect", RPCMode.All, other.gameObject.networkView.viewID);
                Network.Destroy(this.gameObject);
            }
        }
    }
    
    [RPC]
    void applyEffect(NetworkViewID netId) 
    {
        m_stopEffect.Apply((NetworkView.Find(netId)).gameObject);
    }
}
