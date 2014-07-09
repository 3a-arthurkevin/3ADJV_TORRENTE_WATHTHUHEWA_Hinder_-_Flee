using UnityEngine;
using System.Collections;

public class PotionAddHealthScript : MonoBehaviour
{
    [SerializeField]
    int m_lifePointPotion = 10;

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
    }
}
