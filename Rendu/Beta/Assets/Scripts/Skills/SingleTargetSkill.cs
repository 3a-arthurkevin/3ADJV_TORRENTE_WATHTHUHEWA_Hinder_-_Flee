using UnityEngine;
using System.Collections;

public class SingleTargetSkill : ISkill
{
    public SingleTargetSkill() : base()
    {}

    public override void StartSkill()
    {
        Debug.LogError("Start Single Target Skill");
    }

    public override void StopSkill()
    {
        Debug.LogError("Stop Single Target Skill");
    }

    public override bool CheckLaunch(Vector3 hit)
    {//Check range and another data
        Debug.Log("CheckLaunc TargetSkill");
        return true;
    }

    public override void LaunchSkill(Vector3 hit)
    {//Lancement du Skill
        Debug.LogError("Instanciate Projectil");
        if (Network.isServer)
        {
            GameObject prefab_projectile = ProjectilesFactoryScript.getProjectileById(0);

            GameObject projectile = (GameObject)Network.Instantiate(prefab_projectile, m_weaponManager.Player.position, m_weaponManager.Player.rotation, 0);

            LaunchProjectileSingleTargetScript launch = projectile.GetComponent<LaunchProjectileSingleTargetScript>();

            launch.EffectSurvivor = m_survivorEffect;
            launch.EffectZombie = m_zombieEffect;
            launch.Launcher = Network.player;
            launch.Target = hit;
        }

        base.LaunchSkill(hit);
    }
}
