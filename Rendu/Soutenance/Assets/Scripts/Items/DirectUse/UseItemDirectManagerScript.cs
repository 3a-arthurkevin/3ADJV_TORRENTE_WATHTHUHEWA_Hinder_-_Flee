using UnityEngine;
using System.Collections;

public class UseItemDirectManagerScript : MonoBehaviour {

    NetworkViewID m_playerId;

    public void setViewId(NetworkViewID playerId)
    {
        m_playerId = playerId;
    }

    public NetworkViewID getViewId()
    {
        return m_playerId;
    }
}
