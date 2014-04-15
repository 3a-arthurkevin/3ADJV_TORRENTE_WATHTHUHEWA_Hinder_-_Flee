using UnityEngine;
using System.Collections;

public class SingleTargetSkill : ISkill
{
    public SingleTargetSkill() : base()
    {
    }

    public override void StartSkill()
    {
        Debug.Log("Start Single Target Skill");
    }

    public override void StopSkill()
    {
        Debug.Log("Stop Single Target Skill");
    }

    public override bool CheckLaunch(Vector3 hit, string targetName)
    {
        Debug.Log("Check Launch Single");
        return true;
    }
}
