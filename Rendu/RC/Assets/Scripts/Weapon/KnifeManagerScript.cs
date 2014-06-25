using UnityEngine;
using System.Collections;

public class KnifeManagerScript : BaseWeaponManagerScript
{
    protected override void initSkill()
    {
        CoupDeCouteau cp = new CoupDeCouteau();
        cp.WeaponManager = this;
        m_skills[0] = cp;

        LancerDeCouteaux lc = new LancerDeCouteaux();
        lc.WeaponManager = this;
        m_skills[1] = lc;

        PiegeDeCouteau pc = new PiegeDeCouteau();
        pc.WeaponManager = this;
        m_skills[2] = pc;

        EntailleDeCheville ec = new EntailleDeCheville();
        ec.WeaponManager = this;
        m_skills[3] = ec;
    }
}
