using UnityEngine;
using System.Collections;

//Attaché à un GameObject enfant du gameObject personnage (survivant ou zombie)
//Accède au DamageManager pour savoir si joueur touché pour enlever les PV

public class HealthManaTmpScript : MonoBehaviour {

    //Représente le gameObject sur lequel le script est attaché
    //[SerializeField]
    //private GameObject m_parentGameObject;

    //Pour savoir si le perso est mort
    private bool m_zeroLifePoint = false;

    [SerializeField]
    private int m_currentHealth = 100;

    [SerializeField]
    private int m_maxHealth = 100;

	// Use this for initialization
	void Start () 
    {
        //m_parentGameObject = gameObject;
        m_currentHealth = 100;
        m_maxHealth = 100;
	}

    public bool getZeroLifePoint()
    {
        return m_zeroLifePoint;
    }

    //Application des degats (utilisé dans AttackManager)
    //Et remise des PV à 0 si l'entité à moins de 0 PV
    public void applyDamage(int damage)
    {
        m_currentHealth -= damage;
        capHealthWhenDead();
    }

    //Pour utilisation de potion ou autre
    //Remise des PV au seuil max si il est au dela de ce seuil
    public void regenHealth(int regen)
    {
        m_currentHealth += regen;

        capMaxHealth();
    }

    //Pour que l'entité n'ai pas plus de PV que le maximum autorisé
    [RPC]
    public void capMaxHealth()
    {
        if (m_currentHealth > m_maxHealth)
        {
            m_currentHealth = m_maxHealth;
        }
    }

    //Pour que l'entité n'ai pas moins que 0 point de vie
    public void capHealthWhenDead()
    {
        if (m_currentHealth < 0)
        {
            m_currentHealth = 0;
            m_zeroLifePoint = true;
            /*
            if (m_parentGameObject.tag == "Surviant")
            {
                Changement du joueur en zombie
                Voir le nombre de joueur dans la partie de type Survivant
                    --> si il n'y en a plus que 1, lancer le chrono de fin de partir (1 min à tenir)
            }
            else if (m_parentGameObject.tag == "Zombie")
            { 
                Detruire GameObject
            }
            */
        }
    }

}
