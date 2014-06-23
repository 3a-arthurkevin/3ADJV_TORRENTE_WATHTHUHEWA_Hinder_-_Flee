using UnityEngine;
using System.Collections;

public class KnifeManagerScript : BaseWeaponManagerScript
{
    protected override void initSkill()
    {
        CoupDeCouteau cp = new CoupDeCouteau();
        cp.WeaponManager = this;
        m_skills[0] = cp;

        cp = new CoupDeCouteau();
        cp.WeaponManager = this;
        m_skills[1] = cp;

        PiegeDeCouteau pc = new PiegeDeCouteau();
        pc.WeaponManager = this;
        m_skills[2] = pc;

        pc = new PiegeDeCouteau();
        pc.WeaponManager = this;
        m_skills[3] = pc;
    }
}
