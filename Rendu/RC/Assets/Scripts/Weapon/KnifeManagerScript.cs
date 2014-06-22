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

        cp = new CoupDeCouteau();
        cp.WeaponManager = this;
        m_skills[2] = cp;

        cp = new CoupDeCouteau();
        cp.WeaponManager = this;
        m_skills[3] = cp;
    }
}
