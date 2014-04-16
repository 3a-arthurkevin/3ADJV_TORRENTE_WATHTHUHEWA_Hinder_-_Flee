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

    public override bool CheckLaunch(Vector3 hit, string targetName)
    {
        if (targetName.Substring(0, 6) == "Zombie" || targetName.Substring(0, 8) == "Survivor")
            return true;

        return false;
    }

    public override void LaunchSkill(Vector3 hit, string targetName)
    {
        m_coolDown = m_coolDownDuration;
    }
}
