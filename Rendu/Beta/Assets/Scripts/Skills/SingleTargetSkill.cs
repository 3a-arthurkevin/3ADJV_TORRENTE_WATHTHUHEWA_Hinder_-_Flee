using UnityEngine;
using System.Collections;

public class SingleTargetSkill : ISkill
{
    public SingleTargetSkill() : base()
    {}

    public override void StartSkill()
    {
        Debug.Log("Start Single Target Skill");
    }

    public override void StopSkill()
    {
        Debug.Log("Stop Single Target Skill");
    }

    public override bool CheckLaunch(Vector3 hit)
    {//Check range and another data
        Debug.Log("CheckLaunc TargetSkill");
        return true;
    }

    public override void LaunchSkill(Vector3 hit)
    {//Lancement du Skill
        GameObject projectile = (GameObject)Resources.Load("Prefabs/Projectiles/Projectile");

        LaunchProjectileSingleTargetScript launch = projectile.GetComponent<LaunchProjectileSingleTargetScript>();

        launch.EffectSurvivor = m_survivorEffect;
        launch.EffectZombie = m_zombieEffect;
        launch.Launcher = Network.player;
        launch.Target = hit;

        base.LaunchSkill(hit);
    }
}
