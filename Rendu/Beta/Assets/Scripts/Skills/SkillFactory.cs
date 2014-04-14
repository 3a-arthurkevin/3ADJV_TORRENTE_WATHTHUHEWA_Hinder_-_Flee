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

                try
                {
                    effect = EffectsFactory.getEffect(0);
                    effect.SetParam("Damage", "10");
                    skill.addSurvivorEffect(effect);
                }
                catch (System.ArgumentException)
                {
                    Debug.Log("Skill not exist");
                }

                try
                {
                    effect = EffectsFactory.getEffect(0);
                    effect.SetParam("Damage", "20");
                    skill.addZombieEffect(effect);
                }
                catch (System.ArgumentException)
                {
                    Debug.Log("Skill not exist");
                }
                break;

            default:
                throw new System.ArgumentException();
        }

        return skill;
    }
}
