using UnityEngine;
using System.Collections;

public abstract class BaseZombieWeaponManagerScript : BaseWeaponManager
{

    [SerializeField]
    private MoveManagerZombieScript m_moveManager;
    public MoveManagerZombieScript MoveManager
    {
        get { return m_moveManager; }
        set { m_moveManager = value; }
    }

    [SerializeField]
    protected ISkill m_skill;

    protected void Start()
    {
        if (m_networkView == null)
            m_networkView = networkView;

        initSkill();
    }

    protected void Update()
    {
        if (Network.isClient)
            return;

        if(m_skill.CoolDown > 0f)
            m_skill.CoolDown -= Time.deltaTime;

        if(m_skill.CoolDown == 0f && m_moveManager.IsFollowing)
        {
            Vector3 direction = m_moveManager.Surivor.position - m_weaponOwner.position;
            if(direction.sqrMagnitude <= m_skill.Range)
                m_networkView.RPC("LaunchSkill", RPCMode.All, m_moveManager.Data.Path.corners[m_moveManager.Data.NumCorner]);
        }
    }

    [RPC]
    protected override void LaunchSkill(Vector3 hit)
    {
        m_skill.LaunchSkill(hit);
    }
}
