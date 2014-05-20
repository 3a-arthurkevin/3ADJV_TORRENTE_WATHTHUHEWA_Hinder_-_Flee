using UnityEngine;
using System.Collections;

public class SingleTargetSkill : ISkill
{
    public SingleTargetSkill() : base()
    {}

    public override void StartSkill()
    {
    }

    public override void StopSkill()
    {
    }

    public override bool CheckLaunch(Vector3 hit)
    {//Check range and another data
        return true;
    }

    public override void LaunchSkill(Vector3 hit)
    {//Lancement du Skill
        GameObject prefabProjectile = ProjectilesFactoryScript.getProjectileById(0);
        
        if (prefabProjectile == null)
            return;

        GameObject projectile = (GameObject)GameObject.Instantiate(prefabProjectile, m_weaponManager.Player.position, m_weaponManager.Player.rotation);
        LaunchProjectileSingleTargetScript launch = projectile.GetComponent<LaunchProjectileSingleTargetScript>();

        launch.Launcher = m_weaponManager.Player.networkView.viewID;
        launch.ApplyEffect = ApplyEffect;
        launch.Direction = hit;
        launch.Limit = m_maxRange;

        base.LaunchSkill(hit);
    }

    public override void ApplyEffect(GameObject target)
    {
        if (target.tag == "Zombie")
            foreach (IEffect effect in m_zombieEffect)
                effect.Apply(target);

        else if (target.tag == "Survivor")
            foreach (IEffect effect in m_survivorEffect)
                effect.Apply(target);
    }
}
