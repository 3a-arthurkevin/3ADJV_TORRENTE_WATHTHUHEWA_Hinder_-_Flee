using UnityEngine;
using System.Collections;

public static class SkillFactory
{
    public static ISkill getSkill(int skillId)
    {
        ISkill skill = null;
        IEffect effect = null;
        
        switch (skillId)
        {
            case 0:
                skill = new SingleTargetSkill();

                skill.CoolDownDuration = 12f;
                skill.Range = 10f;

                effect = EffectsFactory.getEffect(0);
                effect.SetParam("Damage", "10");
                skill.addSurvivorEffect(effect);

                effect = EffectsFactory.getEffect(0);
                effect.SetParam("Damage", "20");
                skill.addZombieEffect(effect);
                break;

            default:
                Debug.Log("Skill not found");
                break;
        }

        return skill;
    }
}
