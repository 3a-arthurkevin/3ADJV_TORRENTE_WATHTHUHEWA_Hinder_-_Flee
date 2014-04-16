using UnityEngine;
using System.Collections;

public class WeaponManagerScript : MonoBehaviour
{
    [SerializeField]
    private Texture2D m_viseurCursor;
    private Vector2 m_hotSpot = Vector2.zero;

    [SerializeField]
    private string m_name;
    public string Name
    {
        get { return m_name; }
        set { m_name = value; }
    }

    [SerializeField]
    private NetworkPlayer m_owner;
    public NetworkPlayer Owner
    {
        get { return m_owner; }
        set { m_owner = value; }
    }

    [SerializeField]
    private Camera m_characterCamera;
    public Camera CharacterCamera
    {
        get { return m_characterCamera; }
        set { m_characterCamera = value; }
    }

    [SerializeField]
    private NetworkView m_networkView = null;

    [SerializeField]
    private int[] m_skillId = new int[4];
    private ISkill[] m_skills = new ISkill[4];
    private float[] m_coolDown = new float[4];

    private int m_idSkillLaunch = 0;
    private bool m_underSkill = false;
    public bool UnderSkill
    {
        get { return m_underSkill; }
        set { m_underSkill = value; }
    }

    [SerializeField]
    private Transform m_player;
    public Transform Player
    {
        get { return m_player; }
        set { m_player = value; }
    }

    void Start()
    {
        if (m_networkView == null)
            m_networkView = networkView;

        for (int i = 0; i < 4; ++i)
        {
            ISkill skill = SkillFactory.getSkill(m_skillId[i]);

            if (skill == null)
                Debug.Log("Skill : " + i.ToString() + " not found");

            else
                skill.WeaponManager = this;

            m_coolDown[i] = 0f;
            m_skills[i] = skill;
        }
    }

    void Update()
    {
        for (int i = 0; i < 4; ++i)
            if (m_skills[i].CoolDown != 0f)
            {
                m_skills[i].CoolDown -= Time.deltaTime;
                if (m_skills[i].CoolDown < 0)
                    m_skills[i].CoolDown = 0f;
            }

        if (Network.isClient && Network.player == m_owner)
        {
            if (!m_underSkill)
            {
                int wantLaunchSkill = -1;

                if (Input.GetButtonDown("Skill1"))
                {
                    if (m_skills[0].CoolDown == 0f)
                        wantLaunchSkill = 0;
                }
                else if (Input.GetButtonDown("Skill2"))
                {
                    if (m_skills[1].CoolDown == 0f)
                        wantLaunchSkill = 1;
                }
                else if (Input.GetButtonDown("Skill3"))
                {
                    if (m_skills[2].CoolDown == 0f)
                        wantLaunchSkill = 2;
                }
                else if (Input.GetButtonDown("Skill4"))
                {
                    if (m_skills[3].CoolDown == 0f)
                        wantLaunchSkill = 3;
                }

                if (wantLaunchSkill != -1)
                    m_networkView.RPC("wantStartSkill", RPCMode.Server, Network.player, wantLaunchSkill);
            }
            else
            {//Under skill
                if (Input.GetButtonDown("LaunchSkill"))
                {
                    Ray ray = m_characterCamera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 100f))
                        m_networkView.RPC("CheckLaunchSkill", RPCMode.Server, hit.point);
                    else
                        m_networkView.RPC("StopSkill", RPCMode.All);
                }
            }
        }
    }

    [RPC]
    void wantStartSkill(NetworkPlayer player, int skill)
    {
        if (Network.isServer)
        {
            if (!m_underSkill && skill >= 0 && skill < 4)
            {
                if (m_skills[skill].CoolDown == 0f)
                {
                    m_networkView.RPC("StartSkill", RPCMode.All, skill);
                    m_underSkill = true;
                }
                else
                    Debug.LogError("CoolDown not finish for skill : " + skill.ToString());
            }
            else
                Debug.LogError("Skill allready Launch");
        }
    }
    
    [RPC]
    void StartSkill(int skill)
    {
        if (Network.player == m_owner)
            Cursor.SetCursor(m_viseurCursor, m_hotSpot, CursorMode.Auto);

        m_skills[skill].StartSkill();
        m_underSkill = true;
        m_idSkillLaunch = skill;
    }

    [RPC]
    void CheckLaunchSkill(Vector3 hit)
    {
        if(Network.isServer)
        {
            if (m_skills[m_idSkillLaunch].CheckLaunch(hit))
                m_networkView.RPC("LaunchSkill", RPCMode.All, hit);

            else
            {
                m_networkView.RPC("StopSkill", RPCMode.All);
                m_skills[m_idSkillLaunch].StopSkill();
            }
        }
    }

    [RPC]
    void LaunchSkill(Vector3 hit)
    {
        m_skills[m_idSkillLaunch].LaunchSkill(hit);
        StopSkill();
    }

    [RPC]
    void StopSkill()
    {
        if (Network.isClient && Network.player == m_owner)
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        m_skills[m_idSkillLaunch].StopSkill();
        SetUnderSkill(false, -1);
    }

    [RPC]
    void SetUnderSkill(bool underSkill, int skill)
    {
        m_underSkill = underSkill;
        m_idSkillLaunch = skill;
    }

    public void SetPlayer(NetworkPlayer player)
    {
        m_owner = player;
    }

    public void SetCamera(Camera camera)
    {
        m_characterCamera = camera;
    }
}
