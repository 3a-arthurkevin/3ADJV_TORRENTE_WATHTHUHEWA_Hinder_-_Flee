using UnityEngine;
using System.Collections;

public class PotionAddHealthScript : MonoBehaviour
{
    [SerializeField]
    int m_lifePointPotion = 10;


    [SerializeField]
    NetworkView m_netview;

    bool m_isUsed = false;

    public void addLifePoint(GameObject player)
    {
        HealthManagerScript playerHealthManager = player.GetComponent<HealthManagerScript>();
        playerHealthManager.AddLifePoint(m_lifePointPotion);
    }

    public void useItem(GameObject target)
    {
        addLifePoint(target);
        Destroy(this.gameObject);
    }

    void OnTriggerStay(Collider hit)
    {
        Debug.LogError("trigger");
        if (hit.tag == "Survivor")
        {
            Debug.LogError("Trigger survivor");
            if (hit.gameObject.GetComponent<NetworkView>().viewID == this.gameObject.GetComponent<UseItemDirectManagerScript>().getViewId())
            {
                Debug.LogError("does item is used ?");
                if (!m_isUsed)
                {
                    Debug.LogError("item used");
                    hit.gameObject.GetComponent<HealthManagerScript>().AddLifePoint(m_lifePointPotion);
                    m_isUsed = true;
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
