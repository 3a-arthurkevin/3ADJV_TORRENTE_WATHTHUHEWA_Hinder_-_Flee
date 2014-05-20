using UnityEngine;
using System.Collections;

public class AOESkill : ISkill
{
    public AOESkill() : base()
    {}

    public override void StartSkill()
    {
    }

    public override void StopSkill()
    {
    }

    public override bool CheckLaunch(Vector3 hit)
    {//Check range et autre donnée
        return true;
    }

    public override void LaunchSkill(Vector3 hit)
    {//Lancement du skill
        base.LaunchSkill(hit);
    }

    public override void ApplyEffect(GameObject target)
    {
        
    }
}
