using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class SingleTargetSkill : ISkill
{
    public SingleTargetSkill() : base()
    {}

    public override void LaunchSkill(Vector3 hit)
    {//Lancement du Skill
        GameObject prefabProjectile = ProjectilesFactoryScript.getProjectileById(0);
        
        if (prefabProjectile == null)
            return;

        GameObject projectile = (GameObject)GameObject.Instantiate(prefabProjectile, m_weaponManager.Player.position, m_weaponManager.Player.rotation);
        LaunchProjectileSingleTargetScript launch = projectile.GetComponent<LaunchProjectileSingleTargetScript>();

        launch.Launcher = m_weaponManager.Player.networkView.viewID;
        launch.ApplyEffect = ApplyEffect;
        launch.Speed = 2f;
        launch.Direction = hit;
        launch.Limit = m_range;

        Debug.LogError("LaunchSkill");

        base.LaunchSkill(hit);
    }

    public override void setParameter(string key, string value)
    {
        base.setParameter(key, value);
    }

    public override bool canLaunchSkill(Vector3 hit)
    {
        return true;
    }

    protected override abstract void init();
}
