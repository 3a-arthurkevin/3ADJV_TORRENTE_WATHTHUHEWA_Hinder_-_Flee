using UnityEngine;
using System.Collections;

public class PotionAddHealthScript : MonoBehaviour
{
    [SerializeField]
    int m_lifePointPotion = 10;


    [SerializeField]
    NetworkView m_netview;

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

    public void OnTriggerStay(Collider hit)
    {
        if (hit.tag == "survivor")
        {/*
            if (this.gameObject.GetComponent<UseItemDirectManagerScript>().getViewId())
            {
                hit.gameObject.GetComponent<HealthManagerScript>().AddLifePoint(m_lifePointPotion);
            }*/
        }
    }
}
