using UnityEngine;
using System.Collections;

public class ZombiWeaponManagerScript : BaseWeaponManagerScript
{
    protected override void initSkill()
    {
        CoupDePoingZombie cz = new CoupDePoingZombie();
        cz.WeaponManager = this;
        m_skills[0] = cz;

        cz = new CoupDePoingZombie();
        cz.WeaponManager = this;
        m_skills[1] = cz;

        cz = new CoupDePoingZombie();
        cz.WeaponManager = this;
        m_skills[2] = cz;

        /*cz = new CoupDePoingZombie();
        cz.WeaponManager = this;
        m_skills[0] = cz;*/
    }
}
