using UnityEngine;
using System.Collections;

public class WeaponManagerScript : MonoBehaviour
{
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
            try
            {
                m_skills[i] = SkillFactory.getSkill(m_skillId[i]);
                m_skills[i].WeaponManager = this;
                m_coolDown[i] = 0f;
            }
            catch (System.ArgumentException)
            {
                Debug.Log("Skill : " + i.ToString() + " not found");
                m_skills[i] = null;
            }
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
                    if (m_skills[0].CoolDown == 0f)
                        wantLaunchSkill = 0;

                else if (Input.GetButtonDown("Skill2"))
                    if (m_skills[1].CoolDown == 0f)
                        wantLaunchSkill = 1;

                else if (Input.GetButtonDown("Skill3"))
                    if (m_skills[2].CoolDown == 0f)
                        wantLaunchSkill = 2;

                else if (Input.GetButtonDown("Skill4"))
                    if (m_skills[3].CoolDown == 0f)
                        wantLaunchSkill = 3;

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
                    {
                        m_networkView.RPC("CheckLaunchSkill", RPCMode.Server, hit.point);
                    }

                    else
                        m_networkView.RPC("StopSkill", RPCMode.All, false, -1);
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
                    m_networkView.RPC("StartSkill", player, skill);

                else
                    Debug.Log("CoolDown not finish for skill : " + skill.ToString());
            }
        }
    }
    
    [RPC]
    void StartSkill(int skill)
    {
        if (Network.player == m_owner)
        {
            m_skills[skill].StartSkill();
            m_idSkillLaunch = skill;
        }
    }

    [RPC]
    void CheckLaunchSkill(Vector3 hit)
    {
        if(Network.isServer)
        {
            if (m_skills[m_idSkillLaunch].CheckLaunch(hit))
                m_networkView.RPC("LaunchSkill", RPCMode.All, hit);

            else
                m_skills[m_idSkillLaunch].StopSkill();

        }
    }

    [RPC]
    void LaunchSkill(Vector3 hit, string hitName)
    {
        m_skills[m_idSkillLaunch].LaunchSkill(hit);
    }

    [RPC]
    void StopSkill()
    {
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
