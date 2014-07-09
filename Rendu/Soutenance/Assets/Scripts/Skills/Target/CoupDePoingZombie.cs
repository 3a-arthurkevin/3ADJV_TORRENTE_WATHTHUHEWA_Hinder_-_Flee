using UnityEngine;
using System.Collections;

public class CoupDePoingZombie : SingleTargetSkill
{

    protected override void init()
    {
        m_name = "Coup de Poing";
        m_description = "Donne un coup de poing.";
        m_coolDownDuration = 2f;
        m_range = 2f;

        m_survivorEffect.Add(new TakeDamageEffect(10));
    }

    public override void LaunchSkill(Vector3 hit)
    {
        GameObject prefabProjectile = ProjectilesFactoryScript.getProjectileById(2);

        if (prefabProjectile == null)
            return;

        GameObject projectile = (GameObject)GameObject.Instantiate(prefabProjectile, m_weaponManager.Player.position, m_weaponManager.Player.rotation);
        LaunchProjectileSingleTargetScript launch = projectile.GetComponent<LaunchProjectileSingleTargetScript>();

        launch.Launcher = m_weaponManager.Player.networkView.viewID;
        launch.ApplyEffect = ApplyEffect;
        launch.Speed = 2f;
        launch.Direction = hit;
        launch.Limit = m_range;

        m_coolDown = m_coolDownDuration;
    }
}
