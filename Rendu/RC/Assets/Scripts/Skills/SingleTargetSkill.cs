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
        GameObject prefab_projectile = ProjectilesFactoryScript.getProjectileById(0);
        
        if (prefab_projectile == null)
        {
            Debug.Log("Projectile not found");
            return;
        }

        GameObject projectile = (GameObject)GameObject.Instantiate(prefab_projectile, m_weaponManager.Player.position, m_weaponManager.Player.rotation);
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
