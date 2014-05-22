using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AOESkill : ISkill
{
    private float m_aoeRange = 1f;
    public float AoeRange
    {
        get { return m_aoeRange; }
        set { m_aoeRange = value; }
    }

    public AOESkill() : base()
    {}

    public override void StartSkill()
    {
    }

    public override void StopSkill()
    {
    }

    public override bool CheckLaunch(Vector3 hit)
    {
        return true;
    }

    public override void LaunchSkill(Vector3 hit)
    {
        GameObject prefabProjectile = ProjectilesFactoryScript.getProjectileById(1);

        if (prefabProjectile == null)
            return;

        GameObject projectile = (GameObject)GameObject.Instantiate(prefabProjectile, m_weaponManager.Player.position, m_weaponManager.Player.rotation);
        LaunchAOEProjectileScript launch = projectile.GetComponent<LaunchAOEProjectileScript>();

        launch.Launcher = m_weaponManager.Player.networkView.viewID;
        launch.ApplyEffect = ApplyEffect;
        launch.Speed = 2f;
        

        base.LaunchSkill(hit);
    }

    public override void ApplyEffect(GameObject target)
    {
        
    }

    public override void setParameter(string key, string value)
    {
        if (key == "AoeRange")
        {
            try
            {
                m_aoeRange = float.Parse(value);
            }
            catch(System.FormatException)
            {
                Debug.Log("AoeRange haven't valid format in AOESkill setParameter");
            }
        }
        else
            base.setParameter(key, value);
    }
}
