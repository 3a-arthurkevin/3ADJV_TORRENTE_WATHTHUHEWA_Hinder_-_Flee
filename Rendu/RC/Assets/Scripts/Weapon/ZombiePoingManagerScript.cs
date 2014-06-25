using UnityEngine;
using System.Collections;

public class ZombiePoingManagerScript : BaseZombieWeaponManagerScript
{

    protected override void initSkill()
    {
        CoupDePoingZombie cpz = new CoupDePoingZombie();
        cpz.WeaponManager = this;
        m_skill = cpz;
    }
}
