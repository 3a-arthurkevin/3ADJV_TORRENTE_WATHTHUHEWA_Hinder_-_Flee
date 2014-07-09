using UnityEngine;
using System.Collections;

public class HealthManagerScript : MonoBehaviour
{
    public enum CharacterType
    {
        Zombie,
        Survivor
    }

    [SerializeField]
    private GameManagerScript m_gameManager;

    private NetworkPlayer m_owner;
    public NetworkPlayer Owner
    {
        get { return m_owner; }
        set { m_owner = value; }
    }

    [SerializeField]
    private CharacterType m_characterType;
    public CharacterType IsType
    {
        get { return m_characterType; }
    }

    [SerializeField]
    private NetworkView m_networkView;

    [SerializeField]
    private int m_maxLifePoint;

    private int m_currentLifePoint;
    

    /* GUI Part */
    private int m_nbCelluleY = 10;
    private int m_screenWidth;
    private int m_screenHeight;
    private float m_hauteurCellule;
    private Rect layoutBottom;
    private Rect boxEtatPerso;
    private Rect labelLifePoint;
    //private Rect labelAvenir; 
    
    GUIStyle centeredTextStyle;

    [SerializeField]
    private AudioClip m_hitAudio;

    private bool m_alreadyDied = false;
    private bool m_isDestroy = false;
    private bool m_soundPlaying = false;

    void Start()
    {
        m_currentLifePoint = m_maxLifePoint;

        if (m_networkView == null)
            m_networkView = networkView;

        /* GUI Part */
        m_screenWidth = Screen.width;
        m_screenHeight = Screen.height;

        m_hauteurCellule = m_screenHeight / m_nbCelluleY;

        layoutBottom = new Rect(0, m_screenHeight - m_hauteurCellule * 2, m_screenWidth, m_hauteurCellule * 2);
        boxEtatPerso = new Rect(layoutBottom.x, layoutBottom.y, layoutBottom.width * 0.2f, layoutBottom.height);
        labelLifePoint = new Rect(boxEtatPerso.x, boxEtatPerso.y + (0.25f * boxEtatPerso.height), boxEtatPerso.width, boxEtatPerso.height);
        //labelAvenir = new Rect(boxEtatPerso.x, boxEtatPerso.y + (0.50f * boxEtatPerso.height), boxEtatPerso.width, boxEtatPerso.height);

        centeredTextStyle  = new GUIStyle();
        centeredTextStyle.alignment = TextAnchor.UpperCenter;
    }

    public int LifePoint
    {
        get { return m_currentLifePoint; }
        set
        {
            m_currentLifePoint = Mathf.Clamp(value, 0, m_maxLifePoint);

            if (m_currentLifePoint <= 0)
                Died();
        }
    }

    [RPC]
    public void AddLifePoint(int hp)
    {
        if (Network.isServer)
            m_networkView.RPC("AddLifePoint", RPCMode.Others, hp);

        m_currentLifePoint = Mathf.Clamp(m_currentLifePoint + hp, 0, m_maxLifePoint);

        if (m_currentLifePoint <= 0)
            Died();
    }

    [RPC]
    public void RemoveLifePoint(int hp)
    {
        if (Network.isServer)
            m_networkView.RPC("RemoveLifePoint", RPCMode.Others, hp);

        m_currentLifePoint = Mathf.Clamp(m_currentLifePoint - hp, 0, m_maxLifePoint);

        if(Network.isClient)
            StartCoroutine(playHitSound());


        if (Network.isServer && m_currentLifePoint <= 0)
            Died();
    }

    void Died()
    {
        if (m_alreadyDied)
            return;

        if (m_characterType == CharacterType.Survivor)
            SurvivorDied();

        else if (m_characterType == CharacterType.Zombie)
            ZombieDied();
    }

    private void SurvivorDied()
    {
        Debug.LogError("Survivor died");

        m_gameManager.survivorDied(m_networkView.viewID);

        m_alreadyDied = true;
    }

    private void ZombieDied()
    {
        Debug.LogError("Zombie Died");

        if (!m_isDestroy)
        {
            m_gameManager.m_popZombieManager.zombieDied(GetComponent<MoveManagerZombieScript>().Data.IsInFloor, transform);
            Network.Destroy(m_networkView.viewID);
            m_isDestroy = true;
        }

        m_alreadyDied = true;
    }

    public bool isDead()
    {
        return (m_currentLifePoint <= 0);
    }

    void OnGUI()
    {
        if(Network.isClient && Network.player == m_owner)
        {
            GUI.Box(boxEtatPerso, "Etat Perso");

            centeredTextStyle.normal.textColor = Color.red;
            GUI.Label(labelLifePoint, "Vie : " + m_currentLifePoint + "/" + m_maxLifePoint, centeredTextStyle);
            //GUI.Label(labelAvenir, "Label Avenir", centeredTextStyle);
        }
    }

    IEnumerator playHitSound()
    {
        if (!m_soundPlaying)
        {
            m_soundPlaying = true;
            
            AudioSource audioSource = GetComponent<AudioSource>();
            AudioClip previousClip = audioSource.clip;

            audioSource.clip = m_hitAudio;
            audioSource.Play();

            yield return new WaitForSeconds(m_hitAudio.length);

            audioSource.clip = previousClip;
            audioSource.Play();

            m_soundPlaying = false;
        }
    }
}
