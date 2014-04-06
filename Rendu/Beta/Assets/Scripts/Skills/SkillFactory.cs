using UnityEngine;
using System.Collections;

public static class SkillFactory
{
    public static ISkill getSkill(int skillId)
    {
        ISkill skill = null;
        
        switch (skillId)
        {
            case 1:
                skill = new SingleTargetSkill();
                break;

            default:
                throw new System.ArgumentException();
        }

        return skill;
    }
}
